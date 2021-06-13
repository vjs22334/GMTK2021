using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;
    public Text virusLivesText;
    public Text dataPacketLivesText;

    public GameObject GameoverPanel;

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
    public void DisplayGameOver(){
        GameoverPanel.SetActive(true);
    }        
}
