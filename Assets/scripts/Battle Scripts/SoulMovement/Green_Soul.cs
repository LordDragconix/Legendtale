using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Soul : MonoBehaviour
{
    public GameObject shieldPrefab;
    private GameObject currentShield;
    public float shieldOffset = 1f;

    void Start()
    {
        // Set the position to the center of the screen
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        ControlShield();
    }

    void ControlShield()
    {
        Vector3 shieldPosition = Vector3.zero;
        Quaternion shieldRotation = Quaternion.identity;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            shieldPosition = Vector3.up * shieldOffset;
            shieldRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            shieldPosition = Vector3.down * shieldOffset;
            shieldRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            shieldPosition = Vector3.left * shieldOffset;
            shieldRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            shieldPosition = Vector3.right * shieldOffset;
            shieldRotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            return;
        }

        if (currentShield != null)
        {
            Destroy(currentShield);
        }

        currentShield = Instantiate(shieldPrefab, transform.position + shieldPosition, shieldRotation, transform);
        currentShield.transform.localScale = shieldPrefab.transform.localScale;  // Reset the scale to the prefab's original scale
    }
}