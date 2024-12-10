using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlHud : MonoBehaviour
{
    public TextMeshProUGUI textScore;

    public GameObject death;
    public GameObject textLost;

    public void setTextScore(int numScore)
    {
        textScore.text = numScore.ToString();
    }

    public void setImageLost()
    {
        Time.timeScale = 0f;
        death.SetActive(true);
        textLost.SetActive(true);
    }
}

