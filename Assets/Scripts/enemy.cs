using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    float speed;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        speed = GameManager.Instance.enemySpeed;
        Destroy(gameObject,30f);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        transform.position += transform.right*speed*Time.deltaTime;
    }

 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Node")){
            GameManager.Instance.IncreaseVirusScore();
            Destroy(gameObject);
        }
        else if(other.CompareTag("Core")){
            GameManager.Instance.DecreaseVirustLives();
            Destroy(gameObject);
        }
    }
    
}
