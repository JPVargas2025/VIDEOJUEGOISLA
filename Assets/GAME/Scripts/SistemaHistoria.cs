using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SistemaHistoria : MonoBehaviour
{
    [Header("Referencias de la UI Base")]
    public Image contenedorImagen;
    public GameObject panelTextoNarrativo;
    public TextMeshProUGUI textoNarrativoUI;
    public Button botonSiguiente;

    [Header("Configuración de Velocidad Texto")]
    [Range(0.005f, 0.1f)] 
    [Tooltip("Menor número = Más rápido. Mayor número = Más lento.")]
    public float velocidadEscritura = 0.02f;

    [Header("Paneles Especiales (Cierre del Juego)")]
    public GameObject panelVictoriaFinal;
    public Button botonReiniciarJuego;
    public Button botonSalirDelJuego;

    [Header("Contenido Narrativo: INTRODUCCIÓN")]
    public Sprite[] imagenesIntro;
    [Multiline(3)] public string[] textosIntro;

    [Header("Contenido Narrativo: POST MISIÓN 1 (SELVA)")]
    public Sprite[] imagenesMision1;
    [Multiline(3)] public string[] textosMision1;

    [Header("Contenido Narrativo: POST MISIÓN 2 (CUEVA)")]
    public Sprite[] imagenesMision2;
    [Multiline(3)] public string[] textosMision2;

    [Header("Contenido Narrativo: POST MISIÓN 3 (VOLCÁN)")]
    public Sprite[] imagenesMision3;
    [Multiline(3)] public string[] textosMision3;

    [Header("Contenido Narrativo: FINAL (CINEMÁTICA NAVE)")]
    public Sprite[] imagenesFinal;
    [Multiline(3)] public string[] textosFinal;

    private Sprite[] imagenesActivas;
    private string[] textosActivos;
    private int indiceActual = 0;
    private bool estaEscribiendo = false;
    private Coroutine corrutinaEscritura; 

    void Start()
    {
        botonSiguiente.onClick.AddListener(Avanzar);
        
        if (botonReiniciarJuego != null) 
            botonReiniciarJuego.onClick.AddListener(() => SceneManager.LoadScene(0));
        
        if (botonSalirDelJuego != null) 
            botonSalirDelJuego.onClick.AddListener(Application.Quit);
            
        if (panelVictoriaFinal != null) panelVictoriaFinal.SetActive(false);

        // Aseguramos que el panel de texto esté encendido al comenzar
        if (panelTextoNarrativo != null) panelTextoNarrativo.SetActive(true);

        ConfigurarFlujoDeHistoria();
    }

    void ConfigurarFlujoDeHistoria()
    {
        if (GameManager.Instancia == null)
        {
            Debug.LogError("No hay GameManager en la escena. ¡Inicia desde el Menú Principal!");
            return;
        }
        switch (GameManager.Instancia.proximaHistoria)
        {
            case GameManager.ModoHistoria.Intro:
                imagenesActivas = imagenesIntro;
                textosActivos = textosIntro;
                break;

            case GameManager.ModoHistoria.Mision1:
                imagenesActivas = imagenesMision1;
                textosActivos = textosMision1;
                break;

            case GameManager.ModoHistoria.Mision2:
                imagenesActivas = imagenesMision2;
                textosActivos = textosMision2;
                break;

            case GameManager.ModoHistoria.Mision3:
                imagenesActivas = imagenesMision3;
                textosActivos = textosMision3;
                break;

            case GameManager.ModoHistoria.Final:
                imagenesActivas = imagenesFinal;
                textosActivos = textosFinal;
                break;
        }

        indiceActual = 0;
        MostrarFaseActual();
    }

    void MostrarFaseActual()
    {
        if (imagenesActivas == null || imagenesActivas.Length == 0 || textosActivos == null || textosActivos.Length == 0)
        {
            Debug.LogError("¡Faltan datos! Revisa el Inspector del SistemaHistoria; no hay imágenes o textos para este estado.");
            return;
        }

        contenedorImagen.sprite = imagenesActivas[indiceActual];
        
        if (corrutinaEscritura != null) StopCoroutine(corrutinaEscritura);
        corrutinaEscritura = StartCoroutine(EfectoMaquinaEscribir(textosActivos[indiceActual]));
    }

    IEnumerator EfectoMaquinaEscribir(string textoCompleto)
    {
        textoNarrativoUI.text = "";
        estaEscribiendo = true;

        foreach (char letra in textoCompleto)
        {
            textoNarrativoUI.text += letra;
            
            // 🌟 Ahora usa el valor que configures en la barrita de Unity:
            yield return new WaitForSeconds(velocidadEscritura); 
        }

        estaEscribiendo = false;
    }

    void Avanzar()
    {
        if (estaEscribiendo)
        {
            if (corrutinaEscritura != null) StopCoroutine(corrutinaEscritura);
            textoNarrativoUI.text = textosActivos[indiceActual]; 
            estaEscribiendo = false;
            return; 
        }

        indiceActual++;
        if (indiceActual < imagenesActivas.Length)
        {
            MostrarFaseActual();
        }
        else
        {
            if (GameManager.Instancia.proximaHistoria == GameManager.ModoHistoria.Final)
            {
                botonSiguiente.gameObject.SetActive(false);
                textoNarrativoUI.gameObject.SetActive(false);
                
                if (panelTextoNarrativo != null) panelTextoNarrativo.SetActive(false);
                
                if (panelVictoriaFinal != null)
                {
                    panelVictoriaFinal.SetActive(true);
                }
            }
            else
            {
                if (GameManager.Instancia != null)
                {
                    if (GameManager.Instancia.proximaHistoria == GameManager.ModoHistoria.Intro)
                    {
                        GameManager.Instancia.proximaHistoria = GameManager.ModoHistoria.Mision1;
                    }
                    else if (GameManager.Instancia.proximaHistoria == GameManager.ModoHistoria.Mision1)
                    {
                        GameManager.Instancia.proximaHistoria = GameManager.ModoHistoria.Mision2;
                    }
                    else if (GameManager.Instancia.proximaHistoria == GameManager.ModoHistoria.Mision2)
                    {
                        GameManager.Instancia.proximaHistoria = GameManager.ModoHistoria.Mision3;
                    }

                    GameManager.Instancia.GuardarDatosEnJson();
                }

                SceneManager.LoadScene(1);
            }
        }
    }
}