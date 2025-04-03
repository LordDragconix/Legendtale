using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Player : MonoBehaviour
{
    public int maxHP = 20;
    private int currentHP;
    public int defense = 5;

    public string sceneName;
    public HealthBar healthBar;

    private Vector2 lastPosition;
    private bool isMoving;

    private bool isInvincible = false;
    private float invincibilityTime = 0.33f; // Default fallback: 20 frames at 60fps

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        currentHP = maxHP;
        lastPosition = transform.position;
        healthBar.SetMaxHealth(maxHP);

        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadInvincibilityFromSave();
    }

    void Update()
    {
        isMoving = (Vector2)transform.position != lastPosition;
        lastPosition = transform.position;
    }

    public bool IsMoving() => isMoving;

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        healthBar.SetHealth(currentHP);
        Debug.Log($"Player healed {amount} HP. HP: {currentHP}");
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        int reducedDamage = Mathf.Max(1, damage - Mathf.RoundToInt(defense / 5f));
        currentHP -= reducedDamage;
        currentHP = Mathf.Max(0, currentHP);

        healthBar.SetHealth(currentHP);
        Debug.Log($"Player took {reducedDamage} damage. HP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene(sceneName);
    }

    private void LoadInvincibilityFromSave()
    {
        if (GameDataManager.instance != null && GameDataManager.instance.playerSave != null)
        {
            invincibilityTime = GameDataManager.instance.playerSave.INV / 60f;
        }
        else
        {
            Debug.LogWarning("GameDataManager or playerSave is missing, using default INV.");
        }

    }

    System.Collections.IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        float blinkRate = 0.1f;
        float elapsed = 0f;

        while (elapsed < invincibilityTime)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(blinkRate);
            elapsed += blinkRate;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        isInvincible = false;
    }

    [System.Serializable]
    private class PlayerData
    {
        public float INV;
    }

}
