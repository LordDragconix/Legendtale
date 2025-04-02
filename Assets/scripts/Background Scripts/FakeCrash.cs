using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System;

public class FakeCrash : MonoBehaviour
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    public void TriggerFakeCrash()
    {
        // Step 1: Reset Player Stats
        ResetPlayerStats();

        // Step 2: Set Prologue as Finished
        SetPrologueFinished();

        // Step 3: Unlock "Gotta Start Somewhere" Achievement
        UnlockAchievement("Gotta Start Somewhere");

        // Step 4: Show Fake Unity Crash Message
        ShowFakeErrorAndInfoMessages();

    }

    private void ResetPlayerStats()
    {
        string playerDataPath = Path.Combine(Application.persistentDataPath, "playerSave.json");

        PlayerData resetData = new PlayerData
        {
            LV = 1,
            EXP = 0,
            maxHP = 20,
            currentHP = 20,
            ATK = 0,
            DEF = 0,
            SPD = 5,
            ML = 0,
            G = 0,
            armor = "None",
            weapon = "None"
        };

        for (int i = 0; i < resetData.inventory.Count; i++)
        {
            resetData.inventory[i] = "None";
        }

        SaveData(resetData, playerDataPath);
        Debug.Log("Player stats have been fully reset to their base values.");
    }

    private void SetPrologueFinished()
    {
        string universalDataPath = Path.Combine(Application.persistentDataPath, "universalData.json");

        // Load existing Universal Data
        UniversalData universalData = LoadData<UniversalData>(universalDataPath);

        // Update the Prologue_Finished flag
        universalData.Prologue_Finished = true;

        // Save the updated Universal Data
        SaveData(universalData, universalDataPath);

        Debug.Log("Prologue_Finished has been set to TRUE in universalData.json.");
    }

    private void UnlockAchievement(string achievementName)
    {
        string achievementsDataPath = Path.Combine(Application.persistentDataPath, "achievementsData.json");

        AchievementsData achievementsData = LoadData<AchievementsData>(achievementsDataPath);

        foreach (var achievement in achievementsData.normalAchievementsList)
        {
            if (achievement.name == achievementName)
            {
                achievement.unlocked = true;
                break;
            }
        }

        SaveData(achievementsData, achievementsDataPath);
        Debug.Log($"Achievement Unlocked: {achievementName}");
    }

    private void ShowFakeErrorAndInfoMessages()
    {
        new Thread(() =>
        {
            // First, show the error message
            MessageBox(IntPtr.Zero, "An unknown error has occurred due to corrupted files. Please press 'OK' to restore a backup file.", "Undertale.exe Error", 0x00000050);

            // After the user dismisses the error, show the info message
            MessageBox(IntPtr.Zero, "A backup has been found and restored. Please restart the game.", "Legendtale.exe", 0x00000060);

            // Once the second message box is closed, exit the game
            CloseGame();

        }).Start();
    }




    private void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        Debug.Log("Game force-closed after fake crash.");
    }

    private T LoadData<T>(string filePath) where T : new()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(json);
        }
        return new T();
    }

    private void SaveData<T>(T data, string filePath)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }
}
