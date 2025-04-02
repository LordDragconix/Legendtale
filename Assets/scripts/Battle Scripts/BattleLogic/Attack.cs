using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage = 10; // Base damage of the attack
    public bool isOrangeAttack = false; // Orange Attack logic (should pass if moving)
    public bool isBlueAttack = false; // Blue Attack logic (should pass if NOT moving)

    private void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            bool isMoving = player.IsMoving();
            bool shouldDealDamage = false;

            // Corrected Orange and Blue Attack logic
            if (isOrangeAttack && !isMoving)
            {
                // Orange attack deals damage if the player is NOT moving
                shouldDealDamage = true;
            }
            else if (isBlueAttack && isMoving)
            {
                // Blue attack deals damage if the player IS moving
                shouldDealDamage = true;
            }
            else if (!isOrangeAttack && !isBlueAttack)
            {
                // Normal attack always deals damage
                shouldDealDamage = true;
            }

            // Apply damage only if conditions are met
            if (shouldDealDamage)
            {
                player.TakeDamage(damage);
                Destroy(gameObject); // Destroy attack if it deals damage
            }
        }
    }
}
