using UnityEngine;
using UnityEngine.UI;

public class SavePointChecker : MonoBehaviour
{
    public Button targetButton; // Assign the UI button in Inspector

    void Start()
    {
        CheckLastSavePoint();
    }

    void CheckLastSavePoint()
    {
        if (GameDataManager.instance != null)
        {
            string lastSavePoint = GameDataManager.instance.gameData.lastSavePoint;

            if (lastSavePoint == "None" && targetButton != null)
            {
                targetButton.interactable = false;
                Debug.Log("Button disabled: No save point found.");
            }
            else
            {
                targetButton.interactable = true;
                Debug.Log("Button enabled: Save point found.");
            }
        }
        else
        {
            Debug.LogError("GameDataManager instance not found!");
        }
    }
}
