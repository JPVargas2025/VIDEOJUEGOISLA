using UnityEngine;

public class ItemMoneda : MonoBehaviour
{
    [Header("Configuración Visual")]
    public float velocidadRotacion = 45f;

    private void Update()
    {
        // Gira la moneda sutilmente en el aire igual que tus otros ítems
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        // 1. Le sumamos 1 a la variable global del GameManager
        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.datosJugador.monedas++;
        }

        // 2. ¡EL NUEVO COMANDO DE UNITY 6! 
        // Cambiamos FindObjectOfType por FindFirstObjectByType
        ControladorHUD hudActual = Object.FindFirstObjectByType<ControladorHUD>();
        
        if (hudActual != null)
        {
            hudActual.SumarMonedas(1); 
        }

        // 3. Destruimos la moneda física
        Destroy(gameObject);
    }
  }
}