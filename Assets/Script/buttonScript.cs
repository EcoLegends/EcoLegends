using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
/*
string path = Application.streamingAssetsPath+"dati.txt";

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
        StartCoroutine(animation());
    }
    
    public IEnumerator asin(AsyncOperation async){
        while (!async.isDone)     
        {

            yield return new WaitForEndOfFrame();

        }
    }
    public IEnumerator animation(){
        
        yield return new WaitForSeconds(1f);

    }
    public void OnClick()
    {
        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Restart")){       //restart

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"completedMaps.txt";
                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("1");
                writer.Close();

                AsyncOperation async = SceneManager.LoadSceneAsync( "Menu");

                asin(async);
            }

        }

        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Cap1")){       //start cap1

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"mappaScelta.txt";

                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("1");
                writer.Close();
                
                //GameObject.Find("Crossfade").transition.SetTrigger("Start");

                AsyncOperation async = SceneManager.LoadSceneAsync( "MainScene");

                asin(async);

                
            }
        }

        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Cap2")){       //start cap2

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"mappaScelta.txt";

                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("2");
                writer.Close();
                
                AsyncOperation async = SceneManager.LoadSceneAsync( "MainScene");

                asin(async);

                
            }
        }

        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Cap3")){       //start cap3

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"mappaScelta.txt";

                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("3");
                writer.Close();
                
                AsyncOperation async = SceneManager.LoadSceneAsync( "MainScene");

                asin(async);

                
            }
        }

        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Cap4")){       //start cap4

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"mappaScelta.txt";

                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("4");
                writer.Close();
                
                AsyncOperation async = SceneManager.LoadSceneAsync( "MainScene");

                asin(async);

                
            }
        }

        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Cap5")){       //start cap5

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"mappaScelta.txt";

                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("5");
                writer.Close();
                
                AsyncOperation async = SceneManager.LoadSceneAsync( "MainScene");

                asin(async);

                
            }
        }
    }
}
