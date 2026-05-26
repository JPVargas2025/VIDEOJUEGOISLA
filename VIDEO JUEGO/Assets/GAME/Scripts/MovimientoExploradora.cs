using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(CharacterController))]
public class MovimientoExploradora : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float walkSpeed = 5.0f; // Velocidad al caminar
    public float runSpeed = 9.0f;  // Velocidad al correr 🌟
    public float rotationSpeed = 10.0f;
    public float jumpForce = 5.0f; 
    public float gravity = -15.0f; 

    [Header("Configuración de Cámara")]
    public Camera followCamera; 

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (followCamera == null && Camera.main != null)
        {
            followCamera = Camera.main;
        }
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; 
        }

        float h = 0f;
        float v = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) h = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) h = 1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) v = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) v = -1f;
        }

        Vector3 cameraForward = Vector3.Scale(followCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirection = (followCamera.transform.right * h + cameraForward * v).normalized;

        // 🌟 DETECTAR SI CORRE CON SHIFT 🌟
        bool estaCorriendo = false;
        if (Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed)
        {
            estaCorriendo = true;
        }

        // Elegimos la velocidad según si está corriendo
        float velocidadActual = estaCorriendo ? runSpeed : walkSpeed;
        Vector3 movimientoFinal = moveDirection * velocidadActual;

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

            Quaternion rotacionObjetivo = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, rotationSpeed * Time.deltaTime);
        }
        else
        {
            AnimaJugador.Idle(); // Quieto 🧍‍♀️
        }

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2.0f * gravity);
            AnimaJugador.Jump(); 
        }

        playerVelocity.y += gravity * Time.deltaTime;
        movimientoFinal.y = playerVelocity.y;

        controller.Move(movimientoFinal * Time.deltaTime);
    }

    public void ActivarVictoria()
    {
        AnimaJugador.Victory();
        this.enabled = false; 
    }

    public void ActivarMuerte()
    {
        AnimaJugador.Die();
        this.enabled = false; 
    }
}