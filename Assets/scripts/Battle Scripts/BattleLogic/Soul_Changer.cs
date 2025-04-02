using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul_Changer : MonoBehaviour
{
    public GameObject[] soulPrefabs;  // Array to hold the different soul prefabs
    public AudioClip soulChangeSound;  // Single audio clip for all soul changes
    private GameObject currentSoul;  // Reference to the currently active soul
    private AudioSource audioSource;  // Reference to the AudioSource component

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (soulPrefabs.Length > 0)
        {
            // Instantiate the first soul as the initial soul
            currentSoul = Instantiate(soulPrefabs[0], transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSoul(0);  // Change to soul at index 0
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSoul(1);  // Change to soul at index 1
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSoul(2);  // Change to soul at index 2
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeSoul(3);  // Change to soul at index 3
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeSoul(4);  // Change to soul at index 4
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeSoul(5);  // Change to soul at index 5
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeSoul(6);  // Change to soul at index 6
        }
    }

    void ChangeSoul(int soulIndex)
    {
        if (soulIndex >= 0 && soulIndex < soulPrefabs.Length)
        {
            Vector3 position = currentSoul.transform.position;
            Quaternion rotation = currentSoul.transform.rotation;

            Destroy(currentSoul);  // Destroy the current soul

            currentSoul = Instantiate(soulPrefabs[soulIndex], position, rotation);  // Instantiate the new soul

            PlaySoulChangeSound();  // Play the soul change sound
        }
    }

    void PlaySoulChangeSound()
    {
        if (soulChangeSound != null)
        {
            audioSource.clip = soulChangeSound;
            audioSource.Play();
        }
    }
}