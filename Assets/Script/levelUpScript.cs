using UnityEngine;
using TMPro;

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
    }


    void Update()
    {
        
    }
}
