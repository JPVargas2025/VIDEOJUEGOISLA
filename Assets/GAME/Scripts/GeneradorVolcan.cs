using System.Collections;
using UnityEngine;

public class GeneradorVolcan : MonoBehaviour
{
    [Header("Configuración del Generador")]
    public GameObject prefabEnemigo;
    public float intervaloAparicion = 2f;

    [Header("Área de Generación en el Cielo")]
    public float minX = -20f;
    public float maxX = 20f;
    public float minZ = -20f;
    public float maxZ = 20f;
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
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 posicionAparicion = new Vector3(randomX, alturaGeneracionY, randomZ);

        Instantiate(prefabEnemigo, posicionAparicion, Quaternion.identity);
    }

    public void DetenerGeneracion()
    {
        generar = false;
    }
}
