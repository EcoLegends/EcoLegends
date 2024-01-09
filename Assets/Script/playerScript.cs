using System.Collections.Generic;
using UnityEditor;
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

    public string nome = "Test";
    public string textureFile = "Edelgard";
    public int lvl = 1;                                         //statistiche
    public int movement = 3;

    [Space] //armi

    public int weaponRange = 1;
    public int weaponWt;  //peso
    public int weaponMt; //potenza
    public int weaponHit;
    public int weaponCrit;
    [Tooltip("1= Fuoco\n2=Acqua\n3=Terra\n4=Aria")]
    public int unitType;

    [Tooltip("0=Niente\n1= Fuoco\n2=Acqua\n3=Terra\n4=Aria")]
    public int unitEffective;
    [Tooltip("0=Pugni\n1=Bastone\n2=Magia\n3=Arco\n4=Shuriken\n5=Speciale\n6=Spada\n7=Lancia\n8=Ascia")]
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


    private List<Vector2> mov_tiles_coords = new List<Vector2>();
    private List<GameObject> movBlueTiles = new List<GameObject>();
    private List<GameObject> attackRedTiles = new List<GameObject>();
    public void HighlightMov()                                          //spawna i tasselli blu del movimento
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

        Object mov_tile_prefab = AssetDatabase.LoadAssetAtPath("Assets/movTilePrefab.prefab", typeof(GameObject));
        Object attack_tile_prefab = AssetDatabase.LoadAssetAtPath("Assets/attackTilePrefab.prefab", typeof(GameObject));

        for (int i = 0; i < movTiles.Count; i++)        //spawna tasselli blu
        {
            GameObject mov_tile = (GameObject)Instantiate(mov_tile_prefab, new Vector3(movTiles[i].GetComponent<tileScript>().x, movTiles[i].GetComponent<tileScript>().y, -2), Quaternion.identity);
            mov_tile.transform.parent = movTiles[i].transform;
            if (dragging == false) mov_tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

            bool add_vector = true;

            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
                if (p.GetComponent<playerScript>().x == movTiles[i].GetComponent<tileScript>().x && p.GetComponent<playerScript>().y == movTiles[i].GetComponent<tileScript>().y && p != this.gameObject) add_vector = false;
            }

            if (add_vector)
            {
                mov_tiles_coords.Add(new Vector2(movTiles[i].GetComponent<tileScript>().x, movTiles[i].GetComponent<tileScript>().y));
            }
            movBlueTiles.Add(mov_tile);
            

            for(int r = 1; r<=weaponRange; r++)             //tasselli rossi
            {
                int x = -1;
                int y = 0;
                for(int a=0;a<4;a++)
                {
                    
                    int cx = r * x + movTiles[i].GetComponent<tileScript>().x;
                    int cy = r * y + movTiles[i].GetComponent<tileScript>().y;


                    if (cx >= 0 && cx < 10 && cy >= 0 && cy < 10)
                    {
                        if (!movTiles.Contains(map[cx, cy]) &&  !attackTiles.Contains(map[cx, cy]) && map[cx, cy].GetComponent<tileScript>().canBeWalkedOn == true) 
                        {
                            attackTiles.Add(map[cx, cy]);
                            GameObject attack_tile = (GameObject)Instantiate(attack_tile_prefab, new Vector3(map[cx, cy].GetComponent<tileScript>().x, map[cx, cy].GetComponent<tileScript>().y, -2), Quaternion.identity);
                            attack_tile.transform.parent = map[cx, cy].transform;
                            if (dragging == false) attack_tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
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


                if (cx + i >= 0 && cx + i < 10 && cy + j >= 0 && cy + j < 10)
                {

                    if (map[cx + i, cy + j].GetComponent<tileScript>().canBeWalkedOn == true && mov >= map[cx + i, cy + j].GetComponent<tileScript>().travelCost)
                    {

                        bool hasEnemy = false;

                        foreach(GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
                        {
                            if(e.GetComponent<enemyScript>().x == (cx + i) && e.GetComponent<enemyScript>().y == (cy + j)) hasEnemy = true;
                        }
                        if(hasEnemy == false)
                        {
                            GameObject temp = map[cx + i, cy + j];
                            if (!movTiles.Contains(map[cx + i, cy + j]))
                            {
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



    public void Setup(int x, int y, string nome, string textureFile, int lvl, int movement, int weaponRange, int weaponWt, int weaponMt, int weaponHit, int weaponCrit, int unitType, int weaponType, bool weaponIsMagic, 
                        int hp, int str, int mag, int dex, int spd, int lck, int def, int res, int hpGrowth, int strGrowth, int magGrowth, int dexGrowth, int spdGrowth, int lckGrowth, int defGrowth, int resGrowth) {

        this.x = x;
        this.y = y;
        this.nome = nome;
        this.textureFile = textureFile;
        this.lvl = lvl;
        this.movement = movement;

        this.weaponRange = weaponRange;
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

        this.hpGrowth = hpGrowth;
        this.strGrowth = strGrowth;
        this.magGrowth = magGrowth;
        this.dexGrowth = dexGrowth;
        this.spdGrowth = spdGrowth;
        this.lckGrowth = lckGrowth;
        this.defGrowth = defGrowth;
        this.resGrowth = resGrowth;


    }



    GameObject player_tile;
    private void Start()  
    {

        battleManager.units.Add(gameObject);                    //aggiunge alle liste globali


        hp = maxHp;
        healthbar.SetMaxHealth(maxHp);
        healthbar.SetHealth(hp);

        if (x == 0 & y == 0)
        {
            x = (int)transform.position.x;
            y = (int)transform.position.y;
        }

        transform.position = new Vector3(x, y, -9);

        Object texture = AssetDatabase.LoadAssetAtPath("Assets/Resources/Characters/" + textureFile + ".prefab", typeof(GameObject));   //carica la texture del personaggio
        GameObject sprite = (GameObject)Instantiate(texture, new Vector3(x, y, 0), Quaternion.identity);
        sprite.transform.parent = transform;
        sprite.transform.SetAsFirstSibling();


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

        Object[] all = Resources.LoadAll<Sprite>("weaponIcons");

        Debug.Log(all.Length);
             
        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = (Sprite) all[4*weaponType+unitType-1]; //carica icona arma


    }

    public void CanMoveAgain() {
        canMove = true;                                                     //respawn tassello lampeggiante
        player_tile = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/playerTilePrefab.prefab", typeof(GameObject)), new Vector3(x, y, -2), Quaternion.identity);
        player_tile.GetComponent<PlayerTileScript>().PlayerTile(x, y);
        battleManager.unmovedUnits.Add(gameObject);

        Component[] renderers = transform.GetChild(0).GetComponentsInChildren(typeof(Renderer)); //rende normale il personaggio
        foreach (Renderer childRenderer in renderers)
        {
            childRenderer.material.color = new Color(1F, 1F, 1F);
        }

    }


    private bool OnRange = true;



    void Update()
    {
        if (canMove)
        {
            if (dragging)
            {
                Destroy(player_tile);                                                                   //rimuove tassello lampeggiante
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;      //cambia pos

            }
            


        }
        if(!OnRange)
        {
            
            float t = 1 / (new Vector3(x, y, -9)-transform.position).magnitude;                               //traslazione in nuova posizione
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, -9), t*0.1f);
            
            if (transform.position == new Vector3(x, y, -9)) OnRange = true;
        }

        
        
    }

    private void OnMouseEnter()
    {
        if (canMove && battleManager.phase == "Player" && !(Input.GetKey(KeyCode.Mouse0)))
        {
            HighlightMov();
        }
    }


    private void OnMouseExit()
    {
        if(canMove && battleManager.phase == "Player" &&!(Input.GetKey(KeyCode.Mouse0))) 
        {
            if(movBlueTiles.Count > 0)
            {
                mov_tiles_coords.Clear();
                foreach (GameObject g in movBlueTiles) Destroy(g);                          //elimina tasselli blu
                movBlueTiles.Clear();

                
                foreach (GameObject g in attackRedTiles) Destroy(g);                          //elimina tasselli rossi
                attackRedTiles.Clear();
            }
            


        }

        
    }


    private void OnMouseDown()
    {   
        if(canMove && battleManager.phase == "Player" && dragging==false) 
        {
           

            if (movBlueTiles.Count > 0)
            {
                mov_tiles_coords.Clear();
                foreach (GameObject g in movBlueTiles) Destroy(g);                          //elimina tasselli blu
                movBlueTiles.Clear();

                foreach (GameObject g in attackRedTiles) Destroy(g);                          //elimina tasselli rossi
                attackRedTiles.Clear();
            }

            dragging = true;
            HighlightMov();                                                                //spawna tasselli blu movimento
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            

            
        }
        
    }

    
    private void OnMouseUp()
    {
        if (canMove && battleManager.phase == "Player")
        {
            
            dragging = false;
            OnRange = false;
            bool availableMov = false;

            foreach (Vector2 v in mov_tiles_coords)
            {                               //trova se il player e' in una casella blu
                if (v.x == Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x) && v.y == Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y)) { availableMov = true; break; }
            }
            mov_tiles_coords.Clear();
            
            if (availableMov)
            {
                this.x = Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x); //cambia posizione
                this.y = Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y);


                canMove = false;
                battleManager.unmovedUnits.Remove(gameObject);
                foreach(GameObject g in battleManager.unmovedUnits)
                {
                    g.transform.GetChild(0).GetComponent<Animator>().Play("Select");          //inizia animazione di selezione personaggio
                }


                Component[] renderers = transform.GetChild(0).GetComponentsInChildren(typeof(Renderer)); //rende grigio il personaggio
                foreach (Renderer childRenderer in renderers)
                {
                    childRenderer.material.color = new Color(0.3F, 0.3F, 0.3F);
                }


            }else {

                foreach( GameObject e in GameObject.FindGameObjectsWithTag("Enemy")){
                    if(Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x)==e.GetComponent<enemyScript>().x && Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y)==e.GetComponent<enemyScript>().y){
                        Camera.main.transform.position=new Vector3(100,0, 0);

                        Camera.main.GetComponent<battleManager>().pvp(e,this.gameObject);     //inizia pvp

                        Debug.Log("PVP");
                    } 
                }
            }
                                      
            foreach (GameObject g in movBlueTiles) Destroy(g);                          //elimina tasselli blu
            movBlueTiles.Clear();
            foreach (GameObject g in attackRedTiles) Destroy(g);                          //elimina tasselli rossi
            attackRedTiles.Clear();

            Camera.main.GetComponent<battleManager>().UpdateEnemyMov();
       
        }

        this.LevelUp();
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


        for(int i = 0; i < 8; i++)
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
        Debug.Log("Level Up! "+(lvl-1)+" -> "+lvl);

        for( int i=0; i < 8; i++)
        {
            if (increments[i] > 0)
            {
                Debug.Log(statsNames[i] + " " + (statsValues[i]-1) +" +1 -> " + statsValues[i]);
            }
            else Debug.Log(statsNames[i] + " " + statsValues[i]);
        }

    }

    

}




