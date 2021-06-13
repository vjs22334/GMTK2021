using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField]AudioSource source;
   [SerializeField]AudioClip packageDeath,coreDmg,virusDeath,scoreUP,laserActiv,bgMusic;

    private static AudioManager _instance = null;
    public static AudioManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }
    void Awake(){
        if(_instance == null){
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
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
        source.clip = bgMusic;
        source.Play();
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
        source.PlayOneShot(laserActiv,0.5f);
    }


}
