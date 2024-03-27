using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class mapScript : MonoBehaviour
{
    public const int dimX = 16; //altezza
    public const int dimY = 29; //largezza             
    public string[,] mapArray;

    public static bool finitoSpawn = false;


    public string music = "Preparations Theme";

    public GameObject[,] mapTiles = new GameObject[dimY, dimX];
    public static int mapN = 1;
    public int mapNum = 1;
    void Awake()
    {
        string p = Application.streamingAssetsPath+"/mappaScelta.txt";
        StreamReader r = new StreamReader(p);
        string l = r.ReadLine();
        
        mapNum =int.Parse(l);
        r.Close();

        r = new StreamReader(Application.streamingAssetsPath+"/completedMaps.txt");
        l = r.ReadLine();

        int mapCompleted = int.Parse(l);
        r.Close();
        mapN = mapNum;

        string[] musicArr = { "The Shackled Wolves", "Salvation And Loss", "The Apex of The World", "Trial of Heroes" };
        music = musicArr[mapNum-1];

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
        string path = Application.streamingAssetsPath+"/dati.txt";
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

                    if (mapCompleted <= mapNum)
                    {
                        File.WriteAllText(path, string.Empty);
                        StreamWriter writer = new StreamWriter(path, false);
                        writer.WriteLine("Nova,Nova,1,0,4,1,1,5,5,90,5,1,5,false,26,6,6,6,9,5,5,3,70,65,30,45,75,40,30,15,false");
                        writer.WriteLine("Sear,Sear,1,0,4,1,2,3,3,90,5,1,2,true,22,6,6,6,7,6,4,7,45,30,65,45,45,35,25,45,false");
                        writer.Close();
                    }
                    GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial0"), Camera.main.gameObject.transform);

                    StreamReader reader = new StreamReader(path);
                    string[] nova = reader.ReadLine().Split(",");
                    string[] sear = reader.ReadLine().Split(",");
                    reader.Close();

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
                    reader.Close();

                    Object player = Resources.Load("player", typeof(GameObject));
                    GameObject new_player1 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player1.GetComponent<playerScript>().Setup(11, 1, nova[0], nova[1], int.Parse(nova[2]), int.Parse(nova[3]), int.Parse(nova[4]), int.Parse(nova[5]), int.Parse(nova[6]), int.Parse(nova[7]), int.Parse(nova[8]), int.Parse(nova[9]), int.Parse(nova[10]), int.Parse(nova[11]), int.Parse(nova[12]), bool.Parse(nova[13]), int.Parse(nova[14]), int.Parse(nova[15]), int.Parse(nova[16]), int.Parse(nova[17]), int.Parse(nova[18]), int.Parse(nova[19]), int.Parse(nova[20]), int.Parse(nova[21]), int.Parse(nova[22]), int.Parse(nova[23]), int.Parse(nova[24]), int.Parse(nova[25]), int.Parse(nova[26]), int.Parse(nova[27]), int.Parse(nova[28]), int.Parse(nova[29]), bool.Parse(nova[30]));

                    GameObject new_player2 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player2.GetComponent<playerScript>().Setup(14, 1, sear[0], sear[1], int.Parse(sear[2]), int.Parse(sear[3]), int.Parse(sear[4]), int.Parse(sear[5]), int.Parse(sear[6]), int.Parse(sear[7]), int.Parse(sear[8]), int.Parse(sear[9]), int.Parse(sear[10]), int.Parse(sear[11]), int.Parse(sear[12]), bool.Parse(sear[13]), int.Parse(sear[14]), int.Parse(sear[15]), int.Parse(sear[16]), int.Parse(sear[17]), int.Parse(sear[18]), int.Parse(sear[19]), int.Parse(sear[20]), int.Parse(sear[21]), int.Parse(sear[22]), int.Parse(sear[23]), int.Parse(sear[24]), int.Parse(sear[25]), int.Parse(sear[26]), int.Parse(sear[27]), int.Parse(sear[28]), int.Parse(sear[29]), bool.Parse(sear[30]));


                    Object enemy = Resources.Load("enemy", typeof(GameObject));
                    GameObject new_enemy1 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy1.GetComponent<enemyScript>().Setup(9, 9, "???", "terra1", 2, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 21, 3, 4, 5, 9, 4, 4, 1);
                    GameObject new_enemy2 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy2.GetComponent<enemyScript>().Setup(14, 9, "???", "terra1", 2, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 20, 3, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy3 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy3.GetComponent<enemyScript>().Setup(19, 10, "???", "terra2", 2, 3, "move", 1, 2, 3, 3, 90, 0, 3, 2, true, 17, 4, 3, 5, 9, 4, 1, 5);
                    GameObject new_enemy4 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy4.GetComponent<enemyScript>().Setup(22, 11, "???", "terra1", 2, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 19, 4, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy5 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy5.GetComponent<enemyScript>().Setup(4, 12, "???", "terra1", 2, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 20, 3, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy6 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy6.GetComponent<enemyScript>().Setup(12, 14, "???", "terra2", 2, 3, "near", 1, 2, 3, 3, 90, 0, 3, 2, true, 18, 4, 3, 5, 9, 4, 1, 5);
                    GameObject new_enemy7 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy7.GetComponent<enemyScript>().Setup(19, 14, "???", "terra2", 2, 3, "move", 1, 2, 3, 3, 90, 0, 3, 2, true, 17, 4, 3, 5, 9, 4, 1, 5);
                    GameObject new_enemy8 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy8.GetComponent<enemyScript>().Setup(25, 14, "???", "terra1", 2, 3, "move", 1, 1, 5, 5, 90, 0, 3, 0, false, 20, 3, 4, 5, 9, 4, 5, 1);

                    GameObject new_enemy9 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy9.GetComponent<enemyScript>().Setup(10, 13, "Granius?", "Granius", 3, 3, "near", 1, 2, 6, 6, 90, 0, 3, 4, false, 25, 7, 3, 7, 10, 7, 5, 2, "Inconsistant One");
                    GameObject new_enemy10 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy10.GetComponent<enemyScript>().Setup(15, 13, "Thera?", "Thera", 3, 3, "near", 1, 1, 5, 5, 70, 0, 3, 7, false, 29, 10, 5, 5, 8, 6, 6, 3, "Inconsistant One");
                    break;
                }

            case 3:
                {                                     //                       3                  5              7                 9                  11                13              15                   17              19                21                  23               25               27                  29 
                    mapArray = new string[dimX, dimY]{  {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia"},              //layout mappa
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Cespuglio","Cespuglio","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Cespuglio","Cespuglio","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia"},
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Acqua","Acqua","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia"},
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia"},
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia"}, 
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Cespuglio","Cespuglio","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Cespuglio","Sabbia"},
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Cespuglio","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia"},
                                                        {"Sabbia","Sabbia","Cespuglio","Cespuglio","Cespuglio","Sabbia","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Cespuglio","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Sabbia","Sabbia","Sabbia","Sabbia"},
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Cespuglio","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Sabbia"},
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Acqua","Acqua","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia"},
                                                        {"Sabbia","Sabbia","Sabbia","Acqua","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Acqua","Acqua","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Acqua","Sabbia","Sabbia","Sabbia"},
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Cespuglio","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia"},
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Cespuglio","Cespuglio","Cespuglio","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia"},
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Cespuglio","Sabbia"},
                                                        {"Sabbia","Sabbia","Acqua","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Cespuglio","Cespuglio","Sabbia","Sabbia","Sabbia"},
                                                        {"Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia","Sabbia"} };
                    
                    StreamReader reader = new StreamReader(path);
                    string[] nova = reader.ReadLine().Split(",");
                    string[] sear = reader.ReadLine().Split(",");
                    string[] granius = reader.ReadLine().Split(",");
                    string[] thera = reader.ReadLine().Split(",");
                    reader.Close();

                    Object player = Resources.Load("player", typeof(GameObject));
                    GameObject new_player1 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player1.GetComponent<playerScript>().Setup(13, 2, nova[0], nova[1], int.Parse(nova[2]), int.Parse(nova[3]), int.Parse(nova[4]), int.Parse(nova[5]), int.Parse(nova[6]), int.Parse(nova[7]), int.Parse(nova[8]), int.Parse(nova[9]), int.Parse(nova[10]), int.Parse(nova[11]), int.Parse(nova[12]), bool.Parse(nova[13]), int.Parse(nova[14]), int.Parse(nova[15]), int.Parse(nova[16]), int.Parse(nova[17]), int.Parse(nova[18]), int.Parse(nova[19]), int.Parse(nova[20]), int.Parse(nova[21]), int.Parse(nova[22]), int.Parse(nova[23]), int.Parse(nova[24]), int.Parse(nova[25]), int.Parse(nova[26]), int.Parse(nova[27]), int.Parse(nova[28]), int.Parse(nova[29]), bool.Parse(nova[30]));

                    GameObject new_player2 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player2.GetComponent<playerScript>().Setup(10, 1, sear[0], sear[1], int.Parse(sear[2]), int.Parse(sear[3]), int.Parse(sear[4]), int.Parse(sear[5]), int.Parse(sear[6]), int.Parse(sear[7]), int.Parse(sear[8]), int.Parse(sear[9]), int.Parse(sear[10]), int.Parse(sear[11]), int.Parse(sear[12]), bool.Parse(sear[13]), int.Parse(sear[14]), int.Parse(sear[15]), int.Parse(sear[16]), int.Parse(sear[17]), int.Parse(sear[18]), int.Parse(sear[19]), int.Parse(sear[20]), int.Parse(sear[21]), int.Parse(sear[22]), int.Parse(sear[23]), int.Parse(sear[24]), int.Parse(sear[25]), int.Parse(sear[26]), int.Parse(sear[27]), int.Parse(sear[28]), int.Parse(sear[29]), bool.Parse(sear[30]));

                    GameObject new_player3 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player3.GetComponent<playerScript>().Setup(19, 1, granius[0], granius[1], int.Parse(granius[2]), int.Parse(granius[3]), int.Parse(granius[4]), int.Parse(granius[5]), int.Parse(granius[6]), int.Parse(granius[7]), int.Parse(granius[8]), int.Parse(granius[9]), int.Parse(granius[10]), int.Parse(granius[11]), int.Parse(granius[12]), bool.Parse(granius[13]), int.Parse(granius[14]), int.Parse(granius[15]), int.Parse(granius[16]), int.Parse(granius[17]), int.Parse(granius[18]), int.Parse(granius[19]), int.Parse(granius[20]), int.Parse(granius[21]), int.Parse(granius[22]), int.Parse(granius[23]), int.Parse(granius[24]), int.Parse(granius[25]), int.Parse(granius[26]), int.Parse(granius[27]), int.Parse(granius[28]), int.Parse(granius[29]), bool.Parse(granius[30]));

                    GameObject new_player4 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player4.GetComponent<playerScript>().Setup(16, 2, thera[0], thera[1], int.Parse(thera[2]), int.Parse(thera[3]), int.Parse(thera[4]), int.Parse(thera[5]), int.Parse(thera[6]), int.Parse(thera[7]), int.Parse(thera[8]), int.Parse(thera[9]), int.Parse(thera[10]), int.Parse(thera[11]), int.Parse(thera[12]), bool.Parse(thera[13]), int.Parse(thera[14]), int.Parse(thera[15]), int.Parse(thera[16]), int.Parse(thera[17]), int.Parse(thera[18]), int.Parse(thera[19]), int.Parse(thera[20]), int.Parse(thera[21]), int.Parse(thera[22]), int.Parse(thera[23]), int.Parse(thera[24]), int.Parse(thera[25]), int.Parse(thera[26]), int.Parse(thera[27]), int.Parse(thera[28]), int.Parse(thera[29]), bool.Parse(thera[30]));


                    Object enemy = Resources.Load("enemy", typeof(GameObject));
                    GameObject new_enemy1 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy1.GetComponent<enemyScript>().Setup(12, 11, "???", "acqua1", 3, 3, "move", 1, 1, 5, 5, 90, 0, 2, 0, false, 24, 4, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy2 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy2.GetComponent<enemyScript>().Setup(16, 11, "???", "acqua1", 3, 3, "move", 1, 1, 5, 5, 90, 0, 2, 0, false, 23, 3, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy3 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy3.GetComponent<enemyScript>().Setup(1, 12, "???", "acqua1", 3, 3, "move", 1, 1, 5, 5, 90, 0, 2, 0, false, 24, 5, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy4 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy4.GetComponent<enemyScript>().Setup(6, 12, "???", "acqua1", 3, 3, "move", 1, 1, 5, 5, 90, 0, 2, 0, false, 24, 3, 4, 5, 9, 4, 5, 1);
                    GameObject new_enemy5 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy5.GetComponent<enemyScript>().Setup(19, 12, "???", "acqua1", 3, 3, "move", 1, 1, 5, 5, 90, 0, 2, 0, false, 24, 3, 4, 5, 9, 4, 5, 4);
                    GameObject new_enemy6 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy6.GetComponent<enemyScript>().Setup(3, 14, "???", "acqua2", 3, 3, "move", 1, 2, 3, 3, 90, 0, 2, 2, true, 20, 4, 5, 5, 9, 4, 3, 5);
                    GameObject new_enemy7 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy7.GetComponent<enemyScript>().Setup(26, 14, "???", "acqua2", 3, 3, "move", 1, 2, 3, 3, 90, 0, 2, 2, true, 20, 4, 4, 5, 9, 4, 2, 5);
                    GameObject new_enemy8 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy8.GetComponent<enemyScript>().Setup(8, 15, "???", "acqua2", 3, 4, "near", 1, 2, 3, 3, 90, 0, 2, 2, true, 20, 4, 3, 5, 9, 4, 4, 5);
                    GameObject new_enemy9 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy9.GetComponent<enemyScript>().Setup(18, 15, "???", "acqua2", 3, 4, "near", 1, 2, 3, 3, 90, 0, 2, 2, true, 20, 4, 6, 5, 9, 4, 1, 5);


                    GameObject new_enemy10 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);                                 //sylvain stats
                    new_enemy10.GetComponent<enemyScript>().Setup(11, 14, "Acquira?", "Acquira_evil", 4, 3, "near", 1, 1, 6, 6, 90, 0, 2, 6, false, 30, 12, 5, 5, 8, 6, 6, 3, "The Ashen Demon");
                    GameObject new_enemy11 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);                                 //linhardt stats
                    new_enemy11.GetComponent<enemyScript>().Setup(15, 14, "Hydris?", "Hydris_evil", 4, 3, "near", 1, 2, 5, 5, 90, 0, 2, 1, true, 26, 5, 10, 6, 7, 7, 5, 9, "The Ashen Demon");
                    break;
                }
            case 4:
                {
                    GameObject.Find("Main Camera").transform.localPosition = new Vector3(11.23636f, 8.305455f, -10);
                    GameObject.Find("Main Camera").transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                    GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize = 6.5f;
                    //                              3                  5                       7                    9                     11                   13                    15                     17                   19                    21                    23                    25                    27                  29 
                    mapArray = new string[dimX, dimY]{  {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"},   //           //layout mappa
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"}, //
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Roccia","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Foresta","Foresta","Foresta","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Foresta","Ciottoli","Ciottoli"},//
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Roccia","Roccia","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Foresta","Foresta","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Roccia","Roccia","Ciottoli","Ciottoli","Ciottoli","Foresta","Foresta","Ciottoli"},//
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Foresta","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"},//
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"},//
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Roccia","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"},//
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"},//
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"},//
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"},//
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"},//
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Roccia","Roccia","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"},
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Foresta","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Roccia","Ciottoli","Ciottoli","Ciottoli","Roccia","Roccia","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"},
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Foresta","Foresta","Ciottoli","Ciottoli","Ciottoli","Roccia","Roccia","Roccia","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Foresta","Foresta","Ciottoli","Roccia","Roccia","Ciottoli","Ciottoli","Ciottoli"},
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Roccia","Ciottoli","Ciottoli","Ciottoli"},
                                                        {"Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli","Ciottoli"} };

                    GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial30"), Camera.main.gameObject.transform);

                    StreamReader reader = new StreamReader(path);
                    string[] nova = reader.ReadLine().Split(",");
                    string[] sear = reader.ReadLine().Split(",");
                    string[] granius = reader.ReadLine().Split(",");
                    string[] thera = reader.ReadLine().Split(",");
                    string[] acquira = reader.ReadLine().Split(",");
                    string[] hydris = reader.ReadLine().Split(",");
                    string[] aeria = reader.ReadLine().Split(",");
                    string[] skye = reader.ReadLine().Split(",");
                    reader.Close();

                    Object player = Resources.Load("player", typeof(GameObject));
                    GameObject new_player1 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player1.GetComponent<playerScript>().Setup(1, 9, nova[0], nova[1], int.Parse(nova[2]), int.Parse(nova[3]), int.Parse(nova[4]), int.Parse(nova[5]), int.Parse(nova[6]), int.Parse(nova[7]), int.Parse(nova[8]), int.Parse(nova[9]), int.Parse(nova[10]), int.Parse(nova[11]), int.Parse(nova[12]), bool.Parse(nova[13]), int.Parse(nova[14]), int.Parse(nova[15]), int.Parse(nova[16]), int.Parse(nova[17]), int.Parse(nova[18]), int.Parse(nova[19]), int.Parse(nova[20]), int.Parse(nova[21]), int.Parse(nova[22]), int.Parse(nova[23]), int.Parse(nova[24]), int.Parse(nova[25]), int.Parse(nova[26]), int.Parse(nova[27]), int.Parse(nova[28]), int.Parse(nova[29]), bool.Parse(nova[30]));

                    GameObject new_player2 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player2.GetComponent<playerScript>().Setup(1, 7, sear[0], sear[1], int.Parse(sear[2]), int.Parse(sear[3]), int.Parse(sear[4]), int.Parse(sear[5]), int.Parse(sear[6]), int.Parse(sear[7]), int.Parse(sear[8]), int.Parse(sear[9]), int.Parse(sear[10]), int.Parse(sear[11]), int.Parse(sear[12]), bool.Parse(sear[13]), int.Parse(sear[14]), int.Parse(sear[15]), int.Parse(sear[16]), int.Parse(sear[17]), int.Parse(sear[18]), int.Parse(sear[19]), int.Parse(sear[20]), int.Parse(sear[21]), int.Parse(sear[22]), int.Parse(sear[23]), int.Parse(sear[24]), int.Parse(sear[25]), int.Parse(sear[26]), int.Parse(sear[27]), int.Parse(sear[28]), int.Parse(sear[29]), bool.Parse(sear[30]));

                    GameObject new_player3 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player3.GetComponent<playerScript>().Setup(1, 13, granius[0], granius[1], int.Parse(granius[2]), int.Parse(granius[3]), int.Parse(granius[4]), int.Parse(granius[5]), int.Parse(granius[6]), int.Parse(granius[7]), int.Parse(granius[8]), int.Parse(granius[9]), int.Parse(granius[10]), int.Parse(granius[11]), int.Parse(granius[12]), bool.Parse(granius[13]), int.Parse(granius[14]), int.Parse(granius[15]), int.Parse(granius[16]), int.Parse(granius[17]), int.Parse(granius[18]), int.Parse(granius[19]), int.Parse(granius[20]), int.Parse(granius[21]), int.Parse(granius[22]), int.Parse(granius[23]), int.Parse(granius[24]), int.Parse(granius[25]), int.Parse(granius[26]), int.Parse(granius[27]), int.Parse(granius[28]), int.Parse(granius[29]), bool.Parse(granius[30]));

                    GameObject new_player4 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player4.GetComponent<playerScript>().Setup(1, 11, thera[0], thera[1], int.Parse(thera[2]), int.Parse(thera[3]), int.Parse(thera[4]), int.Parse(thera[5]), int.Parse(thera[6]), int.Parse(thera[7]), int.Parse(thera[8]), int.Parse(thera[9]), int.Parse(thera[10]), int.Parse(thera[11]), int.Parse(thera[12]), bool.Parse(thera[13]), int.Parse(thera[14]), int.Parse(thera[15]), int.Parse(thera[16]), int.Parse(thera[17]), int.Parse(thera[18]), int.Parse(thera[19]), int.Parse(thera[20]), int.Parse(thera[21]), int.Parse(thera[22]), int.Parse(thera[23]), int.Parse(thera[24]), int.Parse(thera[25]), int.Parse(thera[26]), int.Parse(thera[27]), int.Parse(thera[28]), int.Parse(thera[29]), bool.Parse(thera[30]));

                    GameObject new_player5 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player5.GetComponent<playerScript>().Setup(1, 5, acquira[0], acquira[1], int.Parse(acquira[2]), int.Parse(acquira[3]), int.Parse(acquira[4]), int.Parse(acquira[5]), int.Parse(acquira[6]), int.Parse(acquira[7]), int.Parse(acquira[8]), int.Parse(acquira[9]), int.Parse(acquira[10]), int.Parse(acquira[11]), int.Parse(acquira[12]), bool.Parse(acquira[13]), int.Parse(acquira[14]), int.Parse(acquira[15]), int.Parse(acquira[16]), int.Parse(acquira[17]), int.Parse(acquira[18]), int.Parse(acquira[19]), int.Parse(acquira[20]), int.Parse(acquira[21]), int.Parse(acquira[22]), int.Parse(acquira[23]), int.Parse(acquira[24]), int.Parse(acquira[25]), int.Parse(acquira[26]), int.Parse(acquira[27]), int.Parse(acquira[28]), int.Parse(acquira[29]), bool.Parse(acquira[30]));

                    GameObject new_player6 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player6.GetComponent<playerScript>().Setup(1, 3, hydris[0], hydris[1], int.Parse(hydris[2]), int.Parse(hydris[3]), int.Parse(hydris[4]), int.Parse(hydris[5]), int.Parse(hydris[6]), int.Parse(hydris[7]), int.Parse(hydris[8]), int.Parse(hydris[9]), int.Parse(hydris[10]), int.Parse(hydris[11]), int.Parse(hydris[12]), bool.Parse(hydris[13]), int.Parse(hydris[14]), int.Parse(hydris[15]), int.Parse(hydris[16]), int.Parse(hydris[17]), int.Parse(hydris[18]), int.Parse(hydris[19]), int.Parse(hydris[20]), int.Parse(hydris[21]), int.Parse(hydris[22]), int.Parse(hydris[23]), int.Parse(hydris[24]), int.Parse(hydris[25]), int.Parse(hydris[26]), int.Parse(hydris[27]), int.Parse(hydris[28]), int.Parse(hydris[29]), bool.Parse(hydris[30]));

                    GameObject new_player7 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player7.GetComponent<playerScript>().Setup(13, 13, aeria[0], aeria[1], int.Parse(aeria[2]), int.Parse(aeria[3]), int.Parse(aeria[4]), int.Parse(aeria[5]), int.Parse(aeria[6]), int.Parse(aeria[7]), int.Parse(aeria[8]), int.Parse(aeria[9]), int.Parse(aeria[10]), int.Parse(aeria[11]), int.Parse(aeria[12]), bool.Parse(aeria[13]), int.Parse(aeria[14]), int.Parse(aeria[15]), int.Parse(aeria[16]), int.Parse(aeria[17]), int.Parse(aeria[18]), int.Parse(aeria[19]), int.Parse(aeria[20]), int.Parse(aeria[21]), int.Parse(aeria[22]), int.Parse(aeria[23]), int.Parse(aeria[24]), int.Parse(aeria[25]), int.Parse(aeria[26]), int.Parse(aeria[27]), int.Parse(aeria[28]), int.Parse(aeria[29]), bool.Parse(aeria[30]));

                    GameObject new_player8 = (GameObject)Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                    new_player8.GetComponent<playerScript>().Setup(11, 12, skye[0], skye[1], int.Parse(skye[2]), int.Parse(skye[3]), int.Parse(skye[4]), int.Parse(skye[5]), int.Parse(skye[6]), int.Parse(skye[7]), int.Parse(skye[8]), int.Parse(skye[9]), int.Parse(skye[10]), int.Parse(skye[11]), int.Parse(skye[12]), bool.Parse(skye[13]), int.Parse(skye[14]), int.Parse(skye[15]), int.Parse(skye[16]), int.Parse(skye[17]), int.Parse(skye[18]), int.Parse(skye[19]), int.Parse(skye[20]), int.Parse(skye[21]), int.Parse(skye[22]), int.Parse(skye[23]), int.Parse(skye[24]), int.Parse(skye[25]), int.Parse(skye[26]), int.Parse(skye[27]), int.Parse(skye[28]), int.Parse(skye[29]), bool.Parse(skye[30]));

                    Object enemy = Resources.Load("enemy", typeof(GameObject));

                    GameObject new_enemy1 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy1.GetComponent<enemyScript>().Setup(7, 5, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy2 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy2.GetComponent<enemyScript>().Setup(8, 10, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy3 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy3.GetComponent<enemyScript>().Setup(12, 9, "???", "volante2", 4, 3, "move", 1, 2, 3, 3, 90, 0, 4, 2, true, 22, 4, 6, 5, 9, 4, 3, 5);
                    GameObject new_enemy4 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy4.GetComponent<enemyScript>().Setup(14, 13, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy5 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy5.GetComponent<enemyScript>().Setup(15, 1, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy6 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy6.GetComponent<enemyScript>().Setup(16, 6, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy7 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy7.GetComponent<enemyScript>().Setup(18, 12, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy8 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy8.GetComponent<enemyScript>().Setup(18, 1, "???", "volante2", 4, 3, "move", 1, 2, 3, 3, 90, 0, 4, 2, true, 22, 4, 6, 5, 9, 4, 3, 5);
                    GameObject new_enemy9 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy9.GetComponent<enemyScript>().Setup(21, 5, "???", "volante2", 4, 3, "move", 1, 2, 3, 3, 90, 0, 4, 2, true, 22, 4, 6, 5, 9, 4, 3, 5);
                    GameObject new_enemy10 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy10.GetComponent<enemyScript>().Setup(21, 0, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy11 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy11.GetComponent<enemyScript>().Setup(23, 9, "???", "volante2", 4, 3, "move", 1, 2, 3, 3, 90, 0, 4, 2, true, 22, 4, 6, 5, 9, 4, 3, 5);
                    GameObject new_enemy12 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy12.GetComponent<enemyScript>().Setup(23, 3, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy13 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy13.GetComponent<enemyScript>().Setup(24, 13, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy14 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy14.GetComponent<enemyScript>().Setup(25, 6, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy15 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy15.GetComponent<enemyScript>().Setup(27, 11, "???", "volante2", 4, 3, "move", 1, 2, 3, 3, 90, 0, 4, 2, true, 22, 4, 6, 5, 9, 4, 3, 5);
                    GameObject new_enemy16 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy16.GetComponent<enemyScript>().Setup(27, 8, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);
                    GameObject new_enemy17 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy17.GetComponent<enemyScript>().Setup(27, 5, "???", "volante2", 4, 3, "move", 1, 2, 3, 3, 90, 0, 4, 2, true, 22, 4, 6, 5, 9, 4, 3, 5);
                    GameObject new_enemy18 = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
                    new_enemy18.GetComponent<enemyScript>().Setup(27, 1, "???", "volante1", 4, 3, "move", 1, 1, 5, 5, 90, 0, 4, 0, false, 26, 6, 4, 5, 9, 4, 5, 3);

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

        finitoSpawn = true;

    }

}
