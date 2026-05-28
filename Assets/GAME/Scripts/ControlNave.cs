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
            if (GameManager.Instancia != null)
            {
                GameManager.Instancia.CargarDatosDesdeJson();
           
                ControladorHUD hudPlaya = FindFirstObjectByType<ControladorHUD>();
                if (hudPlaya != null)
                {
                    hudPlaya.CambiarVidas(GameManager.Instancia.datosJugador.vidas);
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

        if (TieneTodosLosItems())
        {
            textoMensajeNave.text = "¡En hora buena! La nave de Lila está perfectamente reparada y los motores marcan el 100%. ¡Dale \"OK\" para encenderla y salir de aquí!";
        }
        else if (!datos.mision1Completada)
        {
            textoMensajeNave.text = "¡Oh no! Tu nave está apagada y el soporte vital fallando... ¡Ve a la selva por el Cristal de Agua y el Núcleo Mecánico!";
        }
        else if (datos.mision1Completada && !datos.mision2Completada)
        {
            textoMensajeNave.text = "¡Oh no! La nave se ha quedado sin energía eléctrica y los sistemas no responden... ¡Ve a la cueva por el Cristal de Rayo y la Batería Sci-Fi!";
        }
        else if (datos.mision2Completada && !datos.mision3Completada)
        {
            textoMensajeNave.text = "¡Oh no! La nave no tiene combustible para el despegue vertical y la isla está temblando... ¡Ve al volcán por el Cristal de Fuego y el Tanque de Combustible!";
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