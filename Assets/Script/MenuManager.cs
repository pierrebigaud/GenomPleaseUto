using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public AudioClip mainTheme;
    public GameObject titlePanel;
    public GameObject gamePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    private AudioSource audioSource;

    private bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0; 
        titlePanel.SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape)){
          Pause();
       }
    }

    public void Play(){
        titlePanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1;
        audioSource.clip = mainTheme;
        audioSource.Play();
    }

    public void Pause(){
        Time.timeScale = 0;
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ResumeGame(){
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnTitle(){
        Time.timeScale = 0;
        SceneManager.LoadScene("ScenePierre");
        pausePanel.SetActive(false);
    }

    public void ReloadGame(){
        titlePanel.SetActive(false);
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("ScenePierre");
        Time.timeScale = 1;
    }
    public void QuitGame(){
        Application.Quit();
    }


}