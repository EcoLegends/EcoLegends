using Dev.ComradeVanti.WaitForAnim;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PvPscript : MonoBehaviour
{
    // Start is called before the first frame update

    
    GameObject sprite;
    Vector3 oldPos;
    

    

    void Start()
    {

    }

    // // Update is called once per frame
    // void Update()
    // {

    //     if(showDamage){
    //         Debug.Log("au");
    //         Debug.Log(sprite.transform.position);
            
    //         sprite.transform.position=Vector3.Lerp(sprite.transform.position,newPos,t*0.1f);
    //     }
        
    // }


    public void iniziaPvPvero(GameObject e, GameObject p, int[] output, GameObject spritePlayer, GameObject spriteEnemy, GameObject playerParent, GameObject enemyParent, Scene activeScene, GameObject temp, string initial_turn)
    {

        StartCoroutine(iniziaPvP(e, p, output, spritePlayer, spriteEnemy, playerParent, enemyParent,activeScene, temp, initial_turn));

    }



    public IEnumerator iniziaPvP(GameObject e, GameObject p, int[] output, GameObject spritePlayer, GameObject spriteEnemy, GameObject playerParent, GameObject enemyParent, Scene activeScene, GameObject temp, string initial_turn)
    {

        int playerAS = output[3];
        int enemyAS = output[7];
        bool move = false;
        
        List<string> turns = new List<string>();
        turns.Add(initial_turn);

        enemyScript enemy = e.GetComponent<enemyScript>();
        playerScript player = p.GetComponent<playerScript>();

        int distance = (int)Mathf.Abs(player.x - enemy.x) + (int)Mathf.Abs(player.y - enemy.y);

        if(initial_turn=="player"){
            if (enemy.weaponMinRange <= distance && distance <= enemy.weaponMaxRange)
        {
            turns.Add("enemy");
            if (enemyAS >= playerAS + 4) { turns.Add("enemy"); }
        }
        if (playerAS >= enemyAS + 4) { turns.Add("player"); }
        }else{
            if (player.weaponMinRange <= distance && distance <= player.weaponMaxRange)
        {
            turns.Add("player");
            if (playerAS >= enemyAS + 4) { turns.Add("player"); }
        }
        if (enemyAS >= playerAS + 4) { turns.Add("enemy"); }
        }
        

        yield return new WaitForEndOfFrame();


        foreach(string turn in turns){


            if (player.hp <= 0||enemy.hp<=0) break;

            if(turn=="player"){
                int playerHit = UnityEngine.Random.Range(1, 100); //player hit
                if (playerHit <= output[1])
                {

                    int playerCrit = UnityEngine.Random.Range(1, 100);  //se critta

                    Debug.Log("inizio");

                    sprite = GameObject.Find("Info Canvas").transform.GetChild(1).gameObject;
                    GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.localScale = Vector3.zero;
                    sprite.transform.localScale = Vector3.zero;
                    GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.SetActive(true);
                    
                    
                    if(move == false && player.weaponMaxRange==1){

                        for (float i = 0; i < 100; i++)
                        {
                        playerParent.transform.position += new Vector3(0.035f, 0, 0);
                        yield return new WaitForEndOfFrame();

                        }

                        move = true;
                    }
                    
                
                    if (playerCrit <= output[2])
                    {
                        spritePlayer.GetComponent<Animator>().Play("Crit");
                        GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=(output[0]*3).ToString();
                        enemy.hp = Mathf.Clamp(enemy.hp -= output[0] * 3, 0, enemy.maxHp);
                        GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.SetActive(true);
                    }
                    else
                    {
                        spritePlayer.GetComponent<Animator>().Play("Attack");
                        GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=output[0].ToString();
                        enemy.hp = Mathf.Clamp(enemy.hp -= output[0], 0, enemy.maxHp);
                    }
                    
                    yield return new WaitForEndOfFrame();
                    

                    AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                    AnimationClip firstClip = clipInfos[0].clip;
                    float duration = firstClip.length + Time.time;
                    Debug.Log(firstClip.length);

                    

                    while (Time.time < duration)
                    {
                        yield return new WaitForEndOfFrame();
                    }

                    oldPos = sprite.transform.position;

                    spriteEnemy.GetComponent<Animator>().Play("Hurt");
                    yield return new WaitForEndOfFrame();
                    clipInfos = spriteEnemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                    firstClip = clipInfos[0].clip;
                    duration = firstClip.length + Time.time;

                    for (float i = 0; i < 100; i++)
                    {
                        sprite.transform.localScale = new Vector3(Mathf.Clamp(i/10,0,1), Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1));
                        GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.localScale = new Vector3(Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1));
                        GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position += new Vector3(0, 0.01f, 0);
                        sprite.transform.position += new Vector3(0, 0.01f, 0);
                        yield return new WaitForEndOfFrame();

                    }

                    
                    
                    Debug.Log(firstClip.length);
                    while (Time.time < duration)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                    sprite.SetActive(false);
                    sprite.transform.position = oldPos;
                    GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position -= new Vector3(0, 1, 0);
                    Debug.Log("fine");
                    enemy.healthbar.SetHealth(enemy.hp);

                }
                else         //player miss
                {
                    Debug.Log("inizio");

                    if(move == false && player.weaponMaxRange==1){

                        for (float i = 0; i < 100; i++)
                        {
                        playerParent.transform.position += new Vector3(0.035f, 0, 0);
                        yield return new WaitForEndOfFrame();

                        }

                        move = true;
                    }

                    spritePlayer.GetComponent<Animator>().Play("Attack");


                    sprite = GameObject.Find("Info Canvas").transform.GetChild(0).gameObject;
                    sprite.transform.localScale = Vector3.zero;
                    GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.SetActive(true);

                    yield return new WaitForEndOfFrame();
                    AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                    AnimationClip firstClip = clipInfos[0].clip;
                    float duration = firstClip.length + Time.time;
                    float half = firstClip.length / 2 + Time.time;
                    bool enemyMissStarted = false;
                    Debug.Log(firstClip.length);



                    

                    while (Time.time < duration)
                    {
                        if(Time.time > half && !enemyMissStarted)
                        {
                            enemyMissStarted = true;
                            spriteEnemy.GetComponent<Animator>().Play("Miss");
                        }
                        yield return new WaitForEndOfFrame();
                    }

                    oldPos = sprite.transform.position;


                    for (float i = 0; i < 100; i++)
                    {
                        sprite.transform.localScale = new Vector3(Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1));
                        sprite.transform.position += new Vector3(0, 0.01f, 0);
                        yield return new WaitForEndOfFrame();

                    }

                    sprite.SetActive(false);
                    sprite.transform.position = oldPos;
                    Debug.Log("fine");



                }
            }
            else{
                int enemyHit = UnityEngine.Random.Range(1, 100);

                if (enemyHit <= output[5])             //enemy hit
                {

                    int enemyCrit = UnityEngine.Random.Range(1, 100);  //se critta
                    Debug.Log("inizio");

                    GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.localScale = Vector3.zero;
                    sprite = GameObject.Find("Info Canvas").transform.GetChild(2).gameObject;
                    sprite.transform.localScale = Vector3.zero;
                    GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.SetActive(true);    

                    if(move == false && enemy.weaponMaxRange==1){

                        for (float i = 0; i < 100; i++)
                        {
                        enemyParent.transform.position -= new Vector3(0.035f, 0, 0);
                        yield return new WaitForEndOfFrame();

                        }

                        move = true;
                    }
                       
                    
                    if (enemyCrit <= output[6]){
                        spriteEnemy.GetComponent<Animator>().Play("Crit");
                        GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=(output[4]*3).ToString();
                        player.hp = Mathf.Clamp(player.hp -= output[4] * 3, 0, player.maxHp);
                        GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.SetActive(true);
                    }
                    else{
                        spriteEnemy.GetComponent<Animator>().Play("Attack");
                        GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=output[4].ToString();
                        player.hp = Mathf.Clamp(player.hp -= output[4], 0, player.maxHp);
                    }
                    
                    yield return new WaitForEndOfFrame();
                    
                    AnimatorClipInfo[] clipInfos = spriteEnemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                    AnimationClip firstClip = clipInfos[0].clip;
                    float duration = firstClip.length + Time.time;
                    Debug.Log(firstClip.length);

                    GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.localScale = Vector3.zero;
                    sprite.transform.localScale = Vector3.zero;

                    while (Time.time < duration)
                    {
                        yield return new WaitForEndOfFrame();
                    }

                    oldPos = sprite.transform.position;

                    spritePlayer.GetComponent<Animator>().Play("Hurt");
                    yield return new WaitForEndOfFrame();
                    clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                    firstClip = clipInfos[0].clip;
                    duration = firstClip.length + Time.time;
                    for (float i = 0; i < 100; i++)
                    {
                        sprite.transform.localScale = new Vector3(Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1));
                        GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.localScale = new Vector3(Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1));
                        GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position += new Vector3(0, 0.01f, 0);
                        sprite.transform.position += new Vector3(0, 0.01f, 0);
                        yield return new WaitForEndOfFrame();

                    }

                    


                    
                    Debug.Log(firstClip.length);
                    while (Time.time < duration)
                    {
                        yield return new WaitForEndOfFrame();
                    }

                    sprite.SetActive(false);
                    sprite.transform.position = oldPos;
                    GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position -= new Vector3(0, 1, 0);
                    Debug.Log("fine");
                    player.healthbar.SetHealth(player.hp);
                }
                else               //enemy miss
                {
                    Debug.Log("inizio");

                    if(move == false && enemy.weaponMaxRange==1){

                        for (float i = 0; i < 100; i++)
                        {
                        enemyParent.transform.position -= new Vector3(0.035f, 0, 0);
                        yield return new WaitForEndOfFrame();

                        }

                        move = true;
                    } 
                    
                    spriteEnemy.GetComponent<Animator>().Play("Attack");

                    sprite = GameObject.Find("Info Canvas").transform.GetChild(3).gameObject;
                    sprite.transform.localScale = Vector3.zero;
                    GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.SetActive(true);
                    
                    yield return new WaitForEndOfFrame();
                    AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                    AnimationClip firstClip = clipInfos[0].clip;
                    float duration = firstClip.length + Time.time;
                    float half = firstClip.length / 2 + Time.time;
                    bool enemyMissStarted = false;
                    Debug.Log(firstClip.length);



                    

                    while (Time.time < duration)
                    {
                        if (Time.time > half && !enemyMissStarted)
                        {
                            enemyMissStarted = true;
                            spritePlayer.GetComponent<Animator>().Play("Miss");
                        }
                        yield return new WaitForEndOfFrame();
                    }

                    oldPos = sprite.transform.position;


                    for (float i = 0; i < 100; i++)
                    {
                        sprite.transform.localScale = new Vector3(Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1));
                        sprite.transform.position += new Vector3(0, 0.01f, 0);
                        yield return new WaitForEndOfFrame();

                    }

                    sprite.SetActive(false);
                    sprite.transform.position = oldPos;
                    Debug.Log("fine");
                }

            }
            yield return new WaitForSeconds(1);

        }

        if (!battleManager.canMoveEnemy) battleManager.canMoveEnemy = true;



        if (enemy.hp == 0)
        {

            if (battleManager.enemies.Contains(e))
                battleManager.enemies.Remove(e);
            enemy.cancInfo();
            temp.SetActive(true);
            temp.transform.DetachChildren();
            Destroy(temp);
            Destroy(e);
            yield return new WaitForEndOfFrame();
            SceneManager.UnloadSceneAsync(1);
            SceneManager.SetActiveScene(activeScene);

                
            player.endPvp();

            Debug.Log("fine");
            yield break;
        }

        if (player.hp <= 0)
        {

            temp.SetActive(true);
            temp.transform.DetachChildren();
            if (battleManager.units.Contains(p))
                battleManager.units.Remove(p);
            if (battleManager.unmovedUnits.Contains(p))
                battleManager.unmovedUnits.Remove(p);
            Destroy(temp);
            Destroy(p);
            yield return new WaitForEndOfFrame();
            SceneManager.UnloadSceneAsync(1);
            SceneManager.SetActiveScene(activeScene);

            player.endPvp();
            Destroy(p);
            Debug.Log("fine");
            yield break;
        }


        

        temp.SetActive(true);
        temp.transform.DetachChildren();
        Destroy(temp);

        SceneManager.UnloadSceneAsync(1);
        SceneManager.SetActiveScene(activeScene);

        player.endPvp();
        
        Debug.Log("fine");

    }

}
