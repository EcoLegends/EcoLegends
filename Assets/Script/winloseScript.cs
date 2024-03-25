using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winloseScript : MonoBehaviour
{

    public bool vinci = true;


    void OnEnable()
    {
        StartCoroutine(anim());
    }


    IEnumerator anim()
    {
        for(float i = 0; i <= 500; i++)
        {
            transform.parent.localScale = Vector3.one * Camera.main.orthographicSize / 5;
            

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 500);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(5);

        
        for (float i = 500; i >= 0; i--)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 500);
            yield return new WaitForEndOfFrame();
        }

        string scene = "MainScene";
        if (vinci) scene = "Menu";

        AsyncOperation async2 = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        

    }
   
}
