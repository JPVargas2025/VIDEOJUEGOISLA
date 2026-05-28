using UnityEngine;
using TMPro;
using System.Collections;

public class CofreGuardado : MonoBehaviour
{
    [Header("Referencias de UI")]
    public GameObject panelCofre;        
    public TextMeshProUGUI textoMensaje; 
    public GameObject botonSi;           
    public GameObject botonNo;           

    private Coroutine secuenciaActiva;

    void Start()
    {
        if (panelCofre != null)
        {
            panelCofre.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AbrirVentanaGuardado();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CerrarVentanaGuardado();
        }
    }

    private void AbrirVentanaGuardado()
    {
        if (secuenciaActiva != null) StopCoroutine(secuenciaActiva);
        
        textoMensaje.text = "¿Deseas guardar partida?";
        botonSi.SetActive(true);
        botonNo.SetActive(true);
        panelCofre.SetActive(true);
    }

    public void CerrarVentanaGuardado()
    {
        if (secuenciaActiva != null) StopCoroutine(secuenciaActiva);
        panelCofre.SetActive(false);
    }

    public void AlHacerClicEnSi()
    {
        secuenciaActiva = StartCoroutine(SecuenciaGuardado());
    }

    private IEnumerator SecuenciaGuardado()
    {
        botonSi.SetActive(false);
        botonNo.SetActive(false);

        textoMensaje.text = "Guardando partida...";
        
        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.GuardarDatosEnJson(); 
        }
        
        yield return new WaitForSeconds(1.5f); 

        textoMensaje.text = "¡Partida guardada con éxito!";
        yield return new WaitForSeconds(1.5f); 

        panelCofre.SetActive(false);
    }
}