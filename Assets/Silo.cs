using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silo : MonoBehaviour
{
    public GameObject rocketPrefab; // Public member for rocket prefab
    public Transform target;
    public float interval; // Public member for spawn interval

    private float nextSpawnTime; // Track when to spawn the next rocket

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + interval; // Initialize next spawn time
        Invoke(nameof(SpawnRocket), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime) // Check if it's time to spawn
        {
            SpawnRocket(); // Call the spawn method
            nextSpawnTime = Time.time + interval; // Update next spawn time
        }
    }

    void SpawnRocket() // Method to spawn the rocket
    {
        GameObject rocketObject = Instantiate(rocketPrefab, transform.position, transform.rotation); // Spawn the rocket
        Rocket rocket = rocketObject.GetComponent<Rocket>(); // Get the RocketMovement component
        if (rocket != null)
        {
            rocket.target = target.position; // Set the target position (change as needed)
        }
    }
}
