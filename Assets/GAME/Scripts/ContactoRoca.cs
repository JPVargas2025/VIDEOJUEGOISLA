using UnityEngine;
using System.Collections;

public class ContactoRoca : MonoBehaviour
{
  
    public GameObject imagenSorpresa; 

   
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MostrarImagenPorCincoSegundos());
        }
    }

    IEnumerator MostrarImagenPorCincoSegundos()
    {
        imagenSorpresa.SetActive(true);

        yield return new WaitForSeconds(5f); 

        imagenSorpresa.SetActive(false);
    }
}