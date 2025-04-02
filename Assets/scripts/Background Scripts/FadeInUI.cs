using UnityEngine;
using UnityEngine.UI;

public class FadeInUI : MonoBehaviour
{
    public float fadeDuration = 2f; // How long the fade takes in seconds
    private Image image;
    private float timer = 0f;

    void Start()
    {
        image = GetComponent<Image>();
        Color c = image.color;
        c.a = 0f;
        image.color = c;
    }

    void Update()
    {
        if (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);

            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }
    }
}
