
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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

    bool hasBosses = false;

    bool escMenu = false;

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
        transform.localScale = Vector3.one * Camera.main.orthographicSize / 5;

        if (!stop && mapScript.finitoSpawn && GameObject.FindGameObjectsWithTag("Tutorial").Length==0)
        {
            if (units.Count == 0)
            {
                Debug.Log("PERSO!!!!");
                stop = true;
                mapScript.finitoSpawn = false;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                
            }

            if (hasBosses)
            {
                int bossCount = 0;
                foreach(GameObject enemy in enemies) if(enemy.GetComponent<enemyScript>().boss) bossCount++;
                if (bossCount == 0)
                {
                    Debug.Log("VINTO!!!!");
                    stop = true;
                    mapScript.finitoSpawn = false;
                    gameObject.transform.GetChild(2).gameObject.SetActive(true);
                }
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

    public void OnPressEscape(InputAction.CallbackContext ctx)
    {
        if (phase=="Player"&&!stop && mapScript.finitoSpawn && GameObject.FindGameObjectsWithTag("Tutorial").Length == 0 && !escMenu)
        {
            escMenu = true;
            transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (phase == "Player" && !stop && mapScript.finitoSpawn && escMenu == true)
        {
            transform.GetChild(3).gameObject.SetActive(false);
            escMenu = false;
        }
    }

    public void EndTurn()
    {
        transform.GetChild(3).gameObject.SetActive(false);
        escMenu = false;
        foreach (GameObject unit in units)
        {
            unit.GetComponent<playerScript>().canMove = false;

        }
        unmovedUnits.Clear();
    }

    public void Restart()
    {
        StartCoroutine(changeScene("MainScene"));
    }
    public void Menu()
    {
        StartCoroutine(changeScene("Menu"));
    }

    public IEnumerator changeScene(string sceneName)
    {
        stop = true;
        transform.GetChild(3).gameObject.SetActive(false);
        GameObject.Find("Music").GetComponent<musicScript>().Rimuovi();
        GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(1);
        yield return new WaitForSeconds(1);
        AsyncOperation async2 = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
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

            //Vector2 newPos = new Vector2(player.x, player.y);

            //foreach (GameObject t in player.movBlueTiles)
            //{
            //    if (Mathf.Abs(t.transform.position.x - player.transform.position.x) + Mathf.Abs(t.transform.position.y - player.transform.position.y) < Mathf.Abs(newPos.x - player.transform.position.x) + Mathf.Abs(newPos.y - player.transform.position.y))
            //    {
            //        if (Mathf.Abs(t.transform.position.x - enemy.x) + Mathf.Abs(t.transform.position.y - enemy.y) == player.weaponMaxRange)
            //        {
            //            bool check = true;
            //            foreach (GameObject pp in units)
            //            {
            //                if (pp != p && pp.GetComponent<playerScript>().x == t.transform.position.x && pp.GetComponent<playerScript>().y == t.transform.position.y) check = false;
            //            }
            //            if (check)
            //                newPos = new Vector2(t.transform.position.x, t.transform.position.y);
            //        }
            //    }
            //}

            //player.x = (int)newPos.x;

            //player.y = (int)newPos.y;

            int playerAVOBonus = GameObject.Find("map").GetComponent<mapScript>().mapTiles[player.x, player.y].GetComponent<tileScript>().avoBonus; 
            int enemyAVOBonus = GameObject.Find("map").GetComponent<mapScript>().mapTiles[enemy.x, enemy.y].GetComponent<tileScript>().avoBonus;

            if (player.haFattoQualcosa == false && player.nome == "Skye") { playerAVOBonus += 15; }

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

                enemyAtk = Mathf.FloorToInt(enemyAtk * 1.5f);
                enemyHit += 15;
                playerHit -= 15;
            }


            playerDmg = playerAtk - enemyProt;
            enemyDmg = enemyAtk - playerProt;

            playerCrit = (player.weaponCrit + (player.dex+player.lck) / 2) - enemy.lck;
            enemyCrit = (enemy.weaponCrit + (enemy.dex + enemy.lck) / 2) - player.lck;

            
            if (playerAVOBonus > 0 && player.nome == "Granius") { playerCrit += 20; }
            

            playerDmg = (int)Mathf.Clamp(playerDmg, 0, 999);    //mette numeri non <0
            enemyDmg = (int)Mathf.Clamp(enemyDmg, 0, 999);
            playerHit = (int)Mathf.Clamp(playerHit, 0, 100);
            enemyHit = (int)Mathf.Clamp(enemyHit, 0, 100);
            playerCrit = (int)Mathf.Clamp(playerCrit, 0, 100);
            enemyCrit = (int)Mathf.Clamp(enemyCrit, 0, 100);

            

            int distance = (int) Mathf.Abs(player.x - enemy.x) + (int) Mathf.Abs(player.y - enemy.y);
            Debug.Log("DISTANCE: " + distance);

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

    public void WinCondition(Vector3 newpos, string txt, bool bosses)
    {
        stop = true;
        StartCoroutine(winConditionAnim(newpos, txt, bosses));
    }

    IEnumerator winConditionAnim(Vector3 newpos, string txt, bool bosses)
    {

        yield return new WaitForSeconds(0.5f);


        GameObject musicPlayer = (GameObject)Instantiate(Resources.Load("Music", typeof(GameObject)), Vector3.one, Quaternion.identity);
        musicPlayer.name = "Music";
        
        musicPlayer.GetComponent<musicScript>().Stop();
        Vector3 currentpos = transform.position;
        List<GameObject> glowtiles = new List<GameObject>();
        if (newpos != Vector3.zero)
        {
            

            for(float i = 0; i < 20; i++)
            {
                transform.position = Vector3.Lerp(currentpos,newpos,i/20);
                yield return new WaitForEndOfFrame();
            }
            hasBosses = false;
            if (bosses)
            {
                hasBosses = true;


                foreach (GameObject e in enemies)
                {
                    if(e.GetComponent<enemyScript>().boss)
                    {
                        GameObject glowtile = Instantiate(Resources.Load<GameObject>("glowTile 1"));
                        glowtile.transform.parent = e.transform;
                        glowtile.transform.localPosition = new Vector3(0, 0, -10);
                        
                        glowtiles.Add(glowtile);
                    }
                }
                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("Ding");
                yield return new WaitForSeconds(3);
            }
        }
        GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("Battle Condition");

        GameObject winCond = Instantiate(Resources.Load<GameObject>("WinConditions"),transform);
        winCond.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = txt;
        for (float i = 0; i < 20; i++)
        {
            winCond.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, i / 20 * 0.7f);
            winCond.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);
            winCond.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);
            winCond.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);
            winCond.transform.GetChild(4).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);
            winCond.transform.GetChild(5).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);
            winCond.transform.GetChild(6).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);
            yield return new WaitForEndOfFrame();
        }
            

        yield return new WaitForSeconds(3);

        for (float i = 20; i >= 0; i--)
        {
            winCond.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, i / 20 * 0.7f);
            winCond.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);
            winCond.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);
            winCond.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);
            winCond.transform.GetChild(4).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);
            winCond.transform.GetChild(5).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);
            winCond.transform.GetChild(6).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 20);

            foreach(GameObject g in glowtiles) g.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 20);
            yield return new WaitForEndOfFrame();
        }
        foreach (GameObject g in glowtiles) Destroy(g);
        Destroy(winCond);

        if (newpos != Vector3.zero)
        {
            for (float i = 0; i < 20; i++)
            {
                transform.position = Vector3.Lerp(newpos, currentpos, i / 20);
                yield return new WaitForEndOfFrame();
            }
        }
        musicPlayer.GetComponent<musicScript>().ChangeMusic( mapScript.music);
        

        

        switch (mapScript.mapN)
        {
            case 1: GameObject tutorial = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial0"), Camera.main.gameObject.transform); break;
            case 6: GameObject tutorial2 = Instantiate(Resources.Load<GameObject>("Tutorials/tutorial30"), Camera.main.gameObject.transform); break;
        }

        while(!(GameObject.FindGameObjectsWithTag("Tutorial").Length == 0))
        {
            yield return new WaitForEndOfFrame();
        }
        try{
            GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("player_phase");
        }catch(System.Exception e){}
        phase = "animation";
        animationText = "player";
        animationTime = Time.time + 3;
        phaseCanvas = (GameObject)Instantiate(Resources.Load("playerPhaseCanvas", typeof(GameObject)), this.transform);
        stop = false;
        
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
            GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("battlestart");
            GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(2);
            yield return new WaitForSeconds(0.5f);

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
            GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("battlestart");
            GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(2);
            yield return new WaitForSeconds(0.5f);
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
        escMenu = false;
        stop = true;
        units = new List<GameObject>();
        unmovedUnits = new List<GameObject>();
        enemies = new List<GameObject>();

        dialoghiFatti = new List<string>();

        showEnemyMovement = false;
        movTiles = new List<GameObject>();

        removeGUI = false;

    
        unmovedEnemies = new List<GameObject>();

        pvpTutorial = 0;
        phaseTutorial = 0;

        phase = "animation";
        animationText = "player";
        animationTime = Time.time + 3;

        GameObject map = Instantiate(Resources.Load<GameObject>("Mappe/Mappe Prefab/map1"));

}




    GameObject glowtile = null;
    void Update()
    {

        Debug.Log(units.Count +" unmoved "+unmovedUnits.Count);

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

                        
            if (phase == "Enemy" && unmovedEnemies.Count == 0 && animationText == "0" && canMoveEnemy == true )
            {
                if (glowtile != null) Destroy(glowtile);
                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("player_phase");
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
            else if(unmovedEnemies.Count > 0 && canMoveEnemy == true && Time.time > GameObject.Find("LevelLoader").GetComponent<LevelLoad>().time && GameObject.Find("LevelLoader").GetComponent<LevelLoad>().time != 0)
            {
                if (glowtile != null) Destroy(glowtile);
                glowtile = Instantiate(Resources.Load<GameObject>("glowTile"));
                glowtile.transform.parent = unmovedEnemies[0].transform;
                glowtile.transform.localPosition = new Vector3(0, 0, -10);
                canMoveEnemy = false;
                unmovedEnemies[0].GetComponent<enemyScript>().Move();
                unmovedEnemies.RemoveAt(0);
                
            }


            if (phase == "Player" && unmovedUnits.Count == 0 && animationText == "0")
            {
                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("enemyphase");
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

            bool qualcunoPuoMuoversi = false;
            foreach (GameObject unit in units)
            {
                if(unit.GetComponent<playerScript>().canMove) qualcunoPuoMuoversi=true;
               
            }
            if(qualcunoPuoMuoversi==false && unmovedUnits.Count > 0) unmovedUnits.Clear();


        }
        
    }

}
