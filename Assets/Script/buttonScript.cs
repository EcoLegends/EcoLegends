using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
/*
string path = "Assets/Resources/dati.txt";

StreamWriter writer = new StreamWriter(path, false);
writer.WriteLine("...")
writer.Close();
StreamReader reader = new StreamReader(path);
string leggi = reader.ReadLine();
*/
public class buttonScript : MonoBehaviour
{
    public int button;

    public void inizia(AsyncOperation async)
    {

        StartCoroutine(asin(async));

    }
    
    public IEnumerator asin(AsyncOperation async){
        while (!async.isDone)     
            {

                yield return new WaitForEndOfFrame();

            }
    }
    public void OnClick()
    {
        Debug.Log("ciao");
        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Restart")){       //restart

            if(e==this.gameObject){

                string path = "Assets/Resources/completedMaps.txt";
                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("0");
                writer.Close();

            }

        }

        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Cap1")){       //start cap1

            if(e==this.gameObject){
                Scene activeScene = SceneManager.GetActiveScene();

                GameObject temp = new GameObject( "temp" );  

                GameObject[] allObjects = activeScene.GetRootGameObjects();

                foreach (GameObject go in allObjects)   
                {

                    go.transform.SetParent(temp.transform, false);

                }

                AsyncOperation async = SceneManager.LoadSceneAsync( "MainScene", LoadSceneMode.Additive);

                asin(async);


                Scene battleScene = SceneManager.GetSceneByName( "MainScene" );
                SceneManager.SetActiveScene(battleScene);

                temp.SetActive(false);
            }
        }
    }
}
