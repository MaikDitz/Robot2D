using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta2aim : MonoBehaviour
{

    Transform player;

    [SerializeField] GameObject beam;
    [SerializeField] Transform cannon;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Robot").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 destPos = transform.position - transform.TransformDirection(10, 0, 0);
        //Debug.DrawLine(transform.position,  destPos, Color.blue);
        Vector3 aimVector = player.position + new Vector3(0f, 1.9f, 0f);
        Vector3 dir = aimVector - transform.position;
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        //AUTOR: JORGE SÁNCHEZ
    }

    void Disparar()
    {
        Instantiate(beam, cannon);
    }

    public void ActivarTorreta()
    {
        animator.SetBool("Disparando", true);
    }

    public void DesactivarTorreta()
    {
        animator.SetBool("Disparando", false);
    }
}
