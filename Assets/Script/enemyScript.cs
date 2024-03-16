
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class Node //usato in A*
{
    public int x;
    public int y;
    public int g;
    public int h;
    public int f;

    public Node parent;

    public Node(int x, int y, int g)
    {
        this.x = x;
        this.y = y;
        this.g = g;
        this.parent = this;
    }

    public void CalcH(Node end)
    {
        this.h = (int)System.Math.Ceiling(System.Math.Pow(System.Math.Abs(end.x - this.x), 2) + System.Math.Pow(System.Math.Abs(end.y - this.y),2));    
        this.f = g + h;
    }


}


public class enemyScript : MonoBehaviour
{
    

    [Tooltip("Pos X")]
    public int x = 0;
    [Tooltip("Pos Y")]                                      //info
    public int y = 0;


    [Space]

    public string nome = "Test";
    public string textureFile = "Edelgard";
    public int lvl = 1;                                         //statistiche
    public int movement = 3;
    [Tooltip("Tipo di movimento \n'move' => si muove sempre \n'near' => si muove quando nemico e' vicino \n'attack' => non si muove")]
    public string movType = "move";


    [Space] //armi

    public int weaponMinRange = 1;
    public int weaponMaxRange = 1;
    public int weaponWt;  //peso
    public int weaponMt; //potenza
    public int weaponHit;
    public int weaponCrit;
    [Tooltip("1= Fuoco\n2=Acqua\n3=Terra\n4=Aria")]
    public int unitType;

    [Tooltip("0=Niente\n1= Fuoco\n2=Acqua\n3=Terra\n4=Aria")]
    public int unitEffective;
    [Tooltip("0=Pugni\n1=Bastone\n2=Magia\n3=Arco\n4=Pugnale\n5=Spada\n6=Lancia\n7=Ascia")] 
    public int weaponType;
    [Tooltip("false -> fisico\ntrue -> magico")]
    public bool weaponIsMagic = false;


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

    [Space]
    public bool boss = false;
    public string musica = "no";

    public List<Vector3> AStar(int endx, int endy)
    {

        Debug.Log(endx + " " + endy);

        Node end = new Node(endx, endy, 0); 
        Node start = new Node(x,y,0);

        start.CalcH(end);


        GameObject[,] map = GameObject.Find("map").GetComponent<mapScript>().mapTiles;


        List<Node> list1 = new List<Node>();    //lista caselle temp
        List<Node> list2 = new List<Node>();    //lista caselle finali

        list1.Add(start);

        Node lowest;

        bool found = false;
        int i = 0;

        do
        {
            lowest = list1[0];
            foreach (Node node in list1)
            {
                if (node.f < lowest.f) lowest = node;    //trova il nodo con f minore
            }

            
            list1.Remove(lowest);
            list2.Add(lowest);                            //lo aggiunge a list2

            List<Node> adjacent = new List<Node> { new Node(lowest.x + 1, lowest.y, lowest.g + 1), new Node(lowest.x - 1, lowest.y, lowest.g + 1), new Node(lowest.x, lowest.y + 1, lowest.g + 1), new Node(lowest.x, lowest.y - 1, lowest.g + 1) };
            //calcolo caselle adiacenti

            foreach (Node node in adjacent)
            {
                node.CalcH(end);
                        
                if (node.x >= 0 && node.x < battleManager.mapDimX && node.y >= 0 && node.y < battleManager.mapDimY)               //controlla se e' dentro mappa
                {

                    
                    bool hasEnemy = false;
                    foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        if (e.GetComponent<enemyScript>().x == node.x && e.GetComponent<enemyScript>().y == node.y) hasEnemy = true;
                    }
                    foreach (GameObject e in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (e.GetComponent<playerScript>().x == node.x && e.GetComponent<playerScript>().y == node.y) hasEnemy = true;                  //controlla che la casella non abbia player/nemici
                        if (hasEnemy && node.x == end.x && node.y == end.y) hasEnemy = false;
                    }

                    



                    if (map[node.x, node.y].GetComponent<tileScript>().canBeWalkedOn == true && hasEnemy == false)
                    {

                        bool listContains = false;

                        foreach (Node n in list1)                                                   
                        {
                            if (n.x == node.x && node.y == n.y)                                     //se e' gia' in list1 e la sua g e' migliore allora sostituisce valore
                            {                                
                                if (n.g > node.g) {

                                    n.parent = lowest;
                                    n.g = lowest.g + 1;
                                    n.h = (end.x - n.x) ^ 2 + (end.y - n.y) ^ 2;
                                    n.f = n.g + n.h;

                                }
                                listContains = true;
                            }
                        }

                        if (listContains == false)                          //se non e' in list1 viene aggiunto
                        {

                            node.parent = lowest;
                            list1.Add(node);

                        }


                        if (node.x == end.x && node.y == end.y) found = true;   //se trova la casella finale termina

                    }
                }



            }
            i++;    
            if (i > 50) { found = true; Debug.Log("Halted Loop"); }             //termina dopo 50 iterazioni


        }while(found==false);                                                       

        list2.Reverse();

        Node parent = list2[0];

        List<Vector3> path = new List<Vector3>();               //lista spostamento finale
        i = 0;
        do
        {
            path.Add(new Vector3(parent.x, parent.y, -9));          //prende il genitore di ogni casella e la aggiunge a path
            parent = parent.parent;

            i++;
            if (i > 50) { parent = start; Debug.Log("Halted Parent Searching"); }

        } while(parent != start);


        path.Reverse();

        return path;

    }

    private List<GameObject> movBlueTiles = new List<GameObject>();
    private List<GameObject> attackRedTiles = new List<GameObject>();
    public List<Vector2> GetMovTiles()
    {
        List<Vector2> movBlueTiles = new List<Vector2>();
        List<GameObject> movTiles = new List<GameObject>();
        List<int> movTilesDistance = new List<int>();
        GameObject[,] map = GameObject.Find("map").GetComponent<mapScript>().mapTiles;

        movTiles.Add(map[x, y]);
        movTilesDistance.Add(999);

        
        AdjCheck(x, y, movement, ref map, ref movTiles, ref movTilesDistance);

        
        for (int i = 0; i < movTiles.Count; i++)        
        {
            movBlueTiles.Add(new Vector2(movTiles[i].GetComponent<tileScript>().x, movTiles[i].GetComponent<tileScript>().y));
            for (int r = 1; r <= weaponMaxRange; r++)             //tasselli rossi
            {
                int x = -1;
                int y = 0;
                for (int a = 0; a < 4; a++)
                {

                    int cx = r * x + movTiles[i].GetComponent<tileScript>().x;
                    int cy = r * y + movTiles[i].GetComponent<tileScript>().y;


                    if (cx >= 0 && cx < battleManager.mapDimX && cy >= 0 && cy < battleManager.mapDimY)
                    {
                        if (!movTiles.Contains(map[cx, cy]) && map[cx, cy].GetComponent<tileScript>().canBeWalkedOn == true)
                        {

                            movBlueTiles.Add(new Vector2(cx,cy));
                        }

                    }

                    if (x < 1 && y == 0) x = 1;
                    else if (x == 1)
                    {
                        x = 0;
                        y = -1;
                    }
                    else y = 1;


                }

            }
        }

        return movBlueTiles;
    }


    public void HighlightMov()                                          //spawna i tasselli del movimento
    {
        movBlueTiles = new List<GameObject>();
        List<GameObject> movTiles = new List<GameObject>();
        List<int> movTilesDistance = new List<int>();
        GameObject[,] map = GameObject.Find("map").GetComponent<mapScript>().mapTiles;
        attackRedTiles = new List<GameObject>();
        List<GameObject> attackTiles = new List<GameObject>();

        movTiles.Add(map[x, y]);
        movTilesDistance.Add(999);
        
        AdjCheck(x, y, movement, ref map, ref movTiles, ref movTilesDistance);

        Object mov_tile_prefab = Resources.Load("movTilePrefab", typeof(GameObject));
        Object attack_tile_prefab = Resources.Load("attackTilePrefab", typeof(GameObject));


        for (int i = 0; i < movTiles.Count; i++)        //spawna tasselli 
        {
            GameObject mov_tile = (GameObject)Instantiate(mov_tile_prefab, new Vector3(movTiles[i].GetComponent<tileScript>().x, movTiles[i].GetComponent<tileScript>().y, -2), Quaternion.identity);
            mov_tile.transform.parent = movTiles[i].transform;
            mov_tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            movBlueTiles.Add(mov_tile);


            for (int r = 1; r <= weaponMaxRange; r++)             //tasselli rossi
            {
                int x = -1;
                int y = 0;
                for (int a = 0; a < 4; a++)
                {
                    
                    int cx = r * x + movTiles[i].GetComponent<tileScript>().x;
                    int cy = r * y + movTiles[i].GetComponent<tileScript>().y;


                    if (cx >= 0 && cx < battleManager.mapDimX && cy >= 0 && cy < battleManager.mapDimY)
                    {
                        if (!movTiles.Contains(map[cx, cy]) && !attackTiles.Contains(map[cx, cy]))//&& map[cx, cy].GetComponent<tileScript>().canBeWalkedOn == true
                        {
                            attackTiles.Add(map[cx, cy]);
                            GameObject attack_tile = (GameObject)Instantiate(attack_tile_prefab, new Vector3(map[cx, cy].GetComponent<tileScript>().x, map[cx, cy].GetComponent<tileScript>().y, -2), Quaternion.identity);
                            attack_tile.transform.parent = map[cx, cy].transform;
                            attack_tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                            attackRedTiles.Add(attack_tile);
                        }

                    }

                    if (x < 1 && y == 0) x = 1;
                    else if (x == 1)
                    {
                        x = 0;
                        y = -1;
                    }
                    else y = 1;


                }

            }
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


                if (cx + i >= 0 && cx + i < battleManager.mapDimX && cy + j >= 0 && cy + j < battleManager.mapDimY)
                {

                    if (map[cx + i, cy + j].GetComponent<tileScript>().canBeWalkedOn == true && mov >= map[cx + i, cy + j].GetComponent<tileScript>().travelCost)
                    {

                        bool hasEnemy = false;

                        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Player"))
                        {
                            if (e.GetComponent<playerScript>().x == (cx + i) && e.GetComponent<playerScript>().y == (cy + j)) hasEnemy = true;
                        }
                        if (hasEnemy == false)
                        {

                            GameObject temp = map[cx + i, cy + j];
                            if (!movTiles.Contains(map[cx + i, cy + j]))
                            {
                                if(hasEnemy==false) 
                                {
                                    movTiles.Add(map[cx + i, cy + j]);
                                    movTilesDistance.Add(mov);
                                }
                                AdjCheck(cx + i, cy + j, mov - map[cx + i, cy + j].GetComponent<tileScript>().travelCost, ref map, ref movTiles, ref movTilesDistance);

                            }
                            else if (movTilesDistance[movTiles.FindIndex(n => n.Equals(temp))] < mov)
                            {
                                if (hasEnemy == false)
                                {
                                    movTilesDistance[movTiles.FindIndex(n => n.Equals(temp))] = mov;
                                }
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

    public void Setup(int x, int y, string nome, string textureFile, int lvl, int movement, string movType, int weaponMinRange, int weaponMaxRange, int weaponWt, int weaponMt, int weaponHit, int weaponCrit, int unitType, int weaponType, bool weaponIsMagic,
                        int hp, int str, int mag, int dex, int spd, int lck, int def, int res)
    {

        this.x = x;
        this.y = y;
        this.nome = nome;
        this.textureFile = textureFile;
        this.lvl = lvl;
        this.movement = movement;
        this.movType = movType;

        this.weaponMinRange = weaponMinRange;
        this.weaponMaxRange = weaponMaxRange;
        this.weaponWt = weaponWt;
        this.weaponMt = weaponMt;
        this.weaponHit = weaponHit;
        this.weaponCrit = weaponCrit;
        this.unitType = unitType;
        this.weaponType = weaponType;
        this.weaponIsMagic = weaponIsMagic;


        this.maxHp = hp;
        this.str = str;
        this.mag = mag;
        this.dex = dex;
        this.spd = spd;
        this.lck = lck;
        this.def = def;
        this.res = res;

    }

    public void Setup(int x, int y, string nome, string textureFile, int lvl, int movement, string movType, int weaponMinRange, int weaponMaxRange, int weaponWt, int weaponMt, int weaponHit, int weaponCrit, int unitType, int weaponType, bool weaponIsMagic,
                        int hp, int str, int mag, int dex, int spd, int lck, int def, int res, string musica)
    {

        this.x = x;
        this.y = y;
        this.nome = nome;
        this.textureFile = textureFile;
        this.lvl = lvl;
        this.movement = movement;
        this.movType = movType;

        this.weaponMinRange = weaponMinRange;
        this.weaponMaxRange = weaponMaxRange;
        this.weaponWt = weaponWt;
        this.weaponMt = weaponMt;
        this.weaponHit = weaponHit;
        this.weaponCrit = weaponCrit;
        this.unitType = unitType;
        this.weaponType = weaponType;
        this.weaponIsMagic = weaponIsMagic;


        this.maxHp = hp;
        this.str = str;
        this.mag = mag;
        this.dex = dex;
        this.spd = spd;
        this.lck = lck;
        this.def = def;
        this.res = res;

        boss = true;
        this.musica = musica;

    }


    Vector3 nextPosition;

    private void Start()
    {
        nextPosition = transform.position;

        battleManager.enemies.Add(gameObject);                    //aggiunge alle liste globali


        hp = maxHp;
        healthbar.SetMaxHealth(maxHp);
        healthbar.SetHealth(hp);

        if (x == 0 & y == 0)
        {
            x = (int)transform.position.x;
            y = (int)transform.position.y;
        }

        transform.position = new Vector3(x, y, -9);

        var texture = Resources.Load<GameObject>("Characters/" + textureFile);   //carica la texture del personaggio
        GameObject sprite = Instantiate(texture, new Vector3(0, 0, 0), Quaternion.identity);
        sprite.transform.parent = transform;
        sprite.transform.localPosition = new Vector3(0, 0, 0);
        sprite.transform.SetAsFirstSibling();
        transform.Rotate(0, 180, 0);
        int childCount = 0;
        foreach(Transform g in GetComponentInChildren<Transform>())
        {
            childCount++;
            if(childCount!= 1)
            {
                g.localPosition = new Vector3(g.localPosition.x * -1 ,g.localPosition.y, g.localPosition.z * -1);
            }
            if (childCount == 2) g.Rotate(0, 180, 0);
        }


        switch (unitType){                                      //auto assegna efficacia
            case 1:
                unitEffective = 3; //fuoco->terra
                break;
            case 2:
                unitEffective = 1; //acqua->fuoco
                break;
            case 3:
                unitEffective = 2; //terra->acqua
                break;
            default:
                unitEffective = 0; //non efficace
                break;
        }

        UnityEngine.Object[] all = Resources.LoadAll<Sprite>("weaponIcons");

        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = (Sprite)all[unitType - 1]; //carica icona arma
        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = (Sprite)all[4 + weaponType];


    }


    bool nextPositionChanged;

    public bool mouseIsOver = false;
    private GameObject infoGUI;
    public bool infoGUISpawned = false;
    private float infoGUICooldown = 0;

    void Update()
    {
        if (mouseIsOver && !infoGUISpawned && infoGUICooldown <= Time.time && battleManager.phase == "Player")
        {
            if (!(Input.GetKey(KeyCode.Mouse0)))
            {
                infoGUISpawned = true;
                infoGUI = (GameObject)Instantiate(Resources.Load("Info Canvas", typeof(GameObject)), Camera.main.transform.position, Quaternion.identity);
                infoGUI.transform.parent = Camera.main.transform;
                infoGUI.transform.localPosition = new Vector3(0.108f, 0, 11);
                infoGUI.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                infoGUICooldown = Time.time + 0.3f;
                infoGUI.GetComponent<infoGUIScript>().Setup(this);
            }
        }

        if (battleManager.canMoveEnemy==false && nextPositionChanged)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime*5);

            if (transform.position == nextPosition)
            {
                nextPositionChanged = false;
                
                GameObject nearest = GameObject.FindGameObjectsWithTag("Player")[0];
                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (System.Math.Pow(System.Math.Abs(x - p.GetComponent<playerScript>().x), 2) + System.Math.Pow(System.Math.Abs(y - p.GetComponent<playerScript>().y), 2) < (System.Math.Pow(System.Math.Abs(x - nearest.GetComponent<playerScript>().x), 2) + System.Math.Pow(System.Math.Abs(y - nearest.GetComponent<playerScript>().y), 2))) nearest = p;
                }
                
                


                int distance = (int)Mathf.Abs(nearest.transform.position.x - nextPosition.x) + (int)Mathf.Abs(nearest.transform.position.y - nextPosition.y);
                Debug.Log("Inizia PVP Enemy "+distance);
                if(distance<=weaponMaxRange&&distance>=weaponMinRange){

                    int [] output = Camera.main.GetComponent<battleManager>().pvp(this.gameObject, nearest, "enemy");     //inizia pvp
                    StartCoroutine(Camera.main.GetComponent<battleManager>().CaricaCombat(this.gameObject,nearest,output, "enemy"));


                }
                else
                {
                    battleManager.canMoveEnemy = true;
                }



                
            }
        }

        if(movBlueTiles.Count > 0 && battleManager.phase == "Enemy")
        {
            foreach (GameObject g in movBlueTiles) Destroy(g);
            movBlueTiles.Clear();
            foreach (GameObject g in attackRedTiles) Destroy(g);                          
            attackRedTiles.Clear();
        }

        if (battleManager.phase == "Enemy"&&infoGUI != null)
        {
            Destroy(infoGUI);
            infoGUISpawned = false;
        }

        Vector3 check = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(x,y,0);
        if(check.x>0.5||check.x<-0.5||check.y>0.5||check.y<-0.5 || battleManager.removeGUI){
            if(mouseIsOver || battleManager.removeGUI){
                 mouseIsOver = false;
                if(infoGUISpawned)
                {
                    infoGUI.GetComponent<infoGUIScript>().Rimuovi();
                    infoGUISpawned = false;
                }
                

                if (battleManager.phase == "Player" &&!(Input.GetKey(KeyCode.Mouse0)) || battleManager.removeGUI) 
                {
                    if(movBlueTiles.Count > 0)
                    {
                        
                        foreach (GameObject g in movBlueTiles) Destroy(g);                          //elimina tasselli blu
                        movBlueTiles.Clear();

                        
                        foreach (GameObject g in attackRedTiles) Destroy(g);                          //elimina tasselli rossi
                        attackRedTiles.Clear();
                    }
                    


                }
            }
        }

    }


    private void OnMouseEnter()
    {
        mouseIsOver = true;
        if (battleManager.phase == "Player" && !(Input.GetKey(KeyCode.Mouse0)))
        {
            HighlightMov();
        }
    }


    private void OnMouseExit()
    {
        mouseIsOver = false;
        if (battleManager.phase == "Player" && infoGUI != null)
        {
            infoGUI.GetComponent<infoGUIScript>().Rimuovi();
            infoGUISpawned = false;
        }

        if (battleManager.phase == "Player" && !(Input.GetKey(KeyCode.Mouse0)))
        {
            if(movBlueTiles.Count > 0)
            {
                foreach (GameObject g in movBlueTiles) Destroy(g);
                movBlueTiles.Clear();
                foreach (GameObject g in attackRedTiles) Destroy(g);                          
                attackRedTiles.Clear();
            }
            


        }


    }

    public void cancInfo(){
        mouseIsOver = true;
        if (infoGUI != null)
        {
            Destroy(infoGUI);
            infoGUISpawned = false;
        }
        if(movBlueTiles.Count > 0)
        {
            foreach (GameObject g in movBlueTiles) Destroy(g);
            movBlueTiles.Clear();
            foreach (GameObject g in attackRedTiles) Destroy(g);                          
            attackRedTiles.Clear();
        }
    }

    public void Move()
    {

        if (movType == "move" || movType == "near")
        {
            GameObject nearest = GameObject.FindGameObjectsWithTag("Player")[0];
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (System.Math.Pow(System.Math.Abs(x - p.GetComponent<playerScript>().x), 2) + System.Math.Pow(System.Math.Abs(y - p.GetComponent<playerScript>().y), 2) < (System.Math.Pow(System.Math.Abs(x - nearest.GetComponent<playerScript>().x), 2) + System.Math.Pow(System.Math.Abs(y - nearest.GetComponent<playerScript>().y), 2))) nearest = p;
            }

            List<Vector3> path = AStar(nearest.GetComponent<playerScript>().x, nearest.GetComponent<playerScript>().y);


            int movRemaining = movement;

            Debug.Log(movType + GetMovTiles().Contains(new Vector2(nearest.GetComponent<playerScript>().x, nearest.GetComponent<playerScript>().y)));

            if (movType == "near" && GetMovTiles().Contains(new Vector2(nearest.GetComponent<playerScript>().x, nearest.GetComponent<playerScript>().y)) == true || movType=="move")
            {
                foreach (Vector3 p in path)
                {
                    movRemaining -= GameObject.Find("map").GetComponent<mapScript>().mapTiles[(int)p.x, (int)p.y].GetComponent<tileScript>().travelCost;
                    if (movRemaining < 0) break;

                    nextPosition = p;
                    nextPositionChanged = true;

                    x = (int)p.x;
                    y = (int)p.y;

                }
                GameObject.Find("Main Camera").GetComponent<battleManager>().UpdateEnemyMov();
            }
            else
            {
                nextPosition = transform.position;
                nextPositionChanged = true;
            }
        }
        else
        {
            nextPosition = transform.position;
            nextPositionChanged = true;
        }

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


        for (int i = 0; i < 8; i++)
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
        Debug.Log("Level Up! " + (lvl - 1) + " -> " + lvl);

        for (int i = 0; i < 8; i++)
        {
            if (increments[i] > 0)
            {
                Debug.Log(statsNames[i] + " " + (statsValues[i] - 1) + " +1 -> " + statsValues[i]);
            }
            else Debug.Log(statsNames[i] + " " + statsValues[i]);
        }

    }



}




