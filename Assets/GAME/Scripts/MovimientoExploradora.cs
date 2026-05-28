using UnityEngine;

using UnityEngine.InputSystem; 



[RequireComponent(typeof(CharacterController))]

public class MovimientoExploradora : MonoBehaviour

{

    [Header("Configuración de Movimiento")]

    public float walkSpeed = 5.0f;       // Velocidad al caminar

    public float runSpeed = 9.0f;        // Velocidad al correr 🏃‍♀️

    public float rotationSpeed = 10.0f;  // Velocidad de rotación del personaje

    public float jumpForce = 5.0f;       // Fuerza de salto

    public float gravity = -15.0f;       // Fuerza de gravedad ajustable



    [Header("Configuración de Cámara")]

    public Camera followCamera; 



    private CharacterController controller;

    private Vector3 playerVelocity;

    private bool isGrounded;



    void Start()

    {

        controller = GetComponent<CharacterController>();



        // Si no se asignó cámara en el inspector, busca la MainCamera automática

        if (followCamera == null && Camera.main != null)

        {

            followCamera = Camera.main;

        }

    }



    void Update()

    {

        // 1. Verificar si está tocando el suelo nativamente con el CharacterController

        isGrounded = controller.isGrounded;

        

        if (isGrounded && playerVelocity.y < 0)

        {

            // Pequeña fuerza constante hacia abajo para evitar que el personaje "baile" o flote en pendientes

            playerVelocity.y = -2f; 

        }



        // 2. Captura de Inputs con el nuevo Input System

        float h = 0f;

        float v = 0f;



        if (Keyboard.current != null)

        {

            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) h = -1f;

            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) h = 1f;

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) v = 1f;

            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) v = -1f;

        }



        // 3. Calcular la dirección del movimiento orientada a la posición de la cámara

        Vector3 cameraForward = Vector3.Scale(followCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveDirection = (followCamera.transform.right * h + cameraForward * v).normalized;



        // 4. Detectar si corre con Shift izquierdo

        bool estaCorriendo = false;

        if (Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed)

        {

            estaCorriendo = true;

        }



        // Seleccionar velocidad final basada en el estado

        float velocidadActual = estaCorriendo ? runSpeed : walkSpeed;

        Vector3 movimientoFinal = moveDirection * velocidadActual;



        // 5. Control de Animaciones de movimiento y Rotación del personaje

        if (moveDirection != Vector3.zero)

        {

            if (estaCorriendo)

            {

                AnimaJugador.Run(); // Animación de Correr 🏃‍♀️

            }

            else

            {

                AnimaJugador.Walk(); // Animación de Caminar 🚶‍♀️

            }



            // Rotar suavemente al personaje hacia la dirección a la que se mueve

            Quaternion rotacionObjetivo = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, rotationSpeed * Time.deltaTime);

        }

        else

        {

            AnimaJugador.Idle(); // Animación de estar quieto 🧍‍♀️

        }



        // 6. Lógica de Salto (Solo si está en el suelo)

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)

        {

            // Fórmula física matemática exacta para la altura del salto basada en la gravedad

            playerVelocity.y = Mathf.Sqrt(jumpForce * -2.0f * gravity);

            AnimaJugador.Jump(); 

        }



        // 7. Aplicar Gravedad de forma independiente (Acumulativa en el eje Y)

        playerVelocity.y += gravity * Time.deltaTime;



        // 8. Separación de DeltaTime para evitar el efecto de flotación constante

        Vector3 desplazamientoFinal = movimientoFinal * Time.deltaTime; // Aplica deltaTime a X y Z

        desplazamientoFinal.y = playerVelocity.y * Time.deltaTime;       // Aplica su propio deltaTime a Y



        // 9. Ejecutar el movimiento en el CharacterController

        controller.Move(desplazamientoFinal);

    }



    public void ActivarVictoria()

    {

        AnimaJugador.Victory();

        this.enabled = false; // Desactiva el script para que el jugador no pueda moverse tras ganar

    }



    public void ActivarMuerte()

    {

        AnimaJugador.Die();

        this.enabled = false; // Desactiva el script para que el jugador no se mueva al perder

    }

}