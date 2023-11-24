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
        if(phase == "Player" &&  unmovedUnits.Count == 0)
        {
            phase = "Enemy";
            Debug.Log("Enemy Phase");

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<enemyScript>().Move();
            }



            phase = "Player";
            Debug.Log("Player Phase");
            
            foreach(GameObject unit in units)
            {
                unit.GetComponent<playerScript>().CanMoveAgain();
            }


        }
        
        
    }

}
