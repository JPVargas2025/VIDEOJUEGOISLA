using UnityEngine;
using System.IO;

public class TriggerMision : MonoBehaviour
{
    [Header("Configuración del Trigger")]
    public int numeroNivelAAsignar; 

    [Header("Referencia al Panel de UI de la Playa")]
    public PanelNivelesUI gestorPanelUI;

    private string rutaArchivoJSON;

    private void Start()
    {
        rutaArchivoJSON = Path.Combine(Application.streamingAssetsPath, "NivelesData.json");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DatosMision misionCargada = ObtenerMisionDesdeJSON(numeroNivelAAsignar);

            if (misionCargada != null && gestorPanelUI != null)
            {
                gestorPanelUI.MostrarPanelMision(misionCargada);
            }
        }
    }

    private DatosMision ObtenerMisionDesdeJSON(int idBuscar)
    {
        if (File.Exists(rutaArchivoJSON))
        {
            string contenidoJson = File.ReadAllText(rutaArchivoJSON);
            ListaMisiones lista = JsonUtility.FromJson<ListaMisiones>(contenidoJson);

            foreach (DatosMision m in lista.niveles)
            {
                if (m.numeroNivel == idBuscar)
                {
                    return m; 
                }
            }
        }
        
        Debug.LogError("ERROR: No se encontró NivelesData.json o falta el nivel: " + idBuscar);
        return null;
    }
}