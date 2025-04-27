using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class BattleUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Button fightButton;
    public Button itemButton;
    public Slider playerHealthSlider;

    [Header("Timers")]
    public float minDisableTime = 30f;
    public float maxDisableTime = 60f;

    [Header("Boss Routing")]
    public PhaseCombatManager combatManager;

    [Header("Enemies In Battle")]
    public List<EnemyStats> activeEnemies = new List<EnemyStats>();

    private int playerAttackPower;
    private int itemUses = 0;
    private const int maxItemUses = 8;

    private void Start()
    {
        LoadPlayerAttackPower();

        if (combatManager == null)
            combatManager = FindObjectOfType<PhaseCombatManager>();

        fightButton.interactable = true;
        itemButton.interactable = true;
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
                itemButton.interactable = false;

            DisableButtonsTemporarily();
        }
    }

    public void UseFight()
    {
        fightButton.interactable = false;
        DisableButtonsTemporarily();

        int def = combatManager != null ? combatManager.GetCurrentDefense() : 0;
        int damage = CalculateDamage(playerAttackPower, def);

        if (combatManager != null)
        {
            combatManager.DealDamage(damage);
        }
        else if (activeEnemies.Count > 0 && activeEnemies[0] != null)
        {
            // fallback for normal enemies
            EnemyStats target = activeEnemies[0];
            int enemyDef = target.GetDefense();
            int dmg = CalculateDamage(playerAttackPower, enemyDef);
            target.TakeDamage(dmg);
        }
    }

    int CalculateDamage(int atk, int def)
    {
        int reducedDamage = def / 5;
        return Mathf.Max(1, atk - reducedDamage);
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
