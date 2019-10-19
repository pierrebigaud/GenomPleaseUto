using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using System;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    ///
    /// This Script have four purpose
    /// time management : the gameManager detect the time for the cellule
    /// immunity manager : how many point is made and how he lost
    /// True/false : examine if the choice is true or false
    ///

    public Slider slider;
    public bool isGameInPause, gameOver;

    // levels to attach with tags
    public LevelManager days;

    // time comparing the investigation of the cell
    public float fTimeInvestigation;
    public float fTimeInvestigationMax = 10;

    // this variables determines the length of a day.
    public float fTimeDay;

    // number of good answer
    public int score, malusGD = 10, malusBL = 20, BonusGL = 20,BonusBD = 40;

    // value of the deprime meter for the cell
    public float fImmunityfCurrent;
    public float fImmunityBegin;
    public float fImmunityLimite;

    // value for mistakes
    public float fMistakeTime;
    public float fMistakeGoodCellDestroy;
    public float fMistakeBadCellPass;

    // List cells
    public CellCycler Cells;

    // cell to be examined
    public CelluleBehaviour cellToExam;
    public GameObject tentacule;
    public GameObject timer,timerSmall;
    public GameObject goodCell;
    public GameObject badCell;
    public GameObject tooSlow;
    public Sprite imgDestroy;
    public Sprite imgOk;
    public Sprite imgDestroyDes;
    public Sprite imgOkDes;
    public Button destroyBtn;
    public Button okBtn;

    //audio
    AudioSource audioSource;
    public AudioClip pressButton;
    public AudioClip goodCellAudio;
    public AudioClip badCellAudio;
    public AudioClip tooSlowAudio;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        slider.maxValue = fImmunityLimite;
        days = GameObject.FindGameObjectWithTag("days").GetComponent<LevelManager>();
        days.GameChanger = this;
        days.ModifCellCycler = Cells;
    }

    // test of the cell
    public void TestInterrogation(bool isDestroy)
    {
        audioSource.PlayOneShot(pressButton, 1);
        destroyBtn.GetComponent<Button>().interactable = false;
        okBtn.GetComponent<Button>().interactable = false;
        StartCoroutine(buttonReactivate());
        tentacule.GetComponent<Animator>().ResetTrigger("pushButton");
        tentacule.GetComponent<Animator>().SetTrigger("pushButton");
        /// GOOD RESPONSE !----------
        /// if you destroy a bad cell
        if (cellToExam.isBad && isDestroy)
        {
            score+=BonusBD;
            cellToExam.isRejected = true;
        }
        // if you let pass a good cell
        else if (!cellToExam.isBad && !isDestroy)
        {
            score+=BonusGL;
            cellToExam.isRejected = false;
        }

        /// BAD RESPONSE !----------
        // if you destroy a good cell
        else if (!cellToExam.isBad && isDestroy)
        {
            score -= malusGD;
            cellToExam.isRejected = true;
            fImmunityfCurrent += fMistakeGoodCellDestroy;
            StartCoroutine(textAppear(goodCell, goodCellAudio));

        }
        // if you pass a bad cell
        else if (cellToExam.isBad && !isDestroy)
        {
            score -= malusBL;
            fImmunityfCurrent += fMistakeBadCellPass;
            cellToExam.isRejected = false;
            StartCoroutine(textAppear(badCell, badCellAudio));
        }
        
        // base action when test , do a cell cycle and reset the time
        Cells.doCellCycle();
        cellToExam = Cells.cells[5].GetComponentInChildren<CelluleBehaviour>();
        fTimeInvestigation = fTimeInvestigationMax;
        
    }

    IEnumerator buttonReactivate (){
        yield return new WaitForSeconds(1);
        okBtn.GetComponent<Button>().interactable = true;
        destroyBtn.GetComponent<Button>().interactable = true;
    }
    private void Update()
    {
        timer.GetComponent<TextMesh>().text = Math.Floor(fTimeDay) + "";
        timerSmall.GetComponent<TextMesh>().text = "."+Mathf.Floor((fTimeDay- Mathf.Floor(fTimeDay))*10);
        slider.GetComponentInChildren<Image>().color = 
            new Color(Mathf.Clamp((fImmunityfCurrent / fImmunityLimite), 1, 1), 1, 0.5f, Mathf.Clamp((1 - fImmunityfCurrent / fImmunityLimite), 1, 1));
        slider.value = fImmunityfCurrent;
        //if not in pause
        if (!isGameInPause || !gameOver)
        {
            // if no cell to exam, test if there is a cell
            if (cellToExam == null)
            {
                if (Cells.cells[5] != null)
                {
                    cellToExam = Cells.cells[5].GetComponentInChildren<CelluleBehaviour>();
                }
            }
            if (fImmunityfCurrent > fImmunityLimite)
            {
                gameOver = true;
            }

            // if time is up
            if (fTimeInvestigation < 0)
            {
                // resetTime and add mistake to immunity
                fTimeInvestigation = fTimeInvestigationMax;
                fImmunityfCurrent += fMistakeTime;
                StartCoroutine(textAppear(tooSlow, tooSlowAudio));
            }

            // soustract time
            else
            {
                fTimeInvestigation -= 0.01f;
            }

            // if time is up
            if (fTimeDay < 0)
            {
                /// 
                /// this condition access another day to the difficulty settings
                /// 
                days.NextLevel();
            }

            // soustract time
            else
            {
                fTimeDay -= 0.01f;
            }
        }
    }

    IEnumerator textAppear(GameObject text, AudioClip audio) {
        audioSource.PlayOneShot(audio, 1);
        text.SetActive(true);
        yield return new WaitForSeconds(2);
        text.SetActive(false);
    }
}
