// <-- Replace everything in SoulPhaseController.cs with this -->

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulPhaseController : MonoBehaviour
{
    [Header("Soul Stats")]
    public int soulHP = 100;
    public string soulName;

    [Header("UI")]
    public Slider soulHealthSlider;
    public TextMeshProUGUI damageText;

    private bool isActive = false;

    private void Start()
    {
        if (soulHealthSlider != null)
        {
            soulHealthSlider.maxValue = soulHP;
            soulHealthSlider.value = soulHP;
            soulHealthSlider.gameObject.SetActive(false);
        }

        if (damageText != null)
            damageText.gameObject.SetActive(false);
    }

    public void ActivateSoul()
    {
        isActive = true;

        if (soulHealthSlider != null)
        {
            soulHealthSlider.value = soulHP;
            soulHealthSlider.gameObject.SetActive(true);
        }
    }

    public void DeactivateSoul()
    {
        isActive = false;

        if (soulHealthSlider != null)
            soulHealthSlider.gameObject.SetActive(false);

        if (damageText != null)
            damageText.gameObject.SetActive(false);
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void TakeDamage(int amount)
    {
        if (!isActive) return;

        soulHP = Mathf.Max(0, soulHP - amount);
        UpdateSoulUI(amount);

        if (soulHP <= 0)
        {
            Debug.Log($"{soulName} Soul Defeated!");
            DeactivateSoul();

            PhaseCombatManager pcm = FindObjectOfType<PhaseCombatManager>();
            if (pcm != null)
                pcm.SetActiveSoul(null);

            FloweyPhaseDisplayManager display = FindObjectOfType<FloweyPhaseDisplayManager>();
            if (display != null)
                display.EndPhaseManually();
        }
    }

    private void UpdateSoulUI(int damageDealt)
    {
        if (soulHealthSlider != null)
        {
            soulHealthSlider.value = soulHP;
            soulHealthSlider.gameObject.SetActive(true);
        }

        if (damageText != null)
        {
            damageText.text = "-" + damageDealt.ToString();
            damageText.gameObject.SetActive(true);
        }

        StopAllCoroutines();
        StartCoroutine(HideDamageUIAfterSeconds(3f));
    }

    IEnumerator HideDamageUIAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (damageText != null)
            soulHealthSlider.gameObject.SetActive(false);
            damageText.gameObject.SetActive(false);
    }
}
