using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class gestioneMappe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(Application.streamingAssetsPath+"/completedMaps.txt") ==false){ //controlla file mappe completate

            string p = Application.streamingAssetsPath+"/completedMaps.txt";

            StreamWriter writer = new StreamWriter(p, false);
            writer.WriteLine("0");
            writer.Close();
        }
        if (File.Exists(Application.streamingAssetsPath+"/mappaScelta.txt") ==false){ //controlla file mappe scelta

            string p = Application.streamingAssetsPath+"/mappaScelta.txt";

            StreamWriter writer = new StreamWriter(p, false);
            writer.WriteLine("1");
            writer.Close();
        }

        string path = Application.streamingAssetsPath+"/completedMaps.txt";
        StreamReader reader = new StreamReader(path);
        int leggi = int.Parse(reader.ReadLine());
        reader.Close();
        Debug.Log(leggi);

        if(leggi>=2){ //controlla cap2

            GameObject.Find("bottone 2").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("bottone 2").transform.GetChild(1).gameObject.SetActive(true);
        }else{
            
            GameObject.Find("bottone 2").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("bottone 2").transform.GetChild(1).gameObject.SetActive(false);
        }

        if(leggi>=3){ //controlla cap3

            GameObject.Find("bottone 3").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("bottone 3").transform.GetChild(1).gameObject.SetActive(true);
        }else{

            GameObject.Find("bottone 3").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("bottone 3").transform.GetChild(1).gameObject.SetActive(false);
        }

        if(leggi >= 4){ //controlla cap4

            GameObject.Find("bottone 4").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("bottone 4").transform.GetChild(1).gameObject.SetActive(true);
        }else{

            GameObject.Find("bottone 4").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("bottone 4").transform.GetChild(1).gameObject.SetActive(false);
        }

        if(leggi >= 5){ //controlla cap5

            GameObject.Find("bottone 5").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("bottone 5").transform.GetChild(1).gameObject.SetActive(true);
        }else{

            GameObject.Find("bottone 5").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("bottone 5").transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
