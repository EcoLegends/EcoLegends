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
    public int avoBonus = 0;
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
            case "Lava":
                this.description = "Terreno non traversabile.";
                this.canBeWalkedOn = false;
                this.travelCost = 0;

                sp = Resources.LoadAll("Platformer Tileset - Pixelart Grasslands/Sprites/Textures/Tiles/DirtTiles");
                GetComponent<SpriteRenderer>().sprite = (Sprite)sp[62];
                break;
            case "Foresta":
            case "Cespuglio":
                this.description = "Terreno difensivo.";
                this.canBeWalkedOn = true;
                this.travelCost = 2;
                this.avoBonus = 30;
                sp = Resources.LoadAll("Platformer Tileset - Pixelart Grasslands/Sprites/Textures/Decor/TreeTile");
                GetComponent<SpriteRenderer>().sprite = (Sprite)sp[1];
                break;
            case "Acqua":
                this.description = "Terreno difficile da traversare.";
                this.canBeWalkedOn = true;
                this.travelCost = 3;
                sp = Resources.LoadAll("Platformer Tileset - Pixelart Grasslands/Sprites/Textures/Tiles/GrassTiles");
                GetComponent<SpriteRenderer>().sprite = (Sprite)sp[1];
                break;

            default:
                this.description = "Terreno neutrale.";
                this.canBeWalkedOn = true;
                this.travelCost = 1;
                sp = Resources.LoadAll("Platformer Tileset - Pixelart Grasslands/Sprites/Textures/Tiles/GrassTiles");
                //GetComponent<SpriteRenderer>().sprite = (Sprite) sp[98];
                
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
