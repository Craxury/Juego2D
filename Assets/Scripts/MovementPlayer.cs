using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float velX;
    public int JumpForce;
    
    [Space]

    public int numLife;

    [Space]

    public Vector3[] Checkpoint;

    private bool saltos;
    private bool vulnerable;


    private Rigidbody2D phisicsPlayer;
    private Animator anim;
    private SpriteRenderer spritePlayer;
    


    void Start()
    {
        phisicsPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();
        vulnerable = true;
        gameObject.transform.position = Checkpoint[PlayerPrefs.GetInt("checkpoint")];
    }

    void Update()
    {
        Movement();
        Jump();
        Checkpoints();
    }

    private void Movement()
    {
        float InputX = Input.GetAxis("Horizontal");
        phisicsPlayer.velocity = new Vector2(InputX * velX, phisicsPlayer.velocity.y);
        anim.SetFloat("velX",phisicsPlayer.velocity.magnitude);

        if(phisicsPlayer.velocity.x < -0f)
        {
            spritePlayer.flipX = false;
        }
        else if (phisicsPlayer.velocity.x > 0f)
        {
            spritePlayer.flipX = true;
        }
    }

    private void Jump()
    {
        Debug.Log("h");
        if (Input.GetKeyDown(KeyCode.Space) && TouchFloor())
        {
            Debug.Log("h2");
            phisicsPlayer.velocity = new Vector2(phisicsPlayer.velocity.x, 0);
            phisicsPlayer.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    private bool TouchFloor()
    {
        Debug.Log("h3");
        RaycastHit2D touch = Physics2D.Raycast(transform.position + new Vector3(0, -2f, 0), Vector2.down, 0.2f);
        Debug.Log(touch.collider.gameObject.CompareTag("Player"));
        if(touch.collider.gameObject.CompareTag("Floor"))
        {
            return true;
        }
        else
        {
            Debug.Log("nada");
            return false;
        }
    }

    public void TakeLife()
    {
        if (vulnerable)
        {
            numLife--;
            vulnerable = false;
            spritePlayer.color = Color.red;
            if (numLife == 0)
            {
                Destroy(gameObject);
            }
            Invoke("MakeVulnerable", 1f);
        }
    }

    private void MakeVulnerable()
    {
        vulnerable = true;
        spritePlayer.color = Color.white;
    }

    public void Checkpoints()
    {
        if (PlayerPrefs.GetInt("chekpoint") != 0)
        {
            if (gameObject.transform.position.x <= Checkpoint[PlayerPrefs.GetInt("checkpoint")].x)
            {
                PlayerPrefs.SetInt("checkpoint", PlayerPrefs.GetInt("checkpoint") -1);
            }
        }
        if (PlayerPrefs.GetInt("checkpoint") < Checkpoint.Length -1)
        {
            if(gameObject.transform.position.x > Checkpoint[PlayerPrefs.GetInt("checkpoint")+1].x)
            {
                PlayerPrefs.SetInt("checkpoint", PlayerPrefs.GetInt("checkpoint")+1);
            }
        }
    }
}
