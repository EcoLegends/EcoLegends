using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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


    public void pvp(GameObject e, GameObject p)
    {
        //enemy = e.GetComponent<enemyScript>();                 //fai ctrl+k      ctrl+u  per togliere i commenti
        //player = p.GetComponent<playerScript>();

        ////calcolazione del dmg player

        //double playerAtk;

        //int playerHit;

        //if (player.weaponIsMagic == false){

        //    playerAtk = player.weaponMt + player.str;

        //    playerHit=player.weaponHit+player.dex;

        //}

        //else{  

        //    playerAtk = player.weaponMt + player.mag;

        //    playerHit=player.weaponHit+(player.dex+player.lck)/2;

        //}

        //double playerDmg;

        //if(player.unitEffective==enemy.unitType){

        //    playerDmg*=1.5;

        //}
        //else if(enemy.unitType!=player.unitEffective and enemy.unitType!=player.unitType){

        //    playerHit-=15;

        //}

        //double playerAS;

        //playerAS = player.spd - (player.weaponWt + )

        ////calcolazione del dmg enemy

        //double enemyAtk;

        //int enemyHit;

        //if (enemy.weaponIsMagic == false){

        //    enemyAtk = enemy.weaponMt + enemy.str;

        //    enemyHit=enemy.weaponHit + enemy.dex;

        //}

        //else{  

        //    enemyAtk = enemy.weaponMt + enemy.mag;

        //    enemyHit = enemy.weaponHit + (enemy.dex+enemy.lck)/2;

        //}

        //double enemyDmg;

        //if(enemy.unitEffective==player.unitType){

        //    enemyDmg*=1.5;

        //}
        //else if(player.unitType!=enemy.unitEffective and player.unitType!=enemy.unitType){

        //    enemyHit-=15;

        //}
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
