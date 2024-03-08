using Dev.ComradeVanti.WaitForAnim;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PvPscript : MonoBehaviour
{
    // Start is called before the first frame update

    bool showDamage=false;
    GameObject sprite;
    Vector3 oldPos;
    Vector3 newPos;

    float t;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(showDamage){
            Debug.Log("au");
            Debug.Log(sprite.transform.position);
            
            sprite.transform.position=Vector3.Lerp(oldPos,newPos,t*0.1sf);
        }
        
    }


    public void iniziaPvPvero(GameObject e, GameObject p, int[] output, GameObject spritePlayer, GameObject spriteEnemy, Scene activeScene, GameObject temp)
    {

        StartCoroutine(iniziaPvP(e, p, output, spritePlayer, spriteEnemy, activeScene, temp));

    }



    public IEnumerator iniziaPvP(GameObject e, GameObject p, int[] output, GameObject spritePlayer, GameObject spriteEnemy, Scene activeScene, GameObject temp)
    {

        enemyScript enemy = e.GetComponent<enemyScript>();
        playerScript player = p.GetComponent<playerScript>();

        yield return new WaitForEndOfFrame();

        int playerHit = UnityEngine.Random.Range(1, 100);

        if (playerHit <= output[1])
        {

            int playerCrit = UnityEngine.Random.Range(1, 100);  //se critta

            Debug.Log("inizio");
            spritePlayer.GetComponent<Animator>().Play("Attack");
            GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.SetActive(true);
            
            sprite=GameObject.Find("Info Canvas").transform.GetChild(1).gameObject;
            oldPos=sprite.transform.position;
            newPos=new Vector3(oldPos.x,oldPos.y+1,oldPos.z);
            t = 1 / (newPos-oldPos).magnitude; 
            

            if (playerCrit <= output[2])
            {
                GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=output[0].ToString();
                enemy.hp = Math.Clamp(enemy.hp -= output[0] * 3, 0, enemy.maxHp);
            }
            else
            {
                GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=output[0].ToString();
                enemy.hp = Math.Clamp(enemy.hp -= output[0], 0, enemy.maxHp);
            }
            showDamage=true;
            yield return new WaitForEndOfFrame();
            AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
            AnimationClip firstClip = clipInfos[0].clip;
            float duration = firstClip.length + Time.time;
            Debug.Log(firstClip.length);
            while (Time.time < duration)
            {
                yield return new WaitForEndOfFrame();
            }
            showDamage=false;
            sprite.SetActive(false);
            Debug.Log("fine");

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
            enemy.healthbar.SetHealth(enemy.hp);

        }
        else
        {
            Debug.Log("inizio");
            spritePlayer.GetComponent<Animator>().Play("Attack");
            GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
            AnimationClip firstClip = clipInfos[0].clip;
            float duration = firstClip.length + Time.time;
            Debug.Log(duration);
            while (Time.time < duration)
            {
                yield return new WaitForEndOfFrame();
            }
            GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("fine");

        }

        int distance = (int)Mathf.Abs(player.x - enemy.x) + (int)Mathf.Abs(player.y - enemy.y);

        if (enemy.weaponMinRange <= distance && distance <= enemy.weaponMaxRange)
        {

            int enemyHit = UnityEngine.Random.Range(1, 100);

            if (enemyHit <= output[5])
            {

                int enemyCrit = UnityEngine.Random.Range(1, 100);  //se critta
                Debug.Log("inizio");
                spriteEnemy.GetComponent<Animator>().Play("Attack");
                GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.SetActive(true);    
                
                if (enemyCrit <= output[6]){
                    GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=output[4].ToString();
                    player.hp = Math.Clamp(player.hp -= output[4] * 3, 0, player.maxHp);
                }
                else{
                    GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=output[4].ToString();
                    player.hp = Math.Clamp(player.hp -= output[4], 0, player.maxHp);
                }
                yield return new WaitForEndOfFrame();
                AnimatorClipInfo[] clipInfos = spriteEnemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                AnimationClip firstClip = clipInfos[0].clip;
                float duration = firstClip.length + Time.time;
                Debug.Log(duration);
                while (Time.time < duration)
                {
                    yield return new WaitForEndOfFrame();
                }
                GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.SetActive(false);
                Debug.Log("fine");
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
                player.healthbar.SetHealth(player.hp);
            }
            else
            {
                Debug.Log("inizio");
                spriteEnemy.GetComponent<Animator>().Play("Attack");
                GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.SetActive(true);
                yield return new WaitForEndOfFrame();
                AnimatorClipInfo[] clipInfos = spriteEnemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                AnimationClip firstClip = clipInfos[0].clip;
                float duration = firstClip.length + Time.time;
                Debug.Log(duration);
                while (Time.time < duration)
                {
                    yield return new WaitForEndOfFrame();
                }
                GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.SetActive(false);
                Debug.Log("fine");
            }

            if (output[7] >= output[3] + 4)
            {

                enemyHit = UnityEngine.Random.Range(1, 100);

                if (enemyHit <= output[5])
                {

                    int enemyCrit = UnityEngine.Random.Range(1, 100);  //se critta
                    Debug.Log("inizio");
                    spriteEnemy.GetComponent<Animator>().Play("Attack");
                    GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.SetActive(true);

                    if (enemyCrit <= output[6]){
                        
                        GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=output[4].ToString();
                        player.hp = Math.Clamp(player.hp -= output[4] * 3, 0, player.maxHp);
                    }
                    else{
                        GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=output[4].ToString();
                        player.hp = Math.Clamp(player.hp -= output[4], 0, player.maxHp);
                    }
                    yield return new WaitForEndOfFrame();
                    AnimatorClipInfo[] clipInfos = spriteEnemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                    AnimationClip firstClip = clipInfos[0].clip;
                    float duration = firstClip.length + Time.time;
                    Debug.Log(duration);
                    while (Time.time < duration)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                    GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.SetActive(false);
                    Debug.Log("fine");
                    if (player.hp <= 0)
                    {

                        temp.SetActive(true);
                        temp.transform.DetachChildren();
                        if (battleManager.units.Contains(p))
                            battleManager.units.Remove(p);
                        if (battleManager.unmovedUnits.Contains(p))
                            battleManager.unmovedUnits.Remove(p);
                        Destroy(temp);

                        yield return new WaitForEndOfFrame();

                        SceneManager.UnloadSceneAsync(1);
                        SceneManager.SetActiveScene(activeScene);

                        player.endPvp();
                        Destroy(p);
                        Debug.Log("fine");
                        yield break;
                    }

                    player.healthbar.SetHealth(player.hp);

                }
                else
                {
                    Debug.Log("inizio");
                    spriteEnemy.GetComponent<Animator>().Play("Attack");
                    GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.SetActive(true);
                    yield return new WaitForEndOfFrame();
                    AnimatorClipInfo[] clipInfos = spriteEnemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                    AnimationClip firstClip = clipInfos[0].clip;
                    float duration = firstClip.length + Time.time;
                    Debug.Log(duration);
                    while (Time.time < duration)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                    GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.SetActive(false);
                    Debug.Log("fine");
                }
            }

        }

        if (output[3] >= output[7] + 4)
        {

            playerHit = UnityEngine.Random.Range(1, 100);

            if (playerHit <= output[1])
            {

                int playerCrit = UnityEngine.Random.Range(1, 100);  //se critta
                Debug.Log("inizio");
                spritePlayer.GetComponent<Animator>().Play("Attack");
                GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.SetActive(true);

                if (playerCrit <= output[2]){
                    GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=output[0].ToString();
                    enemy.hp = Math.Clamp(enemy.hp -= output[0] * 3, 0, enemy.maxHp);
                }
                else{
                    GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=output[0].ToString();
                    enemy.hp = Math.Clamp(enemy.hp -= output[0], 0, enemy.maxHp);
                }
                yield return new WaitForEndOfFrame();
                AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                AnimationClip firstClip = clipInfos[0].clip;
                float duration = firstClip.length + Time.time;
                Debug.Log(duration);
                while (Time.time < duration)
                {
                    yield return new WaitForEndOfFrame();
                }
                GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.SetActive(false);
                Debug.Log("fine");

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
                enemy.healthbar.SetHealth(enemy.hp);
            }
            else
            {

                Debug.Log("inizio");
                spritePlayer.GetComponent<Animator>().Play("Attack");
                GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.SetActive(true);
                yield return new WaitForEndOfFrame();
                AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                AnimationClip firstClip = clipInfos[0].clip;
                float duration = firstClip.length + Time.time;
                Debug.Log(duration);
                while (Time.time < duration)
                {
                    yield return new WaitForEndOfFrame();
                }
                GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.SetActive(false);
                Debug.Log("fine");
            }

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
