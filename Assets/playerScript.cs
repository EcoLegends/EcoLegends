using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;

    [Tooltip("Movimento")]                                            //statistiche
    public int movement = 3;
    [Tooltip("Pos X")]
    public int x = 0;
    [Tooltip("Pos Y")]
    public int y = 0;
    [Tooltip("Può muoversi")]
    public bool canMove = true;

    private List<Vector2> mov_tiles_coords = new List<Vector2>();
    private List<GameObject> movBlueTiles;
    public void HighlightMov()                                          //spawna i tasselli blu del movimento
    {
        movBlueTiles = new List<GameObject>();
        List<GameObject> movTiles = new List<GameObject>();
        List<int> movTilesDistance = new List<int>();
        GameObject[,] map = GameObject.Find("map").GetComponent<mapScript>().mapTiles;

        movTiles.Add(map[x,y]);
        movTilesDistance.Add(999);
        AdjCheck(x, y, movement, ref map, ref movTiles, ref movTilesDistance);

        Object mov_tile_prefab = AssetDatabase.LoadAssetAtPath("Assets/movTilePrefab.prefab", typeof(GameObject));

                
        for (int i = 0; i < movTiles.Count; i++)
        {
            GameObject mov_tile = (GameObject)Instantiate(mov_tile_prefab, new Vector3(movTiles[i].GetComponent<tileScript>().x, movTiles[i].GetComponent<tileScript>().y, -2), Quaternion.identity);
            mov_tile.transform.parent = movTiles[i].transform;
            mov_tiles_coords.Add(new Vector2(movTiles[i].GetComponent<tileScript>().x, movTiles[i].GetComponent<tileScript>().y));
            movBlueTiles.Add(mov_tile);
        }
    }

    private void AdjCheck(int cx, int cy, int mov, ref GameObject[,] map, ref List<GameObject> movTiles, ref List<int> movTilesDistance)            //trova le caselle adiacenti
    {
        int i = -1;
        int j = 0;
        if(mov > 0)
        {
            for(int a = 0; a<4; a++)
            {
                
                
                if(cx+i>=0 && cx+i<10 && cy + j >= 0 && cy + j < 10)
                {
                    if (map[cx + i, cy + j].GetComponent<tileScript>().canBeWalkedOn == true)
                    {
                            
                        GameObject temp = map[cx + i, cy + j];
                        if (!movTiles.Contains(map[cx + i, cy + j])) {
                            movTiles.Add(map[cx + i, cy + j]);
                            movTilesDistance.Add(mov);
                            Debug.Log("Added " + (cx + i) + "-" + (cy + j) + " with distance " + mov+" from " + (cx) + "-" + (cy));
                            AdjCheck(cx + i, cy + j, mov - map[cx + i, cy + j].GetComponent<tileScript>().travelCost, ref map, ref movTiles, ref movTilesDistance);
                                
                        }
                        else if (movTilesDistance[movTiles.FindIndex(n => n.Equals(temp))] < mov)
                        {
                            movTilesDistance[movTiles.FindIndex(n => n.Equals(temp))] = mov;
                            Debug.Log("Updated " + (cx + i) + "-" + (cy + j) + " with distance " + mov);
                            AdjCheck(cx + i, cy + j, mov - map[cx + i, cy + j].GetComponent<tileScript>().travelCost, ref map, ref movTiles, ref movTilesDistance);
                        }
                           

                            
                    }
                }

                if (i < 1 && j == 0) i=1;
                else if (i == 1)
                {
                    i = 0;
                    j = -1;
                }
                else j=1;
                
            }
            
        }
        
    }
    GameObject player_tile;
    private void Start()
    {
       
        player_tile = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/playerTilePrefab.prefab", typeof(GameObject)), new Vector3(x, y, -2), Quaternion.identity);
        player_tile.GetComponent<PlayerTileScript>().PlayerTile(x, y);
    }

   
    void Update()
    {
        if (canMove)
        {
            if (dragging)
            {
                Destroy(player_tile);
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            }
        }
        
        
    }

    private void OnMouseDown()
    {   
        if(canMove) 
        {
            this.HighlightMov();
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
        }
        
    }

    
    private void OnMouseUp()
    {
        if (canMove)
        {
             dragging = false;
        bool availableMov = false;
        foreach (Vector2 v in mov_tiles_coords) {
            if (v.x == Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x)  && v.y == Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y)) { availableMov = true; break; }
        }
        mov_tiles_coords.Clear();

        if (availableMov)
        {
            this.x = Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x);
            this.y = Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y);
        }
        transform.position = new Vector3(x, y, -9);

        foreach (GameObject g in movBlueTiles) Destroy(g);
        movBlueTiles.Clear();

        canMove = false;

        //temp     VV
        canMove = true;
        player_tile = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/playerTilePrefab.prefab", typeof(GameObject)), new Vector3(x, y, -2), Quaternion.identity);
        player_tile.GetComponent<PlayerTileScript>().PlayerTile(x, y);
        }
       
    }
}




