using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorHUD : MonoBehaviour
{
    [Header("Componentes de Vidas (Corazones)")]
    public Image[] imagenesCorazones;

    [Header("Componentes de Monedas")]
    public TextMeshProUGUI textoMonedas;

    [Header("Componentes de Batería")]
    public Image imagenBateria;
    public TextMeshProUGUI textoBateria;

    [Header("Sprites de la Batería (Estados)")]
    public Sprite[] spritesBateria; 

    private int monedasActuales = 0;
    private int vidasActuales = 3;
    private int porcentajeBateria = 0;

    void Start()
    {
        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.CargarDatosDesdeJson();

            var datos = GameManager.Instancia.datosJugador;
            vidasActuales = datos.vidas;
            monedasActuales = datos.monedas;

            porcentajeBateria = 0;
            if (datos.mision1Completada) porcentajeBateria = 33;
            if (datos.mision2Completada) porcentajeBateria = 66;
            if (datos.mision3Completada) porcentajeBateria = 100;
        }
        else
        {
            vidasActuales = 3;
            monedasActuales = 0;
            porcentajeBateria = 0;
        }
        
        ActualizarUIInicial();
    }

    public void CambiarVidas(int nuevasVidas)
    {
        vidasActuales = Mathf.Clamp(nuevasVidas, 0, 3);

        if (imagenesCorazones != null)
        {
            for (int i = 0; i < imagenesCorazones.Length; i++)
            {
                if (imagenesCorazones[i] != null)
                {
                    imagenesCorazones[i].enabled = i < vidasActuales;
                }
            }
        }

        if (vidasActuales <= 0)
        {
            EjecutarMuerteJugador();
        }
    }

    private void EjecutarMuerteJugador()
    {
        Debug.Log("¡El jugador ha perdido todas las vidas! Fin del juego.");
    }

    public void SumarMonedas(int cantidad)
    {
        monedasActuales += cantidad;
        if (textoMonedas != null) textoMonedas.text = monedasActuales.ToString();

        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.datosJugador.monedas = monedasActuales;
        }
    }

    public void ActualizarBateria(int nuevoPorcentaje)
    {
        porcentajeBateria = nuevoPorcentaje;
        if (textoBateria != null) textoBateria.text = porcentajeBateria + "%";

        if (imagenBateria != null && spritesBateria != null && spritesBateria.Length >= 4)
        {
            switch (porcentajeBateria)
            {
                case 0: imagenBateria.sprite = spritesBateria[0]; break;
                case 33: imagenBateria.sprite = spritesBateria[1]; break;
                case 66: imagenBateria.sprite = spritesBateria[2]; break;
                case 100: imagenBateria.sprite = spritesBateria[3]; break;
            }
        }
    }

    private void ActualizarUIInicial()
    {
        if (textoMonedas != null) textoMonedas.text = monedasActuales.ToString();
        ActualizarBateria(porcentajeBateria);
        CambiarVidas(vidasActuales);
    }
}