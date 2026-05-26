using UnityEngine;

public class GeneradorMonedas : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject monedaPrefab; // Aquí arrastraremos el prefab azul de la moneda
    public int cantidadDeMonedas = 20; // Cuántas monedas quieres que aparezcan

    [Header("Límites del Área de Aparición")]
    public float minX = -20f;
    public float maxX = 20f;
    public float sueloY = 1f; // La altura a la que flotarán las monedas sobre el suelo
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
            // Calcular una posición aleatoria dentro de los rangos que le des
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            Vector3 posicionAleatoria = new Vector3(randomX, sueloY, randomZ);

            // Clonar (Instanciar) la moneda en esa posición exacta
            Instantiate(monedaPrefab, posicionAleatoria, Quaternion.identity);
        }
    }
}