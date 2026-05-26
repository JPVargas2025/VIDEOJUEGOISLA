using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class ControlNave : MonoBehaviour
{
    [Header("UI Elementos de la Nave")]
    public GameObject panelNave;
    public TextMeshProUGUI textoMensajeNave;
    public Button botonOK;

    void Start()
    {
        if (panelNave != null) panelNave.SetActive(false);

        if (botonOK != null)
        {
            botonOK.onClick.RemoveAllListeners();
            botonOK.onClick.AddListener(AlPresionarOK);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 🌟 NUEVO: Antes de mostrar el texto, obligamos al GameManager a leer el JSON
            // para asegurarnos de que la RAM tiene las vidas, monedas y misiones actualizadas del nivel anterior.
            if (GameManager.Instancia != null)
            {
                GameManager.Instancia.CargarDatosDesdeJson();
                
                // 🌟 EXCELENTE PRÁCTICA: Forzamos también al HUD local de la playa a actualizarse 
                // con las monedas, vidas y elementos visuales reales que se acaban de cargar del JSON.
                ControladorHUD hudPlaya = FindFirstObjectByType<ControladorHUD>();
                if (hudPlaya != null)
                {
                    hudPlaya.CambiarVidas(GameManager.Instancia.datosJugador.vidas);
                    // Si tienes un método en tu HUD para actualizar monedas, ponlo aquí, por ejemplo:
                    // hudPlaya.ActualizarMonedas(GameManager.Instancia.datosJugador.monedas);
                }
            }

            ActualizarTextoSegunProgreso();
            panelNave.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) panelNave.SetActive(false);
    }

    private bool TieneTodosLosItems()
    {
        if (GameManager.Instancia == null) return false;
        
        var d = GameManager.Instancia.datosJugador;
        return d.cristal1Recogido && d.herramienta1Recogida &&
               d.cristal2Recogido && d.herramienta2Recogida &&
               d.cristal3Recogido && d.herramienta3Recogida;
    }

    void ActualizarTextoSegunProgreso()
    {
        if (GameManager.Instancia == null) return;

        var datos = GameManager.Instancia.datosJugador;

        // Evaluamos el progreso real del JSON cargado
        if (TieneTodosLosItems())
        {
            textoMensajeNave.text = "¡Enhorabuena! ¡La nave está totalmente arreglada! ¡Has ganado!";
        }
        else if (!datos.mision1Completada)
        {
            textoMensajeNave.text = "¡Oh no! La nave está averiada... ¡Ve a la selva!";
        }
        else if (datos.mision1Completada && !datos.mision2Completada)
        {
            textoMensajeNave.text = "¡Oh no! ¡La nave tiene poco aceite! Ve a la cueva...";
        }
        else if (datos.mision2Completada && !datos.mision3Completada)
        {
            textoMensajeNave.text = "¡Oh no! ¡La nave no tiene combustible! Ve al volcán...";
        }
    }

    public void AlPresionarOK()
    {
        panelNave.SetActive(false); 

        if (GameManager.Instancia == null) return;
        
        if (TieneTodosLosItems())
        {
            Debug.Log("¡Nave reparada al 100%! Despegando hacia la cinemática final...");
            GameManager.Instancia.proximaHistoria = GameManager.ModoHistoria.Final;
            SceneManager.LoadScene(5);
        }
        else
        {
            Debug.Log("Mensaje de la nave cerrado. Al jugador aún le faltan ítems por recolectar.");
        }
    }
}