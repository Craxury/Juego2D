using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour
{
    public float velX = 5f;
    public float velN = 5f;
    private float dash = 7f;
    public float velHeal = 0f;
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
    private PlayerShoot balas2;

    public HPBar barraVida;
    public HPRecovery barraRecovery;
    private int nivelVida;
    public bool powerB;
    private int ataque;
    private bool isattacking;
    private bool ishealing;
    private bool isgrounded;
    private bool ismoving;

    public AudioSource source;

    public AudioClip Attack;
    public AudioClip Heal;
    public AudioClip Steps;
    public AudioClip Upgradehp;
    public AudioClip hit;
    public AudioClip Coins;


    


    void Start()
    {
        source = GetComponent<AudioSource>();
        phisicsPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();
        amunnition = balas.GetComponent<Ammo>();
        vulnerable = true;
        controlHud = HUD.GetComponent<ControlHud>();
        scoreGeneral = 0;
        numLife = 100;
        numLifeMax = numLife;
        numKills = 15;
        nivelVida = 1;
        powerB = true;
    }

    void Update()
    {
        Movement();
        Jump();
        Checkpoints();
        TouchFloor();
        AddLife();
        Power();
        if (Time.time - lastTimeShoot >= 0.5f)
        { Shooting();}
        if(isgrounded == true)
        {
            source.enabled = true;
        }
    }

    private void Movement()
    {
        float InputX = Input.GetAxis("Horizontal");
        phisicsPlayer.velocity = new Vector2(InputX * velX, phisicsPlayer.velocity.y);
        anim.SetFloat("velX",phisicsPlayer.velocity.magnitude);

        if (powerB == true)
        {
            
            if (Input.GetKey(KeyCode.LeftShift) && ishealing == false && isattacking == false && amunnition.isreloading == false)
            {
                velX = dash;
                if(isgrounded == true && ismoving == false && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) 
                {
                    StartCoroutine(Sounds());
                }
                else{}
            }
            else if (ishealing == true || isattacking == true || amunnition.isreloading == true)
            {
                velX =  velHeal;
            }
            else
            { 
                velX = velN;
                    if(isgrounded == true && ismoving == false && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) 
                {
                    StartCoroutine(Sounds());
                }
                else{}
            }
            
        }
        else if (ishealing == true || isattacking == true || amunnition.isreloading == true)
        {
            velX =  velHeal;
        }
        else if (powerB == false)
        {
            velX = dash + 1;
        }

        IEnumerator Sounds()
        {
            ismoving = true;
            source.PlayOneShot(Steps);
            yield return new WaitForSeconds(0.3f);
            ismoving = false;
            StopCoroutine(Sounds());
        }
        

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
            StartCoroutine("JumpPlayer");
        }
    }

    IEnumerator JumpPlayer()
    {
        phisicsPlayer.velocity = new Vector2(phisicsPlayer.velocity.x, 0);
        phisicsPlayer.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2f);
        StopCoroutine("JumpPlayer");
    }

    private void Shooting()
    {
        if(TouchFloor() && Input.GetKeyDown(KeyCode.Z) && magicAmmount != 0 && isattacking == false)
        {
            StartCoroutine("Attacking");
        }
    }

    IEnumerator Attacking()
    {
        isattacking = true;
        anim.SetBool("AttackBool", true);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.65f);
        source.PlayOneShot(Attack);
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
        anim.SetBool("AttackBool", false);
        isattacking = false;
        StopCoroutine("Attacking");
    }
    public bool TouchFloor()
    {
        RaycastHit2D touch = Physics2D.Raycast(transform.position + new Vector3(0, -2f, 0), Vector2.down, 0.2f);
        if(touch.collider != null && touch.collider.gameObject.CompareTag("Floor"))
        {
            anim.SetBool("Floor", true);
            lastPosition=gameObject.transform.position;
            isgrounded =  true;
            return true;
        }
        else
        {
            anim.SetBool("Floor", false);
            isgrounded = false;
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
            source.PlayOneShot(hit);
            if (numLife <= 0)
            {
                StartCoroutine(Dying());
            }
            Invoke("MakeVulnerable", 0.5f);
        }
    }

    IEnumerator Dying()
    {
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(0.4f);
        HUD.GetComponent<controlMenu>().MenuPlay.SetActive(true);
        Time.timeScale = 0;
        StopCoroutine(Dying());
    }


    private void MakeVulnerable()
    {
        vulnerable = true;
        spritePlayer.color = Color.white;
    }

    public void AddScore(int score)
    {
        scoreGeneral += score;
        source.PlayOneShot(Coins);
        controlHud.setTextScore(scoreGeneral);
    }

    public void AddMaxLife(int addLife)
    {
        numLifeMax += addLife;
        nivelVida++;
        barraVida.setMaxLife(numLifeMax);
        numLife = numLifeMax;
        source.PlayOneShot(Upgradehp);
    }

    public void AddLife()
    {
        if( numKills > 15)
        {
            numKills = 15;
        }
        
        if (Input.GetKeyDown(KeyCode.H) && numLife < numLifeMax && numKills >= 5 && ishealing == false)
        {
            StartCoroutine("Healing");
        }
        else if(numLife > numLifeMax)
        {
            numLife = numLifeMax;
        }
        else{}
        
    }

    IEnumerator Healing()
    {
        ishealing = true;
        anim.SetTrigger("Heal");
        anim.SetBool("healing", true);
        numLife += 25 + nivelVida * 3;
        barraVida.addLife(25 + nivelVida * 3);
        barraRecovery.minusRecovery(5);
        numKills = numKills - 5;
        source.PlayOneShot(Heal);
        yield return new WaitForSeconds(1.1f);
        anim.SetBool("healing", false);
        ishealing = false;
        StopCoroutine("Healing");
    }

    public void Power()
    {
        if (Input.GetKeyDown(KeyCode.C) && powerB == true)
        {
            powerB = false;
            StartCoroutine(PowerBoss());
        }
    }

    IEnumerator PowerBoss()
    {
        numLife += 25 + nivelVida * 10;
        barraVida.addLife(25 + nivelVida * 10);
        if (isattacking == true)
        {
            velX = velHeal;
        }
        else if (isattacking == false)
        {
            velX = dash + 1;
        }
        yield return new WaitForSeconds(7f);
        velX = velN;
        yield return new WaitForSeconds(15f);
        StopCoroutine(PowerBoss());
        powerB = true;
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
