using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour
{
    public float velX = 5f;
    private float velN = 5f;
    private float dash = 7f;
    public int JumpForce;
    
    [Space]

    public int numLife;

    public int numLifeMax;
    private int scoreGeneral;
    public int numKills;

    [Space]

    public Vector3[] Checkpoint;

    public Vector3 lastPosition;

    private bool saltos;
    private bool vulnerable;

    private ControlHud controlHud;
    private Rigidbody2D phisicsPlayer;
    public Canvas HUD;
    private Animator anim;
    private SpriteRenderer spritePlayer;
    public GameObject balas;
    public GameObject Magic;
    public int magicAmmount;
    private Ammo amunnition;
    public float lastTimeShoot;

    public HPBar barraVida;
    public HPRecovery barraRecovery;
    private int nivelVida;


    


    void Start()
    {
        phisicsPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();
        amunnition = balas.GetComponent<Ammo>();
        vulnerable = true;
        controlHud = HUD.GetComponent<ControlHud>();
        //gameObject.transform.position = Checkpoint[PlayerPrefs.GetInt("checkpoint")];
        scoreGeneral = 0;
        numLife = 100;
        numLifeMax = numLife;
        numKills = 15;
        nivelVida = 1;
        Debug.Log("StartnumLife: " + numLife);
        Debug.Log("StartnumLifeMax: " + numLifeMax);
    }

    void Update()
    {
        Movement();
        Jump();
        Checkpoints();
        TouchFloor();
        AddLife();
        if (Time.time - lastTimeShoot >= 0.5f)
        { Shooting();}
    }

    private void Movement()
    {
        float InputX = Input.GetAxis("Horizontal");
        phisicsPlayer.velocity = new Vector2(InputX * velX, phisicsPlayer.velocity.y);
        anim.SetFloat("velX",phisicsPlayer.velocity.magnitude);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velX = dash;
        }
        else{ velX = velN;}

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

    private void Shooting()
    {
        if(TouchFloor() && Input.GetKeyDown(KeyCode.Z) && magicAmmount != 0)
        {
            //anim.Play("Shoot");
            lastTimeShoot = Time.time;
            magicAmmount--;
            amunnition.useMagic(magicAmmount);
            if (spritePlayer.flipX == false)
            {
                Instantiate(Magic, transform.position + new Vector3 (-3f, 0, 0), quaternion.identity);
            }
            else 
            {
                Instantiate(Magic, transform.position + new Vector3(2f, 0, 0), quaternion.identity);
            }
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

    public void TakeLife(int damage)
    {
        if (vulnerable)
        {
            //control de la vida
            numLife -= damage;
            //control de la barra de vida
            barraVida.minusLife(numLife);
            vulnerable = false;
            spritePlayer.color = Color.red;
            if (numLife <= 0)
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
        scoreGeneral += score;
        controlHud.setTextScore(scoreGeneral);
    }

    public void AddMaxLife(int addLife)
    {
        numLifeMax += addLife;
        nivelVida++;
        barraVida.setMaxLife(numLifeMax);
        numLife = numLifeMax;
    }

    public void AddLife()
    {
        if( numKills > 15)
        {
            numKills = 15;
        }
        
        if (Input.GetKeyDown(KeyCode.H) && numLife < numLifeMax && numKills >= 5)
        {
            numLife += 25 + nivelVida * 3;
            barraVida.addLife(25 + nivelVida * 3);
            barraRecovery.minusRecovery(5);
            numKills = numKills - 5;
            Debug.Log("numLife: " + numLife);
            Debug.Log("numKills: " + numKills);
        }
        else if(numLife > numLifeMax)
        {
            numLife = numLifeMax;
        }
        else{}
        
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
