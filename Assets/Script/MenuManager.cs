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
    public GameObject _MagifyinGlass;
    public GameObject _BlackScreen;

    private GameManager script;
    private MagnifyingGlassScript scriptMagnifying;

    private AudioSource audioSource;
    private bool playAudio = true;
    private bool showGlass = false;

    // Start is called before the first frame update
    void Start()
    {
        scriptMagnifying = _MagifyinGlass.GetComponent<MagnifyingGlassScript>();
        Time.timeScale = 0; 
        titlePanel.SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape) ){
           if(gamePanel.activeInHierarchy  == true){
               Pause();
            } else if(pausePanel.activeInHierarchy  == true){
               ResumeGame();
            } 
       }
      
     
       script = _GameManager.GetComponent<GameManager>();
        if(script.gameOver){
            hideMagnifyingGlass();
            gamePanel.SetActive(false);
            if(playAudio){
                audioSource.clip = null;
                audioSource.PlayOneShot(gameOverTheme);
                playAudio = false;
            }
           
            StartCoroutine(ReturnTitleAfterTime());
            gameOverPanel.SetActive(true);
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

    public void hideMagnifyingGlass(){
        showGlass = true;
        _MagifyinGlass.SetActive(false);
        _BlackScreen.SetActive(false);        
    }

    public void Pause(){       
        hideMagnifyingGlass();
        Time.timeScale = 0;
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ResumeGame(){
        if(showGlass){
            showGlass = false;
            _MagifyinGlass.SetActive(true);
            _BlackScreen.SetActive(true);
        }
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnTitle(){
        Time.timeScale = 0;
        SceneManager.LoadScene("Scene Gwen");
        pausePanel.SetActive(false);
    }

    public void ReloadGame(){
        titlePanel.SetActive(false);
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("Scene Gwen");
        Time.timeScale = 1;
    }

  

    public void QuitGame(){
        Application.Quit();
    }
 
    IEnumerator ReturnTitleAfterTime()
    {
        yield return new WaitForSeconds(10);
        ReturnTitle();
    }

}