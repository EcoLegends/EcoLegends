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
           

        GetComponent<AudioSource>().PlayOneShot(musicStart);
        GetComponent<AudioSource>().PlayScheduled(AudioSettings.dspTime + musicStart.length);

        
    }

    public void ChangeMusic(string new_music)
    {
        if(music!= new_music) 
        { 
            music = new_music;
            StartCoroutine(FadeOut(GetComponent<AudioSource>(), music));
            
        }
        
    }

    public static IEnumerator FadeOut(AudioSource audioSource,string music)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime/1f;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        AudioClip musicStart = (AudioClip)Resources.Load("Sounds/Music/Intro - " + music);
        audioSource.clip = (AudioClip)Resources.Load("Sounds/Music/Loop - " + music);


        audioSource.PlayOneShot(musicStart);
        audioSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Update is called once per frame
    public void Rimuovi()
    {
        StartCoroutine(FadeOut(GetComponent<AudioSource>()));
    }

    public static IEnumerator FadeOut(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1f;

            yield return null;
        }

        audioSource.Stop();

        Destroy(audioSource.gameObject);
    }
}
