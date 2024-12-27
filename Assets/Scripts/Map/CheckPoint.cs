using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint Instance { get; private set; }

    //private bool hasChangesSinceLastSave = false;

    public Animator animator;

    public Image dialogImage;
    public TMP_Text dialogText;
    public string idleMessage;

    public Image SaveddialogImage;
    public TMP_Text SaveddialogText;
    public string SavedidleMessage;

    //public GameObject checkpointLight;

    public float detectionRadius = 3f;
    private bool playerInRange = false;

    public GameObject player;
    public HealthManager playerHealthScript;

    public ParticleSystem checkpointParticleSystem;

    // Noi dung luu
    private Vector3 savedPosition;
    private float savedHealth;

    private int savedBlood;
    private Vector3 initialPosition;
    private bool initial = false;
    private bool positionChanged = false;

    public bool isSaved = false;

    public BloodPotionManager bloodPotionManager;
    public TypeCoinManager typeCoinManager;

    public List<EnemyHealthManager> enemies;
    public TurnOffTouch[] torches;
    public Torch_OnOff[] torches2;
    public Touch_Plant[] plants;
    public Touch_Plant_No[] plantsNo;

    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("SavedPositionX") && PlayerPrefs.HasKey("SavedPositionY") && PlayerPrefs.HasKey("SavedPositionZ"))
        {
            LoadGame(); 
        }
        else
        {
            player.transform.position = new Vector3(-6f, -1f, 5f);
            if (playerHealthScript != null)
            {
                playerHealthScript.currentHealth = playerHealthScript.maxHealth;
            }
            initialPosition = transform.position;  
        }
        if (playerHealthScript == null)
        {
            playerHealthScript = player.GetComponent<HealthManager>();
        }
       
        if (dialogText != null)
        {
            dialogText.enabled = false;
            dialogImage.enabled = false;
        }
        if (SaveddialogText != null)
        {
            SaveddialogText.enabled = false;
            SaveddialogImage.enabled = false;
        }
        if (checkpointParticleSystem != null && !isSaved)
        {
            checkpointParticleSystem.Stop();
        }
        /*if (checkpointLight != null)
        {
            checkpointLight.SetActive(false);
        }*/
        /*string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Map1_Forest" || sceneName == "Map2_Desert" || sceneName == "Map3_City" || sceneName == "Map4_Cave" || sceneName == "Map5_Ruin")
        {
            SaveGame();
        }*/
        string sceneName = SceneManager.GetActiveScene().name;
        string savedSceneName = PlayerPrefs.GetString("SavedSceneName", "");
        if (sceneName != savedSceneName)
        {
            SaveGame();  // Save if the scene has changed
        }
    }
    void Update()
    {
        if (playerInRange)
        {
            if (!isSaved && dialogText != null)
            {
                dialogText.enabled = true;
                dialogImage.enabled = true;
                dialogText.text = idleMessage;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    initial = true;
                    isSaved = true;
                    SaveGame();
                }
            }
        }
        else
        {
            if (dialogText != null)
            {
                dialogText.enabled = false;
                dialogImage.enabled = false;
            }
            if (positionChanged)
            {
                transform.position = initialPosition;
                positionChanged = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            //initial = true;
            /*if (checkpointLight != null)
            {
                checkpointLight.SetActive(true);
            }*/
            if (!isSaved)
            {
                if (!positionChanged)
                {

                    Vector3 newPosition = transform.position;
                    newPosition.y += 0.2f;
                    transform.position = newPosition;
                    positionChanged = true;

                }
                animator.SetBool("IsActive", true);
                if (checkpointParticleSystem != null)
                {
                    checkpointParticleSystem.Play(); 
                }
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            //initial = false;
            /* if (checkpointLight != null)
             {
                 checkpointLight.SetActive(false);
             }*/
            if (!isSaved)
            {
                transform.position = initialPosition;
                positionChanged = false;
                animator.SetBool("IsActive", false);
                if (checkpointParticleSystem != null)
                {
                    checkpointParticleSystem.Stop(); 
                }
            }
        }
    }
    private IEnumerator ShowDialogForTime(float timeToShow)
    {
        if (SaveddialogText != null && SaveddialogImage != null)
        {
            SaveddialogText.enabled = true;
            SaveddialogImage.enabled = true;
            SaveddialogText.text = SavedidleMessage;
        }

        yield return new WaitForSeconds(timeToShow);

        if (SaveddialogText != null && SaveddialogImage != null)
        {
            SaveddialogText.enabled = false;
            SaveddialogImage.enabled = false;
        }
    }
    public void SaveGame()
    {
        StartCoroutine(ShowDialogForTime(1f));
        
        //ten ban do
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedSceneName", currentSceneName);
        if(initial) { initialPosition.y += 0.2f; }
        //initialPosition = new Vector3(initialPosition.x, initialPosition.y + 0.2f, initialPosition.z);
        //vi tri nguoi choi
        PlayerPrefs.SetFloat("SavedPositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("SavedPositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("SavedPositionZ", player.transform.position.z);

        //vi tri qua cau
        PlayerPrefs.SetFloat("InitialPositionX", initialPosition.x);
        PlayerPrefs.SetFloat("InitialPositionY", initialPosition.y);
        PlayerPrefs.SetFloat("InitialPositionZ", initialPosition.z);

        //mau nguoi choi
        float currentHealth = playerHealthScript.currentHealth;
        PlayerPrefs.SetFloat("SavedHealth", currentHealth);
        
        //cac thuoc tinh checkpoint
        bool isActive = animator.GetBool("IsActive");
        PlayerPrefs.SetInt("SavedAnimatorState", isActive ? 1 : 0);
        PlayerPrefs.SetInt("isSaved", isSaved ? 1 : 0);
        PlayerPrefs.SetInt("positionChanged", positionChanged ? 1 : 0);

        //luong binh mau
        int countBlood = bloodPotionManager.GetComponent<BloodPotionManager>().bottleCount;
        PlayerPrefs.SetInt("SavedBloodPotionCount", countBlood);

        //luong coin
        int countGhost = typeCoinManager.GetComponent<TypeCoinManager>().ghostCount;
        PlayerPrefs.SetInt("SavedGhostCount", countGhost);

        int countBloodSkill = typeCoinManager.GetComponent<TypeCoinManager>().bloodCount;
        PlayerPrefs.SetInt("SavedBloodCount", countBloodSkill);

        //tuong tac
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
    public void LoadGame()
    {
        //vi tri checkpoint

        float xInitial = PlayerPrefs.GetFloat("InitialPositionX", 0f);
        float yInitial = PlayerPrefs.GetFloat("InitialPositionY", 0f);
        float zInitial = PlayerPrefs.GetFloat("InitialPositionZ", 0f);
        initialPosition = new Vector3(xInitial, yInitial, zInitial);
        transform.position = initialPosition;
        //vi tri nguoi choi
        float x = PlayerPrefs.GetFloat("SavedPositionX");
        float y = PlayerPrefs.GetFloat("SavedPositionY");
        float z = PlayerPrefs.GetFloat("SavedPositionZ");
        savedPosition = new Vector3(x, y, z);
        player.transform.position = savedPosition;

        //mau
        savedHealth = PlayerPrefs.GetFloat("SavedHealth");
        if (playerHealthScript != null)
        {
            playerHealthScript.currentHealth = savedHealth;
            playerHealthScript.UpdateHealthbar();
        }

        //binh mau
        int savedBlood = PlayerPrefs.GetInt("SavedBloodPotionCount", 0);
        bloodPotionManager.GetComponent<BloodPotionManager>().bottleCount = savedBlood;

        //tien
        int savedGhost = PlayerPrefs.GetInt("SavedGhostCount", 0);
        typeCoinManager.GetComponent<TypeCoinManager>().ghostCount = savedGhost;
        int savedBloodSkill = PlayerPrefs.GetInt("SavedBloodCount", 0);
        typeCoinManager.GetComponent<TypeCoinManager>().bloodCount = savedBloodSkill;

        //hoat anh
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
            enemies[i].health = PlayerPrefs.GetFloat("Enemy_" + i + "_Health", enemies[i].health);
            if (enemies[i].isDead)
            {
                Destroy(enemies[i].gameObject);
            }
            else
            {
                enemies[i].gameObject.SetActive(true);
            }
        }

    }
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
