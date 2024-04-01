using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
/*
string path = Application.streamingAssetsPath+"/dati.txt";

StreamWriter writer = new StreamWriter(path, false);
writer.WriteLine("...")
writer.Close();
StreamReader reader = new StreamReader(path);
string leggi = reader.ReadLine();
*/
public class buttonScript : MonoBehaviour
{
    public int button;

  
    
    public IEnumerator asin(string scene){

        GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(1);
        yield return new WaitForSeconds(1);

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        while (!async.isDone)     
        {

            yield return new WaitForEndOfFrame();

        }
    }
    public void OnClick()
    {
        GameObject.Find("Music").GetComponent<musicScript>().Rimuovi();
        foreach ( GameObject e in GameObject.FindGameObjectsWithTag("Restart")){       //restart

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"/completedMaps.txt";
                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("1");
                writer.Close();
                
                

                StartCoroutine(asin("Menu"));
            }

        }

        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Cap1")){       //start cap1

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"/mappaScelta.txt";

                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("1");
                writer.Close();

                writer = new StreamWriter(Application.streamingAssetsPath + "/dialogo.txt", false);
                writer.WriteLine(1);
                writer.WriteLine(false);
                writer.Close();


                StartCoroutine(asin("DialogueScene"));
            }
        }

        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Cap2")){       //start cap2

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"/mappaScelta.txt";

                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("2");
                writer.Close();

                writer = new StreamWriter(Application.streamingAssetsPath + "/dialogo.txt", false);
                writer.WriteLine(2);
                writer.WriteLine(false);
                writer.Close();

                StartCoroutine(asin("DialogueScene"));


            }
        }

        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Cap3")){       //start cap3

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"/mappaScelta.txt";

                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("6");
                writer.Close();

                writer = new StreamWriter(Application.streamingAssetsPath + "/dialogo.txt", false);
                writer.WriteLine(6);
                writer.WriteLine(false);
                writer.Close();


                StartCoroutine(asin("DialogueScene")); 

                
            }
        }

        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Cap4")){       //start cap4

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"/mappaScelta.txt";

                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("4");
                writer.Close();

                writer = new StreamWriter(Application.streamingAssetsPath + "/dialogo.txt", false);
                writer.WriteLine(4);
                writer.WriteLine(false);
                writer.Close();

                StartCoroutine(asin("DialogueScene"));


            }
        }

        foreach( GameObject e in GameObject.FindGameObjectsWithTag("Cap5")){       //start cap5

            if(e==this.gameObject){

                string path = Application.streamingAssetsPath+"/mappaScelta.txt";

                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("5");
                writer.Close();

                writer = new StreamWriter(Application.streamingAssetsPath + "/dialogo.txt", false);
                writer.WriteLine(5);
                writer.WriteLine(false);
                writer.Close();

                StartCoroutine(asin("DialogueScene"));


            }
        }
    }
}
