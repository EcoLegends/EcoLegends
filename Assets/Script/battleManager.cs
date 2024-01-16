using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class battleManager : MonoBehaviour
{

    public static string phase;

    public static List<GameObject> units = new List<GameObject>();
    public static List<GameObject> unmovedUnits = new List<GameObject>();
    public static List<GameObject> enemies = new List<GameObject>();

    public bool showEnemyMovement = false;
    private List<GameObject> movTiles = new List<GameObject>();


    private Vector3 _origin;
    private Vector3 _difference;        //roba del tutorial bohh
    private Camera _mainCamera;
    private bool _isDragging;

    List<GameObject> unmovedEnemies = new List<GameObject>();

    string animationText = "0";
    float animationTime = 0f;

    GameObject phaseCanvas;

    private void Awake() //roba del tutorial bohh
    {
        _mainCamera = Camera.main;
    }

    public void OnDrag(InputAction.CallbackContext ctx) //roba del tutorial bohh
    {
        if (ctx.started) _origin = GetMousePosition;

        _isDragging = ctx.started || ctx.performed;
    }

    private void LateUpdate() //roba del tutorial bohh
    {
        if (!_isDragging) return;

        _difference = GetMousePosition - transform.position;
        transform.position = _origin - _difference;
    }

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()); //roba del tutorial bohh


    public void OnPressC(InputAction.CallbackContext ctx)
    {
        if(ctx.started) showEnemyMovement = !showEnemyMovement;
        Debug.Log("Show: "+showEnemyMovement);
        UpdateEnemyMov();
        if(showEnemyMovement == false) foreach(GameObject g in movTiles) Destroy(g);

    }


    playerScript player;
    enemyScript enemy;


    public int[] pvp(GameObject e, GameObject p, string initial_turn)
    {
        enemy = e.GetComponent<enemyScript>();                
        player = p.GetComponent<playerScript>();

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

        int playerAVOBonus = 0; //bonus per quando sei nelle foreste (non ho voglia)
        int enemyAVOBonus = 0;

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

        Vector2 newPos = new Vector2(player.x, player.y);

        foreach (GameObject t in player.movBlueTiles)
        {
            if (Mathf.Abs(t.transform.position.x - player.transform.position.x) + Mathf.Abs(t.transform.position.y - player.transform.position.y) < Mathf.Abs(newPos.x - player.transform.position.x) + Mathf.Abs(newPos.y - player.transform.position.y))
            {
                if (Mathf.Abs(t.transform.position.x - enemy.x) + Mathf.Abs(t.transform.position.y - enemy.y) == player.weaponMaxRange) 
                { 
                    newPos = new Vector2(t.transform.position.x, t.transform.position.y);
                }
            }
        }

        int distance = (int) Mathf.Abs(newPos.x - enemy.x) + (int) Mathf.Abs(newPos.y - enemy.y);               

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



    public void UpdateEnemyMov()
    {
        if (showEnemyMovement)
        {
            foreach (GameObject g in movTiles) Destroy(g);
            List<Vector2> mov = new List<Vector2>();

            foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                foreach(Vector2 v in e.GetComponent<enemyScript>().GetMovTiles())
                {
                    if(!mov.Contains(v)) mov.Add(v);
                }
            }

            Object mov_tile_prefab = AssetDatabase.LoadAssetAtPath("Assets/movTileEnemyAllPrefab.prefab", typeof(GameObject));



            foreach (Vector2 v in mov)
            {
                GameObject mov_tile = (GameObject)Instantiate(mov_tile_prefab, new Vector3(v.x, v.y, -2), Quaternion.identity);
                mov_tile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
                movTiles.Add(mov_tile);
            }


        }
    }


    void Start()
    {
        phase = "animation";
        animationText = "player";
        animationTime = Time.time + 3;

        phaseCanvas = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Resources/playerPhaseCanvas.prefab", typeof(GameObject)), this.transform);
        
        
    }

    

    public static bool canMoveEnemy = true;

    void Update()
    {

        Camera.main.orthographicSize = Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel")*-2 + Camera.main.orthographicSize,2,6.5f); //zoom
        transform.localScale = Vector3.one * Camera.main.orthographicSize / 5;



        if (phase == "Enemy" && unmovedEnemies.Count == 0 && animationText == "0" && canMoveEnemy == true)
        {
            phase = "animation";
            animationText = "player";
            animationTime = Time.time + 3;

            phaseCanvas = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Resources/playerPhaseCanvas.prefab", typeof(GameObject)), this.transform);
            
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

            phaseCanvas = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Resources/enemyPhaseCanvas.prefab", typeof(GameObject)), this.transform);
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
                Destroy(phaseCanvas);
                animationText = "0";
                phase = "Enemy";
                Debug.Log("Enemy Phase");

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
