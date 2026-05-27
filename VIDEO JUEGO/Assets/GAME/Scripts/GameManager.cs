using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    [System.Serializable]
    public class DatosJugador
    {
        public int escenaActual = 1; 
        public int monedas = 0;
        public int vidas = 3;
        
        public bool mision1Completada = false; 
        public bool mision2Completada = false; 
        public bool mision3Completada = false;

        [Header("Nivel 1 - Selva")]
        public bool cristal1Recogido = false;
        public bool herramienta1Recogida = false;

        [Header("Nivel 2 - Cueva")]
        public bool cristal2Recogido = false;
        public bool herramienta2Recogida = false;

        [Header("Nivel 3 - Volcán")]
        public bool cristal3Recogido = false;
        public bool herramienta3Recogida = false;
    }

    public DatosJugador datosJugador = new DatosJugador();

    public enum ModoHistoria { Intro, Mision1, Mision2, Mision3, Final }
    public ModoHistoria proximaHistoria;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
            CargarDatosDesdeJson();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            string ruta = Path.Combine(Application.persistentDataPath, "PartidaGuardada.json");
            if (File.Exists(ruta))
            {
                File.Delete(ruta);
                Debug.Log("Archivo 'PartidaGuardada.json' de prueba eliminado.");
            }
            InicializarNuevaPartida();
            Debug.Log("GameManager reseteado: Los ítems volverán a aparecer.");
        }
    }

    public void InicializarNuevaPartida()
    {
        datosJugador = new DatosJugador();
        proximaHistoria = ModoHistoria.Intro;
        GuardarDatosEnJson(); 
    }

    public void VictoriaMision(int idMision)
    {
        if (idMision == 1)
        {
            datosJugador.mision1Completada = true;
            datosJugador.cristal1Recogido = true;    
            datosJugador.herramienta1Recogida = true;
        }
        if (idMision == 2)
        {
            datosJugador.mision2Completada = true;
            datosJugador.cristal2Recogido = true;
            datosJugador.herramienta2Recogida = true;
        }
        if (idMision == 3)
        {
            datosJugador.mision3Completada = true;
            datosJugador.cristal3Recogido = true;
            datosJugador.herramienta3Recogida = true;
        }

        Debug.Log($"Progreso de Misión {idMision} actualizado en la RAM.");
    }

    public void PerderVida()
    {
        datosJugador.vidas--;
        Debug.Log("Vidas restantes: " + datosJugador.vidas);
        
        ControladorHUD hudActual = FindFirstObjectByType<ControladorHUD>();
        if (hudActual != null)
        {
            hudActual.CambiarVidas(datosJugador.vidas);
        }
      
        if (datosJugador.vidas <= 0)
        {
            Debug.Log("¡GAME OVER!");
            InicializarNuevaPartida();
            
            CronometroMision cronometro = FindFirstObjectByType<CronometroMision>();
            if (cronometro != null)
            {
                cronometro.ActivarGameOver();
            }
            else
            {
                SceneManager.LoadScene(0); 
            }
        }
    }

    public void GuardarDatosEnJson()
    {
        datosJugador.escenaActual = SceneManager.GetActiveScene().buildIndex;
        string ruta = Path.Combine(Application.persistentDataPath, "PartidaGuardada.json");
        string json = JsonUtility.ToJson(datosJugador, true);
        File.WriteAllText(ruta, json);
        Debug.Log("Partida guardada en el disco: " + ruta);
    }

    public void CargarDatosDesdeJson()
    {
        string ruta = Path.Combine(Application.persistentDataPath, "PartidaGuardada.json");
        if (File.Exists(ruta))
        {
            string json = File.ReadAllText(ruta);
            datosJugador = JsonUtility.FromJson<DatosJugador>(json);
            Debug.Log("Partida cargada con éxito.");
        }
    }
} 