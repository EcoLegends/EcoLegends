using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class mapScript : MonoBehaviour
{
    public const int dimX = 16; //altezza
    public const int dimY = 29; //largezza              //   1        2            3          4       5         6         7         8          9        10          11      12       13        14         15        16        17        18        19        20       21         22        23        24         25       26            27      28        29
    public string[,] mapArray;




    public string music = "Preparations Theme";

    public GameObject[,] mapTiles = new GameObject[dimY, dimX];
    public static int mapN = 1;
    public int mapNum = 1;
    void Start()
    {
        mapN = mapNum;
        Camera.main.gameObject.transform.position = new Vector3(13, 4.5f, -10);


        GameObject mapTexture = new GameObject("mapTexture");
        mapTexture.transform.parent = this.transform;
        mapTexture.transform.position = new Vector3(14.0016f, 7.4975f, -1);
        mapTexture.transform.localScale = new Vector3(4.166355f, 4.165751f, 4.1f);
        mapTexture.AddComponent<SpriteRenderer>();
        mapTexture.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapNum);

        battleManager.mapDimX = dimY;
        battleManager.mapDimY = dimX;

        Object musicPlayer = (GameObject)Instantiate(Resources.Load("Music", typeof(GameObject)), Vector3.one, Quaternion.identity);
        musicPlayer.GetComponent<musicScript>().music = music;
        string path = "Assets/Resources/dati.txt";
        switch (mapNum)
        {
            case 1:
                {
                    mapArray = new string[dimX, dimY]{ {"Pianura","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Pianura","Pianura"},              //layout mappa
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

                    StreamWriter writer = new StreamWriter(path, false);
                    writer.WriteLine("Nova,Nova,1,0,4,1,1,5,5,90,0,1,5,false,26,6,6,6,9,5,5,3,70,65,30,45,75,40,30,15,false");
                    writer.WriteLine("Sear,Sear,1,0,4,1,2,3,3,90,0,1,2,true,22,6,6,6,7,6,4,7,45,30,65,45,45,35,25,45,false");
                    writer.Close();



                    StreamReader reader = new StreamReader(path);
                    string[] nova = reader.ReadLine().Split(",");
                    string[] sear = reader.ReadLine().Split(",");


                    Object player = Resources.Load("player", typeof(GameObject));
                    GameObject new_player1 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player1.GetComponent<playerScript>().Setup(12, 1, nova[0], nova[1], int.Parse(nova[2]), int.Parse(nova[3]), int.Parse(nova[4]), int.Parse(nova[5]), int.Parse(nova[6]), int.Parse(nova[7]), int.Parse(nova[8]), int.Parse(nova[9]), int.Parse(nova[10]), int.Parse(nova[11]), int.Parse(nova[12]), bool.Parse(nova[13]), int.Parse(nova[14]), int.Parse(nova[15]), int.Parse(nova[16]), int.Parse(nova[17]), int.Parse(nova[18]), int.Parse(nova[19]), int.Parse(nova[20]), int.Parse(nova[21]), int.Parse(nova[22]), int.Parse(nova[23]), int.Parse(nova[24]), int.Parse(nova[25]), int.Parse(nova[26]), int.Parse(nova[27]), int.Parse(nova[28]), int.Parse(nova[29]), bool.Parse(nova[30]));

                    GameObject new_player2 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player2.GetComponent<playerScript>().Setup(14, 1, sear[0], sear[1], int.Parse(sear[2]), int.Parse(sear[3]), int.Parse(sear[4]), int.Parse(sear[5]), int.Parse(sear[6]), int.Parse(sear[7]), int.Parse(sear[8]), int.Parse(sear[9]), int.Parse(sear[10]), int.Parse(sear[11]), int.Parse(sear[12]), bool.Parse(sear[13]), int.Parse(sear[14]), int.Parse(sear[15]), int.Parse(sear[16]), int.Parse(sear[17]), int.Parse(sear[18]), int.Parse(sear[19]), int.Parse(sear[20]), int.Parse(sear[21]), int.Parse(sear[22]), int.Parse(sear[23]), int.Parse(sear[24]), int.Parse(sear[25]), int.Parse(sear[26]), int.Parse(sear[27]), int.Parse(sear[28]), int.Parse(sear[29]), bool.Parse(sear[30]));

                    Object enemy = Resources.Load("enemy", typeof(GameObject));
                    GameObject new_enemy1 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy1.GetComponent<enemyScript>().Setup(12, 5, "???", "terra1", 1, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 18, 3, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy2 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy2.GetComponent<enemyScript>().Setup(14, 6, "???", "terra1", 1, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 19, 3, 4, 4, 9, 4, 5, 1);
                    GameObject new_enemy3 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy3.GetComponent<enemyScript>().Setup(6, 9, "???", "terra1", 1, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 17, 3, 4, 5, 9, 5, 5, 1);
                    GameObject new_enemy4 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy4.GetComponent<enemyScript>().Setup(7, 10, "???", "terra1", 1, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 18, 3, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy5 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy5.GetComponent<enemyScript>().Setup(17, 10, "???", "terra2", 1, 3, "move", 1, 2, 3, 3, 90, 0, 3, 2, true, 14, 4, 3, 5, 9, 4, 1, 5);
                    GameObject new_enemy6 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy6.GetComponent<enemyScript>().Setup(18, 14, "???", "terra2", 1, 3, "near", 1, 2, 3, 3, 90, 0, 3, 2, true, 15, 4, 3, 5, 8, 4, 1, 5);
                    GameObject new_enemy7 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy7.GetComponent<enemyScript>().Setup(20, 13, "???", "terra1", 1, 3, "near", 1, 1, 5, 5, 90, 0, 3, 0, false, 18, 3, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy8 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy8.GetComponent<enemyScript>().Setup(19, 15, "???", "terra1", 1, 3, "near", 1, 1, 5, 5, 90, 0, 3, 0, false, 20, 6, 4, 5, 9, 4, 5, 1);
                    break;
                }
            case 2:
                {
                    mapArray = new string[dimX, dimY]{  {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},              //layout mappa
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Roccia","Roccia","Pianura","Pianura","Pianura","Pianura","Roccia","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Roccia","Roccia","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Roccia","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Roccia","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Roccia","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Roccia","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Roccia","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Cespuglio","Cespuglio","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"},
                                                        {"Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura","Pianura"} };



                    StreamReader reader = new StreamReader(path);
                    string[] nova = reader.ReadLine().Split(",");
                    string[] sear = reader.ReadLine().Split(",");


                    Object player = Resources.Load("player", typeof(GameObject));
                    GameObject new_player1 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player1.GetComponent<playerScript>().Setup(11, 1, nova[0], nova[1], int.Parse(nova[2]), int.Parse(nova[3]), int.Parse(nova[4]), int.Parse(nova[5]), int.Parse(nova[6]), int.Parse(nova[7]), int.Parse(nova[8]), int.Parse(nova[9]), int.Parse(nova[10]), int.Parse(nova[11]), int.Parse(nova[12]), bool.Parse(nova[13]), int.Parse(nova[14]), int.Parse(nova[15]), int.Parse(nova[16]), int.Parse(nova[17]), int.Parse(nova[18]), int.Parse(nova[19]), int.Parse(nova[20]), int.Parse(nova[21]), int.Parse(nova[22]), int.Parse(nova[23]), int.Parse(nova[24]), int.Parse(nova[25]), int.Parse(nova[26]), int.Parse(nova[27]), int.Parse(nova[28]), int.Parse(nova[29]), bool.Parse(nova[30]));

                    GameObject new_player2 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player2.GetComponent<playerScript>().Setup(14, 1, sear[0], sear[1], int.Parse(sear[2]), int.Parse(sear[3]), int.Parse(sear[4]), int.Parse(sear[5]), int.Parse(sear[6]), int.Parse(sear[7]), int.Parse(sear[8]), int.Parse(sear[9]), int.Parse(sear[10]), int.Parse(sear[11]), int.Parse(sear[12]), bool.Parse(sear[13]), int.Parse(sear[14]), int.Parse(sear[15]), int.Parse(sear[16]), int.Parse(sear[17]), int.Parse(sear[18]), int.Parse(sear[19]), int.Parse(sear[20]), int.Parse(sear[21]), int.Parse(sear[22]), int.Parse(sear[23]), int.Parse(sear[24]), int.Parse(sear[25]), int.Parse(sear[26]), int.Parse(sear[27]), int.Parse(sear[28]), int.Parse(sear[29]), bool.Parse(sear[30]));


                    Object enemy = Resources.Load("enemy", typeof(GameObject));
                    GameObject new_enemy1 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy1.GetComponent<enemyScript>().Setup(9, 9, "???", "terra1", 2, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 20, 3, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy2 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy2.GetComponent<enemyScript>().Setup(14, 9, "???", "terra1", 2, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 20, 3, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy3 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy3.GetComponent<enemyScript>().Setup(19, 10, "???", "terra2", 1, 3, "move", 1, 2, 3, 3, 90, 0, 3, 2, true, 17, 4, 3, 5, 9, 4, 1, 5);
                    GameObject new_enemy4 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy4.GetComponent<enemyScript>().Setup(22, 11, "???", "terra1", 2, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 20, 3, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy5 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy5.GetComponent<enemyScript>().Setup(4, 12, "???", "terra1", 2, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 20, 3, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy6 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy6.GetComponent<enemyScript>().Setup(12, 14, "???", "terra2", 1, 3, "near", 1, 2, 3, 3, 90, 0, 3, 2, true, 17, 4, 3, 5, 9, 4, 1, 5);
                    GameObject new_enemy7 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy7.GetComponent<enemyScript>().Setup(19, 14, "???", "terra2", 1, 3, "move", 1, 2, 3, 3, 90, 0, 3, 2, true, 17, 4, 3, 5, 9, 4, 1, 5);
                    GameObject new_enemy8 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy8.GetComponent<enemyScript>().Setup(25, 14, "???", "terra1", 2, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 20, 3, 4, 5, 9, 4, 5, 1);

                    GameObject new_enemy9 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy9.GetComponent<enemyScript>().Setup(10, 13, "Granius", "Granius_evil", 3, 3, "near", 1, 2, 6, 6, 90, 0, 3, 4, false, 25, 9, 3, 7, 10, 7, 5, 2, "Inconsistant One");
                    GameObject new_enemy10 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy10.GetComponent<enemyScript>().Setup(15, 13, "Thera", "Thera_evil", 3, 3, "near", 1, 1, 5, 5, 90, 0, 3, 7, false, 29, 10, 5, 5, 8, 6, 6, 3, "Inconsistant One");
                    break;
                }
            
        }






        this.name = "map";

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

        

    }

}
