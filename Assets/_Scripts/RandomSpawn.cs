using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    // Array of possible spawn points for both player and objects
    public Transform[] spawnPoints;

    // Reference to the player prefab (your "Player" prefab)
    public GameObject Player;

    // Array of random objects to spawn
    public GameObject[] randomObjects;

    // Number of random objects to spawn (e.g., 3)
    public int numberOfObjectsToSpawn = 3;

    void Start()
    {
        // Check if there are enough spawn points
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        // Spawn the player at a random spawn point
        SpawnPlayerAtRandomPoint();

        // Spawn random objects at other spawn points
        SpawnRandomObjects();
    }

    void SpawnPlayerAtRandomPoint()
    {
        // Pick a random spawn point from the array
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Instantiate the player at the randomly chosen spawn point's position and rotation
        Instantiate(Player, spawnPoints[randomIndex].position, spawnPoints[randomIndex].rotation);
    }

    void SpawnRandomObjects()
    {
        // Create a list of available spawn points (excluding the one used by the player)
        Transform[] availableSpawnPoints = (Transform[])spawnPoints.Clone();

        // Iterate and spawn the specified number of random objects
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            // Ensure we have enough available spawn points
            if (availableSpawnPoints.Length == 0)
            {
                Debug.LogWarning("Not enough spawn points for all objects!");
                break;
            }

            // Pick a random spawn point for the object
            int randomSpawnIndex = Random.Range(0, availableSpawnPoints.Length);

            // Pick a random object from the array
            int randomObjectIndex = Random.Range(0, randomObjects.Length);

            // Instantiate the random object at the chosen spawn point
            Instantiate(randomObjects[randomObjectIndex], availableSpawnPoints[randomSpawnIndex].position, availableSpawnPoints[randomSpawnIndex].rotation);

            // Remove the used spawn point from the array (to avoid reusing it)
            availableSpawnPoints = RemoveSpawnPointAtIndex(availableSpawnPoints, randomSpawnIndex);
        }
    }

    // Helper function to remove the used spawn point from the array
    Transform[] RemoveSpawnPointAtIndex(Transform[] array, int index)
    {
        Transform[] newArray = new Transform[array.Length - 1];
        for (int i = 0, j = 0; i < array.Length; i++)
        {
            if (i != index)
            {
                newArray[j++] = array[i];
            }
        }
        return newArray;
    }
}

