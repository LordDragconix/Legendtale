using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP = 100;
    public int defense = 10;

    public Slider hpSlider;
    public TextMeshProUGUI damageText;

    public void TakeDamage(int amount)
    {
        currentHP = Mathf.Max(0, currentHP - amount);
        if (hpSlider != null)
            hpSlider.value = currentHP;

        if (damageText != null)
        {
            damageText.text = "-" + amount.ToString();
            damageText.gameObject.SetActive(true);
            StartCoroutine(HideDamageText());
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public int GetDefense()
    {
        return defense;
    }

    void Die()
    {
        // Play death animation, remove from list, etc.
        Destroy(gameObject);
    }

    IEnumerator HideDamageText()
    {
        yield return new WaitForSeconds(2f);
        damageText.gameObject.SetActive(false);
    }
}
