using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrap : MonoBehaviour
{
    private int damage;
    void Start()
    {
        damage = -20;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MovementPlayer>().TakeLife(damage);
        }
        collision.gameObject.transform.position = collision.gameObject.GetComponent<MovementPlayer>().lastPosition;
    }
}
