using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class pvpForecastScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(anim());
    }

    IEnumerator anim()
    {
        for(float i= 0; i < 100; i++)
        {
            transform.localPosition = new Vector3(0, -1 + (i / 100), 15);
            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator animExit()
    {
        for (float i = 0; i < 100; i++)
        {
            transform.localPosition = new Vector3(0, (i / -100), 15);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }


    public void SetupPlayer(int hp, int maxhp, int dmg, int hit, int crit, bool doubleAtk, bool canCounter)
    {
        transform.GetChild(0).GetChild(12).GetComponent<heathBarScript>().SetMaxHealth(maxhp);
        transform.GetChild(0).GetChild(12).GetComponent<heathBarScript>().SetHealth(hp);
        transform.GetChild(0).GetChild(12).GetChild(1).GetComponent<Slider>().value = hp / (float) maxhp;

        string dmgVisual = dmg.ToString();

        if (doubleAtk == true) dmgVisual = dmg + "      " + dmg;
        transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = dmgVisual;

        transform.GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>().text = hit+"%";
        transform.GetChild(0).GetChild(10).GetComponent<TextMeshProUGUI>().text = crit + "%";

        if (!canCounter)
        {
            transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = "--";
            transform.GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>().text = "--" + "%";
            transform.GetChild(0).GetChild(10).GetComponent<TextMeshProUGUI>().text = "--" + "%";
        }
    }

    public void SetupEnemy(int hp, int maxhp, int dmg, int hit, int crit, bool doubleAtk, bool canCounter)
    {
        transform.GetChild(0).GetChild(13).GetComponent<heathBarScript>().SetMaxHealth(maxhp);
        transform.GetChild(0).GetChild(13).GetComponent<heathBarScript>().SetHealth(hp);
        transform.GetChild(0).GetChild(13).GetChild(1).GetComponent<Slider>().value = hp / (float)maxhp;

        string dmgVisual = dmg.ToString();

        if (doubleAtk == true) dmgVisual = dmg + "      " + dmg;
        transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = dmgVisual;

        transform.GetChild(0).GetChild(8).GetComponent<TextMeshProUGUI>().text = hit + "%";
        transform.GetChild(0).GetChild(11).GetComponent<TextMeshProUGUI>().text = crit + "%";

        if (!canCounter)
        {
            transform.GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>().text = "--";
            transform.GetChild(0).GetChild(8).GetComponent<TextMeshProUGUI>().text = "--" + "%";
            transform.GetChild(0).GetChild(11).GetComponent<TextMeshProUGUI>().text = "--" + "%";
        }

    }

    public void DamagePlayer(int hp, int maxhp)
    {

        int currentHP = int.Parse(transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text);
        if (currentHP > hp) { 
            transform.GetChild(0).GetChild(12).GetComponent<heathBarScript>().SetHealth(hp);
            transform.GetChild(0).GetChild(12).GetChild(1).GetComponent<Slider>().value = currentHP / (float)maxhp;
            StartCoroutine(AnimHeathbar(transform.GetChild(0).GetChild(12).GetChild(1).GetComponent<Slider>(), hp, maxhp, currentHP));
        }
        else
        {
            transform.GetChild(0).GetChild(12).GetChild(1).GetComponent<Slider>().value = hp / (float)maxhp;
            transform.GetChild(0).GetChild(12).GetComponent<heathBarScript>().SetHealth(currentHP);
            transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = hp.ToString();
            transform.GetChild(0).GetChild(12).GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(0.4431373f, 0.9058824f, 0.572549f);
            StartCoroutine(AnimHeathbar(transform.GetChild(0).GetChild(12).GetComponent<Slider>(), hp, maxhp, currentHP));

        }
            
        

    }
    public void DamageEnemy(int hp, int maxhp)
    {

        int currentHP = int.Parse(transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text);

        if (currentHP > hp)
        {
            transform.GetChild(0).GetChild(13).GetComponent<heathBarScript>().SetHealth(hp);
            transform.GetChild(0).GetChild(13).GetChild(1).GetComponent<Slider>().value = currentHP / (float)maxhp;
            StartCoroutine(AnimHeathbar(transform.GetChild(0).GetChild(13).GetChild(1).GetComponent<Slider>(), hp, maxhp, currentHP));
        }
        else
        {
            transform.GetChild(0).GetChild(13).GetChild(1).GetComponent<Slider>().value = hp / (float)maxhp;
            transform.GetChild(0).GetChild(13).GetComponent<heathBarScript>().SetHealth(currentHP);
            transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = hp.ToString();
            transform.GetChild(0).GetChild(13).GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(0.4431373f, 0.9058824f, 0.572549f);
            StartCoroutine(AnimHeathbar(transform.GetChild(0).GetChild(13).GetComponent<Slider>(), hp, maxhp, currentHP));

        }
    }

    IEnumerator AnimHeathbar(Slider slider, int hp, int maxhp, int currentHP)
    {
        float sliderValue = currentHP;

        if(currentHP > hp)
        {
            for (int i = 0; i < (30 * (currentHP - hp)); i++)
            {
                sliderValue -= (float)(currentHP - hp) / (30 * (currentHP - hp));

                slider.value = sliderValue / (float)maxhp;
                
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            for (int i = 0; i < (30 * (hp - currentHP)); i++)
            {
                sliderValue += (float)(hp - currentHP) / (30 * (hp - currentHP));

                slider.value = sliderValue;

                yield return new WaitForEndOfFrame();
            }
        }
        

                
    }

    public void Rimuovi()
    {
        StartCoroutine(animExit());
    }

}
