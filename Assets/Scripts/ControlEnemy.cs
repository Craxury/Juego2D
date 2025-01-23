using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlEnemy : MonoBehaviour
{
    private GameObject magic;
    public float velocity;

    [Space]

    private Vector3 posInitial;
    public Vector3 posMid;
    public Vector3 posEnd;
    private int moveToEnd;
    private Vector3 posDestiny;

    private Animator anim;

    public float HP;
    public float currentHP;
    public float armor;
    private bool dead;

    [Space]

    public GameObject[] drop;

    private SpriteRenderer sprite;
    private float lastPositionX;
    private PlayerShoot weapon;
    private bool vulnerable;
    private MovementPlayer player;
    private int killcount;
    public HPRecovery barraRecovery;
    private bool attack;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementPlayer>();
        sprite = GetComponent<SpriteRenderer>();
        weapon = GetComponent<PlayerShoot>();
        anim = gameObject.GetComponent<Animator>();
        posInitial = transform.position;
        moveToEnd = 0;
        currentHP = HP;
        dead = false;
        killcount = player.GetComponent<MovementPlayer>().numKills;

    }

    // Update is called once per frame
    void Update()
    {
        movementZone();
        flipX();
        anima(); 
        RayChecking();
    }

    public void RayChecking()
    {
        RaycastHit2D rayCheck;
        if (sprite.flipX == true)
        {
            rayCheck = Physics2D.Raycast(transform.position + new Vector3(1.5f, 0, 0), Vector2.right, 0.8f);
        }
        else
        {
            rayCheck = Physics2D.Raycast(transform.position + new Vector3(-1.5f, 0, 0), Vector2.left, 0.8f);   
        }

        if (rayCheck.collider != null && rayCheck.collider.gameObject.CompareTag("Player") && attack == false)
        {
            attack = true;
            StartCoroutine("Attack");
        }
    }

    public void movementZone()
    {
        if(moveToEnd == 0 && dead == false)
        {
            posDestiny = posMid;
        }
        else if (moveToEnd == 1 && dead == false)
        {
            posDestiny = posEnd;
        }
        else if (moveToEnd == 2 && dead == false)
        {
            posDestiny = posInitial;
        }


        transform.position = Vector3.MoveTowards(transform.position, posDestiny, velocity * Time.deltaTime);
        if(transform.position == posMid) moveToEnd = 1;
        if(transform.position == posEnd) moveToEnd = 2;
        if(transform.position == posInitial) moveToEnd = 0;

    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage / armor;
        vulnerable = false;
        sprite.color = Color.red;
            if (currentHP <= 0)
        {
            StopAllCoroutines();
            StartCoroutine("Dying");
        }
        StartCoroutine("Movement");
    }

    IEnumerator Dying()
    {
        dead = true;
        posDestiny = transform.position;
        velocity = 0;
        sprite.color = Color.white;
        vulnerable = true;
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(1f);
        velocity = 0;
        player.numKills++;
        barraRecovery.addLife(1);
        Destroy(gameObject);
    }

    IEnumerator Movement()
    {
        vulnerable = true;
        velocity = 0;
        yield return new WaitForSeconds(0.5f);
        velocity = 5;
        sprite.color = Color.white;
    }

    private void anima()
    {
        anim.SetFloat("Velocity",velocity);
    }
    private void flipX()
    {
        if (lastPositionX - transform.position.x > 0f)
        {
            sprite.flipX = false;
        }
        else if (lastPositionX - transform.position.x < 0f)
        {
            sprite.flipX = true;
        }
        lastPositionX = transform.position.x;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && dead == false)
        {
            collision.gameObject.GetComponent<MovementPlayer>().TakeLife(10);
        }
    }

    private void CreateRay()
    {
        RaycastHit2D rayCheck;
        if (sprite.flipX == true)
        {
            rayCheck = Physics2D.Raycast(transform.position + new Vector3(1.5f, 0, 0), Vector2.right, 0.8f);
        }
        else
        {
            rayCheck = Physics2D.Raycast(transform.position + new Vector3(-1.5f, 0, 0), Vector2.left, 0.8f);   
        }

        if (rayCheck.collider != null && rayCheck.collider.gameObject.CompareTag("Player"))
        {
            rayCheck.collider.gameObject.GetComponent<MovementPlayer>().TakeLife(30);
        }
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        velocity = 0;
        yield return new WaitForSeconds(1.2f);
        velocity = 5;
        attack = false;
        StopCoroutine("Attack");
    }
}
