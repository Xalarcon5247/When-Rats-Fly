using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour {
    private Spawner[] childSpawners; // Array to hold child spawner scripts

    public float avgDropDelay;

    void Start() {
        // Get all Spawner components in the child objects
        childSpawners = GetComponentsInChildren<Spawner>();
        avgDropDelay = childSpawners[0].avgDropDelay;
    }

    // Method to modify child spawners
    public void setDropDelay(float newDropDelay) {
        foreach (Spawner spawner in childSpawners) {
            // Modify the child spawner script as needed
            spawner.avgDropDelay = newDropDelay; // Example of changing spawn rate
        }
    }
}
