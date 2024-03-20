using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class gestioneMappe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (System.IO.File.Exists("myfile.txt")==false){

            string path = "Assets/Resources/completedMaps.txt";

            StreamWriter writer = new StreamWriter(path, false);
            writer.WriteLine("0");
            writer.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
