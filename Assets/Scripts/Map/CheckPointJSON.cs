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
    public float savedHealth;
    public int savedBloodPotionCount;
    public int savedGhostCount;
    public int savedBloodCount;

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

    private bool playerInRange = false;

    public GameObject player;
    public HealthManager playerHealthScript;

    public Vector3 savedPosition;

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
       
    }

    void Update()
    {
        if (playerInRange)
        {
            if (!isSaved) 
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //initial = true;
                    isSaved = true;
                    SaveGame();
                }
            }
        }
      
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            
        }
    }

    public void SaveGame()
    {
        Debug.Log("File JSON Saved");
        LevelSaveData currentLevelData = new LevelSaveData
        {
            SavedSceneNameJSON = SceneManager.GetActiveScene().name,
            savedPosition = player.transform.position,
            savedHealth = playerHealthScript.currentHealth,
            savedBloodPotionCount = bloodPotionManager.bottleCount,
            savedGhostCount = typeCoinManager.ghostCount,
            savedBloodCount = typeCoinManager.bloodCount,
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
   
    public static void DeleteSaveFile()
    {
        string delsaveFilePath = Application.dataPath + "/savegame.json";
        if (File.Exists(delsaveFilePath))
        {
            File.Delete(delsaveFilePath);
        }
    }
}


