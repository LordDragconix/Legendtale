using UnityEngine;
using System.Collections;

public class OrangeFistDrop : MonoBehaviour
{
    public float dropSpeed = 30f;
    public float returnSpeed = 20f;
    public float waitAtBottomTime = 0.5f;

    [Header("Shake Settings")]
    public float shakeDuration = 0.4f;
    public float shakeMagnitude = 0.15f;

    public Transform topPosition;
    public Transform bottomPosition;

    [HideInInspector]
    public OrangeDropFistSpawner spawner;

    private bool isDropping = false;
    private bool isReturning = false;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (isDropping)
        {
            transform.position = Vector2.MoveTowards(transform.position, bottomPosition.position, dropSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, bottomPosition.position) < 0.1f)
            {
                isDropping = false;
                StartCoroutine(WaitAndReturn());
            }
        }
        else if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, topPosition.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, topPosition.position) < 0.1f)
            {
                isReturning = false;

                // 🔥 Ready to pick next fist
                if (spawner != null)
                {
                    spawner.StartNextFist();
                }
            }
        }
    }

    public void Drop()
    {
        if (!isDropping && !isReturning)
        {
            StartCoroutine(ShakeBeforeDrop());
        }
    }

    private IEnumerator ShakeBeforeDrop()
    {
        Vector3 startPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = startPos + new Vector3(x, y, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPos;
        isDropping = true;
    }

    private IEnumerator WaitAndReturn()
    {
        yield return new WaitForSeconds(waitAtBottomTime);
        isReturning = true;
    }
}
