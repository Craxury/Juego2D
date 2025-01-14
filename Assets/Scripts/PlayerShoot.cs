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
    // Start is called before the first frame update
    void Awake()
    {
        startTime = Time.time;
        //anim = gameObject.GetComponent<Animator>();
        moveForward = 1;
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<SpriteRenderer>().flipX == false)
        { moveForward = -1; }
        else { moveForward = 1; }
    }

    // Update is called once per frame
    void Update()
    {
        CheckLifeTime();
        Vector3 posDestiny = new Vector3 (1000 * moveForward, transform.position.y, 0);
        transform.position = Vector3.MoveTowards (transform.position, posDestiny, speedMagic * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            EndMagic();
            Invoke("ExplodeMagic", 0.3f);
        }
    }

    public void EndMagic()
    {
        speedMagic = 0;
        //anim.Play("impacto");
    }

    private void ExplodeMagic()
    {
        Destroy(this.gameObject);
    }

    private void CheckLifeTime()
    {
        if (Time.time - startTime > lifeTimeMagic)
        {
            Destroy(this.gameObject);
        }
    }
}
