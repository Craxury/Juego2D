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
    public int maxLife;
    private int scorePlayer;

    [Space]

    public Vector3[] Checkpoint;

    public Vector3 lastPosition;

    private bool saltos;
    private bool vulnerable;

    private ControlHud controlHud;
    private Rigidbody2D phisicsPlayer;
    private Animator anim;
    private SpriteRenderer spritePlayer;
    


    void Start()
    {
        phisicsPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();
        vulnerable = true;
        controlHud = GetComponent<ControlHud>();
        gameObject.transform.position = Checkpoint[PlayerPrefs.GetInt("checkpoint")];
        scorePlayer = 0;
        numLife = maxLife;
    }

    void Update()
    {
        Movement();
        Jump();
        Checkpoints();
        TouchFloor();
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
        if (Input.GetKeyDown(KeyCode.Space) && TouchFloor())
        {
            phisicsPlayer.velocity = new Vector2(phisicsPlayer.velocity.x, 0);
            phisicsPlayer.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    private bool TouchFloor()
    {
        RaycastHit2D touch = Physics2D.Raycast(transform.position + new Vector3(0, -2f, 0), Vector2.down, 0.2f);
        if(touch.collider != null && touch.collider.gameObject.CompareTag("Floor"))
        {
            lastPosition=gameObject.transform.position;
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

    public void AddScore(int score)
    {
        scorePlayer += score;
        controlHud.setTextScore(scorePlayer);
    }

    public void AddLife(int life)
    {
        maxLife += life;
    }

    private void Checkpoints()
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
