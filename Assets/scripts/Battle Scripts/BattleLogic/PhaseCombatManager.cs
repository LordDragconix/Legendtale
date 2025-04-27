using UnityEngine;

public class PhaseCombatManager : MonoBehaviour
{
    [Header("Boss & Phase Routing")]
    public GammaFloweyPhaseManager gammaFlowey;
    public SoulPhaseController[] soulPhases;

    private SoulPhaseController currentActiveSoul = null;

    /// <summary>
    /// Called by GammaFloweyPhaseManager when a soul phase starts.
    /// </summary>
    public void SetActiveSoul(SoulPhaseController soul)
    {
        currentActiveSoul = soul;
    }

    /// <summary>
    /// Returns true and the active soul if a soul phase is active.
    /// </summary>
    public bool IsSoulPhaseActive(out SoulPhaseController activeSoul)
    {
        activeSoul = currentActiveSoul;
        return activeSoul != null && activeSoul.IsActive();
    }

    /// <summary>
    /// Handles dealing damage to the correct target (soul or boss).
    /// </summary>
    public void DealDamage(int damage)
    {
        if (IsSoulPhaseActive(out SoulPhaseController soul))
        {
            soul.TakeDamage(damage);
        }
        else if (gammaFlowey != null)
        {
            gammaFlowey.TakeDamage(damage);
        }
    }

    /// <summary>
    /// Returns the current defence value of the active soul/boss.
    /// </summary>
    public int GetCurrentDefense()
    {
        if (IsSoulPhaseActive(out SoulPhaseController _))
            return 0; // Souls don't use defence

        return gammaFlowey != null ? gammaFlowey.GetCurrentDefense() : 0;
    }
}
