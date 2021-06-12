using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour
{
    public NodeType nodeType;
    public Transform laserEmitterMuzzle;
    public float laserRange = 10;

    bool isLaserActive = false;

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

    node nodeHit;

    LineRenderer laserLineRenderer;
    EdgeCollider2D edgeCollider;

    CircleCollider2D circleCollider;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        laserLineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(IsLaserActive){
            //raycast from muzzle
            Debug.DrawRay(laserEmitterMuzzle.position,laserEmitterMuzzle.right*laserRange,Color.green);
            RaycastHit2D[] raycastHits = Physics2D.RaycastAll(laserEmitterMuzzle.position,laserEmitterMuzzle.right,laserRange);
            int i;
            for (i = 0; i < raycastHits.Length; i++)
            {
                if(raycastHits[i].collider == circleCollider || raycastHits[i].collider.GetType() == typeof(EdgeCollider2D)){
                    continue;
                }
                if(raycastHits[i].collider.CompareTag("Node")){
                    nodeHit = raycastHits[i].collider.GetComponent<node>();
                    ConnectLaser();
                    break;
                }
            }
            if(i==raycastHits.Length && nodeHit != null){
                nodeHit.IsLaserActive = false;
                nodeHit = null;
            }
            
        }
        if(nodeHit == null || !IsLaserActive){
            laserLineRenderer.enabled = false;
            edgeCollider.enabled = false;
        }
    }

    private void ConnectLaser()
    {
        if(nodeHit.nodeType == NodeType.repeater){
            nodeHit.IsLaserActive = true;
            laserLineRenderer.enabled = true;
            edgeCollider.enabled = true;
            Vector3[] positions = {transform.position,nodeHit.transform.position};
            Vector2[] Positions2d = {transform.position,nodeHit.transform.position};
            laserLineRenderer.SetPositions(positions);
            edgeCollider.SetPoints(new List<Vector2>(Positions2d));
        }
    }
}

public enum NodeType{
    Source,
    repeater
}

