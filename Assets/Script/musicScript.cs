using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class musicScript : MonoBehaviour
{
    public string music;


    // Start is called before the first frame update
    void Start()
    {                                                     
        AudioClip musicStart = (AudioClip)Resources.Load("Sounds/Music/Intro - "+music);
        GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Sounds/Music/Loop - " + music);

        Debug.Log(musicStart.length);
        


        GetComponent<AudioSource>().PlayOneShot(musicStart);
        Debug.Log(AudioSettings.dspTime);
        GetComponent<AudioSource>().PlayScheduled(AudioSettings.dspTime + musicStart.length);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
