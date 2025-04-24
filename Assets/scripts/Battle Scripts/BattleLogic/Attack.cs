using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage = 10;               // Base damage of the attack
    public bool isOrangeAttack = false;   // Orange logic (damages if NOT moving)
    public bool isBlueAttack = false;     // Blue logic (damages if moving)
    public bool destroyOnHit = true;      // 🔁 NEW: Determines if the attack should be destroyed on hit

    private void OnTriggerStay2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            bool isMoving = player.IsMoving();
            bool shouldDealDamage = false;

            if (isOrangeAttack && !isMoving)
                shouldDealDamage = true;
            else if (isBlueAttack && isMoving)
                shouldDealDamage = true;
            else if (!isOrangeAttack && !isBlueAttack)
                shouldDealDamage = true;

            if (shouldDealDamage)
            {
                player.TakeDamage(damage);

                if (destroyOnHit)
                    Destroy(gameObject); // Only destroy if toggle is on
            }
        }
    }
}
