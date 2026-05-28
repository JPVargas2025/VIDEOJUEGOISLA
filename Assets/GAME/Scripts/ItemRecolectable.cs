using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemRecolectable : MonoBehaviour
{
    public string idItem;
    public string tipoItem;
    public string nombreVisual;

    private bool yaRecogido = false; 

    private void Update()
    {
        transform.Rotate(Vector3.up * 45f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !yaRecogido)
        {
            yaRecogido = true;
            Debug.Log("¡Recolectaste el elemento: " + nombreVisual + "!");
            
            CronometroMision cronometro = FindFirstObjectByType<CronometroMision>();
            if (cronometro != null)
            {
                cronometro.MostrarTextoEmergente("¡Recolectaste el elemento: " + nombreVisual + "!");
            }

            RegistrarRecoleccion();

           
            GetComponent<Collider>().enabled = false;
            
            
            if (GetComponent<MeshRenderer>() != null) GetComponent<MeshRenderer>().enabled = false;
            foreach (Transform hijo in transform) hijo.gameObject.SetActive(false); 
            Destroy(gameObject, 0.1f);
        }
    }

    void RegistrarRecoleccion()
    {
        if (GameManager.Instancia == null)
        {
            Debug.LogWarning("No hay GameManager activo. Recolección registrada solo localmente.");
            return;
        }

        var datos = GameManager.Instancia.datosJugador;
        if (idItem == "cristal_1") datos.cristal1Recogido = true;
        if (idItem == "herramienta_1") datos.herramienta1Recogida = true;
        if (idItem == "cristal_2") datos.cristal2Recogido = true;
        if (idItem == "herramienta_2") datos.herramienta2Recogida = true;
        if (idItem == "cristal_3") datos.cristal3Recogido = true;
        if (idItem == "herramienta_3") datos.herramienta3Recogida = true;
        
        CheckVictoriaNivel();
    }

    void CheckVictoriaNivel()
    {
        if (GameManager.Instancia == null) return;

        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        var datos = GameManager.Instancia.datosJugador;
        bool gano = false;

        if (escenaActual == 2 && datos.cristal1Recogido && datos.herramienta1Recogida) 
        {
            GameManager.Instancia.VictoriaMision(1);
            gano = true;
        }
            
        if (escenaActual == 3 && datos.cristal2Recogido && datos.herramienta2Recogida) 
        {
            GameManager.Instancia.VictoriaMision(2);
            gano = true;
        }
            
        if (escenaActual == 4 && datos.cristal3Recogido && datos.herramienta3Recogida) 
        {
            GameManager.Instancia.VictoriaMision(3);
            gano = true;
        }

        if (gano)
        {
            CronometroMision cronometro = FindFirstObjectByType<CronometroMision>();
            if (cronometro != null)
            {
                cronometro.ActivarVictoria();
            }
        }
    }
}