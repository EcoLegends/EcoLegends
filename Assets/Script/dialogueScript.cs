using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dialogueScript : MonoBehaviour
{
    // Start is called before the first frame update

    bool skip = false;

    void Start()
    {
        StartCoroutine(inizia());
    }


    public void Update()
    {
        if(Input.GetMouseButtonDown(0)) skip = true;

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
            GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("castello interno");
            d.Add(new Dialogo("Nova", "Nova", "Ah, Sear, ricordi quando eravamo solo dei bambini che giocavano nei giardini del palazzo? Il tempo vola così velocemente, non è vero?", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Sì, Nova, è incredibile quanto sia cambiato tutto. Ma in effetti, non ho molto tempo per riflettere sui bei ricordi.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Ci sono così tante questioni politiche da affrontare, soprattutto in un momento come questo.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Oh, non pensare sempre ai doveri, fratello mio! Ricordati di goderti anche i momenti felici.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Sai, è passato esattamente un secolo dalla sconfitta di Morgrath e l'inaugurazione di Eirene come città libera. È un momento da celebrare!", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Hai ragione, Nova. A volte mi perdo troppo nelle preoccupazioni.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Ma sai quanto è importante mantenere la stabilità del regno, specialmente dopo ciò che abbiamo affrontato con Morgrath.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Certo, capisco le tue preoccupazioni.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Ma questo anniversario non riguarda solo la fine di un'epoca buia, riguarda anche il futuro luminoso che abbiamo costruito insieme.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Eirene è un simbolo di speranza per il nostro popolo, e dobbiamo celebrarlo con gioia e gratitudine.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Hai ragione, Nova. Grazie per avermi ricordato l'importanza di queste celebrazioni.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Sarà un onore festeggiare insieme alla città e onorare coloro che hanno combattuto per la nostra libertà.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Esattamente! E ora, lascia che i nostri doveri si fermino per un momento mentre ci immergiamo nella gioia di questo anniversario.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Eirene ci aspetta, e dobbiamo essere pronti a festeggiare come solo i principi del regno del fuoco sanno fare!", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Hai ragione, Nova. Andiamo a celebrare insieme il futuro luminoso di Eirene e del nostro regno!", false, true));

            d.Add(new Dialogo("CAMBIOSCENA", "", "Mappe/Mappa" + mapNum + "_pvp", false, true)); //cambioscena 

            d.Add(new Dialogo("Nova", "Nova", "Sear, non vedo l'ora di arrivare a Eirene! Sarà una festa indimenticabile.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Concordo, Nova. Sarà fantastico passare del tempo nella città delle feste.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Ma... aspetta, cosa sono quei rumori strani?", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Non lo so, sembra provenire dalla boscaglia là davanti. Dobbiamo stare attenti.", true, true));
            d.Add(new Dialogo("Mostro", "terra1", "Uhrrr!", false, false));
            d.Add(new Dialogo("Sear", "Sear", "Oh no, sono dei mostri! Preparati, Nova, dobbiamo difenderci!", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Hai ragione, Sear! Non ci tireremo indietro. Pronti a combattere?", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Assolutamente! Affrontiamo prima questi mostri e poi pensiamo al motivo per cui ci sono dei mostri in questa zona", false, true));
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

            if(dial.nome == "CAMBIOSCENA")
            {
                GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(1);
                yield return new WaitForSeconds(1f);
                GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(dial.text);
                GameObject.Find("LevelLoader").GetComponent<LevelLoad>().transition.Play("Crossfade_start");
                yield return new WaitForSeconds(1f);
                continue;
            }


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
            skip = false;
            for (int i = 0; i <= dial.text.Length; i++)
            {
                deez.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = dial.text.Substring(0, i);
                yield return new WaitForSeconds(0.025f);
                if (skip)
                {
                    deez.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = dial.text;
                    skip = false;
                    break;
                }
            }
            for(int i = 0; i < 30; i++)
            {
                if (skip)
                {
                    skip = false;
                    break;
                }
                yield return new WaitForSeconds(0.1f);
            }
            
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
