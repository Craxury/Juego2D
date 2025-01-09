using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    public int score;
    public int addLife;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
                if(this.gameObject.CompareTag("Score"))
            {
                collision.gameObject.GetComponent<MovementPlayer>().AddScore(score);
            }

            if(this.gameObject.CompareTag("Vida"))
            {
                collision.gameObject.GetComponent<MovementPlayer>().AddMaxLife(addLife);
            }
            Destroy(this.gameObject);
        }
    }
}
