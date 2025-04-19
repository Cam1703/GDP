using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine to spawn enemies every 5 seconds
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
