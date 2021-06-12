using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   public List<GameObject> objectsToSpawn;
   public float spawnRate = 5f;

    public float InitailDelay = 5f;

   /// <summary>
   /// This function is called when the object becomes enabled and active.
   /// </summary>
   void OnEnable()
   {

       InvokeRepeating(nameof(SpawnRandomObject),InitailDelay,spawnRate);
       
   }

   /// <summary>
   /// This function is called when the behaviour becomes disabled or inactive.
   /// </summary>
   void OnDisable()
   {
       CancelInvoke(nameof(SpawnRandomObject));
   }

   void SpawnRandomObject(){
       int i = Random.Range(0,objectsToSpawn.Count*50);
       int index = i/50;
       Instantiate(objectsToSpawn[index],transform.position,transform.rotation);
   }

}
