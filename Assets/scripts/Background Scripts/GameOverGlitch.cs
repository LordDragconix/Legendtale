using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverGlitch : MonoBehaviour
{
    public float timeBeforeGlitch = 3f;
    public float glitchDuration = 2f;
    public AudioSource musicSource;

    public Image gameOverImage; // Drag your UI Image here
    public Sprite glitchedSprite; // The sprite you want to swap to during the glitch

    void Start()
    {
        StartCoroutine(GlitchSequence());
    }

    IEnumerator GlitchSequence()
    {
        yield return new WaitForSeconds(timeBeforeGlitch);

        // Change the image to the glitched one
        if (gameOverImage != null && glitchedSprite != null)
        {
            gameOverImage.sprite = glitchedSprite;
        }

        float lockTime = musicSource.time;
        float timer = 0f;

        while (timer < glitchDuration)
        {
            musicSource.time = lockTime;
            yield return new WaitForSeconds(0.02f);
            timer += 0.02f;
        }

        yield return new WaitForSeconds(0.5f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}