using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPRecovery : MonoBehaviour
{
    private Slider sliderRecovery;
    public Color color;
    public Color color2;
    public Image image;

    void Start()
    {
        sliderRecovery = GetComponent<Slider>();
        color.a = 1;
        color2.a = 0;
    }

    void Update()
    {
        barraMax();
    }

    public void addLife(int addRecovery)
    {
        sliderRecovery.value += addRecovery;
    }

    public void minusRecovery(int Recovery)
    {
        sliderRecovery.value = sliderRecovery.value - Recovery;
    }

    public void setMaxLife(int maxRecovery)
    {
        sliderRecovery.maxValue = maxRecovery;
        sliderRecovery.value = maxRecovery;
    }

    private void barraMax()
    {
        if (sliderRecovery.value == sliderRecovery.maxValue)
        {
            image.color = color;
            color2.a = 0;
        }
        else
        {
            image.color = color2;
            color2.a = 1;
        }
    }
}
