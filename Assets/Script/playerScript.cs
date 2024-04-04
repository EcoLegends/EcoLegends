
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

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
    public int exp = 0;
    public int movement = 3;

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


    public bool haFattoQualcosa;

    bool tutorialFatto = false;

    [Space]
    public bool heal;
    public bool cura;//se vuoi curare
    public heathBarScript healthbar;
    private List<Vector2> mov_tiles_coords = new List<Vector2>();
    public List<GameObject> movBlueTiles = new List<GameObject>();
    private List<GameObject> attackRedTiles = new List<GameObject>();



    List<Vector2> mousepos = new List<Vector2>();




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

        Object mov_tile_prefab = Resources.Load("movTilePrefab", typeof(GameObject));
        Object attack_tile_prefab = Resources.Load("attackTilePrefab", typeof(GameObject));

        for (int i = 0; i < movTiles.Count; i++)        //spawna tasselli blu
        {
            GameObject mov_tile = (GameObject)Instantiate(mov_tile_prefab, new Vector3(movTiles[i].GetComponent<tileScript>().x, movTiles[i].GetComponent<tileScript>().y, -2), Quaternion.identity);
            mov_tile.transform.parent = movTiles[i].transform;
            if (dragging == false) mov_tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

            bool add_vector = true;

            foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
                if (p.GetComponent<playerScript>().x == movTiles[i].GetComponent<tileScript>().x && p.GetComponent<playerScript>().y == movTiles[i].GetComponent<tileScript>().y && p != this.gameObject)
                {
                    if (heal && p.GetComponent<playerScript>().hp!=p.GetComponent<playerScript>().maxHp) mov_tile.GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>("tileOverlay")[3];
                    add_vector = false;
                }
            }

            if (add_vector)
            {
                mov_tiles_coords.Add(new Vector2(movTiles[i].GetComponent<tileScript>().x, movTiles[i].GetComponent<tileScript>().y));
            }
            movBlueTiles.Add(mov_tile);
            

            for(int r = 1; r<=weaponMaxRange; r++)             //tasselli rossi
            {
                int x = -1;
                int y = 0;
                for(int a=0;a<4;a++)
                {
                    
                    int cx = r * x + movTiles[i].GetComponent<tileScript>().x;
                    int cy = r * y + movTiles[i].GetComponent<tileScript>().y;


                    if (cx >= 0 && cx < battleManager.mapDimX && cy >= 0 && cy < battleManager.mapDimY)
                    {
                        if (!movTiles.Contains(map[cx, cy]) &&  !attackTiles.Contains(map[cx, cy])) //&& map[cx, cy].GetComponent<tileScript>().canBeWalkedOn == true
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


                if (cx + i >= 0 && cx + i < battleManager.mapDimX && cy + j >= 0 && cy + j < battleManager.mapDimY)
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



    public void Setup(int x, int y, string nome, string textureFile, int lvl, int exp, int movement, int weaponMinRange, int weaponMaxRange, int weaponWt, int weaponMt, int weaponHit, int weaponCrit, int unitType, int weaponType, bool weaponIsMagic,
                        int hp, int str, int mag, int dex, int spd, int lck, int def, int res, int hpGrowth, int strGrowth, int magGrowth, int dexGrowth, int spdGrowth, int lckGrowth, int defGrowth, int resGrowth, bool heal)
    {

        this.x = x;
        this.y = y;
        this.nome = nome;
        this.textureFile = textureFile;
        this.lvl = lvl;
        this.movement = movement;

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

        this.hpGrowth = hpGrowth;
        this.strGrowth = strGrowth;
        this.magGrowth = magGrowth;
        this.dexGrowth = dexGrowth;
        this.spdGrowth = spdGrowth;
        this.lckGrowth = lckGrowth;
        this.defGrowth = defGrowth;
        this.resGrowth = resGrowth;
        this.heal = heal;

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
        

        var texture = Resources.Load<GameObject>("Characters/" + textureFile);   //carica la texture del personaggio
        GameObject sprite = Instantiate(texture, new Vector3(0, 0, 0), Quaternion.identity);
        sprite.transform.parent = transform;
        sprite.transform.localPosition = new Vector3(0, 0, 0);
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
             
        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = (Sprite)all[unitType-1]; //carica icona arma
        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = (Sprite)all[4 + weaponType];

    }

    public void CanMoveAgain() {
        canMove = true;                                                     //respawn tassello lampeggiante
        player_tile = (GameObject)Instantiate(Resources.Load("playerTilePrefab", typeof(GameObject)), new Vector3(x, y, -2), Quaternion.identity);
        player_tile.GetComponent<PlayerTileScript>().PlayerTile(x, y);
        battleManager.unmovedUnits.Add(gameObject);

        haFattoQualcosa = true;

    }
    

    private bool OnRange = true;

    public bool forecastSpawned = false;
    public bool previousForecastSpawned = false;
    private GameObject forecast;
    private GameObject target;
    private GameObject oldTarget;
    private float forecastCooldown = 0;

    private bool mouseIsOver = false;
    private GameObject infoGUI;
    private bool infoGUISpawned = false;
    private float infoGUICooldown = 0;
    private GameObject newPosTile;

    private int counter = 0;
    void Update()
    {
        if(!mousepos.Contains(new Vector2((int)Camera.main.ScreenToWorldPoint(Input.mousePosition).x, (int)Camera.main.ScreenToWorldPoint(Input.mousePosition).y)))
        {
            //Debug.Log("Added "+ (int)Camera.main.ScreenToWorldPoint(Input.mousePosition).x+ " "+ (int)Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            mousepos.Add(new Vector2((int)Camera.main.ScreenToWorldPoint(Input.mousePosition).x, (int)Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
            if (mousepos.Count > 3) mousepos.RemoveAt(0);
        }


        if(nome=="Aeria")
        {
            if (hp == maxHp) weaponMaxRange = 3;
            else weaponMaxRange = 2;

        }
        if (!battleManager.stop && mapScript.finitoSpawn && GameObject.FindGameObjectsWithTag("Tutorial").Length == 0)
        {
            if (!forecastSpawned && newPosTile != null) Destroy(newPosTile);
            if (!Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition)).Contains(GetComponent<Collider2D>()))
            {

                if (!dragging) counter++;

                if (counter == 20)
                {
                    mouseIsOver = false;
                    if (infoGUI != null)
                    {
                        infoGUI.GetComponent<infoGUIScript>().Rimuovi();
                        infoGUISpawned = false;
                        infoGUI = null;
                    }
                    if (forecast != null)
                    {
                        forecast.GetComponent<forecastScript>().Rimuovi();
                        forecastSpawned = false;
                        forecast = null;
                    }
                    if (movBlueTiles.Count > 0)
                    {
                        mov_tiles_coords.Clear();
                        foreach (GameObject g in movBlueTiles)
                        {
                            if (g != null)
                                Destroy(g);
                        }                          //elimina tasselli blu
                        movBlueTiles.Clear();


                        foreach (GameObject g in attackRedTiles)
                        {
                            if (g != null)
                                Destroy(g);
                        }                          //elimina tasselli rossi
                        attackRedTiles.Clear();
                    }
                }


            }
            else { mouseIsOver = true; counter = 0; }
            if (infoGUI != null && infoGUISpawned && battleManager.unmovedUnits.Count == 0)
            {
                infoGUI.GetComponent<infoGUIScript>().Rimuovi();
                infoGUISpawned = false;
            }

            if (!forecastSpawned && mouseIsOver && !infoGUISpawned && infoGUICooldown <= Time.time && battleManager.phase == "Player")
            {
                if (previousForecastSpawned == forecastSpawned && !(Input.GetKey(KeyCode.Mouse0)) || previousForecastSpawned != forecastSpawned && (Input.GetKey(KeyCode.Mouse0)))
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
            previousForecastSpawned = forecastSpawned;
            if (canMove)
            {
                if (dragging)
                {
                    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;      //cambia pos

                    if (forecastCooldown <= Time.time)
                    {
                        bool cura = false;
                        target = null;
                        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
                        {
                            if (Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x) == e.GetComponent<enemyScript>().x && Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y) == e.GetComponent<enemyScript>().y)
                            {
                                foreach (GameObject t in attackRedTiles)
                                {
                                    if (e.GetComponent<enemyScript>().x == t.transform.position.x && e.GetComponent<enemyScript>().y == t.transform.position.y)
                                    {
                                        target = e;
                                        cura = false;
                                        break;
                                    }
                                }

                            }
                        }

                        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Player"))
                        {
                            if (Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x) == e.GetComponent<playerScript>().x && Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y) == e.GetComponent<playerScript>().y && heal == true && e.GetComponent<playerScript>().hp != e.GetComponent<playerScript>().maxHp && e != gameObject)
                            {

                                foreach (GameObject t in movBlueTiles)
                                {
                                    if (e.GetComponent<playerScript>().x == t.transform.position.x && e.GetComponent<playerScript>().y == t.transform.position.y)
                                    {
                                        target = e;
                                        cura = true;
                                        break;
                                    }
                                }

                            }
                        }

                        if (!forecastSpawned && target != null)
                        {
                            forecastSpawned = true;
                            if (cura == false)
                                forecast = (GameObject)Instantiate(Resources.Load("Forecast Canvas", typeof(GameObject)), Camera.main.transform.position, Quaternion.identity);
                            else
                                forecast = (GameObject)Instantiate(Resources.Load("Forecast HEAL Canvas", typeof(GameObject)), Camera.main.transform.position, Quaternion.identity);
                            forecast.transform.parent = Camera.main.transform;
                            forecast.transform.localPosition = new Vector3(0, 0, 10);
                            forecast.transform.localScale = Vector3.one;
                            int oldX = x;
                            int oldY = y;

                            List<Vector2> positionsToCheck = new List<Vector2> { new Vector2(target.transform.position.x, target.transform.position.y) };
                            List<Vector2> positionsToCheckOld = positionsToCheck;
                            List<GameObject> attackTiles = new List<GameObject>();
                            GameObject[,] map = GameObject.Find("map").GetComponent<mapScript>().mapTiles;
                            for (int r = 1; r <= weaponMaxRange; r++)             //tasselli rossi
                            {
                                for (int i = 0; i < positionsToCheckOld.Count; i++)
                                {
                                    Vector2 v = positionsToCheckOld[i];
                                    int x = -1;
                                    int y = 0;
                                    for (int a = 0; a < 4; a++)
                                    {

                                        int cx = x + (int)v.x;
                                        int cy = y + (int)v.y;

                                        int distance = (int)Mathf.Abs(cx - target.transform.position.x) + (int)Mathf.Abs(cy - target.transform.position.y);
                                        if (distance <= weaponMaxRange && cx >= 0 && cx < battleManager.mapDimX && cy >= 0 && cy < battleManager.mapDimY)
                                        {
                                            if (!attackTiles.Contains(map[cx, cy]))
                                            {
                                                if (distance == weaponMaxRange && mov_tiles_coords.Contains(new Vector2(cx, cy)))
                                                {
                                                    attackTiles.Add(map[cx, cy]);
                                                }

                                                if (!positionsToCheck.Contains(new Vector2(cx, cy))) positionsToCheck.Add(new Vector2(cx, cy));
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
                                positionsToCheckOld = positionsToCheck;

                            }

                            Vector2 nearest = mousepos[3 - weaponMaxRange];
                            if(nearest.x == target.transform.position.x && nearest.y == target.transform.position.y) nearest = mousepos[2 - weaponMaxRange];

                            Vector2 newpos = new Vector2(0, 0);
                            foreach (GameObject tile in attackTiles)
                            {
                                Vector2 pos = new Vector2(tile.transform.position.x, tile.transform.position.y);
                                if(Mathf.Abs(pos.x - nearest.x) + Mathf.Abs(pos.y - nearest.y) < Mathf.Abs(newpos.x - nearest.x) + Mathf.Abs(newpos.y - nearest.y))
                                {
                                    newpos = pos;
                                }
                            }

                            x = (int) newpos.x;
                            y = (int) newpos.y;


                            int[] output = Camera.main.GetComponent<battleManager>().pvp(target, this.gameObject, "player", cura);
                            newPosTile = (GameObject)Instantiate(Resources.Load("newPosTile", typeof(GameObject)), new Vector3(x, y, -2), Quaternion.identity);
                            newPosTile.tag = "Rimuovere";
                            x = oldX;
                            y = oldY;

                            


                            Debug.Log(cura);
                            forecast.GetComponent<forecastScript>().Setup(target, this.gameObject, output, cura, new Vector2(x,y));

                            forecastCooldown = Time.time + 0.3f;


                            if (infoGUI != null)
                            {
                                infoGUI.GetComponent<infoGUIScript>().Rimuovi();
                                infoGUISpawned = false;
                            }

                        }
                        else if (forecastSpawned && target != oldTarget)
                        {
                            forecast.GetComponent<forecastScript>().Rimuovi();
                            forecastSpawned = false;
                            forecastCooldown = Time.time + 0.3f;
                        }
                        oldTarget = target;
                    }



                }
                else if (forecastSpawned)
                {
                    forecast.GetComponent<forecastScript>().Rimuovi();
                    forecastSpawned = false;
                }

            }
            if (!OnRange)
            {

                float t = 1 / (new Vector3(x, y, -9) - transform.position).magnitude;                               //traslazione in nuova posizione
                transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, -9), t * 0.1f);

                if (transform.position == new Vector3(x, y, -9)) OnRange = true;
            }

            Vector3 check = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(x, y, 0);
            if (check.x > 0.5 || check.x < -0.5 || check.y > 0.5 || check.y < -0.5 || battleManager.removeGUI)
            {
                if (mouseIsOver || battleManager.removeGUI)
                {
                    mouseIsOver = false;
                    if (infoGUISpawned)
                    {
                        if (infoGUI != null) infoGUI.GetComponent<infoGUIScript>().Rimuovi();
                        infoGUISpawned = false;
                    }


                    if (canMove && battleManager.phase == "Player" && !(Input.GetKey(KeyCode.Mouse0)) || battleManager.removeGUI)
                    {
                        if (movBlueTiles.Count > 0)
                        {
                            mov_tiles_coords.Clear();
                            foreach (GameObject g in movBlueTiles)
                            {
                                if (g != null)
                                    Destroy(g);
                            }                          //elimina tasselli blu
                            movBlueTiles.Clear();


                            foreach (GameObject g in attackRedTiles)
                            {
                                if (g != null)
                                    Destroy(g);
                            }                          //elimina tasselli rossi
                            attackRedTiles.Clear();
                        }



                    }
                }
            }
        }

    }

    private void OnMouseEnter()
    {
       
        if (!battleManager.stop && mapScript.finitoSpawn && GameObject.FindGameObjectsWithTag("Tutorial").Length == 0)
        {
            if (!tutorialFatto)
            {
                if (nome == "Nova" && mapScript.mapN == 2) { tutorialFatto = true; GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial14"), Camera.main.gameObject.transform); return; }
                if (nome == "Sear" && mapScript.mapN == 2) { tutorialFatto = true; GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial16"), Camera.main.gameObject.transform); return; }

                if (nome == "Granius" && mapScript.mapN == 6) { tutorialFatto = true; GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial18"), Camera.main.gameObject.transform); return; }
                if (nome == "Thera" && mapScript.mapN == 6) { tutorialFatto = true; GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial20"), Camera.main.gameObject.transform); return; }
                if (nome == "Acquira" && mapScript.mapN == 6) { tutorialFatto = true; GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial22"), Camera.main.gameObject.transform); return; }
                if (nome == "Hydris" && mapScript.mapN == 6) { tutorialFatto = true; GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial24"), Camera.main.gameObject.transform); return; }

                if (nome == "Aeria" && mapScript.mapN == 4) { tutorialFatto = true; GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial26"), Camera.main.gameObject.transform); return; }
                if (nome == "Skye" && mapScript.mapN == 4) { tutorialFatto = true; GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial28"), Camera.main.gameObject.transform); return; }
            }
            mouseIsOver = true;

            if (battleManager.phase == "Player" && !(Input.GetKey(KeyCode.Mouse0)))
            {
                HighlightMov();
            }
        }
    }


    private void OnMouseExit()
    {
        if (!battleManager.stop && mapScript.finitoSpawn && GameObject.FindGameObjectsWithTag("Tutorial").Length == 0)
        {
            mouseIsOver = false;
            if (infoGUISpawned && infoGUI != null)
            {
                infoGUI.GetComponent<infoGUIScript>().Rimuovi();
                infoGUISpawned = false;
            }


            if (canMove && battleManager.phase == "Player" && !(Input.GetKey(KeyCode.Mouse0)))
            {
                if (movBlueTiles.Count > 0)
                {
                    mov_tiles_coords.Clear();
                    foreach (GameObject g in movBlueTiles) Destroy(g);                          //elimina tasselli blu
                    movBlueTiles.Clear();


                    foreach (GameObject g in attackRedTiles) Destroy(g);                          //elimina tasselli rossi
                    attackRedTiles.Clear();
                }



            }
        }
        
    }


    private void OnMouseDown()
    {
        if (!battleManager.stop && mapScript.finitoSpawn && GameObject.FindGameObjectsWithTag("Tutorial").Length == 0)
        {
            if (canMove && battleManager.phase == "Player" && dragging == false)
            {


                if (movBlueTiles.Count > 0)
                {
                    mov_tiles_coords.Clear();
                    foreach (GameObject g in movBlueTiles) Destroy(g);                          //elimina tasselli blu
                    movBlueTiles.Clear();

                    foreach (GameObject g in attackRedTiles) Destroy(g);                          //elimina tasselli rossi
                    attackRedTiles.Clear();
                }
                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("select");
                transform.GetChild(0).GetComponent<Animator>().Play("Select");
                dragging = true;
                HighlightMov();                                                                //spawna tasselli blu movimento
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);



            }
        }
    }

    
    private void OnMouseUp()
    {
        
        if (!battleManager.stop && mapScript.finitoSpawn && GameObject.FindGameObjectsWithTag("Tutorial").Length == 0)
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

                    haFattoQualcosa = false;
                    canMove = false;
                    battleManager.unmovedUnits.Remove(gameObject);

                    transform.GetChild(0).GetComponent<Animator>().speed = 0;

                    foreach (GameObject g in battleManager.unmovedUnits)
                    {
                        g.transform.GetChild(0).GetComponent<Animator>().Play("Select");          //inizia animazione di selezione personaggio
                    }


                    Component[] renderers = transform.GetChild(0).GetComponentsInChildren(typeof(Renderer)); //rende grigio il personaggio
                    foreach (Renderer childRenderer in renderers)
                    {
                        childRenderer.material.color = new Color(0.3F, 0.3F, 0.3F);
                    }


                }
                else
                {



                    foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        if (Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x) == e.GetComponent<enemyScript>().x && Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y) == e.GetComponent<enemyScript>().y)
                        {

                            foreach (GameObject t in attackRedTiles)
                            {
                                if (e.GetComponent<enemyScript>().x == t.transform.position.x && e.GetComponent<enemyScript>().y == t.transform.position.y)
                                {
                                    cura = false;
                                    x = (int) newPosTile.transform.position.x;
                                    y = (int) newPosTile.transform.position.y;
                                    int[] output = Camera.main.GetComponent<battleManager>().pvp(e, this.gameObject, "player", cura);     //inizia pvp
                                    Destroy(player_tile);
                                    StartCoroutine(Camera.main.GetComponent<battleManager>().CaricaCombat(e, this.gameObject, output, "player", cura));
                                    Debug.Log("PVP");
                                    break;
                                }
                            }

                        }
                    }
                    foreach (GameObject e in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        if (Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).x) == e.GetComponent<playerScript>().x && Mathf.RoundToInt((Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset).y) == e.GetComponent<playerScript>().y && heal == true && e.GetComponent<playerScript>().hp!=e.GetComponent<playerScript>().maxHp && e!=gameObject)
                        {

                            foreach (GameObject t in movBlueTiles)
                            {
                                if (e.GetComponent<playerScript>().x == t.transform.position.x && e.GetComponent<playerScript>().y == t.transform.position.y)
                                {
                                    cura = true;
                                    x = (int)newPosTile.transform.position.x;
                                    y = (int)newPosTile.transform.position.y;
                                    int[] output = Camera.main.GetComponent<battleManager>().pvp(e, this.gameObject, "player", cura);     //inizia pvp
                                    Destroy(player_tile);
                                    StartCoroutine(Camera.main.GetComponent<battleManager>().CaricaCombat(e, this.gameObject, output, "player", cura));
                                    Debug.Log("PVP");
                                    break;
                                }
                            }

                        }
                    }

                }

                foreach (GameObject g in movBlueTiles) Destroy(g);                          //elimina tasselli blu
                movBlueTiles.Clear();
                foreach (GameObject g in attackRedTiles) Destroy(g);                          //elimina tasselli rossi
                attackRedTiles.Clear();

                Camera.main.GetComponent<battleManager>().UpdateEnemyMov();
                if (!canMove)
                {
                    Destroy(player_tile);                                                                   //rimuove tassello lampeggiante
                }
            }
        }
        //this.LevelUp();
    }

    public void endPvp()
    {
        if (!(battleManager.phase == "Player")) return;
        Destroy(forecast);

        if (mapScript.mapN == 1)
        {
            battleManager.pvpTutorial++;
            
            if(battleManager.pvpTutorial==1)
            {
                GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial4"), battleManager._mainCamera.gameObject.transform);
            }
        }
        

        canMove = false;
        if(gameObject!=null)battleManager.unmovedUnits.Remove(gameObject);

        transform.GetChild(0).GetComponent<Animator>().speed = 0;

        foreach (GameObject g in battleManager.unmovedUnits)
        {
            g.transform.GetChild(0).GetComponent<Animator>().Play("Select");          //inizia animazione di selezione personaggio
        }


        Component[] renderers = transform.GetChild(0).GetComponentsInChildren(typeof(Renderer)); //rende grigio il personaggio
        foreach (Renderer childRenderer in renderers)
        {
            childRenderer.material.color = new Color(0.3F, 0.3F, 0.3F);
        }

        foreach (GameObject g in movBlueTiles) Destroy(g);                          //elimina tasselli blu
        movBlueTiles.Clear();
        foreach (GameObject g in attackRedTiles) Destroy(g);                          //elimina tasselli rossi
        attackRedTiles.Clear();
        GameObject.Find("Main Camera").GetComponent<battleManager>().UpdateEnemyMov();
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("InfoCanvas")){
            Destroy(e);
        }

        StartCoroutine(fixaStoBugDiMerda());

    }

    IEnumerator fixaStoBugDiMerda()
    {
        yield return new WaitForEndOfFrame();
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Rimuovere")) Destroy(p);
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("InfoCanvas")) Destroy(p);

    }

    public void LevelUp()
    {
        

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

        Object canvas = Resources.Load("Level Up Canvas", typeof(GameObject));

        GameObject levelUpCanvas = (GameObject)Instantiate(canvas, new Vector3(0.6f, 3.6f, 0), Quaternion.identity);

        levelUpCanvas.GetComponent<levelUpScript>().Setup(nome,lvl,maxHp,str,mag,dex,spd,lck,def,res,increments);

        lvl++;
        maxHp += increments[0];            //incrementa i valori
        str += increments[1];
        mag += increments[2];
        dex += increments[3];
        spd += increments[4];
        lck += increments[5];
        def += increments[6];
        res += increments[7];

        healthbar.SetMaxHealth(maxHp);


        



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




