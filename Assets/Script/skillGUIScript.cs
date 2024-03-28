using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class skillGUIScript : MonoBehaviour
{

    public void Setup(string skill)
    {
        GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("skill");
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skill;

        string[] arr = { "Vantaggio", "Persistenza", "Istinto Selvaggio", "Giardino Fiorito", "Acqua Cristallina", "Guarigione", "Dio del Vento", "In Guardia" };
        Debug.Log(System.Array.IndexOf(arr, skill));

        Sprite[] sprites = Resources.LoadAll<Sprite>("skillsIcons");
        Debug.Log(sprites.Length);

        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprites[System.Array.IndexOf(arr, skill)];
        StartCoroutine(anim());
    }

    IEnumerator anim()
    {
        for (float i = 0; i <= 100; i++)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 100);
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 100);
            transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 100);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1);
        for (float i = 100; i >= 0; i--)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, i / 100);
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 100);
            transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 100);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }

    
}
