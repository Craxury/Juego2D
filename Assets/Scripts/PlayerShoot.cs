using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private GameObject player;
    public float speedMagic;
    private int moveForward;
    private Animator anim;
    public float lifeTimeMagic;
    private float startTime;
    public int weapon;
    public int lvlweapon;

    private Rigidbody2D phisicsMagic;
    private SpriteRenderer spriteMagic;
    public ControlEnemy TakeDamage;
    // Start is called before the first frame update
    void Awake()
    {
        startTime = Time.time;
        moveForward = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        spriteMagic = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (player.GetComponent<SpriteRenderer>().flipX == false)
        { moveForward = -1; }
        else { moveForward = 1; }
        lvlweapon = 5;
        weapon = lvlweapon;

        if(player.GetComponent<SpriteRenderer>().flipX == false)
        {
            spriteMagic.flipX = false;
        }
        else
        {
            spriteMagic.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckLifeTime();
        Vector3 posDestiny = new Vector3 (1000 * moveForward, transform.position.y, 0);
        transform.position = Vector3.MoveTowards (transform.position, posDestiny, speedMagic * Time.deltaTime);
        weapon = lvlweapon;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            StartCoroutine("EndMagic");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<ControlEnemy>().TakeDamage(weapon);
            StartCoroutine("EndMagic");
        }
    }

    IEnumerator EndMagic()
    {
        speedMagic = 0;
        anim.SetTrigger("Explo");
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
        StopCoroutine("EndMagic");
    }

    private void CheckLifeTime()
    {
        if (Time.time - startTime > lifeTimeMagic)
        {
            StartCoroutine("EndMagic");
        }
    }
}
