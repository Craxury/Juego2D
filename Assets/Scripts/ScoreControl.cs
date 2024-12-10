using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    public int score;
    public int life;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MovementPlayer>().AddScore(score);
            collision.gameObject.GetComponent<MovementPlayer>().AddLife(life);
        }
    }
}
