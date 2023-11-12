using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleManager : MonoBehaviour
{

    public static string phase;

    public static List<GameObject> units = new List<GameObject>();
    public static List<GameObject> unmovedUnits = new List<GameObject>();
    public static List<GameObject> enemies = new List<GameObject>();


    void Start()
    {
        phase = "Player";
    }

    
    void Update()
    {
        Debug.Log(unmovedUnits.Count);
        
    }
}
