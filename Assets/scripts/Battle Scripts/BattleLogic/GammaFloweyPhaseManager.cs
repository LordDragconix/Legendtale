using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GammaFloweyPhaseManager : MonoBehaviour
{
    [Header("Boss Stats")]
    public int bossHP = 5000;
    public int bossDefense = 600;

    [Header("UI References")]
    public Slider bossHealthSlider;
    public TextMeshProUGUI damageText;

    private int[] phaseHPThresholds = new int[] { 4900, 4500, 4000, 3200, 2500, 1500 };
    private bool[] phaseTriggered;

    private FloweyPhaseDisplayManager phaseDisplay;
    private int currentPhaseIndex = 0;

    private void Start()
    {
        phaseTriggered = new bool[phaseHPThresholds.Length];
        phaseDisplay = FindObjectOfType<FloweyPhaseDisplayManager>();

        if (phaseDisplay == null)
            Debug.LogError("FloweyPhaseDisplayManager not found in scene!");

        if (bossHealthSlider != null)
        {
            bossHealthSlider.maxValue = bossHP;
            bossHealthSlider.value = bossHP;
            bossHealthSlider.gameObject.SetActive(false);
        }

        if (damageText != null)
            damageText.gameObject.SetActive(false);
    }

    public int GetCurrentHP() => bossHP;
    public int GetCurrentDefense() => bossDefense;
    public int GetCurrentPhaseIndex() => currentPhaseIndex;

    public void TakeDamage(int amount)
    {
        bossHP = Mathf.Max(0, bossHP - amount);
        UpdateHealthUI(amount);
        CheckForPhaseChange();
    }

    private void UpdateHealthUI(int damageDealt)
    {
        if (bossHealthSlider != null)
        {
            bossHealthSlider.value = bossHP;
            bossHealthSlider.gameObject.SetActive(true);
        }

        if (damageText != null)
        {
            damageText.text = "-" + damageDealt.ToString();
            damageText.gameObject.SetActive(true);
        }

        StopAllCoroutines();
        StartCoroutine(HideDamageUIAfterDelay(3f));
    }

    IEnumerator HideDamageUIAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (damageText != null)
            damageText.gameObject.SetActive(false);

        if (bossHealthSlider != null)
            bossHealthSlider.gameObject.SetActive(false);
    }

    private void CheckForPhaseChange()
    {
        for (int i = 0; i < phaseHPThresholds.Length; i++)
        {
            if (!phaseTriggered[i] && bossHP <= phaseHPThresholds[i])
            {
                TriggerPhase(i);
                break;
            }
        }
    }

    private void TriggerPhase(int phaseIndex)
    {
        phaseTriggered[phaseIndex] = true;
        bossDefense = Mathf.Max(0, bossDefense - 100);
        currentPhaseIndex = phaseIndex + 1;

        Debug.Log($"[GammaFloweyPhaseManager] Soul Phase {phaseIndex + 1} is being prepared...");

        AttackManager.Instance?.StopAttacks();
        MusicManager.Instance?.Play("ToSoulPhase_" + phaseIndex);

        StartCoroutine(DelayedSoulPhaseStart(phaseIndex)); // ✅ This must be here
    }

private IEnumerator DelayedSoulPhaseStart(int phaseIndex)
    {
        yield return new WaitForSeconds(5f);
        Debug.Log($"[GammaFloweyPhaseManager] Starting soul phase {phaseIndex}");

        if (phaseDisplay != null)
        {
            Debug.Log($"[GammaFloweyPhaseManager] Telling FloweyPhaseDisplayManager to activate phase {phaseIndex}");
            phaseDisplay.ActivatePhase(phaseIndex);
        }
        else
        {
            Debug.LogWarning("No phaseDisplay assigned!");
        }

        MusicManager.Instance?.Play("Soul_" + phaseIndex);
    }

}
