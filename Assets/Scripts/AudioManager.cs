using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField]AudioSource source;
   [SerializeField]AudioClip packageDeath,coreDmg,virusDeath,scoreUP,laserActiv;

void Awake()
{
    if(FindObjectsOfType<AudioManager>().Length>1)
    {
        Destroy(gameObject);
    }
    else{
        DontDestroyOnLoad(gameObject);
    }
}
public void Play_packageDeath()
{
    source.PlayOneShot(packageDeath);
}
public void Play_coreDmg()
{
     source.PlayOneShot(coreDmg);
}
public void Play_virusDeath()
{
     source.PlayOneShot(virusDeath);
}
public void Play_scoreUP()
{
    source.PlayOneShot(scoreUP);
}
public void Play_laserActiv()
{
     source.PlayOneShot(laserActiv);
}


}
