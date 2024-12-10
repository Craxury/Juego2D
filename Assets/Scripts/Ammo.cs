using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public GameObject[] magic;
    public GameObject numMagicExtra;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in magic)
        {
            item.SetActive(true);
        }
    }

    public void getMagic(int num)
    {
        if(num < 5)
        {
            magic[num].SetActive(false);
        }
        else if (num == 5)
        {
            foreach(var item in magic)
            {
                item.SetActive(true);
                numMagicExtra.gameObject.SetActive(false);
            }
        }
        else 
        {
            for (int i = 0; i < 4; i++)
            {
                magic[i].SetActive(false);
            }
            numMagicExtra.SetActive(true);
            numMagicExtra.GetComponent<TextMeshProUGUI>().text = num + "X";
        }
    }
}
