using UnityEngine;
using UnityEngine.UI;

public class SliderZeroTrigger : MonoBehaviour
{
    public Slider targetSlider;     // Assign your UI Slider
    public Button targetButton;     // Assign the Button to trigger

    private bool hasTriggered = false;

    void Start()
    {
        if (targetSlider != null)
        {
            targetSlider.onValueChanged.AddListener(OnSliderChanged);
        }
    }

    void OnSliderChanged(float value)
    {
        if (value <= 0f && !hasTriggered)
        {
            hasTriggered = true;
            if (targetButton != null)
            {
                targetButton.onClick.Invoke();  // Simulate button click
            }
        }
        else if (value > 0f)
        {
            hasTriggered = false; // Reset trigger if slider goes back up
        }
    }
}