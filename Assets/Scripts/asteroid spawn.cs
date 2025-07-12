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

    PlayerData PlayerData; // Reference to the SpaceSHip component

    public bool isspawning = false; // Flag to control spawning

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object by tag
        PlayerData = PlayerDataManager.Instance.playerData; // Get the SpaceSHip component from the player
        StartCoroutine(SpawnAsteroids());
    }

    private void Update()
    {
        if (PlayerData != null)
        {
            if (!isspawning && PlayerData.isAlive)
            {
                StartCoroutine(SpawnAsteroids());
                isspawning = true;
            }
            else if (isspawning && !PlayerData.isAlive)
            {
                isspawning = false; // player died, stop spawning
            }
        }
    }

    private IEnumerator SpawnAsteroids()
    {
        while (PlayerData.isAlive)
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
