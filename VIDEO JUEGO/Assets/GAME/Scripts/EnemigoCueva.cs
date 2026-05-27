using UnityEngine;

public class EnemigoCueva : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("Velocidad a la que se mueve el enemigo. Puede ser lenta o rápida.")]
    public float velocidadMovimiento = 3f;

    [Header("Ruta de Patrulla")]
    [Tooltip("Puntos por los que el enemigo transitará en bucle o línea recta.")]
    public Transform[] puntosRuta;
    private int indicePuntoActual = 0;

    private void Update()
    {
        Patrullar();
    }

    private void Patrullar()
    {
        if (puntosRuta == null || puntosRuta.Length == 0) return;

        Transform puntoObjetivo = puntosRuta[indicePuntoActual];
        
        Vector3 objetivo = new Vector3(puntoObjetivo.position.x, transform.position.y, puntoObjetivo.position.z);
        
        transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidadMovimiento * Time.deltaTime);
        transform.LookAt(objetivo);

        // Cambiar al siguiente punto al acercarse al objetivo
        if (Vector3.Distance(transform.position, objetivo) < 0.2f)
        {
            indicePuntoActual = (indicePuntoActual + 1) % puntosRuta.Length;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SistemaDanoJugador sistemaDano = other.GetComponentInParent<SistemaDanoJugador>();

        bool esJugador = other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player"));

        if (esJugador || sistemaDano != null)
        {
            Debug.Log("Enemigo Cueva: ¡Golpe detectado al jugador!");

            if (sistemaDano != null)
            {
                // Ejecuta el daño, activa el parpadeo rojo y la vibración
                sistemaDano.RecibirDano();
            }
            else
            {
                if (GameManager.Instancia != null) GameManager.Instancia.PerderVida();
            }

            Debug.Log("Enemigo Cueva: Destruyendo bomba tras el impacto.");
            Destroy(gameObject);
        }
    }
}
