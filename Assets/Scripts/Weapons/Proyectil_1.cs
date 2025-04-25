// Script Proyectil.cs (a??delo al prefab)
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Configuraci?n")]
    public float velocidad = 10f;
    public float tiempoVida = 3f; // Para auto-destrucci?n

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, tiempoVida); // Auto-destrucci?n
    }

    void Update()
    {
        // Movimiento constante hacia adelante
        rb.linearVelocity = transform.right * velocidad;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) // Cambia por tu tag adecuado
        {
            Destroy(gameObject); // Destruye proyectil al impactar
        }
    }
}