using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class mapScript : MonoBehaviour
{
    public const int dimX = 16; //altezza
    public const int dimY = 29; //largezza              //   1        2            3          4       5         6         7         8          9        10          11      12       13        14         15        16        17        18        19        20       21         22        23        24         25       26            27      28        29
    public string[,] mapArray = new string[dimX, dimY]{ {"Pianura","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Pianura","Pianura"},              //layout mappa
                                                        {"Pianura","Cespuglio","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Cespuglio","Pianura"},
                                                        {"Pianura","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Pianura"},
                                                        {"Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Terriccio","Terriccio","Terriccio","Terriccio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Basalto","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Basalto","Basalto","Basalto","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Pianura","Cespuglio","Pianura","Terriccio","Terriccio","Terriccio","Terriccio","Pianura","Terriccio","Terriccio","Terriccio","Pianura","Terriccio","Terriccio","Terriccio","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Basalto","Basalto","Basalto","Basalto","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio","Terriccio"},
                                                        {"Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Lava","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto"},
                                                        {"Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Lava","Lava","Lava"},
                                                        {"Basalto","Basalto","Lava","Lava","Lava","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Lava","Lava","Lava"},
                                                        {"Basalto","Basalto","Lava","Lava","Lava","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Lava","Basalto","Basalto","Basalto","Lava","Lava","Lava"},
                                                        {"Basalto","Basalto","Lava","Lava","Lava","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto","Basalto"} };





    public string music = "Preparations Theme";

    public GameObject[,] mapTiles = new GameObject[dimY, dimX];
    
    void Start()
    {
        Camera.main.gameObject.transform.position = new Vector3(13, 4.5f, -10);


        GameObject mapTexture = new GameObject("mapTexture");
        mapTexture.transform.parent = this.transform;
        mapTexture.transform.position = new Vector3(14.0016f, 7.4975f, -1);
        mapTexture.transform.localScale = new Vector3(4.166355f, 4.165751f, 4.1f);
        mapTexture.AddComponent<SpriteRenderer>();
        mapTexture.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa1");

        battleManager.mapDimX = dimY;
        battleManager.mapDimY = dimX;

        Object musicPlayer = (GameObject)Instantiate(Resources.Load("Music", typeof(GameObject)), Vector3.one, Quaternion.identity);
        musicPlayer.GetComponent<musicScript>().music = music;

        Object tile = Resources.Load("tilePrefab", typeof(GameObject));                                    //spawn dei tasselli mappa
        for (int i = 0; i < dimX; i++)
        {
            for (int j = 0; j < dimY; j++)
            {
                GameObject new_tile = (GameObject) Instantiate(tile, new Vector3(j, dimX-1-i, 1), Quaternion.identity);
                new_tile.transform.parent = transform;
                new_tile.GetComponent<tileScript>().SetTileData(mapArray[i, j], j, dimX - 1 - i);
                Debug.Log(j +" "+ (dimX - 1 - i));
                mapTiles[j, dimX - 1 - i] = new_tile;
            }
            
        }

        //Object player = Resources.Load("player", typeof(GameObject));
        //GameObject new_player = (GameObject) Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        //new_player.GetComponent<playerScript>().Setup(1, 1, "Byleth", "Diamant", 5, 2, 1, 2, 1, 3, 2, 4, 1, 4, true, 30, 12, 33, 3, 2, 5, 4, 23, 32, 45, 11, 23, 34, 33, 77, 33);

        //Object enemy = Resources.Load("enemy", typeof(GameObject));
        //GameObject new_enemy = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
        //new_enemy.GetComponent<enemyScript>().Setup(8, 8, "Byleth", "Diamant", 5, 2, "move", 1, 2, 1, 3, 2, 4, 1, 4, true, 30, 12, 33, 3, 2, 5, 4, 23, 32, 45, 11, 23, 34, 33, 77, 33);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
