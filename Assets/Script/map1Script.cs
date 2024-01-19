using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class mapScript : MonoBehaviour
{
    public const int dimX = 10;
    public const int dimY = 10;                         //   1          2        3         4        5         6         7         8          9         10
    public string[,] mapArray = new string[dimX, dimY]{ {"Pianura","Pianura","Foresta","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Foresta"},              //layout mappa
                                                        {"Pianura","Pianura","Pianura","Foresta","Roccia","Roccia","Pianura","Pianura","Foresta","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Roccia","Roccia","Pianura","Foresta","Pianura","Foresta"},
                                                        {"Foresta","Pianura","Pianura","Pianura","Pianura","Pianura","Foresta","Pianura","Foresta","Foresta"},
                                                        {"Roccia","Pianura","Pianura","Roccia","Pianura","Pianura","Pianura","Pianura","Pianura","Foresta"},
                                                        {"Foresta","Foresta","Pianura","Foresta","Pianura","Foresta","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Foresta","Roccia","Foresta","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Foresta","Foresta","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"}};


    public string music = "Preparations Theme";

    public GameObject[,] mapTiles = new GameObject[10,10];
    
    void Start()
    {
        Object musicPlayer = (GameObject)Instantiate(Resources.Load("Music", typeof(GameObject)), Vector3.one, Quaternion.identity);
        musicPlayer.GetComponent<musicScript>().music = music;

        Object tile = Resources.Load("tilePrefab", typeof(GameObject));                                    //spawn dei tasselli mappa
        for (int i = 0; i < dimY; i++)
        {
            for (int j = 0; j < dimX; j++)
            {
                GameObject new_tile = (GameObject) Instantiate(tile, new Vector3(j, dimY-1-i, 1), Quaternion.identity);
                new_tile.transform.parent = transform;
                new_tile.GetComponent<tileScript>().SetTileData(mapArray[i, j], j, dimY - 1 - i);
                mapTiles[j, dimY - 1 - i] = new_tile;
            }
            
        }

        Object player = Resources.Load("player", typeof(GameObject));
        GameObject new_player = (GameObject) Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        new_player.GetComponent<playerScript>().Setup(1, 1, "Byleth", "Diamant", 5, 2, 1, 2, 1, 3, 2, 4, 1, 4, true, 30, 12, 33, 3, 2, 5, 4, 23, 32, 45, 11, 23, 34, 33, 77, 33);

        Object enemy = Resources.Load("enemy", typeof(GameObject));
        GameObject new_enemy = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
        new_enemy.GetComponent<enemyScript>().Setup(8, 8, "Byleth", "Diamant", 5, 2, "move", 1, 2, 1, 3, 2, 4, 1, 4, true, 30, 12, 33, 3, 2, 5, 4, 23, 32, 45, 11, 23, 34, 33, 77, 33);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
