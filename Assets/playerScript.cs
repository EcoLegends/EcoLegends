using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

                                       
    [Tooltip("Pos X")]
    public int x = 0;
    [Tooltip("Pos Y")]                                      //info
    public int y = 0;
    [Tooltip("Puo' muoversi")]
    public bool canMove = true;

    [Space]

                                    
    public int lvl = 1;                                         //statistiche
    public int movement = 3;

    [Space] //armi

    public int weaponRange = 1;
    public int weaponWt;  //peso
    public int weaponMt; //potenza
    public int weaponHit;
    public int weaponCrit;
    [Tooltip("1= Fuoco\n2=Acqua\n3=Terra\n4=Aria")]
    public int unitType;
    [Tooltip("1= Fuoco\n2=Acqua\n3=Terra\n4=Aria")]
    public int unitEffective;
    [Tooltip("false -> fisico\ntrue -> magico")]
    public bool weaponIsMagic = false;



    [Space]

    public int maxHp = 1;
    public int hp;                                          
    public int str;
    public int mag;
    public int dex;
    public int spd;
    public int lck;
    public int def;
    public int res;

    [Space]

    public int hpGrowth;                                    //percentuale di crescita della statistica a ogni lvlup
    public int strGrowth;
    public int magGrowth;
    public int dexGrowth;
    public int spdGrowth;
    public int lckGrowth;
    public int defGrowth;
    public int resGrowth;

    [Space]
    public heathBarScript healthbar;


    private List<Vector2> mov_tiles_coords = new List<Vector2>();
    private List<GameObject> movBlueTiles;
    public void HighlightMov()                                          //spawna i tasselli blu del movimento
    {
        movBlueTiles = new List<GameObject>();
        List<GameObject> movTiles = new List<GameObject>();
        List<int> movTilesDistance = new List<int>();
        GameObject[,] map = GameObject.Find("map").GetComponent<mapScript>().mapTiles;

        movTiles.Add(map[x, y]);
        movTilesDistance.Add(999);
        AdjCheck(x, y, movement, ref map, ref movTiles, ref movTilesDistance);

        Object mov_tile_prefab = AssetDatabase.LoadAssetAtPath("Assets/movTilePrefab.prefab", typeof(GameObject));


        for (int i = 0; i < movTiles.Count; i++)        //spawna tasselli blu
        {
            GameObject mov_tile = (GameObject)Instantiate(mov_tile_prefab, new Vector3(movTiles[i].GetComponent<tileScript>().x, movTiles[i].GetComponent<tileScript>().y, -2), Quaternion.identity);
            mov_tile.transform.parent = movTiles[i].transform;

            bool add_vector = true;

            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
                if (p.GetComponent<playerScript>().x == movTiles[i].GetComponent<tileScript>().x && p.GetComponent<playerScript>().y == movTiles[i].GetComponent<tileScript>().y && p != this.gameObject) add_vector = false;
            }

            if (add_vector)
            {
                mov_tiles_coords.Add(new Vector2(movTiles[i].GetComponent<tileScript>().x, movTiles[i].GetComponent<tileScript>().y));
            }
            movBlueTiles.Add(mov_tile);

        }
    }

    private void AdjCheck(int cx, int cy, int mov, ref GameObject[,] map, ref List<GameObject> movTiles, ref List<int> movTilesDistance)            //trova le caselle adiacenti
    {
        int i = -1;
        int j = 0;
        if (mov > 0)
        {
            for (int a = 0; a < 4; a++)
            {


                if (cx + i >= 0 && cx + i < 10 && cy + j >= 0 && cy + j < 10)
                {

                    if (map[cx + i, cy + j].GetComponent<tileScript>().canBeWalkedOn == true)
                    {

                        bool hasEnemy = false;

                        foreach(GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
                        {
                            if(e.GetComponent<enemyScript>().x == (cx + i) && e.GetComponent<enemyScript>().y == (cy + j)) hasEnemy = true;
                        }
                        if(hasEnemy == false)
                        {
                            GameObject temp = map[cx + i, cy + j];
                            if (!movTiles.Contains(map[cx + i, cy + j]))
                            {
                                movTiles.Add(map[cx + i, cy + j]);
                                movTilesDistance.Add(mov);
                                AdjCheck(cx + i, cy + j, mov - map[cx + i, cy + j].GetComponent<tileScript>().travelCost, ref map, ref movTiles, ref movTilesDistance);

                            }
                            else if (movTilesDistance[movTiles.FindIndex(n => n.Equals(temp))] < mov)
                            {
                                movTilesDistance[movTiles.FindIndex(n => n.Equals(temp))] = mov;
                                AdjCheck(cx + i, cy + j, mov - map[cx + i, cy + j].GetComponent<tileScript>().travelCost, ref map, ref movTiles, ref movTilesDistance);
                            }


                        }
                        



                    }
                }

                if (i < 1 && j == 0) i = 1;
                else if (i == 1)
                {
                    i = 0;
                    j = -1;
                }
                else j = 1;

            }

        }

    }



    GameObject player_tile;
    private void Start()  
    {

        battleManager.units.Add(gameObject);                    //aggiunge alle liste globali
        


        hp = maxHp;
        healthbar.SetMaxHealth(maxHp);
        healthbar.SetHealth(hp);

        if (x == 0 & y == 0)
        {
            x = (int)transform.position.x;
            y = (int)transform.position.y;
        }

        transform.position = new Vector3(x, y, -9);


        this.CanMoveAgain(); //spawn tassello lampeggiante
    }

    public void CanMoveAgain() {
        canMove = true;                                                     //respawn tassello lampeggiante
        player_tile = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/playerTilePrefab.prefab", typeof(GameObject)), new Vector3(x, y, -2), Quaternion.identity);
        player_tile.GetComponent<PlayerTileScript>().PlayerTile(x, y);
        battleManager.unmovedUnits.Add(gameObject);

        Component[] renderers = transform.GetChild(0).GetComponentsInChildren(typeof(Renderer)); //rende normale il personaggio
        foreach (Renderer childRenderer in renderers)
        {
            childRenderer.material.color = new Color(1F, 1F, 1F);
        }

    }


    private bool OnRange = true;



    void Update()
    {
        if (canMove)
        {
            if (dragging)
            {
                Destroy(player_tile);                                                                   //rimuove tassello lampeggiante
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;      //cambia pos

            }
            


        }
        if(!OnRange)
        {
            
            float t = 1 / (new Vector3(x, y, -9)-transform.position).magnitude;                               //traslazione in nuova posizione
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, -9), t*0.1f);
            
            if (transform.position == new Vector3(x, y, -9)) OnRange = true;
        }

        
        
    }

    private void OnMouseDown()
    {   
        if(canMove) 
        {
            this.gameObject.transform.GetChild(0).GetComponent<Animator>().Play("Select");          //inizia animazione di selezione personaggio



            this.HighlightMov();                                                                //spawna tasselli blu movimento
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;

            
        }
        
    }

    
    private void OnMouseUp()
    {
        if (canMove)
        {
            
            dragging = false;
            OnRange = false;
            bool availableMov = false;

            foreach (Vector2 v in mov_tiles_coords)
            {                               //trova se il player e' in una casella blu
                if (v.x == Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x) && v.y == Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y)) { availableMov = true; break; }
            }
            mov_tiles_coords.Clear();
            
            if (availableMov)
            {
                this.x = Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x); //cambia posizione
                this.y = Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y);


                canMove = false;
                battleManager.unmovedUnits.Remove(gameObject);

                Component[] renderers = transform.GetChild(0).GetComponentsInChildren(typeof(Renderer)); //rende grigio il personaggio
                foreach (Renderer childRenderer in renderers)
                {
                    childRenderer.material.color = new Color(0.3F, 0.3F, 0.3F);
                }


            }else {

                foreach( GameObject e in GameObject.FindGameObjectsWithTag("Enemy")){
                    if(Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x)==e.GetComponent<enemyScript>().x && Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y)==e.GetComponent<enemyScript>().y){
                        Camera.main.transform.position=new Vector3(100,0, 0);
                        combat(e,this.GameObject);

                        Debug.Log("PVP");
                    } 
                }
            }
                                      
            foreach (GameObject g in movBlueTiles) Destroy(g);                          //elimina tasselli blu
            movBlueTiles.Clear();
           
       
        }

        this.LevelUp();
    }

    public void LevelUp()
    {
        lvl++;

        int[] increments = { 0, 0, 0, 0, 0, 0, 0, 0 }; //incrementi iniziali
            
        int n1 = Random.Range(0, 8);        //garantisce 1o incremento
        int n2;

        do
        {                                   //garantisce 2o incremento
            n2 = Random.Range(0, 8);
        } while (n1 == n2);

        increments[n1] = 1;
        increments[n2] = 1;

        int[] incrementPercentage = { hpGrowth, strGrowth, magGrowth, dexGrowth, spdGrowth, lckGrowth, defGrowth, resGrowth };


        for(int i = 0; i < 8; i++)
        {
            if (Random.Range(1, 100) <= incrementPercentage[i]) increments[i] = 1;        //se il numero e' minore della % di crescita allora stat incrementa
        }


        maxHp += increments[0];            //incrementa i valori
        str += increments[1];
        mag += increments[2];
        dex += increments[3];
        spd += increments[4];
        lck += increments[5];
        def += increments[6];
        res += increments[7];

        healthbar.SetMaxHealth(maxHp);
        

        //cout temp xdd


        string[] statsNames = { "maxhp", "str", "mag", "dex", "spd", "lck", "def", "res" };
        int[] statsValues = { maxHp, str, mag, dex, spd, lck, def, res };
        Debug.Log("Level Up! "+(lvl-1)+" -> "+lvl);

        for( int i=0; i < 8; i++)
        {
            if (increments[i] > 0)
            {
                Debug.Log(statsNames[i] + " " + (statsValues[i]-1) +" +1 -> " + statsValues[i]);
            }
            else Debug.Log(statsNames[i] + " " + statsValues[i]);
        }

    }

    

}




