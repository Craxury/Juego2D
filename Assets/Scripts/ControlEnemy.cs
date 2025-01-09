using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemy : MonoBehaviour
{
    private GameObject magic;
    public float speedEnemy;

    [Space]

    private Vector3 posInitial;
    public Vector3 posMid;
    public Vector3 posEnd;
    private int moveToEnd;
    private Vector3 posDestiny;

    private Animator anim;

    [Space]

    public GameObject[] drop;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        posInitial = transform.position;
        moveToEnd = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveToEnd == 0)
        {
            posDestiny = posMid;
        }
        else if (moveToEnd == 1)
        {
            posDestiny = posEnd;
        }
        else
        {
            posDestiny = posInitial;
        }

        transform.position = Vector3.MoveTowards(transform.position, posDestiny, speedEnemy * Time.deltaTime);
        if(transform.position == posMid) moveToEnd = 1;
        if(transform.position == posEnd) moveToEnd = 2;
        if(transform.position == posInitial) moveToEnd = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MovementPlayer>().TakeLife(30);
        }
    }
}
