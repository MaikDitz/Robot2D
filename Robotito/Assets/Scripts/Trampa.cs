using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa : MonoBehaviour
{
    [SerializeField] string torretaTag;
    [SerializeField] GameObject[] torretas;

    // Start is called before the first frame update
    void Start()
    {
        torretas = GameObject.FindGameObjectsWithTag(torretaTag);
        print(torretas.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Me han pisao");
        foreach(GameObject torreta in torretas)
        {
            Torreta2aim torreta2Aim = torreta.GetComponent<Torreta2aim>();
            torreta2Aim.SendMessage("ActivarTorreta");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject torreta in torretas)
        {
            Torreta2aim torreta2Aim = torreta.GetComponent<Torreta2aim>();
            torreta2Aim.SendMessage("DesactivarTorreta");
        }
    }
}
