using UnityEngine;

public class AnimaJugador : MonoBehaviour 
{     
    private static Animator animator;

    void Awake()     
    {         
        animator = GetComponent<Animator>();
    } 

    public static void Idle()     
    {         
        if (animator != null) 
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", false); // Apagamos correr
        }
    } 

    public static void Walk()
    {         
        if (animator != null) 
        {
            animator.SetBool("walk", true);
            animator.SetBool("run", false); // Caminar no es correr
        }
    } 

    public static void Run() // 🌟 ¡NUEVA FUNCIÓN!
    {         
        if (animator != null) 
        {
            animator.SetBool("walk", true); // Correr también cuenta como moverse
            animator.SetBool("run", true);  // Activamos correr
        }
    } 

    public static void Jump()     
    {         
        if (animator != null) animator.SetTrigger("jump");
    }

    public static void Victory()     
    {         
        if (animator != null) animator.SetTrigger("victory"); 
    }

    public static void Die()     
    {         
        if (animator != null) animator.SetTrigger("die");
    }
}