using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour
{
    public NodeType nodeType;
    public MoveType moveType;
    public Transform laserEmitterMuzzle;
    public float laserRange = 10;

    float turnSpeed; 

    bool isLaserActive = false;

    int laserHitcount = 0;

    public int LaserHitCount{
        get{
            if(nodeType == NodeType.Source){
                return 1;
            }
            else{
                return laserHitcount;
            }
        }
        set{
            laserHitcount = value;
        }
    }

    public bool IsLaserActive {
        get{
            if(nodeType == NodeType.Source){
                return true;
            }
            else{
                return isLaserActive;
            }
        }
        set{
            isLaserActive = value;
        }
    }
    public AudioManager audioManager;

    node nodeHit;

    LineRenderer laserLineRenderer;
    EdgeCollider2D edgeCollider;

    CircleCollider2D circleCollider;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        turnSpeed = GameManager.Instance.NodeTurnSpeed;
        laserLineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(LaserHitCount > 0){
            //raycast from muzzle
            Debug.DrawRay(laserEmitterMuzzle.position,laserEmitterMuzzle.right*laserRange,Color.yellow);
            RaycastHit2D[] raycastHits = Physics2D.RaycastAll(laserEmitterMuzzle.position,laserEmitterMuzzle.right,laserRange);
            int i;
            bool hitnode = false;
            for (i = 0; i < raycastHits.Length; i++)
            {
                if(raycastHits[i].collider == circleCollider || raycastHits[i].collider.GetType() == typeof(EdgeCollider2D)){
                    continue;
                }
                if(raycastHits[i].collider.CompareTag("Node")){
                    if(nodeHit == null && GameManager.Instance.CanAddLaser()){
                        nodeHit = raycastHits[i].collider.GetComponent<node>();
                        if(nodeHit.nodeHit == this || nodeHit.LaserHitCount>0){
                            nodeHit = null;
                        }
                        else{
                            ConnectLaser();
                        }
                    }
                    hitnode = true;
                    break;
                }
            }
            if(!hitnode && nodeHit != null){
                node currNodeHit = nodeHit;
                node prevNodeHit = nodeHit;
                while(currNodeHit!=null){
                    currNodeHit.LaserHitCount--;
                    GameManager.Instance.decreaseLaserCount();
                    currNodeHit = currNodeHit.nodeHit;
                    prevNodeHit.nodeHit = null;
                    prevNodeHit = currNodeHit;
                }
                nodeHit = null;
                GameManager.Instance.decreaseLaserCount();
               
                
            }
            
        }
        if(nodeHit == null || LaserHitCount == 0){
            laserLineRenderer.enabled = false;
            edgeCollider.enabled = false;
        }
    }

    private void ConnectLaser()
    {
        if(nodeHit.nodeType == NodeType.repeater){
            nodeHit.LaserHitCount++;
            GameManager.Instance.increaseLaserCount();
            laserLineRenderer.enabled = true;
            edgeCollider.enabled = true;
            Vector3[] positions = {transform.position,nodeHit.transform.position};
            Vector2[] Positions2d = {Vector3.zero,transform.worldToLocalMatrix*(nodeHit.transform.position-transform.position)};
            laserLineRenderer.SetPositions(positions);
            audioManager.Play_laserActiv();
            edgeCollider.SetPoints(new List<Vector2>(Positions2d));
        }
        else{
            nodeHit = null;
        }
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        GameManager.Instance.SetSelectedNode(this);
    }

    public void Turn(int direction){
        if(moveType == MoveType.Static){
            return;
        }
        transform.rotation = Quaternion.Euler(0,0,transform.rotation.eulerAngles.z + turnSpeed*direction*Time.deltaTime);
    }
}

public enum NodeType{
    Source,
    repeater
}

public enum MoveType{
    NonStatic,
    Static
}



