using System.Collections;
using UnityEngine;

public class GeneradorVolcan : MonoBehaviour
{
    [Header("Configuración del Generador")]
    [Tooltip("El prefab del meteorito/fuego que caerá.")]
    public GameObject prefabEnemigo;
    
    [Tooltip("Tiempo en segundos entre cada aparición de un meteorito.")]
    public float intervaloAparicion = 2f;

    [Header("Área de Generación en el Cielo")]
    public float minX = -20f;
    public float maxX = 20f;
    public float minZ = -20f;
    public float maxZ = 20f;
    [Tooltip("La altura desde la cual caerán los meteoritos.")]
    public float alturaGeneracionY = 30f;

    private bool generar = true;

    private void Start()
    {
        if (prefabEnemigo == null)
        {
            Debug.LogError("Generador Volcán: Falta asignar el prefab del enemigo.");
            return;
        }

        StartCoroutine(RutinaGeneracion());
    }

    private IEnumerator RutinaGeneracion()
    {
        while (generar)
        {
            GenerarEnemigo();
            yield return new WaitForSeconds(intervaloAparicion);
        }
    }

    private void GenerarEnemigo()
    {
        // Calcula una posición aleatoria en el plano XZ, manteniendo la altura Y
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 posicionAparicion = new Vector3(randomX, alturaGeneracionY, randomZ);

        // Instanciar el enemigo
        Instantiate(prefabEnemigo, posicionAparicion, Quaternion.identity);
    }

    // Método opcional para detener la generación si el juego termina
    public void DetenerGeneracion()
    {
        generar = false;
    }
}
