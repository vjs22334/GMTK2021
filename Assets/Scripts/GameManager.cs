using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    node selectedNode;

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
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //input
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            if(selectedNode!=null){
                selectedNode.Turn(-1);
            }
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            if(selectedNode!=null){
                selectedNode.Turn(1);
            }
        }
    }


    public void SetSelectedNode(node newNode){
        selectedNode = newNode;
    }        
}
