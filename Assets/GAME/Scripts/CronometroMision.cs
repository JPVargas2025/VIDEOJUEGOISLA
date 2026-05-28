using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class CronometroMision : MonoBehaviour
{
    [Header("UI del Cronometro")]
    public TextMeshProUGUI textoTiempoUI;
    public GameObject panelPerdisteMisionUI; 
    public GameObject panelVictoriaUI;

    [Header("UI Emergente de Recolección (Panel)")]
    [SerializeField] private GameObject panelNotificacion;    
    [SerializeField] private TextMeshProUGUI textoNotificacion; 
    [SerializeField] private float tiempoVisible = 3f;         

    [Header("UI del Panel FIJO de la Misión (NUEVO)")]
    [SerializeField] private GameObject panelMisionFijo;       
    [SerializeField] private TextMeshProUGUI textoMisionFijo;   

    [Header("Identificador del Nivel")]
    public int nivelActual = 1; 

    private float tiempoRestante;
    private bool cronometroActivo = false;
    private Coroutine rutinaNotificacion;

    void Start()
    {
        CargarTiempoYMensajeDesdeJson();
        
        if(panelPerdisteMisionUI != null) panelPerdisteMisionUI.SetActive(false);
        if(panelVictoriaUI != null) panelVictoriaUI.SetActive(false);
        if(panelNotificacion != null) panelNotificacion.SetActive(false);

        if (panelMisionFijo != null) panelMisionFijo.SetActive(true);
    }

    void CargarTiempoYMensajeDesdeJson()
    {
        string ruta = Path.Combine(Application.streamingAssetsPath, "NivelesData.json");

        if (File.Exists(ruta))
        {
            string contenidoJson = File.ReadAllText(ruta);
            ListaMisiones wrapper = JsonUtility.FromJson<ListaMisiones>(contenidoJson);
            
            foreach (DatosMision config in wrapper.niveles)
            {
                if (config.numeroNivel == nivelActual)
                {
                    tiempoRestante = config.tiempoSegundos;
                    cronometroActivo = true;     
               
                    MostrarTextoEmergente(config.mensajeEntrada);

                    ActualizarPanelMisionFijo(config);
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("No se pudo leer NivelesData.json en StreamingAssets. Usando tiempo por defecto.");
            tiempoRestante = 300f; 
            cronometroActivo = true;

            if (textoMisionFijo != null)
            {
                textoMisionFijo.text = $"<b>NIVEL {nivelActual}</b>\nExplorando la Isla...";
            }
        }
    }

    void ActualizarPanelMisionFijo(DatosMision config)
    {
        if (textoMisionFijo == null) return;

      
        string nombreDeLaMision = !string.IsNullOrEmpty(config.nombreNivel) ? config.nombreNivel : "Misión de Exploración";
        textoMisionFijo.text = $"<b><color=#FF0000>Nivel {config.numeroNivel}</color></b>\n" +
                               $"<b>Nombre:</b> {nombreDeLaMision}\n" +
                               $"<b>Objetivo:</b> {config.mensajeEntrada}";
    }

    void Update()
    {
        if (!cronometroActivo) return;

        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTextoTiempo(tiempoRestante);
        }
        else
        {
            tiempoRestante = 0;
            ActivarGameOver();
        }
    }

    void ActualizarTextoTiempo(float tiempoMatematico)
    {
        float minutos = Mathf.FloorToInt(tiempoMatematico / 60);
        float segundos = Mathf.FloorToInt(tiempoMatematico % 60);

        textoTiempoUI.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void ActivarGameOver()
    {
        if (!cronometroActivo) return; 
        
        cronometroActivo = false;
        Debug.Log("¡Game Over Activado!");
        
        MovimientoExploradora exploradora = FindFirstObjectByType<MovimientoExploradora>();
        if (exploradora != null) exploradora.ActivarMuerte();

        if (panelPerdisteMisionUI != null) panelPerdisteMisionUI.SetActive(true);
    }

    public void ActivarVictoria()
    {
        if (!cronometroActivo) return;

        cronometroActivo = false;
        Debug.Log("¡Victoria Activada!");
        
        MovimientoExploradora exploradora = FindFirstObjectByType<MovimientoExploradora>();
        if (exploradora != null) exploradora.ActivarVictoria();

        if (GameManager.Instancia != null) GameManager.Instancia.VictoriaMision(nivelActual);

        if (panelVictoriaUI != null) panelVictoriaUI.SetActive(true);
    }

    public void MostrarTextoEmergente(string mensaje)
    {
        if (panelNotificacion == null || textoNotificacion == null) return;
        if (rutinaNotificacion != null) StopCoroutine(rutinaNotificacion);

        rutinaNotificacion = StartCoroutine(AnimarTexto(mensaje));
    }

    private IEnumerator AnimarTexto(string mensaje)
    {
        textoNotificacion.text = mensaje;
        panelNotificacion.SetActive(true);
        yield return new WaitForSeconds(tiempoVisible);
        panelNotificacion.SetActive(false);
    }

    public void ReintentarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SalirALaPlaya()
    {
        SceneManager.LoadScene(1);
    }

    public void SiguienteNivel()
    {
        if (GameManager.Instancia != null)
        {
            switch (nivelActual)
            {
                case 1: GameManager.Instancia.proximaHistoria = GameManager.ModoHistoria.Mision1; break;
                case 2: GameManager.Instancia.proximaHistoria = GameManager.ModoHistoria.Mision2; break;
                case 3: GameManager.Instancia.proximaHistoria = GameManager.ModoHistoria.Mision3; break;
                default: GameManager.Instancia.proximaHistoria = GameManager.ModoHistoria.Intro; break;
            }
        }
        SceneManager.LoadScene(5);
    }
}