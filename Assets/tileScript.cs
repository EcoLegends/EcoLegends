using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class tileScript : MonoBehaviour
{

    [Tooltip("Tipo di terreno")]                                                    //variabili tassello
    public string type = "Pianura";
    [Tooltip("Descrizione terreno")]
    public string description = "Terreno neutrale.";
    [Tooltip("Può essere traversato")]
    public bool canBeWalkedOn = true;
    [Tooltip("Costo di movimento")]
    public int travelCost = 1;
    public int x;
    public int y;

    public void SetTileData(string type, int x, int y)                                                                      //setta le stat del tassello quando spawna
    {
        this.type = type;
        Object[] sp;
        this.x = x;
        this.y = y;
        switch (type)
        {
            case "Roccia":
                this.description = "Terreno non traversabile.";
                this.canBeWalkedOn = false;
                this.travelCost = 0;

                sp = Resources.LoadAll("Platformer Tileset - Pixelart Grasslands/Sprites/Textures/Tiles/DirtTiles");
                GetComponent<SpriteRenderer>().sprite = (Sprite)sp[62];
                break;

            default:
                this.description = "Terreno neutrale.";
                this.canBeWalkedOn = true;
                this.travelCost = 1;
                sp = Resources.LoadAll("Platformer Tileset - Pixelart Grasslands/Sprites/Textures/Tiles/GrassTiles");
                GetComponent<SpriteRenderer>().sprite = (Sprite) sp[98];
                
                break;

        }
    }  

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
