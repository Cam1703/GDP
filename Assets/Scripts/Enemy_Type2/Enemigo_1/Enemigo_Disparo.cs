using UnityEngine;

public class EnemigoDisparador : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject prefabProyectil; // Asigna el prefab desde el Inspector
    public Transform puntoDisparo; // Asigna el transform del punto de disparo
    public float cadenciaDisparo = 2f;

    private float temporizadorDisparo;

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
        // Instancia el proyectil en la posición y rotación del punto de disparo
        Debug.Log("Disparo generado");
        Instantiate(prefabProyectil, puntoDisparo.position, puntoDisparo.rotation);
    }
}