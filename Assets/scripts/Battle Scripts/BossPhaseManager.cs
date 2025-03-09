using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossPhaseManager : MonoBehaviour
{
    [Header("Boss UI Elements")]
    public Slider bossHealthSlider;
    public Text phaseText; // Shows "Teal Soul Phase", etc.

    [Header("Boss Stats")]
    public int maxHP = 500;
    private int currentHP;
    public int currentDefense;

    [Header("Phase Settings")]
    public int[] phaseHPThresholds = { 450, 400, 350, 300, 250, 200 }; // HP levels where phases change
    public int[] defenseValues = { 20, 15, 10, 7, 5, 0 }; // DEF values per phase

    private int currentPhase = 0;
    private bool isInSoulPhase = false;

    private void Start()
    {
        currentHP = maxHP;
        currentDefense = defenseValues[0]; // Start with Flowey's first DEF value
        UpdateBossUI();
    }

    public void TakeDamage(int playerAttack)
    {
        int damage = Mathf.Max(1, playerAttack - (currentDefense / 5)); // DEF reduces damage
        currentHP -= damage;
        currentHP = Mathf.Max(0, currentHP);
        UpdateBossUI();

        CheckPhaseTransition();
    }

    void CheckPhaseTransition()
    {
        if (currentPhase < phaseHPThresholds.Length && currentHP <= phaseHPThresholds[currentPhase])
        {
            StartCoroutine(EnterSoulPhase());
        }
    }

    IEnumerator EnterSoulPhase()
    {
        isInSoulPhase = true;
        phaseText.gameObject.SetActive(true);

        // Define phase names based on currentPhase index
        string[] soulPhases = { "Teal Soul Phase", "Orange Soul Phase", "Blue Soul Phase", "Purple Soul Phase", "Green Soul Phase", "Yellow Soul Phase" };

        // Display phase text
        phaseText.text = soulPhases[currentPhase];
        yield return new WaitForSeconds(5); // Show soul phase for 5 seconds

        phaseText.gameObject.SetActive(false);
        isInSoulPhase = false;
        currentPhase++;

        if (currentPhase < defenseValues.Length)
        {
            currentDefense = defenseValues[currentPhase]; // Reduce Flowey's DEF
        }
    }

    void UpdateBossUI()
    {
        bossHealthSlider.value = (float)currentHP / maxHP * bossHealthSlider.maxValue;
    }
}
