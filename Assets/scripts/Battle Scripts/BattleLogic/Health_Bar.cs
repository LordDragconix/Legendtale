using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health_Bar : MonoBehaviour
{
    public Slider slider; // Reference to the UI Slider
    public TextMeshProUGUI numberText; // Reference to the TextMeshProUGUI

    void Start()
    {
        if (slider != null)
            slider.onValueChanged.AddListener(UpdateNumberText); // Add listener for slider value change
    }

    void UpdateNumberText(float value)
    {
        if (numberText != null)
            numberText.text = value.ToString("0"); // Update the TextMeshPro text to show the slider's value
    }
}