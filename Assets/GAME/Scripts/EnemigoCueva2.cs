using UnityEngine;

public class EnemigoCueva2 : MonoBehaviour
{
    [Header("Configuración de Órbita (Sistema Solar)")]
    [Tooltip("El punto central alrededor del cual este enemigo orbitará (Ej. un objeto vacío en el centro de la cueva).")]
    public Transform centroOrbita;
    
    [Tooltip("Velocidad de rotación alrededor del centro.")]
    public float velocidadOrbita = 50f;

    [Tooltip("Si es verdadero, el enemigo siempre mirará en la dirección hacia la que se mueve.")]
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

        // Posición actual antes de movernos para calcular la dirección hacia donde miramos
        Vector3 posicionAnterior = transform.position;

        // Rotar alrededor del punto central sobre el eje Y (Arriba)
        transform.RotateAround(centroOrbita.position, Vector3.up, velocidadOrbita * Time.deltaTime);

        if (mirarHaciaAdelante)
        {
            // Calcular la dirección en la que nos acabamos de mover y mirar hacia allá
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
