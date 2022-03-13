using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa1 : MonoBehaviour
{
    //Array que contendrá todas las torretas
    GameObject[] torretas;
    private void Start()
    {
        //Metemois en nuestro array todas las torretas que tengan el tag
        torretas = GameObject.FindGameObjectsWithTag("Torreta1");
        print(torretas.Length);
    }

    //Si el Robot pisa la trampa
    private void OnTriggerEnter2D(Collider2D other)
    {


        if(other.gameObject.name == "Robot")
        {
            //Activar torretas
            foreach (GameObject torreta in torretas) 
            {
                torreta.SendMessage("Activar");
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Robot")
        {
            foreach (GameObject torreta in torretas)
            {
                torreta.SendMessage("Desactivar");
            }
        }

    }
}
