using UnityEngine;

public class ItemMoneda : MonoBehaviour
{
    [Header("Configuración Visual")]
    public float velocidadRotacion = 45f;

    private void Update()
    {
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.datosJugador.monedas++;
        }

        ControladorHUD hudActual = Object.FindFirstObjectByType<ControladorHUD>();
        
        if (hudActual != null)
        {
            hudActual.SumarMonedas(1); 
        }

        Destroy(gameObject);
    }
  }
}