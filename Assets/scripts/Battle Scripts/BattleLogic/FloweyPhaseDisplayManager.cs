using UnityEngine;

public class FloweyPhaseDisplayManager : MonoBehaviour
{
    [Header("References")]
    public GameObject gammaFlowey;           // The main boss sprite parent
    public GameObject[] phaseUIs;            // Soul phase GameObjects in order

    [Header("Attack Managers")]
    public AttackManager bossAttackManager;          // For Gamma Flowey
    public AttackManager[] soulAttackManagers;        // For each soul

    /// <summary>
    /// Activates the soul phase environment, disables Gamma Flowey, and stops attacks.
    /// </summary>
    /// <param name="phaseIndex">The phase to activate (0 = Teal, 5 = Yellow)</param>
    public void ActivatePhase(int phaseIndex)
    {
        if (phaseIndex < 0 || phaseIndex >= phaseUIs.Length)
        {
            Debug.LogWarning("Invalid phase index!");
            return;
        }

        // Hide Gamma Flowey sprite
        if (gammaFlowey != null)
            gammaFlowey.SetActive(false);

        // Stop boss attacks
        bossAttackManager?.StopAttacks();

        // Stop ALL soul attacks just in case
        foreach (var soulManager in soulAttackManagers)
            soulManager?.StopAttacks();

        // Show only the current soul UI
        for (int i = 0; i < phaseUIs.Length; i++)
            phaseUIs[i].SetActive(i == phaseIndex);

        // Start the soul's attacks
        if (phaseIndex < soulAttackManagers.Length)
            soulAttackManagers[phaseIndex]?.StartAttacks();

        // Activate the correct soul controller
        SoulPhaseController controller = phaseUIs[phaseIndex].GetComponent<SoulPhaseController>();
        if (controller != null)
        {
            controller.ActivateSoul();
            FindObjectOfType<PhaseCombatManager>()?.SetActiveSoul(controller);
        }
    }


    /// <summary>
    /// Ends the soul phase and returns to Gamma Flowey battle.
    /// </summary>
    public void EndPhaseManually()
    {
        foreach (var ui in phaseUIs)
            ui.SetActive(false);

        if (gammaFlowey != null)
            gammaFlowey.SetActive(true);

        // Stop ALL soul attacks
        foreach (var soulManager in soulAttackManagers)
            soulManager?.StopAttacks();

        // Play static sound effect (optional)
        MusicManager.Instance?.Play("SoulToBoss_Static");

        // Resume boss attacks
        bossAttackManager?.StartAttacks();
    }

}
