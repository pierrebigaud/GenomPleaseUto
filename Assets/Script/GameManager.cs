﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    ///
    /// This Script have four purpose
    /// time management : the gameManager detect the time for the cellule
    /// immunity manager : how many point is made and how he lost
    /// True/false : examine if the choice is true or false
    ///


    public bool isGameInPause;

    // time comparing the investigation of the cell
    public float fTimeInvestigation;
    public float fTimeInvestigationMax = 10;

    // value of the deprime meter for the cell
    public float fImmunityfCurrent;
    public float fImmunityBegin;
    public float fImmunityLimite;

    // value for mistakes
    public float fMistakeTime;
    public float fMistakeGoodCellDestroy;
    public float fMistakeBadCellPass;

    // List cells
    public CellCycler Cells ;

    // cell to be examined
    public CelluleBehaviour cellToExam;

    // test of the cell
    public void TestInterrogation(bool isDestroy)
    {

        /// GOOD RESPONSE !----------
        /// if you destroy a bad cell
        if (cellToExam.isBad && isDestroy)
        {
            cellToExam.isRejected = true;
        }
        // if you let pass a good cell
        else if (!cellToExam.isBad && !isDestroy)
        {
            cellToExam.isRejected = false;
        }

        /// BAD RESPONSE !----------
        // if you destroy a good cell
        else if (!cellToExam.isBad && isDestroy)
        {
            cellToExam.isRejected = true;
            fImmunityfCurrent += fMistakeGoodCellDestroy;

        }
        // if you pass a bad cell
        else if (cellToExam.isBad && !isDestroy)
        {
            fImmunityfCurrent += fMistakeBadCellPass;
            cellToExam.isRejected = false;
        }

        // base action when test , do a cell cycle and reset the time
        Cells.doCellCycle();
        cellToExam = Cells.cells[5].GetComponentInChildren<CelluleBehaviour>();
        fTimeInvestigation = fTimeInvestigationMax;
    }

    private void FixedUpdate()
    {

        //if not in pause
        if (!isGameInPause)
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
                //you have lose !
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            // if time is up
            if (fTimeInvestigation < 0)
            {
                // resetTime and add mistake to immunity
                fTimeInvestigation = fTimeInvestigationMax;
                fImmunityfCurrent += fMistakeTime;
            }

            // soustract time
            else
            {
                fTimeInvestigation -= 0.01f;
            }
        }
    }
}
