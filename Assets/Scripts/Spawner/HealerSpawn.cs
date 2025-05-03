using System.Collections;
using UnityEngine;

public class HealerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject healerPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float minDistanceFromPlayer = 2f;
    [SerializeField] private int secondsUntilSpawn = 5;

    private GameObject player;
    private Transform playerTransform;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
                playerTransform = player.transform;
        }

        StartCoroutine(StartSpawningAfterDelay(secondsUntilSpawn));
    }

    private IEnumerator StartSpawningAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        InvokeRepeating("SpawnHealer", 0f, spawnInterval);
    }

    void SpawnHealer()
    {
        if (playerTransform == null) return;

        Vector3 spawnPosition;
        int attempts = 0;
        const int maxAttempts = 10;

        do
        {
            int edge = Random.Range(0, 4);
            spawnPosition = CalcularPosicionSpawn(edge);
            attempts++;
        }
        while (Vector3.Distance(spawnPosition, playerTransform.position) < minDistanceFromPlayer && attempts < maxAttempts);

        Instantiate(healerPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 CalcularPosicionSpawn(int borde)
    {
        Vector3 viewportPos = Vector3.zero;

        switch (borde)
        {
            case 0: viewportPos = new Vector3(Random.Range(0.1f, 0.9f), 0.95f, 0f); break; // Norte
            case 1: viewportPos = new Vector3(Random.Range(0.1f, 0.9f), 0.05f, 0f); break; // Sur
            case 2: viewportPos = new Vector3(0.95f, Random.Range(0.1f, 0.9f), 0f); break; // Este
            case 3: viewportPos = new Vector3(0.05f, Random.Range(0.1f, 0.9f), 0f); break; // Oeste
        }

        Vector3 worldPos = mainCamera.ViewportToWorldPoint(viewportPos);
        worldPos.z = 0f;
        return worldPos;
    }
}
