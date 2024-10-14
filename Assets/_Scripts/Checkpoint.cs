using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition;
    public MenuManager menuManager; // Reference to MenuManager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lastCheckpointPosition = transform.position;
            Debug.Log("Checkpoint reached!");
            menuManager.ShowMenu(); // Show the menu
        }
    }
}
