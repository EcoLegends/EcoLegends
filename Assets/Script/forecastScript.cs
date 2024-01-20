using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class forecastScript : MonoBehaviour
{
    int playerNewHP;
    int enemyNewHP;
    public void Setup(GameObject e, GameObject p, int[] returnList)
    {
        enemyScript enemy = e.GetComponent<enemyScript>();
        playerScript player = p.GetComponent<playerScript>();

        int playerDmg = returnList[0];
        int playerHit = returnList[1];
        int playerCrit = returnList[2];
        int playerAS = returnList[3];
        int enemyDmg = returnList[4];
        int enemyHit = returnList[5];
        int enemyCrit = returnList[6];
        int enemyAS = returnList[7];

        playerNewHP = player.hp;
        enemyNewHP = enemy.hp - playerDmg;

        string enemyHitVisual = enemyHit.ToString();
        string enemyCritVisual = enemyCrit.ToString();

        List<string> turns = new List<string>();
        turns.Add("player");

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

        int distance = (int)Mathf.Abs(newPos.x - enemy.x) + (int)Mathf.Abs(newPos.y - enemy.y);

        if (enemy.weaponMinRange <= distance && distance <= enemy.weaponMaxRange)
        {
            turns.Add("enemy");
            playerNewHP -= enemyDmg;
            if (enemyAS >= playerAS + 4) { playerNewHP -= enemyDmg; turns.Add("enemy"); }
        }
        if (playerAS >= enemyAS + 4) { enemyNewHP -= playerDmg; turns.Add("player"); }

        playerNewHP = Mathf.Clamp(playerNewHP, 0, player.maxHp);
        enemyNewHP = Mathf.Clamp(enemyNewHP, 0, enemy.maxHp);

        string enemyDmgVisual = (player.hp - playerNewHP).ToString();
        string playerDmgVisual = (enemy.hp - enemyNewHP).ToString();
        
        if (!(enemy.weaponMinRange <= distance && distance <= enemy.weaponMaxRange))
        {
            enemyDmgVisual = "--";
            enemyHitVisual = "--";
            enemyCritVisual = "--";
        }
        

        //player

        Sprite img = Resources.Load<Sprite>("Characters/Artwork/Combat/" + player.textureFile);

        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = img;
        Object[] all = Resources.LoadAll<Sprite>("weaponIcons");

        transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = (Sprite)all[4 * player.weaponType + player.unitType - 1]; //carica icona arma
        if (player.unitEffective == enemy.unitType) Destroy(transform.GetChild(0).GetChild(1).GetChild(1).gameObject);
        else if (enemy.unitEffective == player.unitType) Destroy(transform.GetChild(0).GetChild(1).GetChild(0).gameObject);
        else { Destroy(transform.GetChild(0).GetChild(1).GetChild(1).gameObject); Destroy(transform.GetChild(0).GetChild(1).GetChild(0).gameObject); }


        transform.GetChild(0).GetChild(2).GetComponent<heathBarScript>().SetMaxHealth(player.maxHp);
        transform.GetChild(0).GetChild(2).GetComponent<heathBarScript>().SetHealth(playerNewHP);
        transform.GetChild(0).GetChild(2).GetChild(3).localPosition = new Vector3((float)playerNewHP / (float)player.maxHp * 3.155f - 1.64f, 0.3615f, 0);

        transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = player.hp.ToString();
        transform.GetChild(0).GetChild(8).GetComponent<TextMeshProUGUI>().text = playerDmgVisual;
        transform.GetChild(0).GetChild(9).GetComponent<TextMeshProUGUI>().text = playerHit.ToString();
        transform.GetChild(0).GetChild(10).GetComponent<TextMeshProUGUI>().text = playerCrit.ToString();
        transform.GetChild(0).GetChild(13).GetComponent<TextMeshProUGUI>().text = player.nome;


        //enemy

        img = Resources.Load<Sprite>("Characters/Artwork/Combat/" + enemy.textureFile);

        transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().sprite = img;
        
        transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = (Sprite)all[4 * enemy.weaponType + enemy.unitType - 1]; //carica icona arma
        if (enemy.unitEffective == player.unitType) Destroy(transform.GetChild(1).GetChild(1).GetChild(1).gameObject);
        else if (player.unitEffective == enemy.unitType) Destroy(transform.GetChild(1).GetChild(1).GetChild(0).gameObject);
        else { Destroy(transform.GetChild(1).GetChild(1).GetChild(1).gameObject); Destroy(transform.GetChild(1).GetChild(1).GetChild(0).gameObject); }


        transform.GetChild(1).GetChild(2).GetComponent<heathBarScript>().SetMaxHealth(enemy.maxHp);
        transform.GetChild(1).GetChild(2).GetComponent<heathBarScript>().SetHealth(enemyNewHP);
        transform.GetChild(1).GetChild(2).GetChild(3).localPosition = new Vector3((float)enemyNewHP / (float)enemy.maxHp * 3.155f - 1.64f, 0.3615f, 0);

        transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = enemy.hp.ToString();
        transform.GetChild(1).GetChild(8).GetComponent<TextMeshProUGUI>().text = enemyDmgVisual;
        transform.GetChild(1).GetChild(9).GetComponent<TextMeshProUGUI>().text = enemyHitVisual.ToString();
        transform.GetChild(1).GetChild(10).GetComponent<TextMeshProUGUI>().text = enemyCritVisual.ToString();
        transform.GetChild(1).GetChild(13).GetComponent<TextMeshProUGUI>().text = enemy.nome;

        switch (turns.Count)
        {
            case 1:
                {
                    Destroy(transform.GetChild(2).gameObject);
                    Destroy(transform.GetChild(3).gameObject);
                    break;
                }
            case 2:
                {
                    Destroy(transform.GetChild(2).gameObject);
                    Destroy(transform.GetChild(4).gameObject);
                    break;
                }
            case 3:
                {
                    Destroy(transform.GetChild(3).gameObject);
                    Destroy(transform.GetChild(4).gameObject);
                    break;
                }
        }

        int turnN = 0;

        foreach(string turn in turns)
        {
            if(turn == "player")
            {
                transform.GetChild(2).GetChild(turnN).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("freccia pvp blu");
                transform.GetChild(2).GetChild(turnN).GetChild(0).GetComponent<TextMeshProUGUI>().text = playerDmg.ToString();
            }
            else
            {
                transform.GetChild(2).GetChild(turnN).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("freccia pvp rossa");
                transform.GetChild(2).GetChild(turnN).GetChild(0).GetComponent<TextMeshProUGUI>().text = enemyDmg.ToString();
            }
            turnN++;
        }


    }

    private void Start()
    {
        StartCoroutine(animazione());
    }



    float inc = 0.1F;
    float transparency = 100;
    void Update()
    {
        


        if (transparency >= 120 || transparency <= 0)            //inverte il verso dell'incremento
        {
            inc *= -1;
        }

        Color rosso = new Color32(204, 99, 99, 255);

        transparency += inc;
        transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.Lerp(Color.white, rosso, transparency/100);
        transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Image>().color = Color.Lerp(Color.white, rosso, transparency / 100);

        if(playerNewHP <= 0) transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, rosso, (120-transparency) / 100);
        if (enemyNewHP <= 0) transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, rosso, (120-transparency) / 100);

    }

    public void Rimuovi()
    {
        StartCoroutine(uscita());
    }


    IEnumerator animazione()
    {
        float y = -9.8f;
        float size = 0;

        for(float i = 0; i<=100; i+=4)
        {
            y = i*9/100 - 9.8f;
            size = i / 100 * 0.8f;

            transform.localPosition = new Vector3(0, y, 10);
            transform.localScale = new Vector3(size, size, size);

            yield return new WaitForSeconds(0.01f);

        }
        

    }

    IEnumerator uscita()
    {
        float y = -0.8f;
        float size = 0.8f;

        for (float i = 100; i >= 0; i -= 4)
        {
            y = i * 9 / 100 - 9.8f;
            size = i / 100 * 0.8f;

            transform.localPosition = new Vector3(0, y, 10);
            transform.localScale = new Vector3(size, size, size);

            yield return new WaitForSeconds(0.01f);

        }
        Destroy(gameObject);
    }
}
