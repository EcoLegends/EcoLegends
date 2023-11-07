using System.Collections;
using System.Collections.Generic;
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

    [Space]

    public int hp = 1;                                          
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
                if (p.GetComponent<playerScript>().x == movTiles[i].GetComponent<tileScript>().x && p.GetComponent<playerScript>().y == movTiles[i].GetComponent<tileScript>().y) add_vector = false;
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

                        GameObject temp = map[cx + i, cy + j];
                        if (!movTiles.Contains(map[cx + i, cy + j])) {
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
    private void Start()  //spawna tassello lampeggiante
    {
        transform.position = new Vector3(x, y, -9);
        player_tile = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/playerTilePrefab.prefab", typeof(GameObject)), new Vector3(x, y, -2), Quaternion.identity);
        player_tile.GetComponent<PlayerTileScript>().PlayerTile(x, y);
    }


    private bool OnRange = true;
    private float distance;


    void Update()
    {
        if (canMove)
        {
            if (dragging)
            {
                Destroy(player_tile);                                                                   //rimuove tassello lampeggiante
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;      //cambia pos
                distance = (transform.position - new Vector3(x, y, -9)).magnitude;

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
            {                               //trova se il player � in una casella blu
                if (v.x == Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x) && v.y == Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y)) { availableMov = true; break; }
            }
            mov_tiles_coords.Clear();
{
            if (availableMov)
            {
                this.x = Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x); //cambia posizione
                this.y = Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y);
                     
            }
                                      
            foreach (GameObject g in movBlueTiles) Destroy(g);                          //elimina tasselli blu
            movBlueTiles.Clear();

            canMove = false;

            //temp     VV
            canMove = true;                                                     //respawn tassello lampeggiante
            player_tile = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/playerTilePrefab.prefab", typeof(GameObject)), new Vector3(x, y, -2), Quaternion.identity);
            player_tile.GetComponent<PlayerTileScript>().PlayerTile(x, y);
            }
       
        } 
    }
}




