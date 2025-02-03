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


    //public GameObject numMagicExtra;
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < 5; i++)
            {
                balas[i].SetActive(true);
            }
            player.magicAmmount = 5;
        }
        
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
