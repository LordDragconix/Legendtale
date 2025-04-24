using System.Collections.Generic;
using UnityEngine;

public class AttackTimer : MonoBehaviour
{
    public float minTime = 3f;
    public float maxTime = 6f;

    private float currentTime;
    private List<AttackManager> attackManagers = new List<AttackManager>();
    private bool timerRunning = false;

    public void RegisterManager(AttackManager manager)
    {
        if (!attackManagers.Contains(manager))
            attackManagers.Add(manager);
    }

    public void StartTimer()
    {
        currentTime = Random.Range(minTime, maxTime);
        timerRunning = true;
    }

    void Update()
    {
        if (!timerRunning) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            timerRunning = false;
            foreach (var manager in attackManagers)
            {
                manager.StopAttacks();
            }
        }
    }
}
