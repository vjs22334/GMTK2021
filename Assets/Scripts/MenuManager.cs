using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public LevelData levelData;
    public GameObject LevelSelectPanel;
    public Transform LevelButtonParent;
    public GameObject HelpPanel;
    public GameObject CreditPanel;

    GameObject currentOpenPanel;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        levelData.Initialize();
        for (int i = 0; i < LevelButtonParent.childCount; i++)
        {
            Button button =  LevelButtonParent.GetChild(i).GetComponent<Button>();
            if(levelData.levels[i].Unlocked){
               button.interactable = true;
            }
            else{
               button.interactable = false;
            }
            button.transform.GetChild(0).GetComponent<Text>().text = "HighScore:"+levelData.levels[i].highScore;
        }
    }

    public void closeBtn(){
        currentOpenPanel.SetActive(false);
    }

    public void Loadlevel(int index){
        SceneManager.LoadScene(levelData.levels[index].SceneName);
    }

    public void PlayBtn(){
        LevelSelectPanel.SetActive(true);
        currentOpenPanel = LevelSelectPanel;
    }

    public void HelpBtn(){
        HelpPanel.SetActive(true);
        currentOpenPanel = HelpPanel;
    }
    public void CreditBtn(){
        CreditPanel.SetActive(true);
        currentOpenPanel = CreditPanel;
    }
    public void quitbutton(){
        Application.Quit();
    }

}
