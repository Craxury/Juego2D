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
    //public GameObject numMagicExtra;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in balas)
        {
            item.SetActive(true);
        }
    }

    /*public void getMagic(int num)
    {
        for (int i = 0, x = 0; i < 5 && x < num; i++)
        {
            if(balas[i].activeSelf == false)
            {
                balas[i].SetActive(true);
                x++;
            }
        }
    }*/
    

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
                //numMagicExtra.gameObject.SetActive(false);
            }
        }
        else 
        {
            for (int i = 0; i < 4; i++)
            {
                balas[i].SetActive(false);
            }
            //numMagicExtra.SetActive(true);
            //numMagicExtra.GetComponent<TextMeshProUGUI>().text = num + "X";
        }
    }
}
