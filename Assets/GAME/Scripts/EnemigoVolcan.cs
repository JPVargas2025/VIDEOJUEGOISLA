using UnityEngine;

public class EnemigoVolcan : MonoBehaviour
{
    [Header("Configuración de Daño")]
    public string etiquetaSuelo = "Ground";

    private void OnTriggerEnter(Collider other)
    {
        EvaluarColision(other.gameObject, other.GetComponentInParent<SistemaDanoJugador>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        EvaluarColision(collision.gameObject, collision.gameObject.GetComponentInParent<SistemaDanoJugador>());
    }

    private void EvaluarColision(GameObject objetoColisionado, SistemaDanoJugador sistemaDano)
    {
        bool esJugador = objetoColisionado.CompareTag("Player") || (objetoColisionado.transform.parent != null && objetoColisionado.transform.parent.CompareTag("Player"));

        if (esJugador || sistemaDano != null)
        {
            Debug.Log("Enemigo Volcán: ¡Impacto contra el jugador!");

            if (sistemaDano != null)
            {
                sistemaDano.RecibirDano();
            }
            else
            {
                if (GameManager.Instancia != null) GameManager.Instancia.PerderVida();
            }

            Destroy(gameObject);
            return;
        }

        if (objetoColisionado.CompareTag(etiquetaSuelo))
        {
            Debug.Log("Enemigo Volcán: El meteorito chocó contra el suelo y se destruyó.");
            Destroy(gameObject);
        }
    }
}
