using UnityEngine;

public class EnemigoMovimientoPeriodico2D : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("Velocidad a la que se mueve el enemigo")]
    public float velocidad = 5.0f;

    [Tooltip("Dirección en la que se moverá el enemigo")]
    public Vector2 direccion = Vector2.right;

    [Header("Configuración de Tiempo")]
    [Tooltip("Tiempo entre movimientos (en segundos)")]
    public float tiempoEntreMov = 2.0f;

    [Tooltip("Duración de cada movimiento (en segundos)")]
    public float duracionMovimiento = 1.0f;

    private float contadorTiempo = 0.0f;
    private bool estaMoviendose = false;
    private float contadorMovimiento = 0.0f;

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

        contadorTiempo = 0;

        // Normaliza la dirección
        direccion = direccion.normalized;
    }

    void Update()
    {
        // Actualizar parámetros del Animator basado en el estado actual
        UpdateAnimatorParameters();

        if (!estaMoviendose)
        {
            contadorTiempo += Time.deltaTime;
            if (contadorTiempo >= tiempoEntreMov)
            {
                contadorTiempo = 0;
                estaMoviendose = true;
                contadorMovimiento = 0;
            }
        }
        else
        {
            contadorMovimiento += Time.deltaTime;
            MoverEnemigo();
            //if (contadorMovimiento >= duracionMovimiento)
            //{
            //    estaMoviendose = false;
            //    DetenerEnemigo();
            //}
        }
    }

    private void UpdateAnimatorParameters()
    {
        if (animator != null)
        {
            if (estaMoviendose)
            {
                // Mapear la dirección del movimiento a los parámetros del Blend Tree
                // Según tu Blend Tree:
                // Walk_Right: Pos X = -1, Pos Y = 0 (movimiento hacia la derecha)
                // Walk_Left: Pos X = 1, Pos Y = 0 (movimiento hacia la izquierda)
                // Walk_Up: Pos X = 0, Pos Y = 1 (movimiento hacia arriba)
                // Walk_Down: Pos X = 0, Pos Y = -1 (movimiento hacia abajo)

                float animX = 0f;
                float animY = 0f;

                // Determinar la dirección principal basada en la dirección de movimiento
                if (Mathf.Abs(direccion.x) > Mathf.Abs(direccion.y))
                {
                    // Movimiento principalmente horizontal
                    if (direccion.x > 0)
                    {
                        // Movimiento hacia la derecha
                        animX = -1f;
                        animY = 0f;
                    }
                    else
                    {
                        // Movimiento hacia la izquierda
                        animX = 1f;
                        animY = 0f;
                    }
                }
                else
                {
                    // Movimiento principalmente vertical
                    if (direccion.y > 0)
                    {
                        // Movimiento hacia arriba
                        animX = 0f;
                        animY = 1f;
                    }
                    else
                    {
                        // Movimiento hacia abajo
                        animX = 0f;
                        animY = -1f;
                    }
                }

                animator.SetFloat(PARAM_X, animX);
                animator.SetFloat(PARAM_Y, animY);
            }
            else
            {
                // Cuando está quieto, parámetros en 0 (idle)
                animator.SetFloat(PARAM_X, 0f);
                animator.SetFloat(PARAM_Y, 0f);
            }
        }
    }

    private void MoverEnemigo()
    {
        if (rb2d != null)
        {
            rb2d.linearVelocity = direccion * velocidad;
        }
        else
        {
            transform.Translate(direccion * velocidad * Time.deltaTime);
        }
    }

    private void DetenerEnemigo()
    {
        if (rb2d != null)
        {
            rb2d.linearVelocity = Vector2.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 dir3D = new Vector3(direccion.x, direccion.y, 0);
        Gizmos.DrawRay(transform.position, dir3D.normalized * 2);

        // Mostrar estado actual
        if (Application.isPlaying)
        {
            Gizmos.color = estaMoviendose ? Color.green : Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.3f);
        }
    }

    public void SetDireccion(Vector2 nuevaDireccion)
    {
        direccion = nuevaDireccion.normalized;
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