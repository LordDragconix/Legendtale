using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour
{
    private string universalDataPath;

    [Header("Main Menu Scenes")]
    public string newPlayerMenu = "MainMenu_Prologue"; // Scene for new players
    public string returningPlayerMenu = "MainMenu_Default"; // Scene for returning players

    void Start()
    {
        universalDataPath = Path.Combine(Application.persistentDataPath, "universalData.json");
        LoadCorrectMainMenu();
    }

    void LoadCorrectMainMenu()
    {
        bool prologueFinished = false;

        // Check if save file exists and load data
        if (File.Exists(universalDataPath))
        {
            string json = File.ReadAllText(universalDataPath);
            UniversalData universalData = JsonUtility.FromJson<UniversalData>(json);
            prologueFinished = universalData.Prologue_Finished;
        }

        // Load the correct main menu scene
        if (prologueFinished)
        {
            SceneManager.LoadScene(returningPlayerMenu); // Load main menu for returning players
        }
        else
        {
            SceneManager.LoadScene(newPlayerMenu); // Load main menu for new players
        }
    }
}
