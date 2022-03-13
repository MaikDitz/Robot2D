using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamMove : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;

        Invoke("Autodestruir", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Robot")
        {
            collision.gameObject.SendMessage("Morir");
        }

        //Destruyo la bala, independientemente de con quién ha chocado
        Destroy(gameObject);
    }

    void Autodestruir()
    {
        Destroy(gameObject);
    }
}
