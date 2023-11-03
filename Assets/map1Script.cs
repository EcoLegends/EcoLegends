using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class mapScript : MonoBehaviour
{
    public string[,] mapArray = new string[10,10] {  {"Pianura","Pianura","Pianura","Roccia","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},              //layout mappa
                                                     {"Roccia","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                     {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                     {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                     {"Pianura","Pianura","Pianura","Roccia","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                     {"Pianura","Pianura","Pianura","Pianura","Pianura","Roccia","Roccia","Roccia","Pianura","Pianura"},
                                                     {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                     {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                     {"Pianura","Roccia","Pianura","Pianura","Pianura","Pianura","Roccia","Pianura","Pianura","Pianura"},
                                                     {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Roccia"}};


    public GameObject[,] mapTiles = new GameObject[10,10];
    
    void Start()
    {
        Object tile = AssetDatabase.LoadAssetAtPath("Assets/tilePrefab.prefab", typeof(GameObject));                                    //spawn dei tasselli mappa
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject new_tile = (GameObject) Instantiate(tile, new Vector3(i, j, 1), Quaternion.identity);
                new_tile.transform.parent = transform;
                new_tile.GetComponent<tileScript>().setTileData(mapArray[i, j], i, j);
                mapTiles[i, j] = new_tile;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
