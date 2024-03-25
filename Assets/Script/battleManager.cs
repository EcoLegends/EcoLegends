using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using Dev.ComradeVanti.WaitForAnim;
using System.Text.RegularExpressions;

public class battleManager : MonoBehaviour
{

    public static string phase;
    public static int mapDimX;
    public static int mapDimY;

    public static List<GameObject> units = new List<GameObject>();
    public static List<GameObject> unmovedUnits = new List<GameObject>();
    public static List<GameObject> enemies = new List<GameObject>();

    public static List<string> dialoghiFatti = new List<string>();

    public bool showEnemyMovement = false;
    private List<GameObject> movTiles = new List<GameObject>();

    public static bool removeGUI = false;

    private Vector3 _origin;
    private Vector3 _difference;        //roba del tutorial bohh
    public static Camera _mainCamera;
    private bool _isDragging;

    List<GameObject> unmovedEnemies = new List<GameObject>();

    string animationText = "0";
    float animationTime = 0f;


    public static int pvpTutorial = 0;
    private int phaseTutorial = 0;

    GameObject phaseCanvas;


    public static bool canMoveEnemy = true;

    public static bool stop = false;

    private void Awake() //roba del tutorial bohh
    {
        _mainCamera = Camera.main;
    }

    public void OnDrag(InputAction.CallbackContext ctx) //roba del tutorial bohh
    {
        if (!stop && mapScript.finitoSpawn && GameObject.FindGameObjectsWithTag("Tutorial").Length == 0)
        {
            if (ctx.started) _origin = GetMousePosition;

            _isDragging = ctx.started || ctx.performed;
        }
        
    }

    private void LateUpdate() //roba del tutorial bohh
    {
        

        if (!stop && mapScript.finitoSpawn && GameObject.FindGameObjectsWithTag("Tutorial").Length==0)
        {
            if (units.Count == 0)
            {
                Debug.Log("PERSO!!!!");
                stop = true;
                mapScript.finitoSpawn = false;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                
            }

            if (enemies.Count == 0)
            {
                Debug.Log("VINTO!!!!");
                stop = true;
                mapScript.finitoSpawn = false;
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
                

            }
        }


        if (!_isDragging) return;

        _difference = GetMousePosition - transform.position;
        transform.position = _origin - _difference;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, (Camera.main.orthographicSize * 1.76f - 3.3f), mapDimX- (Camera.main.orthographicSize * 1.76f - 3.3f) - 1), Mathf.Clamp(transform.position.y, (Camera.main.orthographicSize * 0.98f - 3.3f), mapDimY- (Camera.main.orthographicSize * 0.98f - 3.3f) - 1), transform.position.z);
    }

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()); //roba del tutorial bohh


    public void OnPressC(InputAction.CallbackContext ctx)
    {
        if (!stop && mapScript.finitoSpawn && GameObject.FindGameObjectsWithTag("Tutorial").Length == 0) { 
            if (ctx.started) showEnemyMovement = !showEnemyMovement;
            Debug.Log("Show: " + showEnemyMovement);
            UpdateEnemyMov();
            if (showEnemyMovement == false) foreach (GameObject g in movTiles) Destroy(g);
        }
    }




    public int[] pvp(GameObject e, GameObject p, string initial_turn, bool cura)
    {
        
        if(cura==false)
        {
            playerScript player = p.GetComponent<playerScript>();
            enemyScript enemy = e.GetComponent<enemyScript>();                
            

            List<string> turns = new List<string>();
            turns.Add(initial_turn);


            int playerAtk;
            int playerHit;
            int playerAS; 
            int playerDmg;
            int playerProt;
            int playerCrit;

            int enemyAtk;
            int enemyHit;
            int enemyAS;
            int enemyDmg;
            int enemyProt;
            int enemyCrit;

            Vector2 newPos = new Vector2(player.x, player.y);

            foreach (GameObject t in player.movBlueTiles)
            {
                if (Mathf.Abs(t.transform.position.x - player.transform.position.x) + Mathf.Abs(t.transform.position.y - player.transform.position.y) < Mathf.Abs(newPos.x - player.transform.position.x) + Mathf.Abs(newPos.y - player.transform.position.y))
                {
                    if (Mathf.Abs(t.transform.position.x - enemy.x) + Mathf.Abs(t.transform.position.y - enemy.y) == player.weaponMaxRange)
                    {
                        bool check = true;
                        foreach (GameObject pp in units)
                        {
                            if (pp != p && pp.GetComponent<playerScript>().x == t.transform.position.x && pp.GetComponent<playerScript>().y == t.transform.position.y) check = false;
                        }
                        if (check)
                            newPos = new Vector2(t.transform.position.x, t.transform.position.y);
                    }
                }
            }

            player.x = (int)newPos.x;

            player.y = (int)newPos.y;

            int playerAVOBonus = GameObject.Find("map").GetComponent<mapScript>().mapTiles[player.x, player.y].GetComponent<tileScript>().avoBonus; 
            int enemyAVOBonus = GameObject.Find("map").GetComponent<mapScript>().mapTiles[enemy.x, enemy.y].GetComponent<tileScript>().avoBonus;
            Debug.Log("AVO BONUS: " + playerAVOBonus + " " + enemyAVOBonus);
            

            //calcolazione del player

            playerAS = player.weaponWt - (player.str / 5);
            if (playerAS < 0) playerAS = 0;
            playerAS = player.spd - playerAS;

            enemyAS = enemy.weaponWt - (enemy.str / 5);
            if (enemyAS < 0) enemyAS = 0;
            enemyAS = enemy.spd - enemyAS;


            if (player.weaponIsMagic == false)
            {

                playerAtk = player.weaponMt + player.str;

                playerHit = (player.weaponHit + player.dex) - (enemyAS + enemyAVOBonus);

                enemyProt = enemy.def;

            }

            else
            {

                playerAtk = player.weaponMt + player.mag;

                playerHit = (player.weaponHit + (player.dex + player.lck) / 2) - ((enemy.spd+enemy.lck)/2+ enemyAVOBonus);

                enemyProt = enemy.res;
            }


            //calcolazione dell'enemy

            


            if (enemy.weaponIsMagic == false)
            {
                

                enemyAtk = enemy.weaponMt + enemy.str;

                enemyHit = (enemy.weaponHit + enemy.dex) - (playerAS + playerAVOBonus);

                playerProt = player.def;

            }

            else
            {

                enemyAtk = enemy.weaponMt + enemy.mag;

                enemyHit = (enemy.weaponHit + (enemy.dex + enemy.lck) / 2) - ((player.spd + player.lck) / 2 + playerAVOBonus);

                playerProt = player.res;
            }


            if (player.unitEffective == enemy.unitType)
            {

                playerAtk = Mathf.FloorToInt(playerAtk * 1.5f);
                playerHit += 15;
                enemyHit -= 15;
            }

            if (enemy.unitEffective == player.unitType)
            {

                enemyAtk = Mathf.FloorToInt(playerAtk * 1.5f);
                enemyHit += 15;
                playerHit -= 15;
            }


            playerDmg = playerAtk - enemyProt;
            enemyDmg = enemyAtk - playerProt;

            playerCrit = (player.weaponCrit + (player.dex+player.lck) / 2) - enemy.lck;
            enemyCrit = (enemy.weaponCrit + (enemy.dex + enemy.lck) / 2) - player.lck;

            playerDmg = (int)Mathf.Clamp(playerDmg, 0, 999);    //mette numeri non <0
            enemyDmg = (int)Mathf.Clamp(enemyDmg, 0, 999);
            playerHit = (int)Mathf.Clamp(playerHit, 0, 100);
            enemyHit = (int)Mathf.Clamp(enemyHit, 0, 100);
            playerCrit = (int)Mathf.Clamp(playerCrit, 0, 100);
            enemyCrit = (int)Mathf.Clamp(enemyCrit, 0, 100);

            

            int distance = (int) Mathf.Abs(player.x - enemy.x) + (int) Mathf.Abs(player.y - enemy.y);               

            if (initial_turn == "player")                                         //doppi turni con attack speed >=4 
            {
                if (enemy.weaponMinRange <= distance && distance <= enemy.weaponMaxRange)
                {
                    turns.Add("enemy");
                    if (enemyAS >= playerAS + 4) turns.Add("enemy");
                }
                if (playerAS >= enemyAS + 4) turns.Add("player");
            }
            else
            {
                if (player.weaponMinRange <= distance && distance <= player.weaponMaxRange)
                {
                    turns.Add("player");
                    if (playerAS >= enemyAS + 4) turns.Add("player");
                }
                if (enemyAS >= playerAS + 4) turns.Add("enemy");
            }


            Debug.Log("PLAYER               ENEMY");
            Debug.Log("DMG: " + playerDmg + "               " + enemyDmg);
            Debug.Log("HIT: " + playerHit + "               " + enemyHit);
            Debug.Log("CRIT: " + playerCrit + "               " + enemyCrit);
            Debug.Log("AS: " + playerAS + "               " + enemyAS);
            Debug.Log("TURNI:");
            foreach (string t in turns) Debug.Log(t);

            int[] returnList = { playerDmg, playerHit, playerCrit, playerAS, enemyDmg, enemyHit, enemyCrit, enemyAS};

            return returnList;
        }

        int [] array = {0,0,0,0,0,0,0,0};

        return array;
    }



    public void UpdateEnemyMov()
    {
        if (showEnemyMovement)
        {
            foreach (GameObject g in movTiles) if(g!=null) Destroy(g);
            List<Vector2> mov = new List<Vector2>();

            foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                foreach(Vector2 v in e.GetComponent<enemyScript>().GetMovTiles())
                {
                    if(!mov.Contains(v)) mov.Add(v);
                }
            }

            UnityEngine.Object mov_tile_prefab = Resources.Load("movTileEnemyAllPrefab", typeof(GameObject));



            foreach (Vector2 v in mov)
            {
                GameObject mov_tile = (GameObject)Instantiate(mov_tile_prefab, new Vector3(v.x, v.y, -2), Quaternion.identity);
                mov_tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
                movTiles.Add(mov_tile);
            }


        }
    }

    public IEnumerator CaricaCombat(GameObject e, GameObject p, int[] output, string initial_turn, bool cura){

    
                    
        playerScript player = p.GetComponent<playerScript>();

        if(cura==false)
        {
            enemyScript enemy = e.GetComponent<enemyScript>();;


            enemy.movType = "move";

            Scene activeScene = SceneManager.GetActiveScene();

            GameObject temp = new GameObject( "temp" );  

            GameObject[] allObjects = activeScene.GetRootGameObjects();

            foreach (GameObject go in allObjects)   
            {

                go.transform.SetParent(temp.transform, false);

            }

            AsyncOperation async = SceneManager.LoadSceneAsync( "CombatScene", LoadSceneMode.Additive);

            while (!async.isDone)     
            {

                yield return new WaitForEndOfFrame();

            }


            Scene battleScene = SceneManager.GetSceneByName( "CombatScene" );
            SceneManager.SetActiveScene(battleScene);

            temp.SetActive(false);       

            GameObject spritePlayer = Instantiate(Resources.Load<GameObject>("Characters/" + player.textureFile));
            SceneManager.MoveGameObjectToScene(spritePlayer, battleScene);

            GameObject playerParent = new GameObject();
            playerParent.transform.position = new Vector3(-2, 0, 0);

            spritePlayer.transform.SetParent(playerParent.transform);
            spritePlayer.transform.localPosition = new Vector3(0, 0, 0);

            GameObject spriteEnemy = Instantiate(Resources.Load<GameObject>("Characters/" + enemy.textureFile));
            
            SceneManager.MoveGameObjectToScene(spriteEnemy, battleScene);

            GameObject enemyParent = new GameObject();
            spriteEnemy.transform.SetParent(enemyParent.transform);
            spriteEnemy.transform.localPosition = new Vector3(0, 0, 0);
            enemyParent.transform.position = new Vector3(2, 0, 0);
            enemyParent.transform.Rotate(0, 180, 0);

            

            SceneManager.SetActiveScene(battleScene);

            GameObject.Find("CombatCamera").GetComponent<PvPscript>().iniziaPvPvero(e, p, output, spritePlayer, spriteEnemy, playerParent, enemyParent,activeScene, temp, initial_turn, cura);
        }
        else{

            playerScript player2 = e.GetComponent<playerScript>();;

            Scene activeScene = SceneManager.GetActiveScene();

            GameObject temp = new GameObject( "temp" );  

            GameObject[] allObjects = activeScene.GetRootGameObjects();

            foreach (GameObject go in allObjects)   
            {

                go.transform.SetParent(temp.transform, false);

            }

            AsyncOperation async = SceneManager.LoadSceneAsync( "CombatScene", LoadSceneMode.Additive);

            while (!async.isDone)     
            {

                yield return new WaitForEndOfFrame();

            }


            Scene battleScene = SceneManager.GetSceneByName( "CombatScene" );
            SceneManager.SetActiveScene(battleScene);

            temp.SetActive(false);       

            GameObject spritePlayer = Instantiate(Resources.Load<GameObject>("Characters/" + player.textureFile));
            SceneManager.MoveGameObjectToScene(spritePlayer, battleScene);

            GameObject playerParent = new GameObject();
            playerParent.transform.position = new Vector3(-2, 0, 0);

            spritePlayer.transform.SetParent(playerParent.transform);
            spritePlayer.transform.localPosition = new Vector3(0, 0, 0);

            GameObject spritePlayer2 = Instantiate(Resources.Load<GameObject>("Characters/" + player2.textureFile));
            
            SceneManager.MoveGameObjectToScene(spritePlayer2, battleScene);

            GameObject player2Parent = new GameObject();
            spritePlayer2.transform.SetParent(player2Parent.transform);
            spritePlayer2.transform.localPosition = new Vector3(0, 0, 0);
            player2Parent.transform.position = new Vector3(2, 0, 0);
            player2Parent.transform.Rotate(0, 180, 0);

            

            SceneManager.SetActiveScene(battleScene);

            GameObject.Find("CombatCamera").GetComponent<PvPscript>().iniziaPvPvero(e, p, output, spritePlayer, spritePlayer2, playerParent, player2Parent,activeScene, temp, initial_turn, cura);
        }


    }



    void Start()
    {
        phase = "animation";
        animationText = "player";
        animationTime = Time.time + 3;

        phaseCanvas = (GameObject)Instantiate(Resources.Load("playerPhaseCanvas", typeof(GameObject)), this.transform);

        stop = false;
        
    }

    

    

    void Update()
    {
        if (!(GameObject.FindGameObjectsWithTag("Tutorial").Length == 0) && phaseCanvas!=null) { 
            animationTime = Time.time + 3;
            phaseCanvas.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            phaseCanvas.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            phaseCanvas.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
        if (!stop && mapScript.finitoSpawn && GameObject.FindGameObjectsWithTag("Tutorial").Length == 0)
        {

            Camera.main.orthographicSize = Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel")*-2 + Camera.main.orthographicSize,2.2f,6.5f); //zoom
            transform.localScale = Vector3.one * Camera.main.orthographicSize / 5;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, (Camera.main.orthographicSize * 1.76f - 3.3f), mapDimX - (Camera.main.orthographicSize * 1.76f - 3.3f) - 1), Mathf.Clamp(transform.position.y, (Camera.main.orthographicSize * 0.98f - 3.3f), mapDimY - (Camera.main.orthographicSize * 0.98f - 3.3f) - 1), transform.position.z);

            

            if (phase == "Enemy" && unmovedEnemies.Count == 0 && animationText == "0" && canMoveEnemy == true)
            {
                phase = "animation";
                animationText = "player";
                animationTime = Time.time + 3;
                UpdateEnemyMov();

                phaseCanvas = (GameObject)Instantiate(Resources.Load("playerPhaseCanvas", typeof(GameObject)), this.transform);
                phaseTutorial++;

                if (mapScript.mapN == 1 && phaseTutorial == 2)
                {

                    GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial8"), Camera.main.gameObject.transform);
                }
                if (mapScript.mapN == 1 && phaseTutorial == 4)
                {

                    GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial10"), Camera.main.gameObject.transform);
                }
                if (mapScript.mapN == 1 && phaseTutorial == 6)
                {

                    GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial12"), Camera.main.gameObject.transform);
                }

            }
            else if(unmovedEnemies.Count > 0 && canMoveEnemy == true)
            {
                canMoveEnemy = false;
                unmovedEnemies[0].GetComponent<enemyScript>().Move();
                unmovedEnemies.RemoveAt(0);
                
            }


            if (phase == "Player" && unmovedUnits.Count == 0 && animationText == "0")
            {
                phase = "animation";
                animationText = "enemy";
                animationTime = Time.time + 3;
                removeGUI=true;

                phaseTutorial++;

                if (mapScript.mapN==1&&phaseTutorial==1)
                {
                    
                    GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial6"), Camera.main.gameObject.transform);
                }

                phaseCanvas = (GameObject)Instantiate(Resources.Load("enemyPhaseCanvas", typeof(GameObject)), this.transform);
            }



            if (phase == "animation")
            {
                if (Time.time < animationTime)
                {
                    float gradient = animationTime - Time.time;
                    if (animationTime - Time.time > 2 ) gradient = 3 - gradient;

                    if (animationTime - Time.time > 1 && animationTime - Time.time < 2) gradient = 1;

                    

                    phaseCanvas.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (gradient));
                    phaseCanvas.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (gradient));
                    phaseCanvas.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (gradient));

                }
                else if (animationText == "enemy")
                {
                    removeGUI = false;
                    Destroy(phaseCanvas);
                    animationText = "0";
                    phase = "Enemy";
                    Debug.Log("Enemy Phase");

                    foreach (GameObject p in GameObject.FindGameObjectsWithTag("Rimuovere")) Destroy(p);
                    foreach (GameObject p in GameObject.FindGameObjectsWithTag("InfoCanvas")) Destroy(p);


                    foreach (GameObject p in units)
                    {
                        Component[] renderers = p.transform.GetChild(0).GetComponentsInChildren(typeof(Renderer)); //rende normale il personaggio
                        foreach (Renderer childRenderer in renderers)
                        {
                            childRenderer.material.color = new Color(1F, 1F, 1F);
                        }

                        p.transform.GetChild(0).GetComponent<Animator>().speed = 1;
                    }

                    foreach (GameObject enemy in enemies)
                    {
                        unmovedEnemies.Add(enemy);
                    }
                    canMoveEnemy = true;


                }
                else if(animationText == "player")
                {
                    Destroy(phaseCanvas);
                    phase = "Player";
                    animationText = "0";
                    Debug.Log("Player Phase");

                    foreach (GameObject unit in units)
                    {
                        unit.GetComponent<playerScript>().CanMoveAgain();
                        unit.transform.GetChild(0).GetComponent<Animator>().Play("Select");          //inizia animazione di selezione personaggio
                    }
                }


            }

    
        }
        
    }

}
