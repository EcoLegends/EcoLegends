using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 500);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(5);

        GameObject.Find("Music(Clone)").GetComponent<musicScript>().Rimuovi();
        for (float i = 500; i >= 0; i--)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 500);
            yield return new WaitForEndOfFrame();
        }



        string scene = "MainScene";
        if (vinci) 
        { 
            scene = "Menu";
            
            StreamReader r = new StreamReader("Assets/Resources/mappaScelta.txt");
            string l = r.ReadLine();

            int mapScelta = int.Parse(l);
            r.Close();

            r = new StreamReader("Assets/Resources/completedMaps.txt");
            l = r.ReadLine();

            int mapCompleted = int.Parse(l);
            r.Close();

            if(mapCompleted <= mapScelta)
            {
                StreamWriter writer = new StreamWriter("Assets/Resources/completedMaps.txt", false);
                writer.WriteLine(mapScelta+1);
                writer.Close();
            }
        }

        AsyncOperation async2 = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        

    }
   
}
