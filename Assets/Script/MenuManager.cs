using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public AudioClip mainTheme;
    public AudioClip gameOverTheme;
    public GameObject titlePanel;
    public GameObject gamePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject _GameManager;
    private GameManager script;

    private AudioSource audioSource;

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
       script = _GameManager.GetComponent<GameManager>();
        if(script.gameOver){
            Time.timeScale = 0; 
            gamePanel.SetActive(false);
            gameOverPanel.SetActive(true);
            audioSource.clip = gameOverTheme;
            audioSource.Play();
        }

    }

    public void Play(){
        titlePanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
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