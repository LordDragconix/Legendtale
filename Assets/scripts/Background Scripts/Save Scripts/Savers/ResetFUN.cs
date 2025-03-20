using UnityEngine;
using System.IO;

public class ResetFUN : MonoBehaviour
{
    private string saveFilePath;

    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/gameData.json";
    }

    public void RandomizeFunValue()
    {
        int newFunValue = Random.Range(1, 101); // Random value between 1 and 100
        Debug.Log("New Fun Value: " + newFunValue);

        SaveFunValue(newFunValue);
    }

    private void SaveFunValue(int funValue)
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);
            gameData.FUN = funValue;

            string updatedJson = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(saveFilePath, updatedJson);

            Debug.Log("Fun Value saved to gameData.json: " + funValue);
        }
        else
        {
            Debug.LogError("Save file not found: " + saveFilePath);
        }
    }
}