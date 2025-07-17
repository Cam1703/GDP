using UnityEngine;

public enum SpawnSide
{
    Top,
    Bottom,
    Left,
    Right
}

public class SpaceInvadersEnemy : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("Velocidad de movimiento perpendicular (zigzag)")]
    public float velocidadPerpendicular = 3.0f;

    [Tooltip("Velocidad de avance hacia el interior del mapa")]
    public float velocidadAvance = 1.0f;

    [Tooltip("Lado desde el que spawneó el enemigo")]
    public SpawnSide ladoSpawn = SpawnSide.Top;

    [Header("Configuración de Límites")]
    [Tooltip("Límites del área de movimiento (World Space)")]
    public Rect limitesMovimiento = new Rect(-10, -6, 20, 12);

    [Tooltip("Margen adicional para los límites perpendiculares")]
    public float margenLimites = 0.5f;

    [Header("Configuración Visual")]
    [Tooltip("Mostrar límites en el Scene View")]
    public bool mostrarLimitesEnEditor = true;

    // Variables privadas
    private Vector2 direccionPerpendicular;
    private Vector2 direccionAvance;
    private float limitePerpendicular1;
    private float limitePerpendicular2;
    private bool moviendoseEnDireccion1 = true;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Parámetros del Animator
    private const string PARAM_X = "Horizontal";
    private const string PARAM_Y = "Vertical";

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        ConfigurarMovimientoSegunSpawn();
    }

    private void ConfigurarMovimientoSegunSpawn()
    {
        switch (ladoSpawn)
        {
            case SpawnSide.Top:
                // Spawneó desde arriba: se mueve horizontalmente (izq-der) y baja
                direccionPerpendicular = Vector2.right;
                direccionAvance = Vector2.down;
                limitePerpendicular1 = limitesMovimiento.xMin + margenLimites;
                limitePerpendicular2 = limitesMovimiento.xMax - margenLimites;
                break;

            case SpawnSide.Bottom:
                // Spawneó desde abajo: se mueve horizontalmente (izq-der) y sube
                direccionPerpendicular = Vector2.right;
                direccionAvance = Vector2.up;
                limitePerpendicular1 = limitesMovimiento.xMin + margenLimites;
                limitePerpendicular2 = limitesMovimiento.xMax - margenLimites;
                break;

            case SpawnSide.Left:
                // Spawneó desde la izquierda: se mueve verticalmente (arriba-abajo) y avanza a la derecha
                direccionPerpendicular = Vector2.up;
                direccionAvance = Vector2.right;
                limitePerpendicular1 = limitesMovimiento.yMin + margenLimites;
                limitePerpendicular2 = limitesMovimiento.yMax - margenLimites;
                break;

            case SpawnSide.Right:
                // Spawneó desde la derecha: se mueve verticalmente (arriba-abajo) y avanza a la izquierda
                direccionPerpendicular = Vector2.up;
                direccionAvance = Vector2.left;
                limitePerpendicular1 = limitesMovimiento.yMin + margenLimites;
                limitePerpendicular2 = limitesMovimiento.yMax - margenLimites;
                break;
        }

        // Determinar dirección inicial aleatoria
        moviendoseEnDireccion1 = Random.Range(0, 2) == 0;
    }

    private void Update()
    {
        MoverEnemigo();
        VerificarLimites();
        UpdateAnimatorParameters();
    }

    private void MoverEnemigo()
    {
        // Calcular dirección perpendicular actual
        Vector2 dirPerpendicular = moviendoseEnDireccion1 ? direccionPerpendicular : -direccionPerpendicular;

        // Calcular velocidad total
        Vector2 velocidadTotal = (dirPerpendicular * velocidadPerpendicular) + (direccionAvance * velocidadAvance);

        // Aplicar movimiento
        if (rb2d != null)
        {
            rb2d.linearVelocity = velocidadTotal;
        }
        else
        {
            transform.Translate(velocidadTotal * Time.deltaTime);
        }
    }

    private void VerificarLimites()
    {
        bool cambiarDireccion = false;

        // Verificar límites según el eje perpendicular
        if (ladoSpawn == SpawnSide.Top || ladoSpawn == SpawnSide.Bottom)
        {
            // Movimiento horizontal
            if (moviendoseEnDireccion1 && transform.position.x >= limitePerpendicular2)
            {
                cambiarDireccion = true;
            }
            else if (!moviendoseEnDireccion1 && transform.position.x <= limitePerpendicular1)
            {
                cambiarDireccion = true;
            }
        }
        else // Left o Right
        {
            // Movimiento vertical
            if (moviendoseEnDireccion1 && transform.position.y >= limitePerpendicular2)
            {
                cambiarDireccion = true;
            }
            else if (!moviendoseEnDireccion1 && transform.position.y <= limitePerpendicular1)
            {
                cambiarDireccion = true;
            }
        }

        if (cambiarDireccion)
        {
            moviendoseEnDireccion1 = !moviendoseEnDireccion1;
        }
    }

    private void UpdateAnimatorParameters()
    {
        if (animator != null)
        {
            // Calcular dirección actual total
            Vector2 dirPerpendicular = moviendoseEnDireccion1 ? direccionPerpendicular : -direccionPerpendicular;
            Vector2 direccionTotal = (dirPerpendicular * velocidadPerpendicular) + (direccionAvance * velocidadAvance);

            float animX = 0f;
            float animY = 0f;

            // Determinar la dirección principal para la animación
            if (Mathf.Abs(direccionTotal.x) > Mathf.Abs(direccionTotal.y))
            {
                // Movimiento principalmente horizontal
                if (direccionTotal.x > 0)
                {
                    animX = -1f; // Derecha
                    animY = 0f;
                }
                else
                {
                    animX = 1f; // Izquierda
                    animY = 0f;
                }
            }
            else
            {
                // Movimiento principalmente vertical
                if (direccionTotal.y > 0)
                {
                    animX = 0f;
                    animY = 1f; // Arriba
                }
                else
                {
                    animX = 0f;
                    animY = -1f; // Abajo
                }
            }

            animator.SetFloat(PARAM_X, animX);
            animator.SetFloat(PARAM_Y, animY);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (mostrarLimitesEnEditor)
        {
            // Dibujar límites del área de movimiento
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(limitesMovimiento.center, limitesMovimiento.size);

            // Dibujar límites perpendiculares
            Gizmos.color = Color.yellow;
            if (ladoSpawn == SpawnSide.Top || ladoSpawn == SpawnSide.Bottom)
            {
                // Líneas verticales para límites horizontales
                Vector3 punto1 = new Vector3(limitePerpendicular1, limitesMovimiento.yMin, 0);
                Vector3 punto2 = new Vector3(limitePerpendicular1, limitesMovimiento.yMax, 0);
                Gizmos.DrawLine(punto1, punto2);

                punto1 = new Vector3(limitePerpendicular2, limitesMovimiento.yMin, 0);
                punto2 = new Vector3(limitePerpendicular2, limitesMovimiento.yMax, 0);
                Gizmos.DrawLine(punto1, punto2);
            }
            else
            {
                // Líneas horizontales para límites verticales
                Vector3 punto1 = new Vector3(limitesMovimiento.xMin, limitePerpendicular1, 0);
                Vector3 punto2 = new Vector3(limitesMovimiento.xMax, limitePerpendicular1, 0);
                Gizmos.DrawLine(punto1, punto2);

                punto1 = new Vector3(limitesMovimiento.xMin, limitePerpendicular2, 0);
                punto2 = new Vector3(limitesMovimiento.xMax, limitePerpendicular2, 0);
                Gizmos.DrawLine(punto1, punto2);
            }
        }

        // Mostrar direcciones actuales
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Vector2 dirPerpendicular = moviendoseEnDireccion1 ? direccionPerpendicular : -direccionPerpendicular;
            Gizmos.DrawRay(transform.position, dirPerpendicular);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, direccionAvance);
        }
    }

    // Método para configurar el lado de spawn desde otro script
    public void SetSpawnSide(SpawnSide nuevoLado)
    {
        ladoSpawn = nuevoLado;
        if (Application.isPlaying)
        {
            ConfigurarMovimientoSegunSpawn();
        }
    }

    // Método para configurar límites desde otro script
    public void SetLimites(Rect nuevosLimites)
    {
        limitesMovimiento = nuevosLimites;
        if (Application.isPlaying)
        {
            ConfigurarMovimientoSegunSpawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthManager playerHealth = collision.gameObject.GetComponent<PlayerHealthManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
            else
            {
                Debug.LogError("PlayerHealthManager component not found on player object.");
            }
            Destroy(gameObject);
        }
    }
}