using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelNivelesUI : MonoBehaviour
{
    [Header("Referencias de UI")]
    public GameObject panelNiveles;
    public TextMeshProUGUI textoTitulo;      
    public TextMeshProUGUI textoDescripcion; 
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

    public void MostrarPanelMision(DatosMision datos)
    {
        if (datos == null) return;

        escenaDestino = datos.nombreEscena;
        
        textoTitulo.text = "Misión " + datos.numeroNivel + ": " + datos.nombreNivel;
        textoDescripcion.text = datos.descripcionPlaya;

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