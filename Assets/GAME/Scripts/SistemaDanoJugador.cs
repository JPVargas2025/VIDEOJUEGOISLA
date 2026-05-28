using UnityEngine;
using System.Collections;

public class SistemaDanoJugador : MonoBehaviour
{
    [Header("Configuración Visual")]
    public Renderer[] renderersJugador; 
    public float duracionEfecto = 1f;
    public Transform modeloVisual; // Asignar el hijo que contiene el modelo 3D para hacerlo vibrar sin mover el CharacterController

    private bool esVulnerable = true;

    private void Start()
    {
        if (renderersJugador == null || renderersJugador.Length == 0)
        {
            renderersJugador = GetComponentsInChildren<Renderer>();
        }
        
        if (modeloVisual == null && transform.childCount > 0)
        {
            modeloVisual = transform.GetChild(0);
        }
    }

    public void RecibirDano()
    {
        if (!esVulnerable) return;

        if (GameManager.Instancia != null)
        {
            GameManager.Instancia.PerderVida();
        }

        StartCoroutine(EfectoDano());
    }

    private IEnumerator EfectoDano()
    {
        esVulnerable = false;
        
        Color colorDano = new Color(1f, 0f, 0f, 0.5f); // Rojo semitransparente
        Color[] coloresOriginales = new Color[renderersJugador.Length];

        // Cambiar color de los renderers
        for (int i = 0; i < renderersJugador.Length; i++)
        {
            if (renderersJugador[i] != null && renderersJugador[i].material.HasProperty("_Color"))
            {
                coloresOriginales[i] = renderersJugador[i].material.color;
                renderersJugador[i].material.color = colorDano;
            }
        }

        float tiempoPasado = 0f;
        Vector3 posOriginalLocal = modeloVisual != null ? modeloVisual.localPosition : Vector3.zero;

        // Bucle de vibración
        while (tiempoPasado < duracionEfecto)
        {
            if (modeloVisual != null)
            {
                float offsetX = Random.Range(-0.1f, 0.1f);
                float offsetZ = Random.Range(-0.1f, 0.1f);
                modeloVisual.localPosition = posOriginalLocal + new Vector3(offsetX, 0f, offsetZ);
            }

            tiempoPasado += Time.deltaTime;
            yield return null;
        }

        // Restaurar posición
        if (modeloVisual != null)
        {
            modeloVisual.localPosition = posOriginalLocal;
        }

        // Restaurar color original
        for (int i = 0; i < renderersJugador.Length; i++)
        {
            if (renderersJugador[i] != null && renderersJugador[i].material.HasProperty("_Color"))
            {
                renderersJugador[i].material.color = coloresOriginales[i];
            }
        }

        esVulnerable = true;
    }
}
