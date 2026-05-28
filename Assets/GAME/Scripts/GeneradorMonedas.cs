using UnityEngine;

public class GeneradorMonedas : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject monedaPrefab; 
    public int cantidadDeMonedas = 20;

    [Header("Límites del Área de Aparición")]
    public float minX = -20f;
    public float maxX = 20f;
    public float sueloY = 1f; 
    public float minZ = -20f;
    public float maxZ = 20f;

    void Start()
    {
        GenerarMonedasAlAzar();
    }

    void GenerarMonedasAlAzar()
    {
        for (int i = 0; i < cantidadDeMonedas; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            Vector3 posicionAleatoria = new Vector3(randomX, sueloY, randomZ);

            Instantiate(monedaPrefab, posicionAleatoria, Quaternion.identity);
        }
    }
}