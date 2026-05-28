using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PanelNivelesUI : MonoBehaviour
{
    [Header("Referencias de UI")]
    public GameObject panelNiveles;
    public TextMeshProUGUI textoTitulo;       // <-- NUEVO: Para "Misión 1: La Selva"
    public TextMeshProUGUI textoDescripcion;  // <-- Para las instrucciones largas
    public Button botonComenzar;
    public Button botonSalir;

    private string escenaDestino;

    void Start()
    {
        if (panelNiveles != null)
            panelNiveles.SetActive(false);

        if (botonComenzar != null)
            botonComenzar.onClick.AddListener(ComenzarNivel);

        if (botonSalir != null)
            botonSalir.onClick.AddListener(CerrarPanel);
    }

    // Ahora recibe el objeto completo que se leyó del JSON
    public void MostrarPanelMision(DatosMision datos)
    {
        if (datos == null) return;

        escenaDestino = datos.nombreEscena;
        
        // Asignamos de forma separada los textos correspondientes del JSON
        textoTitulo.text = datos.tituloMision;
        textoDescripcion.text = datos.descripcionMision;

        panelNiveles.SetActive(true);
    }

    public void ComenzarNivel()
    {
        if (!string.IsNullOrEmpty(escenaDestino))
        {
            SceneManager.LoadScene(escenaDestino);
        }
    }

    public void CerrarPanel()
    {
        panelNiveles.SetActive(false);
    }
}