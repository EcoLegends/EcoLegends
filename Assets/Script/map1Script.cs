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
             //x, y, nome, textureFile, lvl, mov, wMinR, weMaxR, wWt, wMt, wHit, wCrit, unitType, wType, wIsMagic, hp, str, mag, dex, spd, lck, def,res, hpG, strG, magG, dexG, spdG, lckG, defG, resG

            Object player = Resources.Load("player", typeof(GameObject));
        GameObject new_player1 = (GameObject) Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        new_player1.GetComponent<playerScript>().Setup(12,1,"Nova","Nova",1,4,1,1,5,5,90,0,1,5,false,26,6,6,6,9,5,5,3,70,65,30,45,75,40,30,15);

        GameObject new_player2 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        new_player2.GetComponent<playerScript>().Setup(14,1,"Sear","Sear",1,4,1,2,3,3,90,0,1,2,true,22,6,6,6,7,6,4,7,45,30,65,45,45,35,25,45);

        Object enemy = Resources.Load("enemy", typeof(GameObject));
        GameObject new_enemy1 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
        new_enemy1.GetComponent<enemyScript>().Setup(12, 5, "???", "Diamant", 1, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 18, 3, 4, 5, 9, 4, 5, 1);
        GameObject new_enemy2 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
        new_enemy2.GetComponent<enemyScript>().Setup(14, 6, "???", "Diamant", 1, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 18, 3, 4, 5, 9, 4, 5, 1);
        GameObject new_enemy3 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
        new_enemy3.GetComponent<enemyScript>().Setup(6, 9, "???", "Diamant", 1, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 18, 3, 4, 5, 9, 4, 5, 1);
        GameObject new_enemy4 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
        new_enemy4.GetComponent<enemyScript>().Setup(7, 10, "???", "Diamant", 1, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 18, 3, 4, 5, 9, 4, 5, 1);
        GameObject new_enemy5 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
        new_enemy5.GetComponent<enemyScript>().Setup(17, 10, "???", "Edelgard", 1, 3, "move", 1, 2, 3, 3, 90, 0, 3, 2, true, 14, 4, 3, 5, 9, 4, 1, 5);
        GameObject new_enemy6 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
        new_enemy6.GetComponent<enemyScript>().Setup(18, 14, "???", "Edelgard", 1, 3, "near", 1, 2, 3, 3, 90, 0, 3, 2, true, 14, 4, 3, 5, 9, 4, 1, 5);
        GameObject new_enemy7 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
        new_enemy7.GetComponent<enemyScript>().Setup(20, 13, "???", "Diamant", 1, 3, "near", 1, 1, 5, 5, 90, 0, 3, 0, false, 18, 3, 4, 5, 9, 4, 5, 1);
        GameObject new_enemy8 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
        new_enemy8.GetComponent<enemyScript>().Setup(19, 15, "???", "Diamant", 1, 3, "near", 1, 1, 5, 5, 90, 0, 3, 0, false, 18, 3, 4, 5, 9, 4, 5, 1);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
