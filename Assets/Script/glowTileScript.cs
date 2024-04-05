using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glowTileScript : MonoBehaviour
{
   
    float transparency = 4;
    float inc = 1;
    int times = 0;
    // Update is called once per frame
    void Update()
    {
        if(times < 7)
        {
            if (transparency >= 120 || transparency <= 0)            //inverte il verso dell'incremento
            {
                inc *= -1;
                times++;
            }

            transparency += inc *7.5f;

            
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transparency / 100); //cambia trasparenza
        }
        
    }
}
