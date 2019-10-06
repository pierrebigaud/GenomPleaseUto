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
    public int score;

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
    public GameObject timer;
    public GameObject goodCell;
    public GameObject badCell;
    public GameObject tooSlow;
    public Sprite imgDestroy;
    public Sprite imgOk;
    public Sprite imgDestroyDes;
    public Sprite imgOkDes;
    public Button destroyBtn;
    public Button okBtn;

    private void Start() {
        slider.maxValue = fImmunityLimite;
        days = GameObject.FindGameObjectWithTag("days").GetComponent<LevelManager>();
        days.GameChanger = this;
        days.ModifCellCycler = Cells;
    }

    // test of the cell
    public void TestInterrogation(bool isDestroy)
    {
        destroyBtn.GetComponent<Button>().interactable = false;
        okBtn.GetComponent<Button>().interactable = false;
        StartCoroutine(buttonReactivate());
        tentacule.GetComponent<Animator>().ResetTrigger("pushButton");
        tentacule.GetComponent<Animator>().SetTrigger("pushButton");
        /// GOOD RESPONSE !----------
        /// if you destroy a bad cell
        if (cellToExam.isBad && isDestroy)
        {
            score++;
            cellToExam.isRejected = true;
        }
        // if you let pass a good cell
        else if (!cellToExam.isBad && !isDestroy)
        {
            score++;
            cellToExam.isRejected = false;
        }

        /// BAD RESPONSE !----------
        // if you destroy a good cell
        else if (!cellToExam.isBad && isDestroy)
        {
            cellToExam.isRejected = true;
            fImmunityfCurrent += fMistakeGoodCellDestroy;
            StartCoroutine(textAppear(goodCell));

        }
        // if you pass a bad cell
        else if (cellToExam.isBad && !isDestroy)
        {
            fImmunityfCurrent += fMistakeBadCellPass;
            cellToExam.isRejected = false;
            StartCoroutine(textAppear(badCell));
        }
        
        // base action when test , do a cell cycle and reset the time
        Cells.doCellCycle();
        cellToExam = Cells.cells[5].GetComponentInChildren<CelluleBehaviour>();
        fTimeInvestigation = fTimeInvestigationMax;
        
    }

    IEnumerator buttonReactivate (){
        yield return new WaitForSeconds(2);
        okBtn.GetComponent<Button>().interactable = true;
        destroyBtn.GetComponent<Button>().interactable = true;
    }
    private void FixedUpdate()
    {
        timer.GetComponent<TextMesh>().text = Math.Round(fTimeDay, 2) + "";
        slider.GetComponentInChildren<Image>().color = new Color(Mathf.Clamp((fImmunityfCurrent / fImmunityLimite), 1, 1), 1, 0.5f, Mathf.Clamp((1 - fImmunityfCurrent / fImmunityLimite), 1, 1));
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
                StartCoroutine(textAppear(tooSlow));
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

    IEnumerator textAppear(GameObject text) {
        text.SetActive(true);
        yield return new WaitForSeconds(2);
        text.SetActive(false);
    }
}
