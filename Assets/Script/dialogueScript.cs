using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dialogueScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(inizia());
    }

    IEnumerator inizia()
    {

        string p = Application.streamingAssetsPath + "/dialogo.txt";
        StreamReader r = new StreamReader(p);

        int mapNum = int.Parse(r.ReadLine());
        bool mapCompleted = bool.Parse(r.ReadLine());
        r.Close();

        GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapNum + "_pvp");

        GameObject musicPlayer = (GameObject)Instantiate(Resources.Load("Music", typeof(GameObject)), Vector3.one, Quaternion.identity);
        musicPlayer.name = "Music";

        musicPlayer.GetComponent<musicScript>().music = "Enjoy the Evening";

        List<Dialogo> d = new List<Dialogo>();

        if (mapNum == 1 && mapCompleted == false)
        {
            d.Add(new Dialogo("Nova", "Nova", "dialogo mappa 1 okkkkkkk", true, true));
            d.Add(new Dialogo("Sear", "Sear", "ssiii fraaaaa", false, true));
        }
        else if (mapNum == 1 && mapCompleted == true)
        {
            d.Add(new Dialogo("Nova", "Nova", "mappa1 finita", true, true));
            d.Add(new Dialogo("Sear", "Sear", "pro gg fra", false, true));
        }
        else
        {
            if(mapCompleted) d.Add(new Dialogo("Nova", "Nova", "default mappa vinta", true, true));
            else d.Add(new Dialogo("Nova", "Nova", "default mappa iniziata", true, true));
        }



        yield return new WaitForSeconds(0.5f);



        GameObject deez = (GameObject)Instantiate(Resources.Load("Dialogo", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity);

        bool dx = false, sx = false;
        deez.transform.GetChild(0).GetChild(0).localScale = Vector3.zero;
        deez.transform.GetChild(0).GetChild(1).localScale = Vector3.zero;
        deez.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "";


        yield return new WaitForSeconds(1);
        for (float i = 0; i <= 17; i++)
        {
            deez.transform.position = new Vector3(0, i / 20, 0);
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
            if (!dial.player) deez.transform.GetChild(0).GetChild(index).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("enemyNameBanner");
            deez.transform.GetChild(0).GetChild(index).GetChild(0).GetComponent<SpriteRenderer>().sprite = dial.sprite;
            deez.transform.GetChild(0).GetChild(index).GetChild(1).GetComponent<TextMeshProUGUI>().text = dial.nome;

            deez.transform.GetChild(0).GetChild(index).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            deez.transform.GetChild(0).GetChild(index).GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

            if (dial.latoSX && !sx)
            {
                sx = true;
                for (float i = 0; i <= 20; i++)
                {
                    deez.transform.GetChild(0).GetChild(0).localScale = new Vector3(9.259258f * (i / 20), 5.185184f * (i / 20), 9.259258f * (i / 20));
                    yield return new WaitForEndOfFrame();
                }
            }
            if (!dial.latoSX && !dx)
            {
                dx = true;
                for (float i = 0; i <= 20; i++)
                {
                    deez.transform.GetChild(0).GetChild(1).localScale = new Vector3(9.259258f * (i / 20), 5.185184f * (i / 20), 9.259258f * (i / 20));
                    yield return new WaitForEndOfFrame();
                }
            }

            for (int i = 0; i <= dial.text.Length; i++)
            {
                deez.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = dial.text.Substring(0, i);
                yield return new WaitForSeconds(0.025f);
            }
            yield return new WaitForSeconds(3);
        }
        for (float i = 17; i >= 0; i--)
        {
            deez.transform.position = new Vector3(0, i / 20, 0);
            yield return new WaitForEndOfFrame();
        }
        Destroy(deez);

        
        musicPlayer.GetComponent<musicScript>().Rimuovi();
        GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(1);
        yield return new WaitForSeconds(1f);
        AsyncOperation async;
        if (mapCompleted ) async = SceneManager.LoadSceneAsync("Menu");
        else async = SceneManager.LoadSceneAsync("MainScene");

        while (!async.isDone)
        {

            yield return new WaitForEndOfFrame();

        }
    }

}
