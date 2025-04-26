using UnityEngine;

public class EnemigoDisparador : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject prefabProyectil;
    public Transform puntoDisparo;
    public float cadenciaDisparo = 2f;

    private float temporizadorDisparo;
    private EnemigoMovimientoPeriodico2D movimientoEnemigo; // Referencia al script de movimiento

    void Start()
    {
        // Obtener referencia al script de movimiento
        movimientoEnemigo = GetComponent<EnemigoMovimientoPeriodico2D>();

        if (movimientoEnemigo == null)
        {
            Debug.LogError("No se encontró el script de movimiento en el enemigo!");
        }
    }

    void Update()
    {
        temporizadorDisparo -= Time.deltaTime;

        if (temporizadorDisparo <= 0f)
        {
            Disparar();
            temporizadorDisparo = cadenciaDisparo;
        }
    }

    void Disparar()
    {
        if (movimientoEnemigo == null) return;

        // Instanciar y configurar dirección del proyectil
        GameObject proyectil = Instantiate(
            prefabProyectil,
            puntoDisparo.position,
            Quaternion.identity
        );

        // Obtener dirección actual del enemigo
        Vector2 direccionDisparo = movimientoEnemigo.direccion;

        // Configurar dirección y rotación del proyectil
        Proyectil scriptProyectil = proyectil.GetComponent<Proyectil>();
        if (scriptProyectil != null)
        {
            scriptProyectil.SetDireccion(direccionDisparo);

            // Rotar el proyectil para que mire en la dirección de movimiento
            float angulo = Mathf.Atan2(direccionDisparo.y, direccionDisparo.x) * Mathf.Rad2Deg;
            proyectil.transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
        }
        else
        {
            Debug.LogWarning("El prefab del proyectil no tiene componente Proyectil!");
        }
    }
}