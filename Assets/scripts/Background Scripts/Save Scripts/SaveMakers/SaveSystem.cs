using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string playerDataPath;
    private string gameDataPath;
    private string universalDataPath;
    private string achievementsDataPath;

    void Start()
    {
        // Define file paths
        playerDataPath = Path.Combine(Application.persistentDataPath, "playerSave.json");
        gameDataPath = Path.Combine(Application.persistentDataPath, "gameData.json");
        universalDataPath = Path.Combine(Application.persistentDataPath, "universalData.json");
        achievementsDataPath = Path.Combine(Application.persistentDataPath, "achievementsData.json");

        // Create save files if they do not exist
        CreateSaveFiles();
    }

    public void CreateSaveFiles()
    {
        if (!File.Exists(playerDataPath)) SaveData(new PlayerData(), playerDataPath);
        if (!File.Exists(gameDataPath)) SaveData(new GameData(), gameDataPath);
        if (!File.Exists(universalDataPath)) SaveData(new UniversalData(), universalDataPath);
        if (!File.Exists(achievementsDataPath)) SaveData(new AchievementsData(), achievementsDataPath);

        Debug.Log("Save files initialized.");
    }

    private void SaveData<T>(T data, string filePath)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Saved to: " + filePath);
    }

    public void ResetGameData()
    {
        SaveData(new GameData(), gameDataPath);
        Debug.Log("Game data reset.");
    }
}
