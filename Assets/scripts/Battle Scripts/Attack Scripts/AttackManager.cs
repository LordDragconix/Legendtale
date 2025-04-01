using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public List<AttackData> attackList;
    public bool isBossMode = false;
    public bool isContinuous = false;

    private int bossAttackIndex = 0;
    private bool isAttacking = false;

    private GameObject currentAttackInstance = null; // ?? Track the active attack object

    public void StartAttacks()
    {
        if (!isAttacking)
            StartCoroutine(AttackLoop());
    }

    public void StopAttacks()
    {
        isAttacking = false;
        StopAllCoroutines();

        if (currentAttackInstance != null)
        {
            // Try to stop it properly if it has a stop method
            var spawner = currentAttackInstance.GetComponent<BulletSpawner>();
            if (spawner != null)
                spawner.StopAttack();

            Destroy(currentAttackInstance);
        }
    }

    IEnumerator AttackLoop()
    {
        isAttacking = true;

        while (isAttacking)
        {
            AttackData attack = GetNextAttack();

            // Cleanup previous attack before starting a new one
            if (currentAttackInstance != null)
            {
                var spawner = currentAttackInstance.GetComponent<BulletSpawner>();
                if (spawner != null)
                    spawner.StopAttack();

                Destroy(currentAttackInstance);
            }

            // Spawn new attack and keep track of it
            currentAttackInstance = Instantiate(
                attack.attackPrefab,
                transform.position,
                Quaternion.identity,
                transform // parent it to monster
            );

            float duration = attack.waitForManualStop
                ? Mathf.Infinity
                : isBossMode && attack.bossDurationOverride > 0f
                    ? attack.bossDurationOverride
                    : attack.defaultDuration;

            yield return new WaitForSeconds(duration);

            if (!isContinuous && !attack.waitForManualStop)
                break;
        }

        isAttacking = false;
    }

    AttackData GetNextAttack()
    {
        if (isBossMode)
        {
            var attack = attackList[bossAttackIndex];
            bossAttackIndex = (bossAttackIndex + 1) % attackList.Count;
            return attack;
        }

        return attackList[Random.Range(0, attackList.Count)];
    }
}
