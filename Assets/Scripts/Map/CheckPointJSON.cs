using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public Dictionary<string, LevelSaveData> levels = new Dictionary<string, LevelSaveData>();
}

[System.Serializable]
public class LevelSaveData
{
    public string SavedSceneNameJSON;
    public Vector3 savedPosition;
    //public Vector3 initialPosition;
    public float savedHealth;
    public int savedBloodPotionCount;
    public int savedGhostCount;
    public int savedBloodCount;
    public bool savedItemRunOut;
    public int SavedMaxValueItem;
    //public bool isSaved;
    //public bool positionChanged;

    public List<EnemySaveData> enemies = new List<EnemySaveData>();
    public List<TorchSaveData> torches = new List<TorchSaveData>();
    public List<TorchSaveDataEnd> torches2 = new List<TorchSaveDataEnd>();
    public List<PlantSaveData> plants = new List<PlantSaveData>();
    public List<PlantNoSaveData> plantsNo = new List<PlantNoSaveData>();
}

[System.Serializable]
public class EnemySaveData
{
    public bool isDead;
    public float health;
}

[System.Serializable]
public class TorchSaveData
{
    public bool isOn;
}

[System.Serializable]
public class TorchSaveDataEnd
{
    public bool isOn;
}

[System.Serializable]
public class PlantSaveData
{
    public bool hasBloomed;
}

[System.Serializable]
public class PlantNoSaveData
{
    public bool hasBloomed;
}

public class CheckPointJSON : MonoBehaviour
{
    public static CheckPointJSON Instance { get; private set; }

    /*public Animator animator;
    public Image dialogImage;
    public TMP_Text dialogText;
    public string idleMessage;*/
    /*public Image SaveddialogImage;
    public TMP_Text SaveddialogText;
    public string SavedidleMessage;*/

    //public float detectionRadius = 3f;
    private bool playerInRange = false;

    public GameObject player;
    public HealthManager playerHealthScript;
    //public ParticleSystem checkpointParticleSystem;

    public Vector3 savedPosition;
    //private Vector3 initialPosition;
    //private bool initial = false;
    //private bool positionChanged = false;

    public bool isSaved = false;

    public BloodPotionManager bloodPotionManager;
    public TypeCoinManager typeCoinManager;
    public Shop shop;

    public List<EnemyHealthManager> enemies;
    public TurnOffTouch[] torches;
    public Torch_OnOff[] torches2;
    public Touch_Plant[] plants;
    public Touch_Plant_No[] plantsNo;

    public static string saveFilePath;

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
        saveFilePath = Application.dataPath + "/savegame.json";
        //Debug.Log("Save file path: " + saveFilePath);
    }

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        LevelSaveData lvsaveData = LoadSaveData();
        if (lvsaveData == null)
        {
            SaveGame();
        }
        else
        {
            string savedSceneName = lvsaveData.SavedSceneNameJSON;
            if (sceneName != savedSceneName)
            {
                SaveGame();  // Save if the scene has changed
            }
        }
        /*if (File.Exists(saveFilePath))
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
        }*/
    }

    void Update()
    {
        if (playerInRange)
        {
            if (!isSaved) // && dialogText != null)
            {
                /*dialogText.enabled = true;
                dialogImage.enabled = true;
                dialogText.text = idleMessage;*/
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //initial = true;
                    isSaved = true;
                    SaveGame();
                }
            }
        }
       /* else
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
        }*/
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            /*if (!isSaved)
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
            }*/
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            /*if (!isSaved)
            {
                transform.position = initialPosition;
                positionChanged = false;
                animator.SetBool("IsActive", false);
                if (checkpointParticleSystem != null)
                {
                    checkpointParticleSystem.Stop();
                }
            }*/
        }
    }

    public void SaveGame()
    {
        Debug.Log("File JSON Saved");
        LevelSaveData currentLevelData = new LevelSaveData
        {
            SavedSceneNameJSON = SceneManager.GetActiveScene().name,
            savedPosition = player.transform.position,
            //initialPosition = initialPosition,
            savedHealth = playerHealthScript.currentHealth,
            savedBloodPotionCount = bloodPotionManager.bottleCount,
            savedGhostCount = typeCoinManager.ghostCount,
            savedBloodCount = typeCoinManager.bloodCount,
            savedItemRunOut = shop.itemRunOut,
            SavedMaxValueItem = Mathf.FloorToInt(shop.quantitySlider.maxValue)
            //isSaved = isSaved,
            //positionChanged = positionChanged
        };

        // Save enemy states
        foreach (var enemy in enemies)
        {
            currentLevelData.enemies.Add(new EnemySaveData
            {
                isDead = enemy.isDead,
                health = enemy.health
            });
        }

        // Save torch states
        foreach (var torch in torches)
        {
            currentLevelData.torches.Add(new TorchSaveData
            {
                isOn = torch.isTorchOn
            });
        }

        // Save torch_end states
        foreach (var torch in torches2)
        {
            currentLevelData.torches2.Add(new TorchSaveDataEnd
            {
                isOn = torch.isOn
            });
        }

        // Save plant states
        foreach (var plant in plants)
        {
            currentLevelData.plants.Add(new PlantSaveData
            {
                hasBloomed = plant.hasBloomed
            });
        }

        // Save plant_no states
        foreach (var plant in plantsNo)
        {
            currentLevelData.plantsNo.Add(new PlantNoSaveData
            {
                hasBloomed = plant.hasBloomed
            });
        }

        SaveData saveData = new SaveData();
        string levelKey = "Level " + SceneManager.GetActiveScene().buildIndex;  
        saveData.levels.Add(levelKey, currentLevelData);

        // Serialize the save data to JSON
        string json = JsonUtility.ToJson(currentLevelData, true);
        System.IO.File.WriteAllText(saveFilePath, json);
    }
    public LevelSaveData LoadSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            LevelSaveData saveData = JsonUtility.FromJson<LevelSaveData>(json);
            return saveData;
        }
        else
        {
            return null;
        }
    }
    /*
        public void LoadGame()
        {
            if (File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath);
                SaveData saveData = JsonUtility.FromJson<SaveData>(json);

                // Load data
                player.transform.position = saveData.savedPosition;
                playerHealthScript.currentHealth = saveData.savedHealth;
                playerHealthScript.UpdateHealthbar();

                // Load inventory
                bloodPotionManager.bottleCount = saveData.savedBloodPotionCount;
                typeCoinManager.ghostCount = saveData.savedGhostCount;
                typeCoinManager.bloodCount = saveData.savedBloodCount;
                shop.itemRunOut = saveData.savedItemRunOut;
                shop.quantitySlider.maxValue = saveData.SavedMaxValueItem;
                // Load enemies
                for (int i = 0; i < saveData.enemies.Count; i++)
                {
                    var enemyData = saveData.enemies[i];
                    if (i < enemies.Count)
                    {
                        enemies[i].isDead = enemyData.isDead;
                        enemies[i].health = enemyData.health;
                        enemies[i].gameObject.SetActive(!enemyData.isDead);
                    }
                }

                // Load torches and plants
                for (int i = 0; i < saveData.torches.Count; i++)
                {
                    if (i < torches.Length)
                    {
                        torches[i].isTorchOn = saveData.torches[i].isOn;
                    }
                }

                for (int i = 0; i < saveData.plants.Count; i++)
                {
                    if (i < plants.Length)
                    {
                        plants[i].hasBloomed = saveData.plants[i].hasBloomed;
                    }
                }
            }
        }*/
    public static void DeleteSaveFile()
    {
        string delsaveFilePath = Application.dataPath + "/savegame.json";
        if (File.Exists(delsaveFilePath))
        {
            File.Delete(delsaveFilePath);
        }
    }
}


