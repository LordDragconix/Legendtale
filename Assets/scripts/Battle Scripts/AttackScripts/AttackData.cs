using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "Legendtale/Attacks/New Attack")]
public class AttackData : ScriptableObject
{
    public string attackName;
    public GameObject attackPrefab;
    public float defaultDuration = 3f;           // For random/continuous
    public float bossDurationOverride = -1f;     // For boss mode

    public bool waitForManualStop = false;       // Keeps attack running until forced stop

    public float despawnDelay = 0f;
}
