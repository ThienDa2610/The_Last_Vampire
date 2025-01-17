using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GeneralData
{
    public string SavedSceneNameJSON;
    public float savedHealth;
    public Inventory inventory;
}

[System.Serializable]
public class Inventory
{
    public int savedBloodPotionCount;
    public int savedGhostCount;
    public int savedBloodCount;
}
[System.Serializable]
public class MapData
{
    public List<LevelSaveDataEntry1> mapLevels1 = new List<LevelSaveDataEntry1>();
    public List<LevelSaveDataEntry2> mapLevels2 = new List<LevelSaveDataEntry2>();
    public List<LevelSaveDataEntry3> mapLevels3 = new List<LevelSaveDataEntry3>();
    public List<LevelSaveDataEntry4> mapLevels4 = new List<LevelSaveDataEntry4>();
    public List<LevelSaveDataEntry5> mapLevels5 = new List<LevelSaveDataEntry5>();
}
[System.Serializable]
public class LevelSaveDataEntry1
{
    public string levelName;
    public LevelSaveData1 levelData;
}
[System.Serializable]
public class LevelSaveDataEntry2
{
    public string levelName;
    public LevelSaveData2 levelData;
}
[System.Serializable]
public class LevelSaveDataEntry3
{
    public string levelName;
    public LevelSaveData3 levelData;
}
[System.Serializable]
public class LevelSaveDataEntry4
{
    public string levelName;
    public LevelSaveData4 levelData;
}
[System.Serializable]
public class LevelSaveDataEntry5
{
    public string levelName;
    public LevelSaveData5 levelData;
}
[System.Serializable]
public class LevelSaveData1
{
    public Vector3 playerPosition;
    public List<EnemySaveData> enemies = new List<EnemySaveData>();
    public List<TorchSaveData> torches = new List<TorchSaveData>();
    public List<TorchSaveDataEnd> torches2 = new List<TorchSaveDataEnd>();
    public List<PlantSaveData> plants = new List<PlantSaveData>();
    public List<PlantNoSaveData> plantsNo = new List<PlantNoSaveData>();
}
[System.Serializable]
public class LevelSaveData2
{
    public Vector3 playerPosition2;
    public List<EnemySaveData> enemies2 = new List<EnemySaveData>();
    public bool puzzle1_d;
    public bool puzzle1Done_d;
    public bool puzzle2_d;
    public bool puzzle2Done_d;
    public bool puzzle3_d;
    public bool puzzle3Done_d;
    public bool inSS1_d;
    public bool inSS2_d;
    public float heat_d;
    public int activeCameraIndex;
    public bool ItemRunOutInShop_d;
    public int MaxItemInShop_d;
}
[System.Serializable]
public class LevelSaveData3
{
    public Vector3 playerPosition3;
    public List<EnemySaveData> enemies3 = new List<EnemySaveData>();
    public int activeCameraIndex3;
    public bool ItemRunOutInShop_3d;
    public int MaxItemInShop_3d;
}
[System.Serializable]
public class LevelSaveData4
{
    public Vector3 playerPosition4;
    public List<EnemySaveData> enemies4 = new List<EnemySaveData>();
    public List<CaveWorm_CocoonSaved> worms4= new List<CaveWorm_CocoonSaved>();
    public bool ItemRunOutInShop_4d;
    public int MaxItemInShop_4d;
}
[System.Serializable]
public class LevelSaveData5
{
    public Vector3 playerPosition5;
    public List<EnemySaveData> enemies5 = new List<EnemySaveData>();
    public bool ItemRunOutInShop_5d;
    public int MaxItemInShop_5d;
}

[System.Serializable]
public class EnemySaveData
{
    public bool isDead;
    public float health;
}

[System.Serializable]
public class CaveWorm_CocoonSaved
{
    public bool isFallen;
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
[System.Serializable]
public class SaveData
{
    public GeneralData generalData;
    public MapData mapData = new MapData();
}

public class CheckPointJSON : MonoBehaviour
{
    public static CheckPointJSON Instance { get; private set; }


    public GameObject player;
    public HealthManager playerHealthScript;

    public Vector3 savedPosition;

    public BloodPotionManager bloodPotionManager;
    public TypeCoinManager typeCoinManager;

    public List<EnemyHealthManager> enemies_map_1;
    public TurnOffTouch[] torches;
    public Torch_OnOff[] torches2;
    public Touch_Plant[] plants;
    public Touch_Plant_No[] plantsNo;

    public List<EnemyHealthManager> enemies_map_2;
    public Puzzle puzzle1;
    public PicturePuzzle puzzle1Done;
    public ColorChangeAndInput puzzle2;
    public PuzzleInPyramid2 puzzle2Done;
    public VasePuzzle puzzle3;
    public PuzzleInPyramid2 puzzle3Done;
    public GoInSandStorm inSS;
    public HeatBar heat;
    public Camera[] cameras2;
    public Shop shop2;

    public List<EnemyHealthManager> enemies_map_3;
    public Camera[] cameras3;
    public Shop shop3;

    public List<EnemyHealthManager> enemies_map_4;
    public List<CaveWorm_Cocoon> worms_map_4;
    public Shop shop4;

    public List<EnemyHealthManager> enemies_map_5;
    public Shop shop5;

    public static string saveFilePath;

    public GeneralData generalData;

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
        SaveData lvsaveData = LoadSaveData();
        if (lvsaveData == null)
        {
            SaveGame();
        }
        else
        {
            // Access SavedSceneNameJSON from the generalData field
            string savedSceneName = lvsaveData.generalData.SavedSceneNameJSON;
            if (sceneName != savedSceneName)
            {
                SaveGame();  // Save if the scene has changed
            }
        }
    }


    public void SaveGame()
    {
        Debug.Log("File JSON Saved");

        // Assign values to the generalData object
        generalData = new GeneralData
        {
            SavedSceneNameJSON = SceneManager.GetActiveScene().name, // Access SavedSceneNameJSON through generalData
            savedHealth = playerHealthScript.currentHealth,
            inventory = new Inventory
            {
                savedBloodPotionCount = bloodPotionManager.bottleCount,
                savedGhostCount = typeCoinManager.ghostCount,
                savedBloodCount = typeCoinManager.bloodCount,
            }
        };

        SaveData saveData = new SaveData
        {
            generalData = generalData,
            mapData = new MapData()
        };

        string currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "Map1_Forest")
        {
            LevelSaveData1 currentLevelData1 = new LevelSaveData1
            {
                playerPosition = player.transform.position
            };
            // Save enemy states
            foreach (var enemy in enemies_map_1)
            {
                currentLevelData1.enemies.Add(new EnemySaveData
                {
                    isDead = enemy.isDead,
                    health = enemy.health
                });
            }

            // Save torch states
            foreach (var torch in torches)
            {
                currentLevelData1.torches.Add(new TorchSaveData
                {
                    isOn = torch.isTorchOn
                });
            }

            // Save torch_end states
            foreach (var torch in torches2)
            {
                currentLevelData1.torches2.Add(new TorchSaveDataEnd
                {
                    isOn = torch.isOn
                });
            }

            // Save plant states
            foreach (var plant in plants)
            {
                currentLevelData1.plants.Add(new PlantSaveData
                {
                    hasBloomed = plant.hasBloomed
                });
            }

            // Save plant_no states
            foreach (var plant in plantsNo)
            {
                currentLevelData1.plantsNo.Add(new PlantNoSaveData
                {
                    hasBloomed = plant.hasBloomed
                });
            }
            LevelSaveDataEntry1 levelEntry1 = new LevelSaveDataEntry1
            {
                levelName = "Map1_Forest",
                levelData = currentLevelData1
            };
            saveData.mapData.mapLevels1.Add(levelEntry1);
        }


        if (currentSceneName == "Map2_Desert")
        {
            LevelSaveData2 currentLevelData2 = new LevelSaveData2
            {
                playerPosition2 = player.transform.position,  // Corrected field name
                puzzle1_d = puzzle1.GetComponent<Puzzle>().isPuzzleDone,
                puzzle1Done_d = puzzle1Done.GetComponent<PicturePuzzle>().done,
                puzzle2_d = puzzle2.GetComponent<ColorChangeAndInput>().isPuzzleDone,
                puzzle2Done_d = puzzle2Done.GetComponent<PuzzleInPyramid2>().done,
                puzzle3_d = puzzle3.GetComponent<VasePuzzle>().isPuzzleDone,
                puzzle3Done_d = puzzle3Done.GetComponent<PuzzleInPyramid2>().done,
                inSS1_d = inSS.GetComponent<GoInSandStorm>().SS1,
                inSS2_d = inSS.GetComponent<GoInSandStorm>().SS2,
                heat_d = heat.heatSlider.value,
                ItemRunOutInShop_d = shop2.GetComponent<Shop>().itemRunOut,
                MaxItemInShop_d = Mathf.FloorToInt(shop2.quantitySlider.maxValue)
            };

            //Map 2-------------------------------------------
            // Save enemy states
            foreach (var enemy in enemies_map_2)
            {
                currentLevelData2.enemies2.Add(new EnemySaveData
                {
                    isDead = enemy.isDead,
                    health = enemy.health
                });
            }
            for (int i = 0; i < cameras2.Length; i++)
            {
                if (cameras2[i].gameObject.activeInHierarchy)
                {
                    currentLevelData2.activeCameraIndex = i;
                    break;
                }
            }
            LevelSaveDataEntry2 levelEntry2 = new LevelSaveDataEntry2
            {
                levelName = "Map2_Desert",
                levelData = currentLevelData2
            };
            saveData.mapData.mapLevels2.Add(levelEntry2);
        }
        if (currentSceneName == "Map3_City")
        {
            LevelSaveData3 currentLevelData3 = new LevelSaveData3
            {
                playerPosition3 = player.transform.position,  // Corrected field name
                
                ItemRunOutInShop_3d = shop3.GetComponent<Shop>().itemRunOut,
                MaxItemInShop_3d = Mathf.FloorToInt(shop3.quantitySlider.maxValue)
            };

            //Map 3-------------------------------------------
            // Save enemy states
            foreach (var enemy in enemies_map_3)
            {
                currentLevelData3.enemies3.Add(new EnemySaveData
                {
                    isDead = enemy.isDead,
                    health = enemy.health
                });
            }
            for (int i = 0; i < cameras3.Length; i++)
            {
                if (cameras3[i].gameObject.activeInHierarchy)
                {
                    currentLevelData3.activeCameraIndex3 = i;
                    break;
                }
            }
            LevelSaveDataEntry3 levelEntry3 = new LevelSaveDataEntry3
            {
                levelName = "Map3_City",
                levelData = currentLevelData3
            };
            saveData.mapData.mapLevels3.Add(levelEntry3);
        }


        if (currentSceneName == "Map4_Cave")
        {
            LevelSaveData4 currentLevelData4 = new LevelSaveData4
            {
                playerPosition4 = player.transform.position,  // Corrected field name

                ItemRunOutInShop_4d = shop4.GetComponent<Shop>().itemRunOut,
                MaxItemInShop_4d = Mathf.FloorToInt(shop4.quantitySlider.maxValue)
            };

            //Map 4-------------------------------------------
            // Save enemy states
            foreach (var enemy in enemies_map_4)
            {
                currentLevelData4.enemies4.Add(new EnemySaveData
                {
                    isDead = enemy.isDead,
                    health = enemy.health
                });
            }
            foreach (var worm in worms_map_4)
            {
                currentLevelData4.worms4.Add(new CaveWorm_CocoonSaved
                {
                    isFallen = worm.hasFallen,
                    health = worm.GetHealth()
                });
            }
            LevelSaveDataEntry4 levelEntry4 = new LevelSaveDataEntry4
            {
                levelName = "Map4_Cave",
                levelData = currentLevelData4
            };
            saveData.mapData.mapLevels4.Add(levelEntry4);
        }

        if (currentSceneName == "Map5_Ruin")
        {
            LevelSaveData5 currentLevelData5 = new LevelSaveData5
            {
                playerPosition5 = player.transform.position,  // Corrected field name

                ItemRunOutInShop_5d = shop5.GetComponent<Shop>().itemRunOut,
                MaxItemInShop_5d = Mathf.FloorToInt(shop5.quantitySlider.maxValue)
            };

            //Map 5-------------------------------------------
            // Save enemy states
            foreach (var enemy in enemies_map_5)
            {
                currentLevelData5.enemies5.Add(new EnemySaveData
                {
                    isDead = enemy.isDead,
                    health = enemy.health
                });
            }
            
            LevelSaveDataEntry5 levelEntry5 = new LevelSaveDataEntry5
            {
                levelName = "Map5_Ruin",
                levelData = currentLevelData5
            };
            saveData.mapData.mapLevels5.Add(levelEntry5);
        }


        // Serialize the save data to JSON
        string json = JsonUtility.ToJson(saveData, true);
        System.IO.File.WriteAllText(saveFilePath, json);
    }


    public SaveData LoadSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
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


