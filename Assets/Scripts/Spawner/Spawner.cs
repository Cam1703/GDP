using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public float spawnRate = 2f;

    private float minSpawnRate = 0.5f;
    private float maxSpawnRate = 3f;
    private float spawnRateChangeInterval = 5f; // cada cuántos segundos cambia el spawnRate
    private float spawnRateDelta = -0.2f; // al principio disminuirá

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnEnemy", 0f, spawnRate);
        InvokeRepeating("AdjustSpawnRate", spawnRateChangeInterval, spawnRateChangeInterval);
    }

    void SpawnEnemy()
    {
        int edge = Random.Range(0, 4); // 0:Norte, 1:Sur, 2:Este, 3:Oeste
        Vector3 spawnPosition = CalcularPosicionSpawn(edge);

        // Instanciar prefab aleatorio
        GameObject chosenPrefab = (Random.Range(0, 2) == 0) ? enemyPrefab1 : enemyPrefab2;
        GameObject enemy = Instantiate(chosenPrefab, spawnPosition, Quaternion.identity);

        // Configurar dirección según borde
        EnemigoMovimientoPeriodico2D movimiento = enemy.GetComponent<EnemigoMovimientoPeriodico2D>();
        if (movimiento != null)
        {
            movimiento.direccion = ObtenerDireccionPorBorde(edge);
        }
    }

    void AdjustSpawnRate()
    {
        spawnRate += spawnRateDelta;

        if (spawnRate <= minSpawnRate || spawnRate >= maxSpawnRate)
        {
            // Invertir la dirección del cambio
            spawnRateDelta *= -1;
            spawnRate = Mathf.Clamp(spawnRate, minSpawnRate, maxSpawnRate);
        }

        // Reiniciar el InvokeRepeating con el nuevo spawnRate
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", 0f, spawnRate);
    }

    Vector3 CalcularPosicionSpawn(int borde)
    {
        Vector3 viewportPoint = Vector3.zero;
        float padding = 0.1f; // Margen fuera de pantalla

        switch (borde)
        {
            case 0: // Norte
                viewportPoint = new Vector3(Random.Range(0f, 1f), 1 + padding, mainCamera.nearClipPlane);
                break;
            case 1: // Sur
                viewportPoint = new Vector3(Random.Range(0f, 1f), -padding, mainCamera.nearClipPlane);
                break;
            case 2: // Este
                viewportPoint = new Vector3(1 + padding, Random.Range(0f, 1f), mainCamera.nearClipPlane);
                break;
            case 3: // Oeste
                viewportPoint = new Vector3(-padding, Random.Range(0f, 1f), mainCamera.nearClipPlane);
                break;
        }

        return mainCamera.ViewportToWorldPoint(viewportPoint);
    }

    Vector2 ObtenerDireccionPorBorde(int borde)
    {
        return borde switch
        {
            0 => Vector2.down,    // Norte -> Sur
            1 => Vector2.up,      // Sur -> Norte
            2 => Vector2.left,    // Este -> Oeste
            3 => Vector2.right,   // Oeste -> Este
            _ => Vector2.right
        };
    }
}
