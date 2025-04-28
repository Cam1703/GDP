using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Configuraci�n")]
    public float velocidad = 10f;
    public float tiempoVida = 3f;

    private Rigidbody2D rb;
    private Vector2 _screenBounds;
    private Vector2 direccionDisparo; // Nueva variable para almacenar direcci�n

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, tiempoVida);

        // Rotaci�n autom�tica seg�n direcci�n
        float angulo = Mathf.Atan2(direccionDisparo.y, direccionDisparo.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
    }

    // M�todo p�blico para definir la direcci�n desde el enemigo
    public void SetDireccion(Vector2 nuevaDireccion)
    {
        direccionDisparo = nuevaDireccion.normalized;
    }

    void Update()
    {
        // Movimiento en la direcci�n configurada
        rb.linearVelocity = direccionDisparo * velocidad;

        // Destrucci�n al salir de pantalla
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (transform.position.x < -_screenBounds.x || transform.position.x > _screenBounds.x ||
           transform.position.y < -_screenBounds.y || transform.position.y > _screenBounds.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) // Evitar colisi�n con otros enemigos
        {
            if (collision.CompareTag("Player"))
            {
                PlayerHealthManager playerHealth = GetComponent<PlayerHealthManager>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                }
            }
            Destroy(gameObject);
        }
    }
}