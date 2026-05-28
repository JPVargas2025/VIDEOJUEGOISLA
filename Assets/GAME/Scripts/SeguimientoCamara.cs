using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    [Header("Objetivo del Cuello")]
    public Transform targetCamara; 

    [Header("Configuración")]
    public float suavizado = 8.0f; 
    
    private Vector3 offset = new Vector3(0f, 0.5f, -3.5f); 

    void LateUpdate()
    {
        if (targetCamara == null) return;

        Vector3 posicionDeseada = targetCamara.position + (targetCamara.forward * offset.z) + (targetCamara.up * offset.y);
        
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);

        transform.LookAt(targetCamara.position);
    }
}