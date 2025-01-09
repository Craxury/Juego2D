using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlHud : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textLife;

    public GameObject death;
    public GameObject textLost;



    public void setTextScore(int numScore)
    {
        textScore.text = "Score: " + numScore.ToString();
    }

    public void setImageLost()
    {
        Time.timeScale = 0f;
        death.SetActive(true);
        textLost.SetActive(true);
    }
}

