using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    // Singleton instance for easy access
    public static CheckPoint Instance { get; private set; }

    /*private bool hasChangesSinceLastSave = false;*/

    public Animator animator; // Animator to control checkpoint animation

    // UI elements for dialog at checkpoint
    public TMP_Text dialogText;
    public string idleMessage;

    // UI elements for the "Saved" dialog
    public Image SaveddialogImage;
    public TMP_Text SaveddialogText;
    public string SavedidleMessage;

    /*public GameObject checkpointLight;*/

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

    public List<EnemyHealthManager> enemies; 
    public TurnOffTouch[] torches; 
    public Torch_OnOff[] torches2; 
    public Touch_Plant[] plants;
    public Touch_Plant_No[] plantsNo;

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
        if (PlayerPrefs.HasKey("SavedPositionX") && PlayerPrefs.HasKey("SavedPositionY") && PlayerPrefs.HasKey("SavedPositionZ"))
        {
            LoadGame(); // Load game data if available
        }
        else
        {
            // Set default position if no saved data is found
            player.transform.position = new Vector3(-6f, -1f, 5f);
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
        /*if (checkpointLight != null)
        {
            checkpointLight.SetActive(false);
        }*/
        
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
        PlayerPrefs.SetFloat("SavedPositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("SavedPositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("SavedPositionZ", player.transform.position.z);

        // Save player health
        float currentHealth = playerHealthScript.currentHealth;
        PlayerPrefs.SetFloat("SavedHealth", currentHealth);

        // Save initial checkpoint position
        if (initial) { initialPosition.y += 0.2f; }
        PlayerPrefs.SetFloat("InitialPositionX", initialPosition.x);
        PlayerPrefs.SetFloat("InitialPositionY", initialPosition.y);
        PlayerPrefs.SetFloat("InitialPositionZ", initialPosition.z);

        // Save animator state (whether checkpoint is active or not)
        bool isActive = animator.GetBool("IsActive");
        PlayerPrefs.SetInt("SavedAnimatorState", isActive ? 1 : 0);
        PlayerPrefs.SetInt("isSaved", isSaved ? 1 : 0);
        PlayerPrefs.SetInt("positionChanged", positionChanged ? 1 : 0);

        // Save potion and coin counts
        int countBlood = bloodPotionManager.GetComponent<BloodPotionManager>().bottleCount;
        PlayerPrefs.SetInt("SavedBloodPotionCount", countBlood);
        int countGhost = typeCoinManager.GetComponent<TypeCoinManager>().ghostCount;
        PlayerPrefs.SetInt("SavedGhostCount", countGhost);
        int countBloodSkill = typeCoinManager.GetComponent<TypeCoinManager>().bloodCount;
        PlayerPrefs.SetInt("SavedBloodCount", countBloodSkill);

        // Save states of torches, plants, and enemies
        for (int i = 0; i < torches2.Length; i++)
        {
            PlayerPrefs.SetInt("Torch2_" + i + "_State", torches2[i].GetComponent<Torch_OnOff>().isOn ? 1 : 0);
        }

        for (int i = 0; i < torches.Length; i++)
        {
            PlayerPrefs.SetInt("Torch_" + i + "_State", torches[i].GetComponent<TurnOffTouch>().isTorchOn ? 1 : 0);
        }
        for (int i = 0; i < plants.Length; i++)
        {
            PlayerPrefs.SetInt("Plant_" + i + "_State", plants[i].GetComponent<Touch_Plant>().hasBloomed ? 1 : 0);
        }

        for (int i = 0; i < plantsNo.Length; i++)
        {
            PlayerPrefs.SetInt("PlantNo_" + i + "_State", plantsNo[i].GetComponent<Touch_Plant_No>().hasBloomed ? 1 : 0);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            PlayerPrefs.SetInt("Enemy_" + i + "_Dead", enemies[i].isDead ? 1 : 0);
            PlayerPrefs.SetFloat("Enemy_" + i + "_Health", enemies[i].health);
        }

        PlayerPrefs.Save();
    }

    // Load the saved game data
    public void LoadGame()
    {
        // Load checkpoint
        float xInitial = PlayerPrefs.GetFloat("InitialPositionX", 0f);
        float yInitial = PlayerPrefs.GetFloat("InitialPositionY", 0f);
        float zInitial = PlayerPrefs.GetFloat("InitialPositionZ", 0f);
        initialPosition = new Vector3(xInitial, yInitial, zInitial);

        transform.position = initialPosition;
        // Load player position and health
        float x = PlayerPrefs.GetFloat("SavedPositionX");
        float y = PlayerPrefs.GetFloat("SavedPositionY");
        float z = PlayerPrefs.GetFloat("SavedPositionZ");
        savedPosition = new Vector3(x, y, z);
        player.transform.position = savedPosition;

        // Load saved health
        savedHealth = PlayerPrefs.GetFloat("SavedHealth");
        if (playerHealthScript != null)
        {
            playerHealthScript.currentHealth = savedHealth;
            playerHealthScript.UpdateHealthbar();
        }

        // Load potion and coin counts
        int savedBlood = PlayerPrefs.GetInt("SavedBloodPotionCount", 0);
        bloodPotionManager.GetComponent<BloodPotionManager>().bottleCount = savedBlood;
        int savedGhost = PlayerPrefs.GetInt("SavedGhostCount", 0);
        typeCoinManager.GetComponent<TypeCoinManager>().ghostCount = savedGhost;
        int savedBloodSkill = PlayerPrefs.GetInt("SavedBloodCount", 0);
        typeCoinManager.GetComponent<TypeCoinManager>().bloodCount = savedBloodSkill;

        // Load animator state
        int savedAnimatorState = PlayerPrefs.GetInt("SavedAnimatorState", 0); 
        animator.SetBool("IsActive", savedAnimatorState == 1);

        isSaved = PlayerPrefs.GetInt("isSaved", 0) == 1;
        positionChanged = PlayerPrefs.GetInt("positionChanged", 0) == 1;

        

        for (int i = 0; i < torches.Length; i++)
        {
            int torchState = PlayerPrefs.GetInt("Torch_" + i + "_State", 1); // Default to on (1)
            torches[i].GetComponent<TurnOffTouch>().SetTorchState(torchState == 1);
        }

        for (int i = 0; i < torches2.Length; i++)
        {
            int torch2State = PlayerPrefs.GetInt("Torch2_" + i + "_State", 1); // Default to on (1)
            torches2[i].GetComponent<Torch_OnOff>().SetTorchState(torch2State == 1);
        }

        for (int i = 0; i < plants.Length; i++)
        {
            int plants2State = PlayerPrefs.GetInt("Plant_" + i + "_State", 1); // Default to on (1)
            plants[i].GetComponent<Touch_Plant>().SetPlantState(plants2State == 1);
        }

        for (int i = 0; i < plantsNo.Length; i++)
        {
            int plantsNoState = PlayerPrefs.GetInt("PlantNo_" + i + "_State", 1); // Default to on (1)
            plantsNo[i].GetComponent<Touch_Plant_No>().SetPlantState(plantsNoState == 1);
        }

        // Load enemies state
        for (int i = 0; i < enemies.Count; i++)
        {
            int enemyDead = PlayerPrefs.GetInt("Enemy_" + i + "_Dead", 0); // Default to alive (0)
            enemies[i].isDead = enemyDead == 1;
            if (enemies[i].isDead)
            {
                Destroy(enemies[i].gameObject);
            }
            else
            {
                enemies[i].health = PlayerPrefs.GetFloat("Enemy_" + i + "_Health", enemies[i].maxHealth);
                enemies[i].UpdateHealthbar();
            }
        }

    }

    //Clear data when player choose new game
    public static void ClearGameData()
    {
        PlayerPrefs.DeleteKey("SavedSceneName");
        PlayerPrefs.DeleteKey("SavedPositionX");
        PlayerPrefs.DeleteKey("SavedPositionY");
        PlayerPrefs.DeleteKey("SavedPositionZ");
        PlayerPrefs.DeleteKey("SavedHealth");
        PlayerPrefs.DeleteKey("SavedAnimatorState");
        PlayerPrefs.DeleteKey("SavedBloodPotionCount");
        PlayerPrefs.DeleteKey("SavedGhostCount");
        PlayerPrefs.DeleteKey("SavedBloodCount");
        PlayerPrefs.DeleteKey("isSaved");
        PlayerPrefs.DeleteKey("positionChanged");
        PlayerPrefs.DeleteKey("Torch_");
        PlayerPrefs.DeleteKey("Torch2_");
        PlayerPrefs.DeleteKey("Plant_");
        PlayerPrefs.DeleteKey("PlantNo_");
        PlayerPrefs.DeleteKey("Enemy_");
    }
}
