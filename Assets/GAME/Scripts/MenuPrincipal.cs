using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Paneles de la UI")]
    public GameObject panelMenuPrincipal;
    public GameObject panelInstrucciones;

    [Header("Botones de Interacción")]
    public Button botonNuevoJuego;
    public Button botonCargarPartida;
    public Button botonInstrucciones;
    public Button botonSalir;
    
    [Header("Botones de Cierre/Regreso")]
    public Button botonVolverInstrucciones;

    void Start()
    {
        botonNuevoJuego.onClick.AddListener(IniciarNuevoJuego);
        botonCargarPartida.onClick.AddListener(CargarPartidaGuardada);
        botonInstrucciones.onClick.AddListener(MostrarInstrucciones);
        botonSalir.onClick.AddListener(SalirDelJuego);
        
        botonVolverInstrucciones.onClick.AddListener(RegresarAlMenu);

        ComprobarPartidaGuardada();
    }

    void ComprobarPartidaGuardada()
    {
        string rutaGuardado = Path.Combine(Application.persistentDataPath, "PartidaGuardada.json");
        botonCargarPartida.interactable = File.Exists(rutaGuardado);
    }

    void IniciarNuevoJuego()
    {
        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.InicializarNuevaPartida();
        }

        panelMenuPrincipal.SetActive(false);

        // CARGAMOS LA ESCENA 5 (HISTORIA INTERMEDIA)
        SceneManager.LoadScene(5); 
    }

    void CargarPartidaGuardada()
    {
        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.CargarDatosDesdeJson();
            // Carga la escena donde se quedó guardado el jugador (Playa/Nivel)
            SceneManager.LoadScene(GameManager.Instancia.datosJugador.escenaActual);
        }
    }

    void MostrarInstrucciones()
    {
        panelMenuPrincipal.SetActive(false);
        panelInstrucciones.SetActive(true);
    }

    void RegresarAlMenu()
    {
        panelInstrucciones.SetActive(false);
        panelMenuPrincipal.SetActive(true);
        ComprobarPartidaGuardada();
    }

    void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}