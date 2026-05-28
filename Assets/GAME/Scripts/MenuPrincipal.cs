using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Importante para manejar los nuevos componentes de texto

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
    
    [Header("Botones del Panel de Instrucciones")]
    public Button botonVolverInstrucciones;
    public Button botonSiguientePagina;     
    public Button botonAtrasPagina;    
     
    [Header("Textos del Panel de Instrucciones")]
    public TextMeshProUGUI textoContenido; 
    public TextMeshProUGUI textoSubtitulo;  
    public TextMeshProUGUI textoContador;   

    [Header("Contenido de las Instrucciones")]
    [TextArea(3, 10)] 
    public string[] paginasInstrucciones;  
    private int paginaActualIndex = 0;

    void Start()
    {
        botonNuevoJuego.onClick.AddListener(IniciarNuevoJuego);
        botonCargarPartida.onClick.AddListener(CargarPartidaGuardada);
        botonInstrucciones.onClick.AddListener(MostrarInstrucciones);
        botonSalir.onClick.AddListener(SalirDelJuego);
       
        botonVolverInstrucciones.onClick.AddListener(RegresarAlMenu);
        if (botonSiguientePagina != null) botonSiguientePagina.onClick.AddListener(AvanzarPagina);
        if (botonAtrasPagina != null) botonAtrasPagina.onClick.AddListener(RetrocederPagina);

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
        SceneManager.LoadScene(5); 
    }

    void CargarPartidaGuardada()
    {
        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.CargarDatosDesdeJson();
            SceneManager.LoadScene(GameManager.Instancia.datosJugador.escenaActual);
        }
    }

    void MostrarInstrucciones()
    {
        panelMenuPrincipal.SetActive(false);
        panelInstrucciones.SetActive(true);
        
        paginaActualIndex = 0;
        ActualizarPaginaUI();
    }

    void ActualizarPaginaUI()
    {
        if (paginasInstrucciones == null || paginasInstrucciones.Length == 0) return;

        textoContenido.text = paginasInstrucciones[paginaActualIndex];

        if (textoContador != null)
        {
            textoContador.text = (paginaActualIndex + 1) + " / " + paginasInstrucciones.Length;
        }

        if (textoSubtitulo != null)
        {
            if (paginaActualIndex == 0) textoSubtitulo.text = "SINOPSIS";
            else if (paginaActualIndex == 1) textoSubtitulo.text = "OBJETIVO PRINCIPAL";
            else if (paginaActualIndex == 2) textoSubtitulo.text = "MOVIMIENTO PLAYER";
            else if (paginaActualIndex == 3) textoSubtitulo.text = "RECOLECTABLES";
            else if (paginaActualIndex == 4) textoSubtitulo.text = "AMENAZAS";
            else if (paginaActualIndex == 5) textoSubtitulo.text = "VIDAS Y TIEMPO";
            else if (paginaActualIndex == 6) textoSubtitulo.text = "PLAYA(HUB CENTRAL)";
        }
       
        if (paginaActualIndex == 0)
        {
            if (botonAtrasPagina != null) botonAtrasPagina.gameObject.SetActive(false); 
            if (botonVolverInstrucciones != null) botonVolverInstrucciones.gameObject.SetActive(true); 
            
          
            if (botonSiguientePagina != null) 
                botonSiguientePagina.gameObject.SetActive(paginasInstrucciones.Length > 1);
        }

        else if (paginaActualIndex == paginasInstrucciones.Length - 1)
        {
            if (botonAtrasPagina != null) botonAtrasPagina.gameObject.SetActive(true); 
            if (botonSiguientePagina != null) botonSiguientePagina.gameObject.SetActive(false); 
            if (botonVolverInstrucciones != null) botonVolverInstrucciones.gameObject.SetActive(true);
        }
        else
        {
            if (botonAtrasPagina != null) botonAtrasPagina.gameObject.SetActive(true); 
            if (botonSiguientePagina != null) botonSiguientePagina.gameObject.SetActive(true); 
            if (botonVolverInstrucciones != null) botonVolverInstrucciones.gameObject.SetActive(true);
        }
    }

    public void AvanzarPagina()
    {
        if (paginaActualIndex < paginasInstrucciones.Length - 1)
        {
            paginaActualIndex++;
            ActualizarPaginaUI();
        }
    }

    public void RetrocederPagina()
    {
        if (paginaActualIndex > 0)
        {
            paginaActualIndex--;
            ActualizarPaginaUI();
        }
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