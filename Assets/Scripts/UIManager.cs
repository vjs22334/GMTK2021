using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;
    public Text virusLivesText;
    public Text dataPacketLivesText;
    public Text ScoreToReach;
    public Text LasersAllowed;

    public GameObject GameoverPanel;

    public GameObject PausePanel;

    public GameObject LevelCompletePanel;
    public GameObject nextLevelBtn;
    public Text levelCompleteText;

    private static UIManager _instance = null;
    public static UIManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }
    void Awake(){
        if(_instance == null){
            _instance = this;
        }
        else if(_instance != this){
            Destroy(this.gameObject);
        }
    }

    public void SetScore(int score){
        ScoreText.text = score.ToString();
    }

    public void SetVirusLives(int lives){
        virusLivesText.text = lives.ToString();
    }
    public void SetDataPacketLives(int lives){
        dataPacketLivesText.text = lives.ToString();
    }
    public void SetScoreToReach(int score){
        ScoreToReach.text = score.ToString();
    }
    public void SetlasersAllowed(int Lasers){
        LasersAllowed.text = Lasers.ToString();
    }
    public void DisplayGameOver(){
        GameoverPanel.SetActive(true);
    }
    public void DisplayPause(){
        PausePanel.SetActive(true);
    }

    public void resume(){
        PausePanel.SetActive(false);
        LevelCompletePanel.SetActive(false);
    }         
    public void DisplayLevelCompleted(bool lastLevel){
        LevelCompletePanel.SetActive(true);
        if(lastLevel){
            levelCompleteText.text = "You have completed the last level. Thanks for playing!";
            nextLevelBtn.SetActive(false);
        }
    } 
}
