using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoad : MonoBehaviour
{
    public Animator transition; 
    public bool change = false;
    void Update()
    {
        // if(1>2){
        //     LoadNextLevel();
        // }
    }

    public void LoadNextLevel(){

        StartCoroutine(Load());
    }
    public IEnumerator Load(){
        
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

    }

}
