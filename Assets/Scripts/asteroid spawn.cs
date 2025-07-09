using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawn : MonoBehaviour
{
    public List<GameObject> spawnpointlist;
    public GameObject asteroidPrefab;

    private GameObject player; // Reference to the player object

    public float minSpawnTime = 1f; // minimum interval in seconds
    public float maxSpawnTime = 3f; // maximum interval in seconds

    SpaceSHip spaceship; // Reference to the SpaceSHip component

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object by tag
        spaceship = player.GetComponent<SpaceSHip>(); // Get the SpaceSHip component from the player
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        while (spaceship.isalive)
        {
            // Wait for a random amount of time before spawning
            float wait = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(wait);

            // Spawn the asteroid at a random spawn point
            int i = Random.Range(0, spawnpointlist.Count);
            GameObject asteroid = Instantiate(asteroidPrefab, spawnpointlist[i].transform.position, Quaternion.identity);
            asteroid.GetComponent<asteroids>().target = player;
        }
    }
}
