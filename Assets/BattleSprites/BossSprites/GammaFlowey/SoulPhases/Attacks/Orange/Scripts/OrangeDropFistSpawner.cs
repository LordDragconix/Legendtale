using UnityEngine;

public class OrangeDropFistSpawner : MonoBehaviour
{
    [Header("Fist Prefabs")]
    public GameObject leftFistPrefab;
    public GameObject rightFistPrefab;

    [Header("Left Fist Points")]
    public Transform leftSpawnPoint;
    public Transform leftBottomPoint;

    [Header("Right Fist Points")]
    public Transform rightSpawnPoint;
    public Transform rightBottomPoint;

    private OrangeFistDrop leftFistInstance;
    private OrangeFistDrop rightFistInstance;

    void Start()
    {
        // Spawn both fists at start and store references
        GameObject left = Instantiate(leftFistPrefab, leftSpawnPoint.position, Quaternion.identity, transform);
        GameObject right = Instantiate(rightFistPrefab, rightSpawnPoint.position, Quaternion.identity, transform);

        leftFistInstance = left.GetComponent<OrangeFistDrop>();
        rightFistInstance = right.GetComponent<OrangeFistDrop>();

        // Assign positions and spawner ref
        leftFistInstance.topPosition = leftSpawnPoint;
        leftFistInstance.bottomPosition = leftBottomPoint;
        leftFistInstance.spawner = this;

        rightFistInstance.topPosition = rightSpawnPoint;
        rightFistInstance.bottomPosition = rightBottomPoint;
        rightFistInstance.spawner = this;

        // Start the attack cycle
        StartNextFist();
    }

    public void StartNextFist()
    {
        bool dropLeft = Random.value > 0.5f;

        if (dropLeft)
        {
            leftFistInstance.Drop();
        }
        else
        {
            rightFistInstance.Drop();
        }
    }
}
