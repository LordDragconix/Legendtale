using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance;

    public List<AttackData> attackList;
    public bool isBossMode = false;
    public bool isContinuous = false;

    private int bossAttackIndex = 0;
    private bool isAttacking = false;

    private GameObject currentAttackInstance = null;

    private List<SpiralProjectileSpawner> activeSpawners = new List<SpiralProjectileSpawner>();

    public void RegisterSpawner(SpiralProjectileSpawner spawner)
    {
        if (spawner != null && !activeSpawners.Contains(spawner))
            activeSpawners.Add(spawner);
    }

    private void StopAllRegisteredSpawners()
    {
        foreach (var s in activeSpawners)
        {
            if (s != null)
                s.StopSpawning();
        }
        activeSpawners.Clear();
    }

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
            var spawner = currentAttackInstance.GetComponent<BulletSpawner>();
            if (spawner != null)
                spawner.StopAttack();

            Destroy(currentAttackInstance);
            StopAllRegisteredSpawners();

        }
    }

    IEnumerator AttackLoop()
    {
        isAttacking = true;

        GameObject previousAttackInstance = null;
        AttackData previousAttackData = null;

        while (isAttacking)
        {
            AttackData currentAttack = GetNextAttack();

            // Stop and detach previous attack (but don't destroy yet)
            if (previousAttackInstance != null && previousAttackData != null)
            {
                var prevSpawner = previousAttackInstance.GetComponent<BulletSpawner>();
                if (prevSpawner != null)
                    prevSpawner.StopAttack();

                previousAttackInstance.transform.SetParent(null);
                Destroy((Object)previousAttackInstance, previousAttackData.despawnDelay); // Explicit cast to avoid overload issues

                StopAllRegisteredSpawners();


            }

            // Spawn new attack and track it
            currentAttackInstance = Instantiate(
                currentAttack.attackPrefab,
                transform.position,
                Quaternion.identity,
                transform
            );

            // Store current as previous for next loop
            previousAttackInstance = currentAttackInstance;
            previousAttackData = currentAttack;

            float duration = currentAttack.waitForManualStop
                ? Mathf.Infinity
                : isBossMode && currentAttack.bossDurationOverride > 0f
                    ? currentAttack.bossDurationOverride
                    : currentAttack.defaultDuration;

            yield return new WaitForSeconds(duration);

            if (!isContinuous && !currentAttack.waitForManualStop)
                break;
        }

        isAttacking = false;
    }

    AttackData GetNextAttack()
    {
        if (attackList == null || attackList.Count == 0)
            return null;

        if (isBossMode)
        {
            var attack = attackList[bossAttackIndex];
            bossAttackIndex = (bossAttackIndex + 1) % attackList.Count;
            return attack;
        }

        return attackList[Random.Range(0, attackList.Count)];
    }
    void Awake()
    {
        Instance = this;
    }

}
