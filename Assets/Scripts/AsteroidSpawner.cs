using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    public GameObject asteroidPrefab;   // The asteroid prefab to spawn
    public float spawnRate = 2f;        // Time between spawns
    public float spawnDistance = 15f;   // Distance from center to spawn
    public float asteroidLifetime = 30f; // How long asteroids live before disappearing

    private float timer = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Count up the timer
        timer += Time.deltaTime;

        // If enough time has passed, spawn an Asteroid
        if (timer >= spawnRate)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
       
        int edge = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (edge)
        {
            case 0: // Top
                spawnPosition = new Vector3(Random.Range(-spawnDistance, spawnDistance), spawnDistance, 0);
                break;
            case 1: // Bottom
                spawnPosition = new Vector3(Random.Range(-spawnDistance, spawnDistance), -spawnDistance, 0);
                break;
            case 2: // Left
                spawnPosition = new Vector3(-spawnDistance, Random.Range(-spawnDistance, spawnDistance), 0);
                break;
            case 3: // Right
                spawnPosition = new Vector3(spawnDistance, Random.Range(-spawnDistance, spawnDistance), 0);
                break;
        }

        if (asteroidPrefab == null)
        {
            Debug.LogError("AsteroidSpawner: asteroidPrefab is not assigned or has been destroyed. Assign the prefab asset (not a scene instance) in the Inspector.");
            return;
        }

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        Destroy(asteroid, asteroidLifetime);
    }
}