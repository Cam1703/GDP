using System.Collections;
using UnityEngine;

public class HealerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject healerPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float minDistanceFromPlayer = 2f;
    [SerializeField] private float initialSpawnDelay = 5f;

    private Transform playerTransform;
    private Camera mainCamera;

    private const int MaxSpawnAttempts = 10;

    private void Start()
    {
        mainCamera = Camera.main;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            StartCoroutine(SpawnLoopAfterDelay(initialSpawnDelay));
        }
        else
        {
            Debug.LogWarning("Player not found. Healer spawning disabled.");
        }
    }

    private IEnumerator SpawnLoopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        InvokeRepeating(nameof(SpawnHealer), 0f, spawnInterval);
    }

    private void SpawnHealer()
    {
        if (playerTransform == null) return;

        for (int attempt = 0; attempt < MaxSpawnAttempts; attempt++)
        {
            Vector3 spawnPosition = GetRandomEdgePosition();

            if (Vector3.Distance(spawnPosition, playerTransform.position) >= minDistanceFromPlayer)
            {
                Instantiate(healerPrefab, spawnPosition, Quaternion.identity);
                Debug.Log($"Healer spawned at {spawnPosition} after {attempt + 1} attempts.");
                return;
            }
        }
    }

    private Vector3 GetRandomEdgePosition()
    {
        float x, y;
        switch (Random.Range(0, 4))
        {
            case 0: // Top
                x = Random.Range(0.1f, 0.9f);
                y = 0.95f;
                break;
            case 1: // Bottom
                x = Random.Range(0.1f, 0.9f);
                y = 0.05f;
                break;
            case 2: // Right
                x = 0.95f;
                y = Random.Range(0.1f, 0.9f);
                break;
            case 3: // Left
                x = 0.05f;
                y = Random.Range(0.1f, 0.9f);
                break;
            default:
                x = y = 0.5f;
                break;
        }

        Vector3 viewportPos = new Vector3(x, y, 0f);
        Vector3 worldPos = mainCamera.ViewportToWorldPoint(viewportPos);
        worldPos.z = 0f;
        return worldPos;
    }
}
