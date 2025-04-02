using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Button fightButton;
    public Button itemButton;
    public Slider playerHealthSlider;
    public Slider bossHealthSlider;
    public TextMeshProUGUI damageText;

    [Header("Enemy Stats")]
    public int enemyHP = 500;
    public int enemyDefense = 10;

    [Header("Player Stats")]
    private int playerAttackPower;

    [Header("Timers")]
    public float minDisableTime = 30f;
    public float maxDisableTime = 60f;

    private int itemUses = 0;
    private const int maxItemUses = 8;

    private void Start()
    {
        LoadPlayerAttackPower();
        fightButton.interactable = true;
        itemButton.interactable = true;
        bossHealthSlider.gameObject.SetActive(false);
        damageText.gameObject.SetActive(false);
    }

    void LoadPlayerAttackPower()
    {
        string path = Application.persistentDataPath + "/playerSave.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            playerAttackPower = data.ATK;
        }
        else
        {
            Debug.LogWarning("Player save file not found. Defaulting ATK to 0.");
            playerAttackPower = 0;
        }
    }

    public void UseItem()
    {
        if (itemUses < maxItemUses)
        {
            int healAmount = Random.Range(42, 100);
            FindObjectOfType<Player>().Heal(healAmount);
            itemUses++;

            if (itemUses >= maxItemUses)
            {
                itemButton.interactable = false; // Disable item button after 8 uses
            }

            DisableButtonsTemporarily(); // Temporarily disable both buttons
        }
    }

    public void UseFight()
    {
        fightButton.interactable = false; // Disable fight button immediately
        DisableButtonsTemporarily(); // Temporarily disable both buttons

        int damageDealt = CalculateDamage();
        enemyHP -= damageDealt;

        bossHealthSlider.gameObject.SetActive(true);
        bossHealthSlider.value = Mathf.Clamp(enemyHP, 0, bossHealthSlider.maxValue);

        damageText.text = "-" + damageDealt.ToString();
        damageText.gameObject.SetActive(true);

        StartCoroutine(HideBossHealthBarAfterSeconds(3));
    }

    IEnumerator HideBossHealthBarAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        damageText.gameObject.SetActive(false);
        bossHealthSlider.gameObject.SetActive(false);
    }

    int CalculateDamage()
    {
        int reducedDamage = enemyDefense / 5;
        int finalDamage = Mathf.Max(1, playerAttackPower - reducedDamage);
        return finalDamage;
    }

    void DisableButtonsTemporarily()
    {
        fightButton.interactable = false;
        itemButton.interactable = false;
        StartCoroutine(ReEnableButtonsAfterDelay());
    }

    IEnumerator ReEnableButtonsAfterDelay()
    {
        yield return new WaitForSeconds(Random.Range(minDisableTime, maxDisableTime));

        if (itemUses < maxItemUses)
            itemButton.interactable = true;

        fightButton.interactable = true;
    }
}

namespace Legendtale
{
    [System.Serializable]
    public class PlayerData
    {
        public int ATK;
    }
}