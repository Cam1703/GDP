using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs de enemigos (al menos 1)")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Parámetros de spawn")]
    [Tooltip("Tiempo inicial entre spawns (segundos)")]
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float minSpawnRate = 0.8f;
    [SerializeField] private float maxSpawnRate = 4f;
    [Tooltip("Intervalo para ajustar el spawn rate (segundos)")]
    [SerializeField] private float rateAdjustInterval = 10f;
    [Tooltip("Delta inicial de cambio de spawnRate")]
    [SerializeField] private float spawnRateDelta = -0.2f;

    private Camera mainCamera;
    private float padding = 0.1f;
    private float nearClip;

    private void Awake()
    {
        // Cacheamos la cámara y valores constantes
        mainCamera = Camera.main;
        if (mainCamera == null)
            Debug.LogError("[EnemySpawner] No se encontró Camera.main en la escena.");

        nearClip = mainCamera != null ? mainCamera.nearClipPlane : 0f;
    }

    private void Start()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogError("[EnemySpawner] Debes asignar al menos un prefab en 'enemyPrefabs'.");
            enabled = false;
            return;
        }

        // Iniciar corutinas
        StartCoroutine(SpawnLoop());
        StartCoroutine(AdjustRateLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private IEnumerator AdjustRateLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(rateAdjustInterval);

            spawnRate += spawnRateDelta;
            if (spawnRate <= minSpawnRate || spawnRate >= maxSpawnRate)
            {
                spawnRateDelta = -spawnRateDelta;
                spawnRate = Mathf.Clamp(spawnRate, minSpawnRate, maxSpawnRate);
            }
        }
    }

    private void SpawnEnemy()
    {
        // Elegir borde de spawn y posición
        int edge = Random.Range(0, 4);
        Vector3 spawnPos = GetSpawnPosition(edge);

        // Instanciar prefab aleatorio
        var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        var enemy = Instantiate(prefab, spawnPos, Quaternion.identity);

        // Ajustar dirección si contiene el componente
        if (enemy.TryGetComponent<EnemigoMovimientoPeriodico2D>(out var movimiento))
        {
            movimiento.direccion = GetDirectionForEdge(edge);
        }
    }

    private Vector3 GetSpawnPosition(int edge)
    {
        Vector3 vp = edge switch
        {
            0 => new Vector3(Random.value, 1 + padding, nearClip),     // Norte
            1 => new Vector3(Random.value, -padding, nearClip),        // Sur
            2 => new Vector3(1 + padding, Random.value, nearClip),     // Este
            3 => new Vector3(-padding, Random.value, nearClip),        // Oeste
            _ => Vector3.zero
        };
        return mainCamera.ViewportToWorldPoint(vp);
    }

    private Vector2 GetDirectionForEdge(int edge)
    {
        return edge switch
        {
            0 => Vector2.down,
            1 => Vector2.up,
            2 => Vector2.left,
            3 => Vector2.right,
            _ => Vector2.zero
        };
    }
}
