using UnityEngine;
public class EnemigoSelva : MonoBehaviour

{

    [Header("Configuración de Velocidad")]

    public float velocidadPatrulla = 2f;

    public float velocidadPersecucion = 4f;



    [Header("Configuración de Detección")]

    public float rangoPersecucion = 5f;

    public Transform jugador;



    [Header("Ruta de Patrulla")]

    public Transform[] puntosPatrulla;

    private int indicePuntoActual = 0;



    private void Start()

    {

        if (jugador == null)

        {

            GameObject objJugador = GameObject.FindGameObjectWithTag("Player");

            if (objJugador != null)

                jugador = objJugador.transform;

        }

    }



    private void Update()

    {

        if (jugador == null) return;



        float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);



        if (distanciaAlJugador <= rangoPersecucion)

        {

            PerseguirJugador();

        }

        else

        {

            Patrullar();

        }

    }



    private void Patrullar()

    {

        if (puntosPatrulla == null || puntosPatrulla.Length == 0) return;



        Transform puntoObjetivo = puntosPatrulla[indicePuntoActual];


        Vector3 objetivo = new Vector3(puntoObjetivo.position.x, transform.position.y, puntoObjetivo.position.z);


        transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidadPatrulla * Time.deltaTime);

        transform.LookAt(objetivo);



        if (Vector3.Distance(transform.position, objetivo) < 0.2f)

        {

            indicePuntoActual = (indicePuntoActual + 1) % puntosPatrulla.Length;

        }

    }



    private void PerseguirJugador()

    {

        Vector3 objetivo = new Vector3(jugador.position.x, transform.position.y, jugador.position.z);

        transform.position = Vector3.MoveTowards(transform.position, objetivo, velocidadPersecucion * Time.deltaTime);

        transform.LookAt(objetivo);

    }



    private void OnTriggerEnter(Collider other)

    {

        SistemaDanoJugador sistemaDano = other.GetComponentInParent<SistemaDanoJugador>();



        bool esJugador = other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player"));



        if (esJugador || sistemaDano != null)

        {

            Debug.Log("¡Golpe detectado con éxito!");



            if (sistemaDano != null)

            {

                sistemaDano.RecibirDano();

            }

            else

            {

                if (GameManager.Instancia != null) GameManager.Instancia.PerderVida();

            }

            Debug.Log("Destruyendo al enemigo tras el impacto.");

            Destroy(gameObject);

        }

    }

}

