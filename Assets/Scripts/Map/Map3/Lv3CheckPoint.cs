using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Lv3CheckPoint : MonoBehaviour
{
    // Singleton instance for easy access
    public static Lv3CheckPoint Instance { get; private set; }

    /*private bool hasChangesSinceLastSave = false;*/

    public Animator animator; // Animator to control checkpoint animation

    // UI elements for dialog at checkpoint
    public TMP_Text dialogText;
    public string idleMessage;

    // UI elements for the "Saved" dialog
    public Image SaveddialogImage;
    public TMP_Text SaveddialogText;
    public string SavedidleMessage;

    // Radius to detect if the player is close enough to the checkpoint
    public float detectionRadius = 3f;
    private bool playerInRange = false;

    // Particle system to show when checkpoint is activated
    public ParticleSystem checkpointParticleSystem;

    // Saved:
    // Player and health management references
    public GameObject player;
    public HealthManager playerHealthScript;

    private Vector3 savedPosition; // Player's saved position
    private float savedHealth; // Player's saved health

    private Vector3 initialPosition; // Initial position of the checkpoint
    private bool initial = false; // Track if saving at the checkpoint
    private bool positionChanged = false; // Track if the checkpoint has moved

    public bool isSaved = false; // Flag to check if the game is saved

    // Other game objects to be saved (like enemies, plants, etc.)
    public BloodPotionManager bloodPotionManager;
    public TypeCoinManager typeCoinManager;
    public Shop shop;

    //Diffent things of maps
    public List<EnemyHealthManager> enemies;
    public Camera[] cameras;

    public GameObject counterIcon;
    public GameObject bloodWaveIcon;

    // Initialize the instance and check for saved data
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set the singleton instance
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instance
        }
    }

    // Start method where the checkpoint data is loaded
    void Start()
    {
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }
        int savedActiveCameraIndex = PlayerPrefs.GetInt("ActiveCameraIndex3", 0); // Default to first camera
        cameras[savedActiveCameraIndex].gameObject.SetActive(true);
        int counterSkillCheck = PlayerPrefs.GetInt("CounterState", 0);
        Counter.counterLearned = counterSkillCheck == 1;
        if (counterSkillCheck == 1 && !counterIcon.activeSelf)
        {
            counterIcon.SetActive(true);
        }
        int bloodWaveCheck = PlayerPrefs.GetInt("BloodWaveState", 0);
        CastBloodWave.bloodWaveLearned = bloodWaveCheck == 1;
        if (bloodWaveCheck == 1 && !bloodWaveIcon.activeSelf)
        {
            bloodWaveIcon.SetActive(true);
        }
        if (PlayerPrefs.HasKey("SavedPosition3X") && PlayerPrefs.HasKey("SavedPosition3Y") && PlayerPrefs.HasKey("SavedPosition3Z"))
        {
            LoadGame(); // Load game data if available
        }
        else
        {
            if (PlayerPrefs.HasKey("SavedMaxHealth"))
            {
                float maxHealth = PlayerPrefs.GetFloat("SavedMaxHealth");
                if (playerHealthScript != null)
                {
                    playerHealthScript.maxHealth = maxHealth;
                }
            }
            if (PlayerPrefs.HasKey("SavedBloodPotionCount"))
            {
                int savedBlood = PlayerPrefs.GetInt("SavedBloodPotionCount", 0);
                bloodPotionManager.GetComponent<BloodPotionManager>().bottleCount = savedBlood;
                int savedGhost = PlayerPrefs.GetInt("SavedGhostCount", 0);
                typeCoinManager.GetComponent<TypeCoinManager>().ghostCount = savedGhost;
                int savedBloodSkill = PlayerPrefs.GetInt("SavedBloodCount", 0);
                typeCoinManager.GetComponent<TypeCoinManager>().bloodCount = savedBloodSkill;
            }
            // Set default position if no saved data is found
            player.transform.position = new Vector3(-5f, -2f, 0f);
            //x = 180 at quiz, x = 210 at boss

            if (playerHealthScript != null)
            {
                playerHealthScript.currentHealth = playerHealthScript.maxHealth;
            }
            initialPosition = transform.position;  // Set initial checkpoint position
        }
        
        // If player health script isn't set, get it
        if (playerHealthScript == null)
        {
            playerHealthScript = player.GetComponent<HealthManager>();
        }
        // Set up initial UI visibility
        if (dialogText != null)
        {
            dialogText.enabled = false;
        }
        if (SaveddialogText != null)
        {
            SaveddialogText.enabled = false;
            SaveddialogImage.enabled = false;
        }
        // Stop particle system if not saved yet
        if (checkpointParticleSystem != null && !isSaved)
        {
            checkpointParticleSystem.Stop();
        }

        // Check for scene change and save game if necessary
        string sceneName = SceneManager.GetActiveScene().name;
        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "");
        if (sceneName != savedSceneName)
        {
            SaveGame();  // Save if the scene has changed
        }
    }

    // Update the checkpoint dialog when the player is in range
    void Update()
    {
        if (playerInRange)
        {
            if (!isSaved && dialogText != null)
            {
                dialogText.enabled = true;
                dialogText.text = idleMessage;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    initial = true;
                    isSaved = true;
                    sfxManager.Instance.PlaySound2D("check_point");
                    SaveGame(); // Save game when F is pressed
                    CheckPointJSON.Instance.SaveGame();
                }
            }
        }
        else
        {
            if (dialogText != null)
            {
                dialogText.enabled = false;
            }
            // Reset checkpoint position if not in range
            if (positionChanged)
            {
                transform.position = initialPosition;
                positionChanged = false;
            }
        }
    }

    // Detect when the player enters the checkpoint trigger area
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            // If checkpoint is not saved, show particles and animator
            if (!isSaved)
            {
                if (!positionChanged)
                {
                    // Move checkpoint position slightly up to indicate activation
                    Vector3 newPosition = transform.position;
                    newPosition.y += 0.2f;
                    transform.position = newPosition;
                    positionChanged = true;

                }
                animator.SetBool("IsActive", true);// Play checkpoint animation
                if (checkpointParticleSystem != null)
                {
                    checkpointParticleSystem.Play(); // Activate particle system
                }
            }

        }
    }

    // Detect when the player exits the checkpoint trigger area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;

            // If checkpoint is not saved, reset position and stop effects
            if (!isSaved)
            {
                transform.position = initialPosition;
                positionChanged = false;
                animator.SetBool("IsActive", false); // Stop animation
                if (checkpointParticleSystem != null)
                {
                    checkpointParticleSystem.Stop(); // Stop particle system 
                }
            }
        }
    }

    // Show the saved message for a specified amount of time
    private IEnumerator ShowDialogForTime(float timeToShow)
    {
        if (SaveddialogText != null && SaveddialogImage != null)
        {
            SaveddialogText.enabled = true;
            SaveddialogImage.enabled = true;
            SaveddialogText.text = SavedidleMessage; // Display saved message
        }

        yield return new WaitForSeconds(timeToShow);  // Wait for the specified time

        if (SaveddialogText != null && SaveddialogImage != null)
        {
            SaveddialogText.enabled = false; // Hide saved message
            SaveddialogImage.enabled = false;
        }
    }

    // Save the game data (position, health, etc.)
    public void SaveGame()
    {
        Debug.Log("Prefab saved");
        StartCoroutine(ShowDialogForTime(1f)); // Show saved message for 1 second

        // Save scene name
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedSceneName", currentSceneName);

        // Save player position
        //------------change--------------
        PlayerPrefs.SetFloat("SavedPosition3X", player.transform.position.x);
        PlayerPrefs.SetFloat("SavedPosition3Y", player.transform.position.y);
        PlayerPrefs.SetFloat("SavedPosition3Z", player.transform.position.z);

        // Save player health
        float currentHealth = playerHealthScript.currentHealth;
        PlayerPrefs.SetFloat("SavedHealth", currentHealth);
        float maxHealth = playerHealthScript.maxHealth;
        PlayerPrefs.SetFloat("SavedMaxHealth", maxHealth);
        //------------change--------------
        // Save initial checkpoint position
        if (initial) { initialPosition.y += 0.2f; }
        PlayerPrefs.SetFloat("InitialPosition3X", initialPosition.x);
        PlayerPrefs.SetFloat("InitialPosition3Y", initialPosition.y);
        PlayerPrefs.SetFloat("InitialPosition3Z", initialPosition.z);

        // Save animator state (whether checkpoint is active or not)
        bool isActive = animator.GetBool("IsActive");
        //------------change--------------
        PlayerPrefs.SetInt("SavedAnimatorState3", isActive ? 1 : 0);
        PlayerPrefs.SetInt("isSaved3", isSaved ? 1 : 0);
        PlayerPrefs.SetInt("positionChanged3", positionChanged ? 1 : 0);

        // Save potion and coin counts
        int countBlood = bloodPotionManager.GetComponent<BloodPotionManager>().bottleCount;
        PlayerPrefs.SetInt("SavedBloodPotionCount", countBlood);
        int countGhost = typeCoinManager.GetComponent<TypeCoinManager>().ghostCount;
        PlayerPrefs.SetInt("SavedGhostCount", countGhost);
        int countBloodSkill = typeCoinManager.GetComponent<TypeCoinManager>().bloodCount;
        PlayerPrefs.SetInt("SavedBloodCount", countBloodSkill);

        //------------change--------------
        PlayerPrefs.SetInt("SavedItemRunOut3", shop.GetComponent<Shop>().itemRunOut ? 1 : 0);
        int maxValueItem = Mathf.FloorToInt(shop.quantitySlider.maxValue);
        PlayerPrefs.SetInt("SavedMaxValueItem3", maxValueItem);

        // Save states of torches, plants, and enemies

        //------------change--------------
        for (int i = 0; i < enemies.Count; i++)
        {
            PlayerPrefs.SetInt("Enemy3_" + i + "_Dead", enemies[i].isDead ? 1 : 0);
            PlayerPrefs.SetFloat("Enemy3_" + i + "_Health", enemies[i].health);
        }
        // Save the index of the active camera
        int activeCameraIndex = -1;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].gameObject.activeInHierarchy)
            {
                activeCameraIndex = i;
                break;
            }
        }
        PlayerPrefs.SetInt("ActiveCameraIndex3", activeCameraIndex);
        PlayerPrefs.Save();
    }

    // Load the saved game data
    public void LoadGame()
    {
        //------------change--------------
        // Load checkpoint
        float xInitial = PlayerPrefs.GetFloat("InitialPosition3X", 0f);
        float yInitial = PlayerPrefs.GetFloat("InitialPosition3Y", 0f);
        float zInitial = PlayerPrefs.GetFloat("InitialPosition3Z", 0f);
        initialPosition = new Vector3(xInitial, yInitial, zInitial);

        transform.position = initialPosition;
        // Load player position and health
        //------------change--------------
        float x = PlayerPrefs.GetFloat("SavedPosition3X");
        float y = PlayerPrefs.GetFloat("SavedPosition3Y");
        float z = PlayerPrefs.GetFloat("SavedPosition3Z");
        savedPosition = new Vector3(x, y, z);
        player.transform.position = savedPosition;

        // Load saved health
        savedHealth = PlayerPrefs.GetFloat("SavedHealth");
        float maxHealth = PlayerPrefs.GetFloat("SavedMaxHealth");
        if (playerHealthScript != null)
        {
            playerHealthScript.currentHealth = savedHealth;
            playerHealthScript.maxHealth = maxHealth;
            playerHealthScript.UpdateHealthbar();
        }

        // Load potion and coin counts
        int savedBlood = PlayerPrefs.GetInt("SavedBloodPotionCount", 0);
        bloodPotionManager.GetComponent<BloodPotionManager>().bottleCount = savedBlood;
        int savedGhost = PlayerPrefs.GetInt("SavedGhostCount", 0);
        typeCoinManager.GetComponent<TypeCoinManager>().ghostCount = savedGhost;
        int savedBloodSkill = PlayerPrefs.GetInt("SavedBloodCount", 0);
        typeCoinManager.GetComponent<TypeCoinManager>().bloodCount = savedBloodSkill;

        //------------change--------------
        int savedItemRunOut = PlayerPrefs.GetInt("SavedItemRunOut3", 0);
        shop.itemRunOut = savedItemRunOut == 1;
        int savedMaxItem = PlayerPrefs.GetInt("SavedMaxValueItem3");
        if (shop != null)
        {
            shop.quantitySlider.maxValue = savedMaxItem;
        }

        // Load animator state
        //------------change--------------
        int savedAnimatorState = PlayerPrefs.GetInt("SavedAnimatorState3", 0);
        animator.SetBool("IsActive", savedAnimatorState == 1);

        isSaved = PlayerPrefs.GetInt("isSaved3", 0) == 1;
        positionChanged = PlayerPrefs.GetInt("positionChanged3", 0) == 1;
/*
        //Load Learn Skill
        int counterSkillCheck = PlayerPrefs.GetInt("CounterState", 0);
        Counter.counterLearned = counterSkillCheck == 1;
        if (counterSkillCheck == 1 && !counterIcon.activeSelf)
        {
            counterIcon.SetActive(true);
        }
        int bloodWaveCheck = PlayerPrefs.GetInt("BloodWaveState", 0);
        CastBloodWave.bloodWaveLearned = bloodWaveCheck == 1;
        if (bloodWaveCheck == 1 && !bloodWaveIcon.activeSelf)
        {
            bloodWaveIcon.SetActive(true);
        }*/
        //------------change--------------
        // Load enemies state
        for (int i = 0; i < enemies.Count; i++)
        {
            int enemyDead = PlayerPrefs.GetInt("Enemy3_" + i + "_Dead", 0); // Default to alive (0)
            enemies[i].isDead = enemyDead == 1;

            if (enemies[i].isDead)
            {
                Destroy(enemies[i].gameObject);
            }
            else
            {
                enemies[i].health = PlayerPrefs.GetFloat("Enemy3_" + i + "_Health", enemies[i].maxHealth);
                enemies[i].UpdateHealthbar();
            }
        }
        // Load the index of the active camera
        int savedActiveCameraIndex = PlayerPrefs.GetInt("ActiveCameraIndex3", 0); // Default to the first camera if not found

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == savedActiveCameraIndex); // Activate the camera at the saved index
        }
    }

    //Clear data when player choose new game
    public static void ClearGameData()
    {
        PlayerPrefs.DeleteKey("SavedSceneName");
        //------------change--------------
        PlayerPrefs.DeleteKey("SavedPosition3X");
        PlayerPrefs.DeleteKey("SavedPosition3Y");
        PlayerPrefs.DeleteKey("SavedPosition3Z");

        PlayerPrefs.DeleteKey("SavedHealth");
        PlayerPrefs.DeleteKey("InitialPosition3X");
        PlayerPrefs.DeleteKey("InitialPosition3Y");
        PlayerPrefs.DeleteKey("InitialPosition3Z");
        PlayerPrefs.DeleteKey("SavedAnimatorState3");
        PlayerPrefs.DeleteKey("isSaved3");
        PlayerPrefs.DeleteKey("positionChanged3");
/*        PlayerPrefs.DeleteKey("SavedBloodPotionCount");
        PlayerPrefs.DeleteKey("SavedGhostCount");
        PlayerPrefs.DeleteKey("SavedBloodCount");*/
        PlayerPrefs.DeleteKey("SavedItemRunOut3");
        PlayerPrefs.DeleteKey("SavedMaxValueItem3");
        PlayerPrefs.DeleteKey("Enemy3_");
        PlayerPrefs.DeleteKey("ActiveCameraIndex3");
        PlayerPrefs.DeleteKey("CounterState");
        PlayerPrefs.DeleteKey("BloodWaveState");

        //PlayerPrefs.DeleteKey("SavedMaxHealth");
    }
}
