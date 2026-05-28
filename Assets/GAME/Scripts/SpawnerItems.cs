using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class SpawnerItems : MonoBehaviour
{
    [Header("Lista de Prefabs de tu Juego")]
    public List<GameObject> listaPrefabs; 

    void Start()
    {
        GenerarItemsDeLaEscena();
    }

    void GenerarItemsDeLaEscena()
    {
        string ruta = Path.Combine(Application.streamingAssetsPath, "ItemsData.json");
        if (!File.Exists(ruta)) return;

        string json = File.ReadAllText(ruta);
        ItemsWrapper datos = JsonUtility.FromJson<ItemsWrapper>(json);
        int escenaActual = SceneManager.GetActiveScene().buildIndex;

        foreach (ItemConfig item in datos.items)
        {
            if (item.escenaAsignada == escenaActual)
            {
                bool yaRecogido = false;
                
                if (GameManager.Instancia != null)
                {
                    var pDatos = GameManager.Instancia.datosJugador;
                    if (item.id == "cristal_1" && pDatos.cristal1Recogido) yaRecogido = true;
                    if (item.id == "herramienta_1" && pDatos.herramienta1Recogida) yaRecogido = true;
                    if (item.id == "cristal_2" && pDatos.cristal2Recogido) yaRecogido = true;
                    if (item.id == "herramienta_2" && pDatos.herramienta2Recogida) yaRecogido = true;
                    if (item.id == "cristal_3" && pDatos.cristal3Recogido) yaRecogido = true;
                    if (item.id == "herramienta_3" && pDatos.herramienta3Recogida) yaRecogido = true;
                }

                if (yaRecogido) continue; 

                GameObject prefabAEmitir = BuscarPrefabPorID(item.id);

                if (prefabAEmitir == null)
                {
                    Debug.LogError("No se encontró ningún prefab en la lista con el ID: " + item.id);
                    continue;
                }

                Vector3 posicion = new Vector3(item.posX, item.posY, item.posZ);
                GameObject nuevoItem = Instantiate(prefabAEmitir, posicion, Quaternion.identity);
                
                ItemRecolectable componente = nuevoItem.GetComponent<ItemRecolectable>();
                if (componente == null) componente = nuevoItem.AddComponent<ItemRecolectable>();
                
                componente.idItem = item.id;
                componente.tipoItem = item.tipo;
                componente.nombreVisual = item.nombre;
            }
        }
    }

    GameObject BuscarPrefabPorID(string idBuscado)
    {
        foreach (GameObject prefab in listaPrefabs)
        {
            ItemRecolectable scriptItem = prefab.GetComponent<ItemRecolectable>();
            if (scriptItem != null && scriptItem.idItem == idBuscado)
            {
                return prefab;
            }
        }
        return null;
    }
}