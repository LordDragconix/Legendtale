using UnityEngine;

public class LaserCannonSpawner : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform firePoint; // Where the beam starts (e.g. Flowey’s mouth)
    public float delayBetweenShots = 3f;

    void Start()
    {
        InvokeRepeating(nameof(FireLaser), 0f, delayBetweenShots);
    }

    void FireLaser()
    {
        Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
    }
}
