
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class PvPscript : MonoBehaviour
{

    
    GameObject sprite;
    Vector3 oldPos;

    public bool finitoLvlUp = false;




    public void iniziaPvPvero(GameObject e, GameObject p, int[] output, GameObject spritePlayer, GameObject spriteEnemy, GameObject playerParent, GameObject enemyParent, Scene activeScene, GameObject temp, string initial_turn, bool cura)
    {
        GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("battlestart");
        StartCoroutine(iniziaPvP(e, p, output, spritePlayer, spriteEnemy, playerParent, enemyParent,activeScene, temp, initial_turn, cura));

    }



    public IEnumerator iniziaPvP(GameObject e, GameObject p, int[] output, GameObject spritePlayer, GameObject spriteEnemy, GameObject playerParent, GameObject enemyParent, Scene activeScene, GameObject temp, string initial_turn, bool cura)
    {

        playerScript player = p.GetComponent<playerScript>();
        GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN + "_pvp");
        GameObject.Find("Davanti").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN + "_pvpDavanti");


        if (cura==false)
        {
            


            int playerAS = output[3];
            int enemyAS = output[7];
            bool move = false;

            List<string> turns = new List<string>();
            

            enemyScript enemy = e.GetComponent<enemyScript>();
            if (!battleManager.dialoghiFatti.Contains(player.nome + "-" + enemy.nome))
            {
                battleManager.dialoghiFatti.Add(player.nome + "-" + enemy.nome);
                string dialogo = player.nome + "-" + enemy.nome;
                if(mapScript.mapN == 2) 
                {
                    if (enemy.boss)
                    {
                        if(GameObject.Find("Music(Clone)").GetComponent<musicScript>().music!= enemy.musica) GameObject.Find("Music(Clone)").GetComponent<musicScript>().ChangeMusic("Boss Intro");
                        List<Dialogo> d = new List<Dialogo>();

                        Debug.Log(dialogo);

                        switch (dialogo)
                        {
                            case "Nova-Granius?":
                                {
                                    d.Add(new Dialogo("Nova", "Nova", "Granius! Cosa diavolo sta succedendo qui?", true, true));
                                    d.Add(new Dialogo("Nova", "Nova", "Sono arrivata appena in tempo per vederti assediato da questi mostri!", true, true));
                                    d.Add(new Dialogo("Granius?",enemy.textureFile, "Nova... finalmente sei arrivata. Ma non hai idea di cosa sia successo.", false,false));
                                    d.Add(new Dialogo("Granius?",enemy.textureFile, "Il nostro impero è stato invaso e distrutto dall'inquinamento che avanza nella nostra terra.", false, false));
                                    d.Add(new Dialogo("Granius?", enemy.textureFile, "Morgrath, lui è l'unico che ci può salvare. Non posso più fidarmi di te o di nessun altro.", false, false));
                                    d.Add(new Dialogo("Nova", "Nova", "Cosa stai dicendo, Granius? Tu e Morgrath?", true, true));
                                    d.Add(new Dialogo("Nova", "Nova", "Sei sempre stato un difensore della natura, un nostro alleato! E ora ti allei col Grande Inquinatore?", true, true));
                                    d.Add(new Dialogo("Granius?", enemy.textureFile, "Le cose sono cambiate. Ora devo affrontarti, Nova. Non c'è altra scelta.", false, false));
                                    d.Add(new Dialogo("Nova", "Nova", "Non posso crederci... ma non importa cosa sia successo.", true, true));
                                    d.Add(new Dialogo("Nova", "Nova", "Granius, io ti salverà e non mi arrenderò senza lottare. Se è così che deve essere, allora che lo sia.", true, true));
                                    break;
                                }
                            case "Sear-Granius?":
                                {
                                    d.Add(new Dialogo("Sear", "Sear", "Granius! Resisti! Arrivo subito! Lascia che ti aiuti!", true, true));
                                    d.Add(new Dialogo("Granius?", enemy.textureFile, "Ah, Sear... finalmente sei arrivato. Peccato che sia troppo tardi per te.", false, false));
                                    d.Add(new Dialogo("Sear", "Sear", "Cosa stai dicendo, Granius? Cosa ti è successo?", true, true));
                                    d.Add(new Dialogo("Granius?", enemy.textureFile, "Nulla che tu possa capire, Sear. Sono diventato più potente di quanto avrei mai immaginato.", false, false));
                                    d.Add(new Dialogo("Granius?", enemy.textureFile, "Morgrath mi ha aperto gli occhi. Ora vedo la vera natura del mondo.", false, false));
                                    d.Add(new Dialogo("Sear", "Sear", "Morgrath? Il Grande Inquinatore? No, Granius, non puoi credere a quello che ti ha detto. ", true, true));
                                    d.Add(new Dialogo("Sear", "Sear", "È solo un inganno, e tu sei stato trascinato nella sua oscurità.", true, true));
                                    d.Add(new Dialogo("Granius?", enemy.textureFile, "Non c'è ritorno per me, Sear. Sono diventato ciò che dovevo essere.", false, false));
                                    d.Add(new Dialogo("Granius?", enemy.textureFile, "E ora devo eliminare chiunque cerchi di ostacolarmi.", false, false)); 
                                    d.Add(new Dialogo("Sear", "Sear", "Allora sarà una battaglia. Granius, prometto che ti salverò!", true, true));
                                    break;
                                }
                            case "Nova-Thera?":
                                {
                                    d.Add(new Dialogo("Nova", "Nova", "Thera! Vengo subito! Prendi la mia mano e scappiamo da qui!", true, true));
                                    d.Add(new Dialogo("Thera?", enemy.textureFile, "Nova... è troppo tardi. Non posso scappare. E non ho bisogno del tuo aiuto.", false, false));
                                    d.Add(new Dialogo("Nova", "Nova", "Cosa intendi, Thera? Cosa sta succedendo qui?", true, true));
                                    d.Add(new Dialogo("Thera?", enemy.textureFile, "Sono cambiata, Nova.", false, false));
                                    d.Add(new Dialogo("Thera?", enemy.textureFile, "L'inquinamento ha rivelato la vera natura delle cose, e ho visto di non avere nessuna speranza contro di loro.", false, false));
                                    d.Add(new Dialogo("Thera?", enemy.textureFile, "Ora sono con Morgrath.", false, false));
                                    d.Add(new Dialogo("Nova", "Nova", "No, Thera, non puoi arrenderti così! Cosa ti ha fatto Morgrath?", true, true));
                                    d.Add(new Dialogo("Thera?", enemy.textureFile, "Morgrath non mi ha fatto nulla. Mi ha solo aperto gli occhi sulla realt�.", false, false));
                                    d.Add(new Dialogo("Thera?", enemy.textureFile, "E ora devo eliminare qualsiasi ostacolo si frapponga sulla nostra strada.", false, false)); 
                                    d.Add(new Dialogo("Nova", "Nova", "Allora sono obbligata a fermarti, Thera. Non vi lascerò avere la meglio senza lottare.", true, true));
                                    
                                    break;
                                }
                            case "Sear-Thera?":
                                {
                                    d.Add(new Dialogo("Sear", "Sear", "Thera! Ti raggiungo subito! Tieniti forte, arrivo!", true, true));
                                    d.Add(new Dialogo("Thera?", enemy.textureFile, "Sear... non hai bisogno di preoccuparti per me. La mia situazione è sotto controllo.", false, false));
                                    d.Add(new Dialogo("Sear", "Sear", "Cosa stai dicendo, Thera? Cosa sta succedendo qui?", true, true));
                                    d.Add(new Dialogo("Thera?", enemy.textureFile, "Non importa. Quello che devi sapere è che sono ora dalla parte di Morgrath.", false, false));
                                    d.Add(new Dialogo("Thera?", enemy.textureFile, "E non ci sarà pietà per chiunque cerchi di fermarci.", false, false));
                                    d.Add(new Dialogo("Sear", "Sear", "Ma che stai dicendo? Thera, non puoi lasciare che Morgrath ti manipoli in questo modo! Non sei tu!", true, true));
                                    d.Add(new Dialogo("Thera?", enemy.textureFile, "È inutile, Sear. Il mio destino è stato scritto, e ora devo seguirlo fino in fondo.", false, false));
                                    d.Add(new Dialogo("Sear", "Sear", "Allora sarà una battaglia, Thera. Non posso permetterti di vincere.", true, true));


                                    break;
                                }
                        }

                        GameObject deez = (GameObject)Instantiate(Resources.Load("Dialogo", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity);

                        bool dx=false, sx=false;
                        deez.transform.GetChild(0).GetChild(0).localScale = Vector3.zero;
                        deez.transform.GetChild(0).GetChild(1).localScale = Vector3.zero;
                        deez.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

                        
                        

                        yield return new WaitForSeconds(1);
                        for (float i=0; i <= 85; i++)
                        {
                            deez.transform.position = new Vector3(0, i / 100, 0);
                            yield return new WaitForEndOfFrame();
                        }
                        yield return new WaitForSeconds(0.5f);
                        foreach (Dialogo dial in d)
                        {
                            deez.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 90);
                            deez.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 90);
                            deez.transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 90);
                            deez.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 90);

                            deez.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
                            int index = 0;
                            if (!dial.latoSX) index = 1;
                            if(!dial.player) deez.transform.GetChild(0).GetChild(index).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("enemyNameBanner");
                            deez.transform.GetChild(0).GetChild(index).GetChild(0).GetComponent<SpriteRenderer>().sprite = dial.sprite;
                            deez.transform.GetChild(0).GetChild(index).GetChild(1).GetComponent<TextMeshProUGUI>().text = dial.nome;

                            deez.transform.GetChild(0).GetChild(index).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                            deez.transform.GetChild(0).GetChild(index).GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

                            if (dial.latoSX && !sx)
                            {
                                sx = true;
                                for(float i = 0; i <= 100; i++)
                                {
                                    deez.transform.GetChild(0).GetChild(0).localScale = new Vector3(9.259258f* (i/100), 5.185184f * (i/100), 9.259258f * (i/100));
                                    yield return new WaitForEndOfFrame();
                                }
                            }
                            if (!dial.latoSX && !dx)
                            {
                                dx = true;
                                for (float i = 0; i <= 100; i++)
                                {
                                    deez.transform.GetChild(0).GetChild(1).localScale = new Vector3(9.259258f * (i / 100), 5.185184f * (i / 100), 9.259258f * (i / 100));
                                    yield return new WaitForEndOfFrame();
                                }
                            }
                            
                            for(int i=0;i<=dial.text.Length;i++)
                            {
                                deez.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text =  dial.text.Substring(0,i);
                                yield return new WaitForSeconds(0.025f);
                            }
                            yield return new WaitForSeconds(3);
                        }
                        for (float i = 85; i >= 0; i--)
                        {
                            deez.transform.position = new Vector3(0, i / 100, 0);
                            yield return new WaitForEndOfFrame();
                        }
                        Destroy(deez);


                        if (enemy.textureFile == "Granius" || enemy.textureFile == "Thera")
                        {
                            enemy.textureFile = enemy.textureFile + "_evil";
                            Destroy(enemy.gameObject.transform.GetChild(0).gameObject);
                            var texture = Resources.Load<GameObject>("Characters/" + enemy.textureFile);   //carica la texture del personaggio
                            GameObject sprite = Instantiate(texture, new Vector3(0, 0, 0), Quaternion.identity);
                            sprite.transform.parent = enemy.gameObject.transform;
                            sprite.transform.localPosition = new Vector3(0, 0, 0);
                            sprite.transform.SetAsFirstSibling();
                            sprite.transform.Rotate(0, 180, 0);

                            Destroy(spriteEnemy);

                            spriteEnemy = Instantiate(Resources.Load<GameObject>("Characters/" + enemy.textureFile), Vector3.zero, Quaternion.identity);


                            spriteEnemy.transform.parent = enemyParent.transform;
                            spriteEnemy.transform.localEulerAngles = new Vector3(0,0,0);
                            spriteEnemy.transform.localPosition = new Vector3(0, 0, 0);
                            enemyParent.transform.position = new Vector3(2, 0, 0);
                            
                        }

                    }
                }
            }

            int distance = (int)Mathf.Abs(player.x - enemy.x) + (int)Mathf.Abs(player.y - enemy.y);

            if(temp.transform.GetChild(3).GetComponent<mapScript>().mapTiles[player.x, player.y].GetComponent<tileScript>().avoBonus > 0 && player.nome=="Granius")
            {
                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                skill.GetComponent<skillGUIScript>().Setup("Istinto Selvaggio");
            }

            if (player.haFattoQualcosa==false  && player.nome == "Skye")
            {
                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                skill.GetComponent<skillGUIScript>().Setup("In Guardia");
            }

            if (initial_turn=="enemy" && player.nome == "Sear" && (float)player.hp/ (float)player.maxHp <= 0.25f)
            {
                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                skill.GetComponent<skillGUIScript>().Setup("Vantaggio");
                initial_turn = "player";
            }
            turns.Add(initial_turn);

            if (player.nome == "Aeria" && player.weaponMaxRange==3)
            {
                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                skill.GetComponent<skillGUIScript>().Setup("Dio del Vento");
            }
            

            float playerInitialHP = player.hp / (float)player.maxHp*100;

            if (initial_turn=="player"){
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
            yield return new WaitForSeconds(1);
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
                        int dmg;
                    
                        if (playerCrit <= output[2])
                        {
                            GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("crit");
                            if (move) spritePlayer.GetComponent<Animator>().Play("Crit");
                            else spritePlayer.GetComponent<Animator>().Play("Crit2");
                            GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=(output[0]*3).ToString();
                            enemy.hp = Mathf.Clamp(enemy.hp -= output[0] * 3, 0, enemy.maxHp);
                            GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.SetActive(true);
                            damageDealt += output[0] * 3;
                            dmg = output[0] * 3;
                        }
                        else
                        {
                            if (move) spritePlayer.GetComponent<Animator>().Play("Attack");
                            else spritePlayer.GetComponent<Animator>().Play("Attack2");
                            GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=output[0].ToString();
                            enemy.hp = Mathf.Clamp(enemy.hp -= output[0], 0, enemy.maxHp);
                            damageDealt += output[0];
                            dmg = output[0];
                        }
                        
                        if(enemy.hp <= 0 && player.nome=="Thera" && UnityEngine.Random.Range(1, 100) < player.dex)
                        {
                            GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                            skill.GetComponent<skillGUIScript>().Setup("Giardino Fiorito");
                            player.hp = Mathf.Clamp(player.hp += dmg / 2, 0, player.maxHp);
                            player.healthbar.SetHealth(player.hp);
                            
                            GameObject.Find("Info Canvas").transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = (dmg/2).ToString();
                            GameObject.Find("Info Canvas").transform.GetChild(6).gameObject.SetActive(true);
                            GameObject.Find("Info Canvas").transform.GetChild(6).position = new Vector3(playerParent.transform.position.x -0.3f, GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.y, GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(6).localScale = Vector3.zero;
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

                        GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("dmg");
                        spriteEnemy.GetComponent<Animator>().Play("Hurt");
                        yield return new WaitForEndOfFrame();
                        clipInfos = spriteEnemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                        firstClip = clipInfos[0].clip;
                        duration = firstClip.length + Time.time;
                        if(GameObject.Find("Info Canvas").transform.GetChild(6).gameObject.activeInHierarchy) GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("heal");

                        for (float i = 0; i < 100; i++)
                        {
                            sprite.transform.localScale = new Vector3(Mathf.Clamp(i/10,0,1), Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1));
                            GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.localScale = new Vector3(Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1));
                            GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position += new Vector3(0, 0.01f, 0);

                            GameObject.Find("Info Canvas").transform.GetChild(6).position += new Vector3(0, 0.01f, 0);
                            GameObject.Find("Info Canvas").transform.GetChild(6).localScale = new Vector3(Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1));

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
                        GameObject.Find("Info Canvas").transform.GetChild(6).gameObject.SetActive(false);
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
                                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("miss");
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

                        int hpPrec = player.hp;
                        int dmgPrec = output[4];
                        if (player.nome == "Acquira" && UnityEngine.Random.Range(1, 100) < player.dex)
                        {
                            GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                            skill.GetComponent<skillGUIScript>().Setup("Acqua Cristallina");
                            output[4] = output[4] / 2;
                        }
                        if (enemyCrit <= output[6]){
                            GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("crit");
                            if (move) spriteEnemy.GetComponent<Animator>().Play("Crit");
                            else spriteEnemy.GetComponent<Animator>().Play("Crit2");
                            GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=(output[4]*3).ToString();
                            player.hp = Mathf.Clamp(player.hp -= output[4] * 3, 0, player.maxHp);
                            if(player.hp==0 && player.nome=="Nova" && playerInitialHP >= 30)
                            {
                                GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (hpPrec - 1).ToString();
                                player.hp = 1;
                                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                                skill.GetComponent<skillGUIScript>().Setup("Persistenza");
                            }
                            GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.SetActive(true);
                        }
                        else{
                            if (move) spriteEnemy.GetComponent<Animator>().Play("Attack");
                            else spriteEnemy.GetComponent<Animator>().Play("Attack2");
                            GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=output[4].ToString();
                            player.hp = Mathf.Clamp(player.hp -= output[4], 0, player.maxHp);
                            if (player.hp == 0 && player.nome == "Nova" && playerInitialHP >= 30)
                            {
                                GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (hpPrec - 1).ToString();
                                player.hp = 1;
                                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                                skill.GetComponent<skillGUIScript>().Setup("Persistenza");
                            }
                        }
                        output[4] = dmgPrec;


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

                        GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("dmg");
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
                        GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.SetActive(false);
                        GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position -= new Vector3(0, 1, 0);
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
                                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("miss");
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

            if (player.hp>=0 && player.nome == "Hydris")
            {
                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                skill.GetComponent<skillGUIScript>().Setup("Guarigione");
                player.hp = Mathf.Clamp(player.hp += 5 / 2, 0, player.maxHp);
                player.healthbar.SetHealth(player.hp);
                GameObject.Find("Info Canvas").transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = (5).ToString();
                GameObject.Find("Info Canvas").transform.GetChild(6).gameObject.SetActive(true);
                GameObject.Find("Info Canvas").transform.GetChild(6).position = new Vector3(playerParent.transform.position.x - 0.3f, GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.y, GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.z);
                GameObject.Find("Info Canvas").transform.GetChild(6).localScale = Vector3.zero;
                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("heal");
                for (float i = 0; i < 100; i++)
                {
                   
                    GameObject.Find("Info Canvas").transform.GetChild(6).position += new Vector3(0, 0.01f, 0);
                    GameObject.Find("Info Canvas").transform.GetChild(6).localScale = new Vector3(Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1), Mathf.Clamp(i / 10, 0, 1));

                    yield return new WaitForEndOfFrame();

                }


                yield return new WaitForSeconds(1);
                
                GameObject.Find("Info Canvas").transform.GetChild(6).gameObject.SetActive(false);
            }

            if (!battleManager.canMoveEnemy) battleManager.canMoveEnemy = true;
            if (enemy.hp == 0)
            {

                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("defeat");
                GameObject particle = Instantiate(Resources.Load<GameObject>("Petals Prefab 1"));
                particle.transform.position = new Vector3(enemyParent.transform.position.x-0.5f, enemyParent.transform.position.y+0.5f, -3.5f);

                particle.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

                if (enemy.textureFile.Contains("evil"))
                {
                    enemy.textureFile = enemy.textureFile.Replace("_evil", "");
                    Destroy(enemy.gameObject.transform.GetChild(0).gameObject);
                    var texture = Resources.Load<GameObject>("Characters/" + enemy.textureFile);   //carica la texture del personaggio
                    GameObject sprite = Instantiate(texture, new Vector3(0, 0, 0), Quaternion.identity);
                    sprite.transform.parent = enemy.gameObject.transform;
                    sprite.transform.localPosition = new Vector3(0, 0, 0);
                    sprite.transform.SetAsFirstSibling();
                    sprite.transform.Rotate(0, 180, 0);

                    Destroy(spriteEnemy);

                    spriteEnemy = Instantiate(Resources.Load<GameObject>("Characters/" + enemy.textureFile), Vector3.zero, Quaternion.identity);


                    spriteEnemy.transform.parent = enemyParent.transform;
                    spriteEnemy.transform.localEulerAngles = new Vector3(0, 0, 0);
                    spriteEnemy.transform.localPosition = new Vector3(0, 0, 0);
                    for (float i = 0; i <= 100; i++)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                }
                else
                {
                    for (float i = 0; i <= 100; i++)
                    {

                        enemyParent.transform.localScale = new Vector3(1 - i / 100, 1 - i / 100, 1 - i / 100);
                        yield return new WaitForEndOfFrame();
                    }
                }
                
            }

                int expGained = Mathf.Clamp(damageDealt, 0, 20);
            if (player.hp == 0) expGained = 0;
            if (expGained != 0)
            {


                int expNeeded = (int)Mathf.Round(100 * Mathf.Pow(1.1f, player.lvl - 2));
                if (enemy.hp == 0)
                {
                    expGained = Mathf.Clamp(30 + (enemy.lvl - 1), 0, 100);
                    if (enemy.boss) expGained += 20;
                }


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
                    if(i%2==0 || expGained < 15) expGUI.transform.GetChild(7).GetComponent<AudioSource>().PlayOneShot(expSfx);
                    if (player.exp >= expNeeded)
                    {
                        player.exp = 0;
                        expNeeded = (int)Mathf.Round(100 * Mathf.Pow(1.1f, player.lvl - 1));
                        expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetMaxHealth(expNeeded);
                        expGUI.transform.GetChild(6).GetChild(1).GetComponent<UnityEngine.UI.Image>().color = new Color(0, 186, 50);
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

                List<string> nomi = new List<string> { "Nova", "Sear", "Granius", "Thera", "Acquira", "Hydris", "Aeria", "Skye" };
                try
                {
                    string[] arrLine = System.IO.File.ReadAllLines(Application.streamingAssetsPath+"dati.txt");
                    arrLine[nomi.IndexOf(player.nome)] = player.nome + "," + player.textureFile + "," + player.lvl + "," + player.exp + "," + player.movement + "," + player.weaponMinRange + "," + player.weaponMaxRange + "," + player.weaponWt + "," + player.weaponMt + "," + player.weaponHit + "," + player.weaponCrit + "," + player.unitType + "," + player.weaponType + "," + player.weaponIsMagic + "," + player.maxHp + "," + player.str + "," + player.mag + "," + player.dex + "," + player.spd + "," + player.lck + "," + player.def + "," + player.res + "," + player.hpGrowth + "," + player.strGrowth + "," + player.magGrowth + "," + player.dexGrowth + "," + player.spdGrowth + "," + player.lckGrowth + "," + player.defGrowth + "," + player.resGrowth + "," + player.heal;
                    System.IO.File.WriteAllLines(Application.streamingAssetsPath+"dati.txt", arrLine);
                }
                catch (System.Exception) { }
                
            }

            if (enemy.hp == 0)
            {
                battleManager.canMoveEnemy = true;
                if (battleManager.enemies.Contains(e))
                    battleManager.enemies.Remove(e);
                enemy.cancInfo();
                temp.SetActive(true);
                temp.transform.DetachChildren();
                Destroy(temp);
                Destroy(e);
                yield return new WaitForEndOfFrame();
                SceneManager.UnloadSceneAsync(2);
                SceneManager.SetActiveScene(activeScene);

                    
                player.endPvp();

                Debug.Log("fine");
                yield break;
            }

            if (player.hp <= 0)
            {
                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("defeat");
                temp.SetActive(true);
                temp.transform.DetachChildren();
                player.endPvp();
                if (battleManager.units.Contains(p))
                    battleManager.units.Remove(p);
                if (battleManager.unmovedUnits.Contains(p))
                    battleManager.unmovedUnits.Remove(p);
                Destroy(temp);
                yield return new WaitForEndOfFrame();
                Destroy(p);
                SceneManager.UnloadSceneAsync(2);
                SceneManager.SetActiveScene(activeScene);

                
                Destroy(p);
                Debug.Log("fine");
                yield break;
            }
        }
        else{

            GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN+"_pvp");
            GameObject.Find("Davanti").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN + "_pvpDavanti");

            playerScript player2 = e.GetComponent<playerScript>();
            yield return new WaitForSeconds(1);
            sprite = GameObject.Find("Info Canvas").transform.GetChild(6).gameObject;
            sprite.transform.localScale = Vector3.zero;
            sprite.SetActive(true);

            spritePlayer.GetComponent<Animator>().Play("Heal");
            GameObject.Find("Info Canvas").transform.GetChild(6).GetComponent<TextMeshProUGUI>().text=((8 + player.mag / 3)).ToString();
            player2.hp = Mathf.Clamp(player2.hp += (8+player.mag/3), 0, player2.maxHp);

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

            GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("heal");
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

        SceneManager.UnloadSceneAsync(2);
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
