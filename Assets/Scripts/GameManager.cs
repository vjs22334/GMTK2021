using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Level Data")]
    public int LevelIndex;
    public LevelData levelData;

    Level CurrLevel;

    [Header("node data")]
    public int maxLasersAllowed = 3;

    public float NodeTurnSpeed = 100;

    [Header("enemy and spawner data")]
    public float enemySpeed = 2f;

    public float minSpawnTime = 3f;

    public float maxSpawnTime = 6f;

    public int spawnCount = 1;

    public Spawner[] spawners;

    [Header("score Data")]

    public int scorePerVirus = 1;

    public int scorePerDataPacket = 1;

    int score;

    public int Score{
        get{
            return score;
        }
        set{
            score = value;
            UIManager.Instance.SetScore(value);
        }
    }

    [Header("Lives")]
    public int maxVirusLives = 3;
    public int maxDataPacketLives = 3;

    int virusLives;
    int dataPacketLives;
  

    public int VirusLives{
        get{
            return virusLives;
        }
        set{
            virusLives = value;
            UIManager.Instance.SetVirusLives(value);
        }
    }

    public int DataPacketLives{
        get{
            return dataPacketLives;
        }
        set{
            dataPacketLives = value;
            UIManager.Instance.SetDataPacketLives(value);
        }
    }

    float currSpawnTime;
    node selectedNode;

    int currNoOflasers = 0;

    private static GameManager _instance = null;
    public static GameManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager>();
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

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Time.timeScale = 1;
        currSpawnTime = 2;
        VirusLives = maxVirusLives;
        DataPacketLives = maxDataPacketLives;
        Score = 0;
        CurrLevel = levelData.levels[LevelIndex];
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //input
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            if(selectedNode!=null){
                selectedNode.Turn(1);
            }
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            if(selectedNode!=null){
                selectedNode.Turn(-1);
            }
        }

        //spawning
        if(currSpawnTime <= 0){
            List<Spawner> spwanersAvailable = new List<Spawner>(spawners);
            for (int i = 0; i < spawnCount; i++)
            {
                int j = Random.Range(0,spwanersAvailable.Count);
                spwanersAvailable[j].SpawnRandomObject();
                spwanersAvailable.RemoveAt(j);
            }
            currSpawnTime = Random.Range(minSpawnTime,maxSpawnTime);
        }
        else{
            currSpawnTime -= Time.deltaTime;
        }
       
    }


    public void SetSelectedNode(node newNode){
        if(selectedNode!=null){
            selectedNode.selectionCircle.SetActive(false);
        }
        selectedNode = newNode;
        selectedNode.selectionCircle.SetActive(true);
    }

    public void decreaseLaserCount(){
        if(currNoOflasers > 0){
            currNoOflasers--;
        }
    }

    public void increaseLaserCount(){
        currNoOflasers++;
    }

    public bool CanAddLaser(){
        return currNoOflasers<maxLasersAllowed;
    }

    public void IncreaseVirusScore(){
        Score += scorePerVirus;
       
        AudioManager.Instance.Play_virusDeath();
    }

    public void IncreaseDatapacketScore(){
        Score += scorePerDataPacket;
        AudioManager.Instance.Play_scoreUP();
        if(Score > CurrLevel.ScoreToUnlock){
            Time.timeScale = 0;
            levelData.CheckAndSetHighScore(LevelIndex,score);
            if(LevelIndex < levelData.levels.Length-1)
                UIManager.Instance.DisplayLevelCompleted(false);
            else
                UIManager.Instance.DisplayLevelCompleted(true);
        }
    }

    public void DecreaseDatapacketLives(){
        DataPacketLives--;
        AudioManager.Instance.Play_packageDeath();
        if(DataPacketLives <= 0){
            GameOver();
        }
    }
    public void DecreaseVirustLives(){
        VirusLives--;
        
        AudioManager.Instance.Play_coreDmg();
        if(VirusLives <= 0){
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        UIManager.Instance.DisplayGameOver();
    }

    public void pause(){
        Time.timeScale = 0;
        UIManager.Instance.DisplayPause();
    }

    public void NextLevel(){
        if(LevelIndex<levelData.levels.Length-1){
            SceneManager.LoadScene(levelData.levels[LevelIndex+1].SceneName);
        }
    }

    public void retry(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Continue(){
        Time.timeScale = 1;
        UIManager.Instance.resume();
    }

    public void mainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
