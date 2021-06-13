using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public Level[] levels;

    public void Initialize(){
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].Unlocked = PlayerPrefs.GetInt("unlocked"+i,0) == 1;
            levels[i].highScore = PlayerPrefs.GetInt("highScore"+i,0);
        }
    }

    public void CheckAndSetHighScore(int levelIndex,int highScore){
        if(levels[levelIndex].highScore < highScore){
            levels[levelIndex].highScore = highScore;
            PlayerPrefs.SetInt("highScore"+levelIndex,highScore);
        }

        if(levels[levelIndex].ScoreToUnlock < highScore){
            levels[levelIndex].Unlocked = true;
            PlayerPrefs.SetInt("unlocked"+levelIndex,1);
        }
    }
}

[System.Serializable]
public struct Level{
    public int ScoreToUnlock;
    public string SceneName;
    public bool endlessAllowed;
    public int highScore;
    public bool Unlocked;
}
