using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int maxHP = 20; // Max HP of the player
    private int currentHP;
    public int defense = 5; // Defense value

    public string sceneName;

    private Vector2 lastPosition;
    private bool isMoving;

    public HealthBar healthBar; // Reference to a health bar UI (assign in Inspector)

    void Start()
    {
        currentHP = maxHP;
        lastPosition = transform.position;
        healthBar.SetMaxHealth(maxHP); // Initialize health bar
    }

    void Update()
    {
        // Check if player is moving
        isMoving = (Vector2)transform.position != lastPosition;
        lastPosition = transform.position;
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        healthBar.SetHealth(currentHP);
        Debug.Log($"Player healed {amount} HP. HP: {currentHP}");
    }


    public bool IsMoving()
    {
        return isMoving;
    }

    public void TakeDamage(int damage)
    {
        int reducedDamage = Mathf.Max(1, damage - Mathf.RoundToInt(defense / 5f)); // Defense formula
        currentHP -= reducedDamage;
        currentHP = Mathf.Max(0, currentHP); // Ensure HP doesn't go below 0

        healthBar.SetHealth(currentHP); // Update health bar

        Debug.Log($"Player took {reducedDamage} damage. HP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene(sceneName);
    }
}
