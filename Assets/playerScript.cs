using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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

    public bool hasMoved = false;

    public void HighlightMov()                                          //spawna i tasselli blu del movimento
    {
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


    
    void Update()
    {
        if (dragging)
        {            
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            Debug.Log(Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x)+" "+Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y)); 
                       
        }
    }

    private void OnMouseDown()
    {   
        this.HighlightMov();
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    
    private void OnMouseUp()
    {
        
        dragging = false;
    }
}




