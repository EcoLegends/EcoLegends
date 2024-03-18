
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

public class PvPscript : MonoBehaviour
{
    // Start is called before the first frame update

    
    GameObject sprite;
    Vector3 oldPos;

    public bool finitoLvlUp = false;


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

        playerScript player = p.GetComponent<playerScript>();

        if(player.heal==false)
        {
            int playerAS = output[3];
            int enemyAS = output[7];
            bool move = false;

            GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN+"_pvp");
            GameObject.Find("Davanti").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN + "_pvpDavanti");
            List<string> turns = new List<string>();
            turns.Add(initial_turn);

            enemyScript enemy = e.GetComponent<enemyScript>();
            

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

            if (enemy.boss) GameObject.Find("Music(Clone)").GetComponent<musicScript>().ChangeMusic(enemy.musica);
            yield return new WaitForEndOfFrame();
            UnityEngine.Object mov_tile_prefab = Resources.Load("movTileEnemyAllPrefab", typeof(GameObject));
            GameObject mov_tile = (GameObject)Instantiate(mov_tile_prefab, new Vector3(88, 88, -2), Quaternion.identity);
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("tileViola")) Destroy(g);

            int damageDealt = 0;

            yield return new WaitForSeconds(1);


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

                            
                            GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position.z);
                            move = true;
                        }
                        
                    
                        if (playerCrit <= output[2])
                        {
                            if (move) spritePlayer.GetComponent<Animator>().Play("Crit");
                            else spritePlayer.GetComponent<Animator>().Play("Crit2");
                            GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=(output[0]*3).ToString();
                            enemy.hp = Mathf.Clamp(enemy.hp -= output[0] * 3, 0, enemy.maxHp);
                            GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.SetActive(true);
                            damageDealt += output[0] * 3;
                        }
                        else
                        {
                            if (move) spritePlayer.GetComponent<Animator>().Play("Attack");
                            else spritePlayer.GetComponent<Animator>().Play("Attack2");
                            GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=output[0].ToString();
                            enemy.hp = Mathf.Clamp(enemy.hp -= output[0], 0, enemy.maxHp);
                            damageDealt += output[0];
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
                        yield return new WaitForSeconds(1);
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

                            GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position.z);

                            move = true;
                        }

                        if (move) spritePlayer.GetComponent<Animator>().Play("Attack");
                        else spritePlayer.GetComponent<Animator>().Play("Attack2");

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
                        yield return new WaitForSeconds(1);
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
                            GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position.z);
                            yield return new WaitForEndOfFrame();

                            }

                            move = true;
                        }
                        
                        
                        if (enemyCrit <= output[6]){
                            if (move) spriteEnemy.GetComponent<Animator>().Play("Crit");
                            else spriteEnemy.GetComponent<Animator>().Play("Crit2");
                            GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=(output[4]*3).ToString();
                            player.hp = Mathf.Clamp(player.hp -= output[4] * 3, 0, player.maxHp);
                            GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.SetActive(true);
                        }
                        else{
                            if (move) spriteEnemy.GetComponent<Animator>().Play("Attack");
                            else spriteEnemy.GetComponent<Animator>().Play("Attack2");

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
                        yield return new WaitForSeconds(1);
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
                            GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position.z);
                            move = true;
                        } 
                        
                        if(move) spriteEnemy.GetComponent<Animator>().Play("Attack");
                        else spriteEnemy.GetComponent<Animator>().Play("Attack2");

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
                        
                        yield return new WaitForSeconds(1);
                        sprite.SetActive(false);
                        sprite.transform.position = oldPos;
                        Debug.Log("fine");
                    }

                }
            

            }

            if (!battleManager.canMoveEnemy) battleManager.canMoveEnemy = true;


            int expGained = Mathf.Clamp(damageDealt, 0, 20);
            if (expGained != 0)
            {


                int expNeeded = (int)Mathf.Round(100 * Mathf.Pow(1.1f, player.lvl - 2));
                if (enemy.hp == 0) expGained = Mathf.Clamp(30 + (enemy.lvl - 1),0, 100);


                GameObject expGUI = (GameObject)Instantiate(Resources.Load("ExpCanvas", typeof(GameObject)), Camera.main.transform.position, Quaternion.identity);
                expGUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player.nome;
                expGUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (expNeeded - player.exp).ToString();
                expGUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "+" + (expGained).ToString();
                expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetMaxHealth(expNeeded);
                expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetHealth(player.exp);

                float y = expGUI.transform.position.y * -1;
                for (int i = 0; i < 100; i++)
                {
                    y += 0.01516f;
                    expGUI.transform.position = new Vector3(expGUI.transform.position.x, y, -1);
                    yield return new WaitForEndOfFrame();
                }

                yield return new WaitForSeconds(0.5f);
                bool lvlup = false;
                AudioClip expSfx = (AudioClip)Resources.Load("Sounds/SFX/Exp");
                
                for (int i = 0; i < expGained; i++)
                {
                    yield return new WaitForSeconds(1/(float)expGained);
                    player.exp++;
                    if(i%2==0 || expGained < 15)expGUI.transform.GetChild(7).GetComponent<AudioSource>().PlayOneShot(expSfx);
                    if (player.exp >= expNeeded)
                    {
                        player.exp = 0;
                        expNeeded = (int)Mathf.Round(100 * Mathf.Pow(1.1f, player.lvl - 1));
                        expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetMaxHealth(expNeeded);
                        expGUI.transform.GetChild(6).GetChild(1).GetComponent<Image>().color = new Color(0, 186, 50);
                        lvlup = true;
                    }
                    expGUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (expNeeded - (player.exp)).ToString();
                    expGUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "+" + (expGained - i).ToString();

                    expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetHealth(player.exp);

                }
                expGUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "0";

                yield return new WaitForSeconds(2);

                for (int i = 0; i < 100; i++)
                {
                    y -= 0.01516f;
                    expGUI.transform.position = new Vector3(expGUI.transform.position.x, y, expGUI.transform.position.z);
                    yield return new WaitForEndOfFrame();
                }

                if (lvlup)
                {
                    finitoLvlUp = false;
                    LevelUp(player);
                    while (!finitoLvlUp) yield return new WaitForEndOfFrame();
                }

            }

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
        }
        else{

            GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN+"_pvp");
            GameObject.Find("Davanti").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN + "_pvpDavanti");

            playerScript player2 = e.GetComponent<playerScript>();

            sprite = GameObject.Find("Info Canvas").transform.GetChild(6).gameObject;
            sprite.transform.localScale = Vector3.zero;
            sprite.SetActive(true);

            spritePlayer.GetComponent<Animator>().Play("Heal");
            GameObject.Find("Info Canvas").transform.GetChild(6).GetComponent<TextMeshProUGUI>().text=(player.lvl+9).ToString();
            player2.hp = Mathf.Clamp(player2.hp += 8+player.mag/3, 0, player2.maxHp);

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
                    spritePlayer.GetComponent<Animator>().Play("Select");
                }
                yield return new WaitForEndOfFrame();
            }

            oldPos = sprite.transform.position;

                        
            for (float i = 0; i < 100; i++)
            {
                sprite.transform.localScale = new Vector3(Mathf.Clamp(i/10,0,1), Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1));
                sprite.transform.position += new Vector3(0, 0.01f, 0);
                yield return new WaitForEndOfFrame();

            }

            yield return new WaitForSeconds(1);
            sprite.SetActive(false);
            sprite.transform.position = oldPos;
            
            player2.healthbar.SetHealth(player2.hp);


        }

        

        temp.SetActive(true);
        temp.transform.DetachChildren();
        Destroy(temp);

        SceneManager.UnloadSceneAsync(1);
        SceneManager.SetActiveScene(activeScene);

        player.endPvp();
        
        Debug.Log("fine");
        

    }


    public void LevelUp(playerScript player)
    {


        int[] increments = { 0, 0, 0, 0, 0, 0, 0, 0 }; //incrementi iniziali

        int n1 = UnityEngine.Random.Range(0, 8);        //garantisce 1o incremento
        int n2;

        do
        {                                   //garantisce 2o incremento
            n2 = UnityEngine.Random.Range(0, 8);
        } while (n1 == n2);

        increments[n1] = 1;
        increments[n2] = 1;

        int[] incrementPercentage = { player.hpGrowth, player.strGrowth, player.magGrowth, player.dexGrowth, player.spdGrowth, player.lckGrowth, player.defGrowth, player.resGrowth };


        for (int i = 0; i < 8; i++)
        {
            if (UnityEngine.Random.Range(1, 100) <= incrementPercentage[i]) increments[i] = 1;        //se il numero e' minore della % di crescita allora stat incrementa
        }

        Object canvas = Resources.Load("Level Up Canvas", typeof(GameObject));

        GameObject levelUpCanvas = (GameObject)Instantiate(canvas, new Vector3(0.159606f, 0.9915071f, 0), Quaternion.identity);
        levelUpCanvas.GetComponent<levelUpScript>().Setup(player.nome, player.lvl, player.maxHp, player.str, player.mag, player.dex, player.spd, player.lck, player.def, player.res, increments);

        player.lvl++;
        player.maxHp += increments[0];            //incrementa i valori
        player.str += increments[1];
        player.mag += increments[2];
        player.dex += increments[3];
        player.spd += increments[4];
        player.lck += increments[5];
        player.def += increments[6];
        player.res += increments[7];

        player.healthbar.SetMaxHealth(player.maxHp);

    }

}
