using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RobotManager : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;
    BoxCollider2D bc;

    float speed; //Velocidad enb X a la que me muevo
    [SerializeField] float maxSpeed; //velocidad de desplazamiento máxima

    float desplX;

    float jumpForce;

    bool alive = true;

    bool facingRight = true;

    //PowerUp para revivir
    bool revivir;

    bool queMeMato;
    float velocidadCaida;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        maxSpeed = 4f;
        jumpForce = 10f;

        revivir = false;

        queMeMato = false;


        //Ajusto el BoxCollider de inicio
        bc = GetComponent<BoxCollider2D>();
        bc.offset = new Vector2(0.04f, 1.24f);
        bc.size = new Vector2(1.2f, 2.23f);
    }

    // Update is called once per frame
    void Update()
    {
        desplX = Input.GetAxis("Horizontal");
        if (alive)
        {
            Girar();

            Saltar();

            Crouch();

            Correr();

            Roll();

            UpdateCollider();

            if (rb.velocity.y < -10)
            {
                queMeMato = true;
                if (rb.velocity.y < 0)
                {
                    velocidadCaida = rb.velocity.y;
                }

            }
        }
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            Caminar();
        }

    }


    void UpdateCollider()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RobotCrouchIdle"))
        {
            bc.offset = new Vector2(0.41f, 0.9f);
            bc.size = new Vector2(1.46f, 1.54f);
        }
        else
        {
            bc.offset = new Vector2(0.04f, 1.24f);
            bc.size = new Vector2(1.2f, 2.23f);
        }
    }
    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && animator.GetCurrentAnimatorStateInfo(0).IsName("RobotWalk"))
        {
            print("rodando");
            Vector2 rollDir;
            animator.SetTrigger("Rodar");
            if (facingRight)
            {
                rollDir = new Vector2(7f, 0f);
            }
            else
            {
                rollDir = new Vector2(-7f, 0f);
            }

            rb.AddForce(rollDir, ForceMode2D.Impulse);
        }
    }

    void Caminar()
    {
        //print(animator.GetCurrentAnimatorStateInfo(0).IsName("RobotRoll"));
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("RobotRoll"))
        {
            rb.velocity = new Vector2(desplX * maxSpeed, rb.velocity.y);
        }
        speed = rb.velocity.x;
        speed = Mathf.Abs(speed);
        animator.SetFloat("SpeedX", speed);
        //print(speed);
    }

    void Girar()
    {
        if (desplX < 0 && facingRight)
        {
            Flip();
        }
        else if (desplX > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;

    }

    void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && animator.GetBool("IsGrounded"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Crouch", true);
            maxSpeed = 2f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Crouch", false);
            maxSpeed = 4f;
        }
    }

    void Correr()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //Cambio la velocidad
            maxSpeed = 7f;
            animator.SetBool("Running", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("Running", false);
            maxSpeed = 4f;
        }
    }

    public void Morir()
    {
        animator.SetTrigger("Morir");

        if (alive)
        {
            if (revivir)
            {
                Invoke("Revivir", 3f);
            }
            else
            {
                Invoke("Reiniciar", 3f);
            }
        }

        alive = false;




    }

    void Revivir()
    {
        revivir = false;
        animator.SetTrigger("Revivir");
        alive = true;
    }

    void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }

    //Control de suelo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            //print("Estoy tocando suelo");
            if (rb.velocity.y < 0.2f)
            {
                animator.SetBool("IsGrounded", true);
            }

            if (queMeMato)
            {
                Morir();
            }



        }

        else if (collision.gameObject.tag == "PowerUp")
        {
            revivir = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Plataforma")
        {
            transform.parent = collision.gameObject.transform;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            //print("NO Estoy tocando suelo");
            animator.SetBool("IsGrounded", false);
        }
        if (collision.gameObject.tag == "Plataforma")
        {
            transform.parent = null;
        }

    }
}
