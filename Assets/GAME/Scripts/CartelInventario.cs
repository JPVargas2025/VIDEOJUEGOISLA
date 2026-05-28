using UnityEngine;
using UnityEngine.UI;

public class CartelInventario : MonoBehaviour
{
    [Header("Referencias de UI")]
    public GameObject panelInventario;
    
    [Header("Imágenes de Ítems (Asignar en orden: C1, H1, C2, H2, C3, H3)")]
    [Tooltip("0: Cristal Selva, 1: Herramienta Selva, 2: Cristal Cueva, 3: Herramienta Cueva, 4: Cristal Volcán, 5: Herramienta Volcán")]
    public Image[] imagenesItems;

    void Start()
    {
        if (panelInventario != null)
        {
            panelInventario.SetActive(false);
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

        // Arreglo temporal con el estado de cada ítem
        bool[] itemsRecolectados = new bool[]
        {
            datos.cristal1Recogido,
            datos.herramienta1Recogida,
            datos.cristal2Recogido,
            datos.herramienta2Recogida,
            datos.cristal3Recogido,
            datos.herramienta3Recogida
        };

        // Cambiar el color según si se recogió o no
        for (int i = 0; i < 6; i++)
        {
            if (imagenesItems[i] != null)
            {
                if (itemsRecolectados[i])
                {
                    imagenesItems[i].color = Color.white; // A todo color
                }
                else
                {
                    // Convertir a escala de grises (Color.gray funciona bien en la mayoría de los casos sin material custom)
                    imagenesItems[i].color = Color.gray; 
                }
            }
        }
    }
}
