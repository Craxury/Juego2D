using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
public class Ammo : MonoBehaviour
{
    public GameObject[] balas;
    private MovementPlayer player;
    private Animator anim;
    public bool isreloading;


    //public GameObject numMagicExtra;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementPlayer>();
        foreach (var item in balas)
        {
            item.SetActive(true);
        }
    }

    void Update()
    {
        
        getMagic();

        
    }

    public void getMagic()
    {
        if (Input.GetKeyDown(KeyCode.R) && player.GetComponent<MovementPlayer>().TouchFloor())
        {
            StartCoroutine(Reload());
        }
        
    }

    IEnumerator Reload()
    {
        for (int i = 0; i < 5; i++)
            {
                anim.SetBool("Reload", true);
                balas[i].SetActive(true);
            }
            isreloading = true;
            player.magicAmmount = 5;
            player.velX = player.velHeal;
            yield return new WaitForSeconds(0.5f);
            isreloading = false;
            anim.SetBool("Reload", false);
            player.velX = player.velN;
        
        StopCoroutine(Reload());
    }
    

    public void useMagic(int num)
    {
        if(num < 5)
        {
            balas[num].SetActive(false);
        }
        else if (num == 5)
        {
            foreach(var item in balas)
            {
                item.SetActive(true);
            }
        }
        else 
        {
            for (int i = 0; i < 4; i++)
            {
                balas[i].SetActive(false);
            }
        }
    }
}
