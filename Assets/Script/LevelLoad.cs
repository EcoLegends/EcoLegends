using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoad : MonoBehaviour
{
    public Animator transition; 
    public bool change = false;
    public float time;
    void Update()
    {
        // if(1>2){
        //     LoadNextLevel();
        // }
    }

    public void LoadNextLevel(int speed){

        transition.speed = speed;

        transition.Play("Crossfade_End");
        time = 0;
    }

    private void OnEnable()
    {
        transition.Play("Crossfade_start");
        time = Time.time + 0.5f;
    }
}
