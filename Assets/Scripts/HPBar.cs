using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{

    private Slider sliderLife;

    void Start()
    {
        sliderLife = GetComponent<Slider>();
    }

    public void addLife(int addLife)
    {
        sliderLife.value += addLife;
    }

    public void minusLife(int takeDamage)
    {
        sliderLife.value -= takeDamage;
    }

    public void setMaxLife(int AddMaxLife)
    {
        sliderLife.maxValue += AddMaxLife;
    }
}
