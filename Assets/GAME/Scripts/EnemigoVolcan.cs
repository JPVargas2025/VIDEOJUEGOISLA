using UnityEngine;

public class EnemigoVolcan : MonoBehaviour
{
    [Header("Configuración de Daño")]
    [Tooltip("El enemigo se destruirá al tocar cualquier objeto con la etiqueta Ground.")]
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

        // 1. Verificar si tocó al jugador
        if (esJugador || sistemaDano != null)
        {
            Debug.Log("Enemigo Volcán: ¡Impacto contra el jugador!");

            if (sistemaDano != null)
            {
                // Aplica el daño y el efecto de parpadeo/vibración
                sistemaDano.RecibirDano();
            }
            else
            {
                if (GameManager.Instancia != null) GameManager.Instancia.PerderVida();
            }

            // El meteorito desaparece al impactar al jugador
            Destroy(gameObject);
            return;
        }

        // 2. Verificar si tocó el suelo
        if (objetoColisionado.CompareTag(etiquetaSuelo))
        {
            Debug.Log("Enemigo Volcán: El meteorito chocó contra el suelo y se destruyó.");
            // El meteorito desaparece al impactar el suelo
            Destroy(gameObject);
        }
    }
}
