using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float delayBeforeFire = 0.5f;
    public float beamDuration = 1.5f;

    private bool isActive = false;

    void Start()
    {
        Invoke(nameof(ActivateBeam), delayBeforeFire);
        Destroy(gameObject, delayBeforeFire + beamDuration);
    }

    void ActivateBeam()
    {
        isActive = true;

        // TODO: Add visual change, flash, or sound here when beam activates
    }
}
