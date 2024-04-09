using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

        List<GameObject> pgs = new List<GameObject>();

        string music = "Enjoy the Evening";
        

        List<Dialogo> d = new List<Dialogo>();

        if (mapNum == 1 && mapCompleted == false)
        {
            GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("castello interno");

            GameObject pg = Instantiate(Resources.Load<GameObject>("Characters/Nova"), new Vector3(0.9506399f, -0.49f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Sear"), new Vector3(2.331744f, -0.49f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);


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

            d.Add(new Dialogo("INIZIOCAMBIOSCENA", "", "Mappe/Mappa" + mapNum + "_pvp", false, true)); //cambioscena 
            d.Add(new Dialogo("SPAWN", "Nova", "-2.304,-0.209", true, true));
            d.Add(new Dialogo("SPAWN", "Sear", "-0.488,-0.132", false, true));
            d.Add(new Dialogo("FINECAMBIOSCENA", "", "Mappe/Mappa" + mapNum + "_pvp", false, true)); //cambioscena 


            d.Add(new Dialogo("Nova", "Nova", "Sear, non vedo l'ora di arrivare a Eirene! Sarà una festa indimenticabile.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Concordo, Nova. Sarà fantastico passare del tempo nella città delle feste.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Ma... aspetta, cosa sono quei rumori strani?", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Non lo so, sembra provenire dalla boscaglia là davanti. Dobbiamo stare attenti.", true, true));
            d.Add(new Dialogo("Mostro", "terra1", "Uhrrr!", false, false));
            d.Add(new Dialogo("Sear", "Sear", "Oh no, sono dei mostri! Preparati, Nova, dobbiamo difenderci!", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Hai ragione, Sear! Non ci tireremo indietro. Pronti a combattere?", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Assolutamente! Affrontiamo prima questi mostri e poi pensiamo al motivo per cui ci sono dei mostri in questa zona", false, true));
        }
        else if (mapNum == 2 && mapCompleted == false)
        {
            d.Add(new Dialogo("Nova", "Nova", "test: ecco la gemma del fuoco", true, true));

            d.Add(new Dialogo("MOSTRA", "", "Gemma_Fuoco", true, true));

            d.Add(new Dialogo("Sear", "Sear", "wow bella", false, true));
        }




        else if (mapNum == 5 && mapCompleted == false)
        {
            music = "Stalwart Preparations";

            GameObject pg = Instantiate(Resources.Load<GameObject>("Characters/Nova"), new Vector3(-1.261f, -0.3729106f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Thera"), new Vector3(-3.015f, -0.3729106f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Hydris"), new Vector3(-2.28f, -0.3729106f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Skye"), new Vector3(-0.54f, -0.009f, 0), Quaternion.identity);
            pgs.Add(pg);


            pg = Instantiate(Resources.Load<GameObject>("Characters/Sear"), new Vector3(2.861f, -0.3729106f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            GameObject pg1 = Instantiate(Resources.Load<GameObject>("Characters/Granius"), new Vector3(0, 0, 0), Quaternion.identity);
            pgs.Add(pg1);
            pg = new GameObject();
            pg.transform.position = new Vector3(2.202f, -0.3729106f, 0);
            pg1.transform.parent = pg.transform;
            pg1.transform.localPosition = Vector3.zero;
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pg = Instantiate(Resources.Load<GameObject>("Characters/Acquira"), new Vector3(0.91f, -0.3729106f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Aeria"), new Vector3(1.603f, -0.159f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);



            d.Add(new Dialogo("Nova", "Nova", "Siamo arrivati finalmente a Eirene...", true, true));
            d.Add(new Dialogo("Sear", "Sear", "La città è in condizioni peggiori di quello che pensavo. Le fiamme stanno divorando tutte le abitazioni.", false, true));
            d.Add(new Dialogo("Hydris", "Hydris", "Speriamo che i cittadini si siano messi al riparo...", true, true));
            d.Add(new Dialogo("Granius", "Granius", "Morgrath la paghera! Come ha potuto radere al suolo la nostra capitale?", false, true));
            d.Add(new Dialogo("Skye", "Skye", "Calmatevi tutti! Abbiamo bisogno di un piano per sconfiggere Morgrath.", true, true));
            d.Add(new Dialogo("Aeria", "Aeria", "Skye ha ragione, calmiamoci tutti e sentiamo il suo piano.", false, true));
            d.Add(new Dialogo("Skye", "Skye", "La città ha due entrate principali, una a destra e una a sinistra.", true, true));
            d.Add(new Dialogo("Skye", "Skye", "Conviene sfruttarle tutte e due a nostro vantaggio dividendoci in due gruppi, in modo da ripulire tutti i mostri velocemente.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Per poi riunirsi al centro e attaccare Morgrath tutti insieme?", false, true));
            d.Add(new Dialogo("Skye", "Skye", "Esatto, mi hai proprio letto nel pensiero.", true, true));
            d.Add(new Dialogo("Acquira", "Acquira", "Mi piace, veloci e dritti all'obbiettivo. Dovrebbe andare.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Questa è la nostra ultima battaglia, mettetecela tutta e preparatevi a qualsiasi imprevisto. Ho fidicia in voi.", true, true));
        }
        else if (mapNum == 5 && mapCompleted == true)
        {
            d.Add(new Dialogo("Nova", "Nova", "mappa1 finita", true, true));
            d.Add(new Dialogo("Sear", "Sear", "pro gg fra", false, true));
        }

        else
        {
            if(mapCompleted) d.Add(new Dialogo("Nova", "Nova", "default mappa vinta", true, true));
            else d.Add(new Dialogo("Nova", "Nova", "default mappa iniziata", true, true));
        }

        GameObject musicPlayer = (GameObject)Instantiate(Resources.Load("Music", typeof(GameObject)), Vector3.one, Quaternion.identity);
        musicPlayer.name = "Music";

        musicPlayer.GetComponent<musicScript>().music = music;

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

            if(dial.nome == "INIZIOCAMBIOSCENA")
            {
                GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(1);
                yield return new WaitForSeconds(1f);
                GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(dial.text);
                foreach (GameObject g in pgs) Destroy(g);
                pgs.Clear();
                continue;
            }
            if (dial.nome == "FINECAMBIOSCENA")
            {
                GameObject.Find("LevelLoader").GetComponent<LevelLoad>().transition.Play("Crossfade_start");
                yield return new WaitForSeconds(1f);
                continue;
            }
            if (dial.nome == "SPAWN")
            {
                GameObject pg = Instantiate(Resources.Load<GameObject>("Characters/"+dial.texture), new Vector3(float.Parse(dial.text.Split(",")[0],CultureInfo.InvariantCulture), float.Parse(dial.text.Split(",")[1],CultureInfo.InvariantCulture), 0), Quaternion.identity);
                if(!dial.latoSX) pg.transform.Rotate(new Vector3(0, 180, 0));
                pgs.Add(pg);
                continue;
            }
            if (dial.nome == "MOSTRA")
            {
                GameObject item = Instantiate(Resources.Load<GameObject>("gemmaGUI"));
                item.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(dial.text);
                for(float i=0;i<=10;i++) 
                {
                    item.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, i / 10);
                    item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 10);
                    yield return new WaitForEndOfFrame();
                }

                skip = false;
                for (int i = 0; i < 30; i++)
                {
                    if (skip)
                    {
                        skip = false;
                        break;
                    }
                    yield return new WaitForSeconds(0.1f);
                }

                for (float i = 10; i >= 0; i--)
                {
                    item.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, i / 10);
                    item.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 10);
                    yield return new WaitForEndOfFrame();
                }

                Destroy(item);
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
