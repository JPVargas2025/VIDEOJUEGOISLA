using UnityEngine;
using UnityEngine.UI;
using TMPro; // 🌟 Necesario para los componentes de texto
using System.IO;

public class CartelInventario : MonoBehaviour
{
    [Header("Referencias de UI")]
    public GameObject panelInventario;
    
    [Header("Imágenes de Ítems")]
    public Image[] imagenesItems; 

    [Header("Textos de Ítems (Asignar en el mismo orden de arriba)")]
    public TextMeshProUGUI[] textosTitulos;      
    public TextMeshProUGUI[] textosDescripciones; 

    private string rutaJson;
    private ItemsWrapper baseDatosItems; 

    void Start()
    {
        if (panelInventario != null)
        {
            panelInventario.SetActive(false);
        }

        rutaJson = Path.Combine(Application.streamingAssetsPath, "ItemsData.json");
        CargarBaseDatosItems();
    }

    private void CargarBaseDatosItems()
    {
        if (File.Exists(rutaJson))
        {
            string contenido = File.ReadAllText(rutaJson);
            baseDatosItems = JsonUtility.FromJson<ItemsWrapper>(contenido);
        }
        else
        {
            Debug.LogError("No se encontró el archivo ItemsData.json en StreamingAssets");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActualizarInventario();
            if (panelInventario != null)
            {
                panelInventario.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (panelInventario != null)
            {
                panelInventario.SetActive(false);
            }
        }
    }

    private void ActualizarInventario()
    {
        if (GameManager.Instancia == null) return;
        if (imagenesItems == null || imagenesItems.Length < 6)
        {
            Debug.LogWarning("Faltan referencias a las imágenes del inventario.");
            return;
        }

        var datos = GameManager.Instancia.datosJugador;

        bool[] itemsRecolectados = new bool[]
        {
            datos.cristal1Recogido,
            datos.herramienta1Recogida,
            datos.cristal2Recogido,
            datos.herramienta2Recogida,
            datos.cristal3Recogido,
            datos.herramienta3Recogida
        };

       
        for (int i = 0; i < 6; i++)
        {
            
            if (baseDatosItems != null && i < baseDatosItems.items.Length)
            {
                ItemConfig infoItem = baseDatosItems.items[i]; // Tu molde original

                if (textosTitulos != null && i < textosTitulos.Length && textosTitulos[i] != null)
                {
                    textosTitulos[i].text = infoItem.nombre;
                }

                if (textosDescripciones != null && i < textosDescripciones.Length && textosDescripciones[i] != null)
                {
                    textosDescripciones[i].text = infoItem.descripcion;
                }
            }

            if (imagenesItems[i] != null)
            {
                if (itemsRecolectados[i])
                {
                    imagenesItems[i].color = Color.white;
                }
                else
                {
                    imagenesItems[i].color = Color.gray;  
                }
            }
        }
    }
}