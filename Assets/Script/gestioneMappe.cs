using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class gestioneMappe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (System.IO.File.Exists("completedMaps.txt")==false){ //controlla file mappe completate

            string p = "Assets/Resources/completedMaps.txt";

            StreamWriter writer = new StreamWriter(p, false);
            writer.WriteLine("0");
            writer.Close();
        }
        if (System.IO.File.Exists("mappaScelta.txt")==false){ //controlla file mappe scelta

            string p = "Assets/Resources/mappaScelta.txt";

            StreamWriter writer = new StreamWriter(p, false);
            writer.WriteLine("1");
            writer.Close();
        }

        string path = "Assets/Resources/completedMaps.txt";
        StreamReader reader = new StreamReader(path);
        string leggi = reader.ReadLine();

        if(leggi=="2"){ //controlla cap2

            GameObject.Find("bottone 2").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("bottone 2").transform.GetChild(1).gameObject.SetActive(true);
        }else{
            
            GameObject.Find("bottone 2").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("bottone 2").transform.GetChild(1).gameObject.SetActive(false);
        }

        if(leggi=="3"){ //controlla cap3

            GameObject.Find("bottone 3").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("bottone 3").transform.GetChild(1).gameObject.SetActive(true);
        }else{

            GameObject.Find("bottone 3").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("bottone 3").transform.GetChild(1).gameObject.SetActive(false);
        }

        if(leggi=="4"){ //controlla cap4

            GameObject.Find("bottone 4").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("bottone 4").transform.GetChild(1).gameObject.SetActive(true);
        }else{

            GameObject.Find("bottone 4").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("bottone 4").transform.GetChild(1).gameObject.SetActive(false);
        }

        if(leggi=="5"){ //controlla cap3

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
