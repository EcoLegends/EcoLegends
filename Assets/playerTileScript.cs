using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTileScript : MonoBehaviour
{
    public int x;
    public int y;

    float inc = 0.1F;
    float transparency = 100;

    public void PlayerTile(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transparency >= 120 || transparency <= 0)
        {
            inc *= -1;
        }

        transparency += inc;
        GetComponent<Renderer>().material.SetColor("_Color",new Color(1,1,1,transparency/100));
    }
}
