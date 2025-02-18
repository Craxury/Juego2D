using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        soundEffect();
        StartCoroutine(Wait());
        CargarEscena();
    }
        public void CargarEscena()
    {
        SceneManager.LoadScene("Juego");
        Time.timeScale=1f;
    }
        public void OnClickExit()
    {
        soundEffect();
        StartCoroutine(Wait());
        Application.Quit();
    }
        public void OnClickCreddit()
    {
        soundEffect();
        StartCoroutine(Wait());
        Invoke("LoadCredits",0.6f);
    }
        public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
        public void OnclickMenu()
    {
        soundEffect();
        StartCoroutine(Wait());
        SceneManager.LoadScene("Menu");
        Time.timeScale=1f;
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
        soundEffect();
        StartCoroutine(Wait());
        Time.timeScale=1f;
        CargarEscena();
    }

    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale=1f;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        StopCoroutine(Wait());
    }
}
