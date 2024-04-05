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
        GameObject.Find("Music").GetComponent<musicScript>().Stop();
        if (vinci) GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("win");
        
        for (float i = 0; i <= 100; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 100);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(5);

        
        for (float i = 100; i >= 0; i--)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 100);
            yield return new WaitForEndOfFrame();
        }

        GameObject.Find("Music").GetComponent<musicScript>().Rimuovi();

        string scene = "MainScene";
        if (vinci) 
        { 
            scene = "DialogueScene";
            
            StreamReader r = new StreamReader(Application.streamingAssetsPath+"/mappaScelta.txt");
            string l = r.ReadLine();

            int mapScelta = int.Parse(l);
            r.Close();

            r = new StreamReader(Application.streamingAssetsPath+"/completedMaps.txt");
            l = r.ReadLine();

            int mapCompleted = int.Parse(l);
            r.Close();
            StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + "/dialogo.txt", false);
            writer.WriteLine(mapScelta);
            writer.WriteLine(true);
            writer.Close();

            if (mapCompleted <= mapScelta)
            {
                if(mapScelta!=6)
                {
                    writer = new StreamWriter(Application.streamingAssetsPath + "/completedMaps.txt", false);
                    writer.WriteLine(mapScelta + 1);
                    writer.Close();
                }
                


                if (mapScelta == 2)
                {
                    writer = new StreamWriter(Application.streamingAssetsPath+"/dati.txt", true);
                    writer.WriteLine("Granius,Granius,4,0,4,1,2,6,6,90,5,3,4,false,27,9,3,8,10,8,7,5,65,40,25,70,80,45,30,15,false");
                    writer.WriteLine("Thera,Thera,4,0,4,1,1,7,8,75,5,3,7,false,29,13,5,7,6,8,7,4,90,60,20,30,50,35,35,20,false");
                    writer.WriteLine("Acquira,Acquira,6,0,4,1,1,6,6,85,5,2,6,false,30,15,5,7,8,6,8,5,85,55,30,40,40,40,45,30,false");
                    writer.WriteLine("Hydris,Hydris,6,0,4,1,2,4,4,80,5,2,1,true,26,5,12,6,7,7,5,9,40,30,55,40,40,55,25,50,true");
                    writer.Close();
                }
                if(mapScelta == 3)
                {
                    writer = new StreamWriter(Application.streamingAssetsPath+"/dati.txt", true);
                    writer.WriteLine("Aeria,Aeria,6,0,4,1,2,6,6,90,5,4,3,false,28,14,5,9,8,6,8,5,45,40,20,75,50,35,20,30,false");
                    writer.WriteLine("Skye,Skye,6,0,4,1,2,2,4,100,10,4,1,true,25,5,13,6,7,6,5,9,45,20,60,40,40,35,10,50,true");
                    writer.Close();
                }
                if (mapScelta == 6)
                {  
                    writer = new StreamWriter(Application.streamingAssetsPath + "/mappaScelta.txt", false);
                    writer.WriteLine("3");
                    writer.Close();

                    writer = new StreamWriter(Application.streamingAssetsPath + "/dialogo.txt", false);
                    writer.WriteLine(3);
                    writer.WriteLine(false);
                    writer.Close();

                }

            }
        }

        GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(1);
        yield return new WaitForSeconds(1);
        AsyncOperation async2 = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
        

    }
   
}
