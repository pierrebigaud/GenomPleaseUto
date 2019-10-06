using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// this script has the function of modyfying the difficulty of the game throughout days
    /// 
    /// it is a list of a class name Days, who apply variables, to the game.
    /// 
    /// </summary>
    /// 

    [System.Serializable]
    public class Day
    {
        /// <summary>
        /// Day is a class who contains all the information about the level played
        /// the time played
        /// the time with killing time
        /// the variables for mistakes
        /// the variables of cell behaviour who adjust difficulties
        /// </summary>
        public float fTimeDaysLevel;
        public float fTimeInvestigationLevel;
        public float fMistakeTime;
        public float fMistakeGoodDestroy;
        public float fMistakeBadLetPass;

        public GameObject gEmptycellule;
    }

    //number days 
    public int nbrDays;

    //regainHealth
    public float fImmunityGain;

    //list days
    public Day[] days;

    // cellCycler in scene
    public CellCycler ModifCellCycler;

    // GameManager in scene
    public GameManager GameChanger;

    //the next method has to change level and reinitilize the gameObject with new value
    public void NextLevel()
    {
        // we create an index who can be used to generate the next level (the nbr days is used to show the number of days)
        int index;
        nbrDays++;

        // if days is not implemented properly
        if (days.Length<=0)
        {
            index = 0;
            SceneManager.LoadScene(0);
        }
        // if we reached the last levels
        else if (nbrDays>=days.Length)
        {
            index = days.Length;
        }
        // move to nextLevel
        else
        {
            index = nbrDays;
            GameChanger.fImmunityfCurrent -= fImmunityGain;
            if (GameChanger.fImmunityfCurrent<0)
            {
                GameChanger.fImmunityfCurrent = 0;
            }
        }
        //change value game manager
        GameChanger.fTimeInvestigation = days[index].fTimeInvestigationLevel;
        GameChanger.fTimeDay = days[index].fTimeDaysLevel;
        GameChanger.fMistakeTime = days[index].fMistakeTime;
        GameChanger.fMistakeGoodCellDestroy = days[index].fMistakeGoodDestroy;
        GameChanger.fMistakeBadCellPass = days[index].fMistakeBadLetPass;
        
        // change Cell
        ModifCellCycler.cellObject= days[index].gEmptycellule;
    }
}
