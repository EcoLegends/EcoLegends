using Dev.ComradeVanti.WaitForAnim;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PvPscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void iniziaPvPvero(GameObject e, GameObject p, int[] output, GameObject spritePlayer, GameObject spriteEnemy, Scene activeScene, GameObject temp){

        StartCoroutine(iniziaPvP( e,  p,  output,  spritePlayer,  spriteEnemy,  activeScene,  temp));

    }



    public IEnumerator iniziaPvP(GameObject e, GameObject p, int[] output, GameObject spritePlayer, GameObject spriteEnemy, Scene activeScene, GameObject temp){

        enemyScript enemy = e.GetComponent<enemyScript>();                
        playerScript player = p.GetComponent<playerScript>();

        int playerHit = UnityEngine.Random.Range(1, 100);

        if(playerHit <= output[1]){

            int playerCrit = UnityEngine.Random.Range(1, 100);  //se critta

            if(playerCrit <= output[2])
                Math.Clamp(enemy.hp-=output[0]*3,0,enemy.maxHp);
            else
                Math.Clamp(enemy.hp-=output[0],0,enemy.maxHp);

            
            Debug.Log("inizio");
            spritePlayer.GetComponent<Animator>().Play("Attack");
            Debug.Log("no");
            Debug.Log("ok");
            AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
            AnimationClip firstClip = clipInfos[0].clip;
            float duration = firstClip.length + Time.time;
            Debug.Log(duration);
            while(Time.time<duration){
                yield return new WaitForEndOfFrame();
                Debug.Log("ciccio");
            }
            Debug.Log("fine");
            if(enemy.hp==0){
                Destroy(e);
                if(battleManager.enemies.Contains(e))
                    battleManager.enemies.Remove(e);
                enemy.cancInfo();
                temp.SetActive(true);
                temp.transform.DetachChildren();
                Destroy(temp);

                SceneManager.UnloadSceneAsync(1);
                SceneManager.SetActiveScene(activeScene);

                StartCoroutine(player.endPvp());

                Debug.Log("fine");
                yield break;
            }
            enemy.healthbar.SetHealth(enemy.hp);

        }

        int distance = (int) Mathf.Abs(player.x - enemy.x) + (int) Mathf.Abs(player.y - enemy.y);

        if (enemy.weaponMinRange <= distance && distance <= enemy.weaponMaxRange){
            
                int enemyHit = UnityEngine.Random.Range(1, 100);

                if(enemyHit <= output[5]){

                    int enemyCrit = UnityEngine.Random.Range(1, 100);  //se critta

                    if(enemyCrit <= output[6])
                        player.hp-=output[4]*3;
                    else
                        player.hp-=output[4];
                    Debug.Log("inizio");
                    yield return spriteEnemy.GetComponent<Animator>().PlayAndWait("Attack");
                    Debug.Log("fine");
                    if(player.hp<=0){
                        Destroy(p);
                        temp.SetActive(true);
                        temp.transform.DetachChildren();
                        Destroy(temp);

                        SceneManager.UnloadSceneAsync(1);
                        SceneManager.SetActiveScene(activeScene);

                        StartCoroutine(player.endPvp());

                        Debug.Log("fine");
                        yield break;
                    }
                    player.healthbar.SetHealth(player.hp);
                }

                if (output[7] >= output[3] + 4){

                    enemyHit = UnityEngine.Random.Range(1, 100);

                    if(enemyHit <= output[5]){

                        int enemyCrit = UnityEngine.Random.Range(1, 100);  //se critta

                        if(enemyCrit <= output[6])
                            Math.Clamp(player.hp-=output[4]*3,0,player.maxHp);
                        else
                            Math.Clamp(player.hp-=output[4],0,player.maxHp);

                        Debug.Log("inizio");
                        yield return spriteEnemy.GetComponent<Animator>().PlayAndWait("Attack");
                        Debug.Log("fine");
                        if(player.hp<=0){
                            Destroy(p);
                            temp.SetActive(true);
                            temp.transform.DetachChildren();
                            Destroy(temp);

                            SceneManager.UnloadSceneAsync(1);
                            SceneManager.SetActiveScene(activeScene);

                            StartCoroutine(player.endPvp());

                            Debug.Log("fine");
                            yield break;
                        }

                        player.healthbar.SetHealth(player.hp);

                    }
                }
                
        }
            
        if (output[3] >= output[7] + 4){

            playerHit = UnityEngine.Random.Range(1, 100);

            if(playerHit <= output[1]){

                int playerCrit = UnityEngine.Random.Range(1, 100);  //se critta

                if(playerCrit <= output[2])
                    Math.Clamp(enemy.hp-=output[0]*3,0,enemy.maxHp);

                else
                    Math.Clamp(enemy.hp-=output[0],0,enemy.maxHp);

                Debug.Log("inizio");
                yield return spritePlayer.GetComponent<Animator>().PlayAndWait("Attack");
                Debug.Log("fine");

                if(enemy.hp<=0){
                    Destroy(e);
                    if(battleManager.enemies.Contains(e))
                        battleManager.enemies.Remove(e);
                    enemy.cancInfo();
                    temp.SetActive(true);
                    temp.transform.DetachChildren();
                    Destroy(temp);

                    SceneManager.UnloadSceneAsync(1);
                    SceneManager.SetActiveScene(activeScene);

                    StartCoroutine(player.endPvp());

                    Debug.Log("fine");
                    yield break;
                }    
                enemy.healthbar.SetHealth(enemy.hp);
            }

        }
        

        temp.SetActive(true);
        temp.transform.DetachChildren();
        Destroy(temp);

        SceneManager.UnloadSceneAsync(1);
        SceneManager.SetActiveScene(activeScene);

        StartCoroutine(player.endPvp());

        Debug.Log("fine");

    }
}
