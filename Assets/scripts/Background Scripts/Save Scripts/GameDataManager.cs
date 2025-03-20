using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;

    private string gameDataPath;
    private string playerSavePath;
    private string achievementsDataPath;
    private string universalDataPath;

    public GameData gameData;
    public PlayerData playerSave;
    public AchievementsData achievementsData;
    public UniversalData universalData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this between scenes

            gameDataPath = Application.persistentDataPath + "/gameData.json";
            playerSavePath = Application.persistentDataPath + "/playerSave.json";
            achievementsDataPath = Application.persistentDataPath + "/achievementsData.json";
            universalDataPath = Application.persistentDataPath + "/universalData.json";

            LoadAllData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadAllData()
    {
        gameData = LoadData<GameData>(gameDataPath, new GameData());
        playerSave = LoadData<PlayerData>(playerSavePath, new PlayerData());
        achievementsData = LoadData<AchievementsData>(achievementsDataPath, new AchievementsData());
        universalData = LoadData<UniversalData>(universalDataPath, new UniversalData());

        Debug.Log("All data files loaded successfully.");
    }

    public void SaveAllData()
    {
        SaveData(gameDataPath, gameData);
        SaveData(playerSavePath, playerSave);
        SaveData(achievementsDataPath, achievementsData);
        SaveData(universalDataPath, universalData);

        Debug.Log("All data files saved successfully.");
    }

    private T LoadData<T>(string filePath, T defaultData)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            SaveData(filePath, defaultData); // Create a new default file
            return defaultData;
        }
    }

    private void SaveData<T>(string filePath, T data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }
}
