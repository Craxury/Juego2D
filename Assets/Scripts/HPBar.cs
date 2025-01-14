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

    public void minusLife(int numLife)
    {
        sliderLife.value = numLife;
    }

    public void setMaxLife(int numMaxLife)
    {
        sliderLife.maxValue = numMaxLife;
        sliderLife.value = numMaxLife;
    }
}
