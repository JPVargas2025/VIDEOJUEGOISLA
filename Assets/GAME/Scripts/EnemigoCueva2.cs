using UnityEngine;

public class EnemigoCueva2 : MonoBehaviour
{
    [Header("Configuración de Órbita (Sistema Solar)")]
    public Transform centroOrbita;
    public float velocidadOrbita = 50f;
    public bool mirarHaciaAdelante = true;

    private void Update()
    {
        Orbitar();
    }

    private void Orbitar()
    {
        if (centroOrbita == null)
        {
            Debug.LogWarning("EnemigoCueva2: Falta asignar un 'centroOrbita' en el Inspector.");
            return;
        }

        Vector3 posicionAnterior = transform.position;

        transform.RotateAround(centroOrbita.position, Vector3.up, velocidadOrbita * Time.deltaTime);

        if (mirarHaciaAdelante)
        {
            Vector3 direccionMovimiento = (transform.position - posicionAnterior).normalized;
            if (direccionMovimiento != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direccionMovimiento);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SistemaDanoJugador sistemaDano = other.GetComponentInParent<SistemaDanoJugador>();

        bool esJugador = other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player"));

        if (esJugador || sistemaDano != null)
        {
            Debug.Log("Enemigo Cueva Órbita: ¡Golpe detectado al jugador!");

            if (sistemaDano != null)
            {
                // Ejecuta el daño, activa el parpadeo rojo y la vibración
                sistemaDano.RecibirDano();
            }
            else
            {
                if (GameManager.Instancia != null) GameManager.Instancia.PerderVida();
            }

            Debug.Log("Enemigo Cueva Órbita: Destruyendo bomba tras el impacto.");
            Destroy(gameObject);
        }
    }
}
