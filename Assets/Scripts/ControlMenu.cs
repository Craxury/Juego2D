using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlMenu : MonoBehaviour
{
     private AudioSource audioSource;
     public AudioClip buttonSound;
     public GameObject MenuPlay;
        public void Start()
    {
     audioSource=GetComponent<AudioSource>();
    }
        public void OnClickPlay()
    {
        CargarEscena();
    }
        public void CargarEscena()
    {
        SceneManager.LoadScene("Juego");

    }
        public void OnClickExit()
    {
        Application.Quit();
    }
        public void OnClickCreddit()
    {
        soundEffect();
        Invoke("LoadCredits",0.6f);
    }
        public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
        public void OnclickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
        public void soundEffect()
    {
        audioSource.PlayOneShot(buttonSound);
    }
        public void setMenuActive()
    {
        Time.timeScale=1f;
        MenuPlay.SetActive(true);
    }

        public void OnClickRestart()
    {
        Time.timeScale=1f;
        CargarEscena();
    }
}
