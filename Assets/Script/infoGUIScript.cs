using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class infoGUIScript : MonoBehaviour
{
    
    public void Setup(playerScript player)
    {
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Characters/Artwork/Idle/Portrait/" + player.textureFile);

        transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = player.nome;
        transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = player.lvl.ToString();
        transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = player.movement.ToString();
        transform.GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>().text = player.hp.ToString();
        transform.GetChild(0).GetChild(9).GetComponent<TextMeshProUGUI>().text = player.maxHp.ToString();
        if (player.weaponIsMagic)
        {
            transform.GetChild(0).GetChild(11).GetComponent<TextMeshProUGUI>().text = player.mag.ToString();
            transform.GetChild(0).GetChild(13).GetComponent<TextMeshProUGUI>().text = (player.weaponHit + (player.dex + player.lck) / 2).ToString();
        }
        else
        {
            transform.GetChild(0).GetChild(11).GetComponent<TextMeshProUGUI>().text = player.str.ToString();
            transform.GetChild(0).GetChild(13).GetComponent<TextMeshProUGUI>().text = (player.weaponHit + player.dex).ToString();
            
        }
        transform.GetChild(0).GetChild(15).GetComponent<TextMeshProUGUI>().text = player.spd.ToString();
        int playerAS = player.weaponWt - (player.str / 5);
        if (playerAS < 0) playerAS = 0;
        playerAS = player.spd - playerAS;
        int playerAVOBonus = GameObject.Find("map").GetComponent<mapScript>().mapTiles[player.x, player.y].GetComponent<tileScript>().avoBonus;
        
        if (player.haFattoQualcosa == false && player.nome == "Skye") { playerAVOBonus += 15; }
        playerAS += playerAVOBonus;

        transform.GetChild(0).GetChild(17).GetComponent<TextMeshProUGUI>().text = playerAS.ToString();
        transform.GetChild(0).GetChild(19).GetComponent<TextMeshProUGUI>().text = player.def.ToString();
        transform.GetChild(0).GetChild(21).GetComponent<TextMeshProUGUI>().text = player.res.ToString();
    }

    public void Setup(enemyScript player)
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("enemyinfoGUI");
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("enemyNameBanner");

        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Characters/Artwork/Idle/Portrait/" + player.textureFile);
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().flipX = true;

        transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = player.nome;
        transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = player.lvl.ToString();
        transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = player.movement.ToString();
        transform.GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>().text = player.hp.ToString();
        transform.GetChild(0).GetChild(9).GetComponent<TextMeshProUGUI>().text = player.maxHp.ToString();
        if (player.weaponIsMagic)
        {
            transform.GetChild(0).GetChild(11).GetComponent<TextMeshProUGUI>().text = player.mag.ToString();
            transform.GetChild(0).GetChild(13).GetComponent<TextMeshProUGUI>().text = (player.weaponHit + (player.dex + player.lck) / 2).ToString();
        }
        else
        {
            transform.GetChild(0).GetChild(11).GetComponent<TextMeshProUGUI>().text = player.str.ToString();
            transform.GetChild(0).GetChild(13).GetComponent<TextMeshProUGUI>().text = (player.weaponHit + player.dex).ToString();

        }
        transform.GetChild(0).GetChild(15).GetComponent<TextMeshProUGUI>().text = player.spd.ToString();
        int playerAS = player.weaponWt - (player.str / 5);
        if (playerAS < 0) playerAS = 0;
        playerAS = player.spd - playerAS;

        transform.GetChild(0).GetChild(17).GetComponent<TextMeshProUGUI>().text = playerAS.ToString();
        transform.GetChild(0).GetChild(19).GetComponent<TextMeshProUGUI>().text = player.def.ToString();
        transform.GetChild(0).GetChild(21).GetComponent<TextMeshProUGUI>().text = player.res.ToString();
    }

    private void Start()
    {
        StartCoroutine(animazione());
    }



    public void Rimuovi()
    {
        StartCoroutine(uscita());
    }


    IEnumerator animazione()
    {
        for(float i = 0; i <= 100; i+=10)
        {
            GetComponent<CanvasGroup>().alpha = i / 100;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i / 100);
            transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i / 100);
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i / 100);

            yield return new WaitForSeconds(0.01f);
        }

    }

    IEnumerator uscita()
    {
        for (float i = 100; i >= 0; i -= 10)
        {
            GetComponent<CanvasGroup>().alpha = i / 100;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i / 100);
            transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i / 100);
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i / 100);

            yield return new WaitForSeconds(0.01f);
        }

        Destroy(gameObject);
    }
}
