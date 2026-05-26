using UnityEngine;
using System.IO;

// Estructura de datos para una misión individual en el JSON
[System.Serializable]
public class DatosMision
{
    public int idMision;
    public string tituloMision;
    public string descripcionMision;
    public string nombreEscena;
}

// Estructura para la lista completa de misiones del JSON
[System.Serializable]
public class ListaMisiones
{
    public DatosMision[] misiones;
}

public class TriggerMision : MonoBehaviour
{
    [Header("Configuración del Trigger")]
    public int idDeEstaMision; // Árbol = 1, Roca = 2, Volcán = 3

    [Header("Referencia al Panel de UI")]
    public PanelNivelesUI gestorPanelUI;

    private string rutaArchivoJSON;

    private void Start()
    {
        // Definimos la ruta del archivo JSON (guardado en la carpeta del juego)
        rutaArchivoJSON = Path.Combine(Application.persistentDataPath, "datos_misiones.json");
        
        // Crear un JSON de prueba si no existe para que no te tire error la primera vez
        CrearJsonPruebaSiNoExiste();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cargamos los datos en tiempo real desde el archivo JSON
            DatosMision misionCargada = ObtenerMisionDesdeJSON(idDeEstaMision);

            if (misionCargada != null && gestorPanelUI != null)
            {
                // Pasamos los datos del JSON directamente al panel de la UI
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

            foreach (DatosMision m in lista.misiones)
            {
                if (m.idMision == idBuscar)
                {
                    return m; // Encontró la misión correcta
                }
            }
        }
        Debug.LogError("No se encontró el archivo JSON o la misión con ID: " + idBuscar);
        return null;
    }

    private void CrearJsonPruebaSiNoExiste()
    {
        if (!File.Exists(rutaArchivoJSON))
        {
            ListaMisiones listaPrueba = new ListaMisiones();
            listaPrueba.misiones = new DatosMision[]
            {
                new DatosMision { idMision = 1, tituloMision = "Misión 1: La Selva", descripcionMision = "Debes recuperar 1 Cristal y 1 Herramienta antes de que se agote el tiempo. ¡Cuidado con los monos!", nombreEscena = "Selva" },
                new DatosMision { idMision = 2, tituloMision = "Misión 2: La Cueva", descripcionMision = "Explora las profundidades oscuras de la isla. Encuentra el segundo cristal evitando los derrumbes.", nombreEscena = "Cueva" },
                new DatosMision { idMision = 3, tituloMision = "Misión 3: El Volcán", descripcionMision = "Fase final. Sube al cráter inestable del volcán para recuperar el último componente de la nave.", nombreEscena = "Volcan" }
            };

            string jsonString = JsonUtility.ToJson(listaPrueba, true);
            File.WriteAllText(rutaArchivoJSON, jsonString);
            Debug.Log("Archivo JSON de misiones creado automáticamente en: " + rutaArchivoJSON);
        }
    }
}