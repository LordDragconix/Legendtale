using UnityEngine;

public class WarningFlash : MonoBehaviour
{
    public Color color1 = Color.red;
    public Color color2 = Color.yellow;
    public float flashSpeed = 2f;

    private SpriteRenderer sr;
    private float t;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        t += Time.deltaTime * flashSpeed;
        float lerp = Mathf.PingPong(t, 1f);
        sr.color = Color.Lerp(color1, color2, lerp);
    }
}
