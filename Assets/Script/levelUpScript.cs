using UnityEngine;
using TMPro;
using System.Collections;

public class levelUpScript : MonoBehaviour
{
    public string nome;
    public int lvl;
    public int hp;
    public int str;
    public int mag;
    public int dex;
    public int spd;
    public int lck;
    public int def;
    public int res;
    public int[] increments = new int[8];

   

    public void Setup(string nome, int lvl, int hp, int str, int mag, int dex, int spd, int lck, int def, int res, int[] increments)
    {
        this.nome = nome;
        this.lvl = lvl;
        this.hp = hp;
        this.str = str;
        this.mag = mag;
        this.dex = dex;
        this.spd = spd;
        this.lck = lck;
        this.def = def;
        this.res = res;
        this.increments = increments;

        string[] names = { "HP", "Str", "Mag", "Dex", "Spd", "Lck", "Def", "Res" };
        int[] values = { hp, str, mag, dex, spd, lck, def, res };

        Debug.Log(transform.childCount);

        for (int i = 1; i <= 8; i++)
        {
            transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = names[i-1] + " " + values[i-1].ToString();
            transform.GetChild(i).GetChild(0).transform.localScale = Vector3.zero;
        }

        transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = lvl.ToString();
        transform.GetChild(9).GetChild(0).transform.localScale = Vector3.zero;

        transform.GetChild(10).GetComponent<TextMeshProUGUI>().text = nome;

        StartCoroutine(animazione());
    }

    IEnumerator animazione()
    {
        AudioClip statsUp = (AudioClip)Resources.Load("Sounds/SFX/Stats Up");
        AudioClip levelUp = (AudioClip)Resources.Load("Sounds/SFX/Level Up");

        string[] names = { "HP", "Str", "Mag", "Dex", "Spd", "Lck", "Def", "Res" };
        int[] values = { hp, str, mag, dex, spd, lck, def, res };

        float scale = 0;

        for(int i = 0; i < 20; i++)
        {
            scale += (0.36f / 20);
            GetComponent<RectTransform>().localScale = new Vector3 (scale, scale, scale);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        transform.GetChild(11).GetComponent<AudioSource>().PlayOneShot(levelUp);

        for (float i = 1; i <= 20; i++)
        {
            transform.GetChild(9).GetChild(0).transform.localScale = new Vector3(i / 20, i / 20, i / 20);
            if(i>=10) transform.GetChild(9).transform.localScale = new Vector3((i+10) / 20, (i+10) / 20, (i+10) / 20);
            yield return new WaitForEndOfFrame();

        }
        transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = (lvl + 1).ToString();
        for (float i = 20; i >= 0; i--)
        {
            if (i >= 10) transform.GetChild(9).transform.localScale = new Vector3((i + 10) / 20, (i + 10) / 20, (i + 10) / 20);
            transform.GetChild(9).GetChild(0).transform.localScale = new Vector3(i / 20, i / 20, i / 20);
            yield return new WaitForEndOfFrame();

        }

        yield return new WaitForSeconds(0.5f);


        for(int j = 0; j < 8; j++)
        {
            if (increments[j] == 1)
            {
                transform.GetChild(11).GetComponent<AudioSource>().PlayOneShot(statsUp);
                for (float i = 1; i <= 10; i++)
                {
                    transform.GetChild(j+1).GetChild(0).transform.localScale = new Vector3(i / 10, i / 10, i / 10);
                    if (i >= 5) transform.GetChild(j+1).transform.localScale = new Vector3((i + 5) / 10, (i + 5) / 10, (i + 5) / 10);
                    yield return new WaitForEndOfFrame();

                }
                transform.GetChild(j+1).GetComponent<TextMeshProUGUI>().text = (names[j]+" "+(values[j] + 1)).ToString();
                for (float i = 10; i >= 0; i--)
                {
                    if (i >= 5) transform.GetChild(j+1).transform.localScale = new Vector3((i + 5) / 10, (i + 5) / 10, (i + 5) / 10);
                    transform.GetChild(j+1).GetChild(0).GetChild(1).transform.localScale = new Vector3(i / 10, i / 10, i / 10);
                    yield return new WaitForEndOfFrame();

                }
                yield return new WaitForSeconds(0.05f);
            }
            
        }


        yield return new WaitForSeconds(1);
        scale = 0.36f;

        for (int i = 0; i < 20; i++)
        {
            scale -= (0.36f / 20);
            GetComponent<RectTransform>().localScale = new Vector3(scale, scale, scale);
            yield return new WaitForEndOfFrame();
        }
        GameObject.Find("CombatCamera").GetComponent<PvPscript>().finitoLvlUp = true;
        Destroy(this.gameObject);
        
    }
    void Update()
    {
        
    }
}
