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
            GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("prologo");
            GameObject.Find("Sfondo").transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);

            d.Add(new Dialogo("", "", "Questo è il continente di Eldoria.", true, true));
            d.Add(new Dialogo("", "", "È abitata da nazioni di elementi diversi:", true, true));
            d.Add(new Dialogo("", "", "Il Regno del Fuoco, l'Impero della Terra, la Repubblica dell'Acqua e la Federazione dell'Aria.", true, true));
            d.Add(new Dialogo("", "", "Un giorno, il cielo si oscurò e apparve un entità maligna nota come l'Inquinatore Morgrath che portò il continente alla rovina.", true, true));
            d.Add(new Dialogo("", "", "Furono quattro eroi passati alla storia come EcoLegends che salvarono Eldoria.", true, true));
            d.Add(new Dialogo("", "", "Imprigionarono l'Inquinatore sottoterra con la magia, fondando la città Eirene come simbolo di pace.", true, true));
            d.Add(new Dialogo("", "", "Sono passati più di cento anni dal Grande Inquinamento e la magia si sta indebolendo...", true, true));

            d.Add(new Dialogo("INIZIOCAMBIOSCENA", "despawn", "castello interno", false, true)); //cambioscena 
            d.Add(new Dialogo("SPAWN", "Nova", "0.9506399,-0.49", true, true));
            d.Add(new Dialogo("SPAWN", "Sear", "2.331744,-0.49", false, true));
            d.Add(new Dialogo("FINECAMBIOSCENA", "", "Mappe/Mappa" + mapNum + "_pvp", false, true)); //cambioscena 


            d.Add(new Dialogo("Nova", "Nova", "Ah, Sear, ricordi quando eravamo solo dei bambini che giocavano nei giardini del palazzo? Il tempo vola così velocemente, non è vero?", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Sì, Nova, è incredibile quanto sia cambiato tutto. Ma in effetti, non ho molto tempo per riflettere sui bei ricordi.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Ci sono così tante questioni politiche da affrontare, soprattutto in un momento come questo.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Oh, non pensare sempre ai doveri, fratello mio! Ricordati di goderti anche i momenti felici.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Sai, è passato esattamente un secolo dalla sconfitta di Morgrath e l'inaugurazione di Eirene come città libera. È un momento da celebrare!", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Hai ragione, Nova. A volte mi preoccupo troppo.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Ma sai quanto è importante mantenere la stabilità del regno, specialmente dopo ciò che abbiamo affrontato con Morgrath.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Certo, capisco le tue preoccupazioni.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Ma questo anniversario non riguarda solo la fine di un'epoca buia, riguarda anche il futuro luminoso che abbiamo costruito insieme.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Eirene è un simbolo di speranza per il nostro popolo, e dobbiamo celebrarlo con gioia e gratitudine.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Sarà un onore festeggiare insieme alla città e onorare coloro che hanno combattuto per la nostra libertà.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Esattamente! E ora, lascia che i nostri doveri si fermino per un momento mentre ci immergiamo nella gioia di questo anniversario.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Eirene ci aspetta, e dobbiamo essere pronti a festeggiare come solo i principi del Regno del Fuoco sanno fare!", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Hai ragione, Nova. Andiamo a celebrare insieme il futuro luminoso di Eirene e del nostro regno!", false, true));

            d.Add(new Dialogo("INIZIOCAMBIOSCENA", "despawn", "Mappe/Mappa" + mapNum + "_pvp", false, true)); //cambioscena 
            d.Add(new Dialogo("SPAWN", "Nova", "-2.304,-0.209", true, true));
            d.Add(new Dialogo("SPAWN", "Sear", "-0.488,-0.132", false, true));
            d.Add(new Dialogo("FINECAMBIOSCENA", "", "Mappe/Mappa" + mapNum + "_pvp", false, true)); //cambioscena 


            d.Add(new Dialogo("Nova", "Nova", "Sear, non vedo l'ora di arrivare a Eirene! Sarà una festa indimenticabile.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Concordo, Nova. Sarà fantastico passare del tempo nella città delle feste.", false, true));

            d.Add(new Dialogo("CAMBIAMUSICA", "", "Stalwart Preparations", false, true)); //cambio musica 

            d.Add(new Dialogo("Sear", "Sear", "Ma... aspetta, cosa sono quei rumori strani?", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Non lo so, sembra che ci stiano venendo incontro. Dobbiamo stare attenti.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Preparati Nova dobbiamo diffenderci!", false, true));
        }
        else if (mapNum == 1 && mapCompleted == true)
        {            
            GameObject pg = Instantiate(Resources.Load<GameObject>("Characters/Nova"), new Vector3(-0.6681742f, 0.009735733f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Sear"), new Vector3(1.322946f, 0.06313743f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            d.Add(new Dialogo("Sear", "Sear", "Il potere oscuro attorno a questi mostri sembra quello di Morgrath.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Ma non è già stato sconfitto e imprigionato nella città di Eirene?", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Deve essere successo qualcosa a Eirene... È meglio accelerare.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Sear, guarda questa gemma sulla mia corona.", true, true));
            d.Add(new Dialogo("MOSTRA", "", "Gemma_Fuoco", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Ha iniziato a brillare dopo che abbiamo combattuto quei mostri.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Credi che abbia qualcosa a che fare con loro?", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Hmm, potrebbe essere. Penso si sia attivata al contatto con l'inquinamento.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Ma cosa fa esattamente questa gemma?", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Ho sentito dire che è stata forgiata per assorbire e purificare l'inquinamento.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Potrebbe essere la chiave per risolvere il problema alla radice!", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Dobbiamo investigare di più su questa gemma e su come possiamo usarla per fermare l'inquinamento.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Bisogna raggiungere Eirene al più presto... I mostri provenivano da quella direzione...", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Deve essere successo qualcosa di grave...", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Allora sbrighiamoci...", false, true));

            d.Add(new Dialogo("INIZIOCAMBIOSCENA", "despawn", "Mappe/Mappa" + mapNum + "_pvp", false, true)); //cambioscena 
            
            d.Add(new Dialogo("SPAWN", "Nova", "-2.432724,-0.1351202", true, true));
            d.Add(new Dialogo("SPAWN", "Sear", "-1.438484,-0.1114478", true, false));
            d.Add(new Dialogo("SPAWN", "terra1", "0.4488141,0.06090879", true, true));
            d.Add(new Dialogo("SPAWN", "terra1", "1.431041,0.1595911", true, false));
            d.Add(new Dialogo("SPAWN", "terra1", "2.523518,0.2312951", true, false));

            d.Add(new Dialogo("FINECAMBIOSCENA", "", "Mappe/Mappa" + mapNum + "_pvp", false, true)); //cambioscena 

            d.Add(new Dialogo("???", "terra1", "Grrraugh!", false, false));
            d.Add(new Dialogo("Nova", "Nova", "Ci stanno sbarrando la strada... Dobbiamo affrontarli di nuovo.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Nova, non ha senso sprecare energia contro questi mostri... Sono troppi!", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Conviene dirigersi verso l'Impero della Terra e chiedere rinforzi.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Hai ragione... Non ce la faremo mai da soli. Andiamo!", false, true));
        }

        else if (mapNum == 2 && mapCompleted == false){

            
            GameObject pg = Instantiate(Resources.Load<GameObject>("Characters/Nova"), new Vector3(-2.257322f, -0.1896601f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Sear"), new Vector3(-1.094686f, -0.1996828f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            d.Add(new Dialogo("Nova", "Nova", "Sear, quanto dobbiamo camminare ancora prima di raggiungere la capitale dell'Impero della Terra?", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Siamo quasi arrivati. Dobbiamo solo resistere ancora un po'.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Ma guardati intorno! I campi, le foreste... tutto è in rovina a causa di quest'inquinamento! È orribile.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Lo so Nova, ma dobbiamo rimanere concentrati sul nostro obiettivo.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Una volta raggiunta la città, troveremo una soluzione per fermare tutto questo.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Guarda, Sear! Finalmente vedo le mura della città! Siamo arrivati!", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Sì, finalmente siamo qui. Ma guarda come è stata danneggiata... È ancora più devastata di quanto immaginassi.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Guarda lì! Granius e Thera sono in pericolo! Dobbiamo aiutarli!", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Hai ragione. Non possiamo lasciarli da soli.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Preparati Nova, dobbiamo affrontare questi mostri inquinanti e proteggere i nostri amici!", false, true));
        }

        else if (mapNum == 2 && mapCompleted == true)
        {
            GameObject pg = Instantiate(Resources.Load<GameObject>("Characters/Nova"), new Vector3(1.89f, -0.17f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Thera"), new Vector3(-2.268654f, -0.17f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Granius"), new Vector3(-1.190072f, -0.17f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Sear"), new Vector3(2.678753f, -0.17f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);



            d.Add(new Dialogo("Nova", "Nova", "Per fortuna siamo riusciti a salvarvi, Thera e Granius. Quell'inquinamento vi stava quasi consumando!", false, true));
            d.Add(new Dialogo("Thera", "Thera", "Non so come ringraziarvi, Nova e Sear. Senza di voi, non so cosa sarebbe successo.", true, true));
            d.Add(new Dialogo("Granius", "Granius", "Davvero, siete stati coraggiosi e rapidi nel prendere quella decisione.", true, true));
            d.Add(new Dialogo("Granius", "Granius", "E questa gemma... non l'avevo mai vista prima. Che cos'è esattamente?", true, true));
            d.Add(new Dialogo("Sear", "Sear", "È una gemma speciale che Nova portava sulla sua corona.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Si è attivata al contatto con l'inquinamento ed è capace di purificarlo. Senza di essa, temo che avremmo perso.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Dobbiamo essere grati di aver scoperto il potere di questa gemma in tempo.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Ora dobbiamo capire come usare questa conoscenza per fermare l'inquinamento che minaccia la nostra terra.", false, true));
            d.Add(new Dialogo("Thera", "Thera", "Aspetta un attimo, Nova. Anch'io ho una gemma simile!", true, true));
            d.Add(new Dialogo("MOSTRA", "", "Gemma_Terra", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Davvero? Non me ne hai mai parlato prima.", false, true));
            d.Add(new Dialogo("Thera", "Thera", "L'ho tenuta nascosta tra i fiori della mia ascia per non perderla.", true, true));
            d.Add(new Dialogo("Thera", "Thera", "Ora che sappiamo come funziona, possiamo unire le forze e aumentare le nostre possibilità di sconfiggere l'inquinamento.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Questo è incredibile!", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Con entrambe le gemme a nostra disposizione, possiamo purificare l'inquinamento in modo più efficiente e veloce.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Grazie, Thera, per avercelo detto.", false, true));
            d.Add(new Dialogo("Granius", "Granius", "Sembra che il destino ci stia dando una mano.", true, true));
            d.Add(new Dialogo("Granius", "Granius", "Dobbiamo sfruttare al massimo questa opportunità e proteggere Eldoria insieme.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Allora, cosa aspettiamo? Andiamo a sconfiggere Morgrath a Eirene!", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Probabilmente è riuscito ad uscire dalla prigione in qualche modo.", false, true));
            d.Add(new Dialogo("Thera", "Thera", "Forse è meglio unire prima tutti i poteri rimaste e poi sconfiggere Morgrath tutti insieme.", true, true));
            d.Add(new Dialogo("Granius", "Granius", "Vista la situazione, penso che ogni elemento abbia una gemma che può purificare l'inquinamento.", true, true));
            d.Add(new Dialogo("Granius", "Granius", "Queste gemme devono essere servite ai nostri antenati per combattere e imprigionare Morgrath.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Probabile, allora primaa dirigiamoci verso la Repubblica dell'Acqua!", false, true));
            


        }

        else if (mapNum == 6 && mapCompleted == false) //mappa 3 fake
        {
            GameObject pg = Instantiate(Resources.Load<GameObject>("Characters/Nova"), new Vector3(-0.903f, -0.2f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Thera"), new Vector3(-1.622701f, -0.32f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Granius"), new Vector3(-2.36424f, -0.29f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Sear"), new Vector3(-0.2195939f, -0.14f, 0), Quaternion.identity);
            pgs.Add(pg);


            pg = Instantiate(Resources.Load<GameObject>("Characters/Hydris"), new Vector3(2.746848f, -0.12f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Acquira"), new Vector3(1.896f, -0.24f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);

           
            music = "A Beacon of Light Awakens";

            d.Add(new Dialogo("Sear", "Sear", "Acquira e Hydris? Stavamo cercando proprio voi.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Morgrath è tornato e sta inquinando tutto il continente, ci serve il vostro aiuto per fermarlo.", true, true));
            d.Add(new Dialogo("CAMBIAMUSICA", "", "Stalwart Preparations", false, true)); //cambio musica 
            d.Add(new Dialogo("Acquira", "Acquira", "Ne siamo già al corrente, sono arrivate molte orde di mostri sulle nostre coste.", false, true));
            d.Add(new Dialogo("Hydris", "Hydris", "È da ore che proteggiamo le nostre spiagge ma ne continuano ad arrivare. Non so quanto riusciremo a resistere.", false, true));
            d.Add(new Dialogo("Granius", "Granius", "Non preoccupatevi, adesso che siamo qui possiamo aiutarvi.", true, true));
            d.Add(new Dialogo("Thera", "Thera", "Facciamo vedere a questi mostri chi comanda!", true, true));

        }
        else if (mapNum == 3 && mapCompleted == false) 
        {
            GameObject pg = Instantiate(Resources.Load<GameObject>("Characters/Nova"), new Vector3(-0.903f, -0.2f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Thera"), new Vector3(-1.622701f, -0.32f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Granius"), new Vector3(-2.36424f, -0.29f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Sear"), new Vector3(-0.2195939f, -0.14f, 0), Quaternion.identity);
            pgs.Add(pg);


            pg = Instantiate(Resources.Load<GameObject>("Characters/Hydris"), new Vector3(2.746848f, -0.12f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Acquira"), new Vector3(1.896f, -0.24f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);


            
            d.Add(new Dialogo("Sear", "Sear", "Ce l'abbiamo fatta, per fortuna l'inquinamento non ha avuto la meglio.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Ora che siete al sicuro possiamo dirigerci verso..", true, true));
            d.Add(new Dialogo("CAMBIAMUSICA", "", "Indomitable Will", false, true)); //cambio musica 
            d.Add(new Dialogo("Acquira", "Acquira", "Fermi dove siete!", false, true));

            d.Add(new Dialogo("INIZIOCAMBIOSCENA", "", "Mappe/Mappa" + mapNum + "_pvp", false, true)); //cambioscena 

            d.Add(new Dialogo("SPAWN", "acqua1", "0.696,-0.630", false, true));
            d.Add(new Dialogo("SPAWN", "acqua1", "0.696,0.1211906", false, false));
            d.Add(new Dialogo("SPAWN", "acqua2", "-3.124906,-0.3192049", true, false));

            d.Add(new Dialogo("FINECAMBIOSCENA", "", "Mappe/Mappa" + mapNum + "_pvp", false, true)); //cambioscena 

            d.Add(new Dialogo("Granius", "Granius", "Cosa sta succedendo?", true, true));
            d.Add(new Dialogo("Hydris", "Hydris", "Siete circondati, arrendetevi ora o saremo costretti a sconfiggervi.", false, false));
            d.Add(new Dialogo("Thera", "Thera", "Ma che stai dicendo Hydris!? Noi siamo dalla vostra parte.", true, true));
            d.Add(new Dialogo("Hydris", "Hydris", "Siete caduti dritti nella nostra trappola. Il piano di Morgrath era infallibile.", false, false));
            d.Add(new Dialogo("Nova", "Nova", "Come avete potuto unirvi a Morgrath? Siamo amici!", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Vi sta manipolando, non dovete credere a quello che vi ha detto!", true, true));
            d.Add(new Dialogo("Acquira", "Acquira", "Silenzio!", false, false));
            d.Add(new Dialogo("Acquira", "Acquira", "Se non volete arrendervi non rimane altro che combattere.", false, false));
            d.Add(new Dialogo("Granius", "Granius", "Prepariamoci, questa battaglia non sarà per niente facile...", true, true));
        }

        else if (mapNum == 3 && mapCompleted == true)
        {
            GameObject pg = Instantiate(Resources.Load<GameObject>("Characters/Nova"), new Vector3(2.183945f, -0.2f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Thera"), new Vector3(3.031333f, -0.2f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Granius"), new Vector3(-2.683f, -0.2f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Sear"), new Vector3(1.249646f, -0.2f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Hydris"), new Vector3(-0.858f, -0.2f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Acquira"), new Vector3(-1.93f, -0.2f, 0), Quaternion.identity);
            pgs.Add(pg);



            d.Add(new Dialogo("Acquira", "Acquira", "Per fortuna ci avete salvati... non avevamo idea di essere sotto il controllo di Morgrath.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Non preoccupatevi, non è colpa vostra se Morgrath è riuscito ad avere la meglio su di voi.", false, true));
            d.Add(new Dialogo("Hydris", "Hydris", "Ci dispiace molto... spero di non aver fatto male a nessuno.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Hmm... Hydris, quella gemma che hai sul bastone, potrebbe essere per caso una gemma elementare?", false, true));
            d.Add(new Dialogo("Hydris", "Hydris", "Una gemma elementare? Cosa sarebbe?", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Le gemme elementari sono state usate dai nostri antenati per sconfiggere ed imprigionare Morgrath.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Sono capaci di purificare l'inquinamento, senza di loro non saremo riusciti a salvarvi.", false, true));
            d.Add(new Dialogo("Hydris", "Hydris", "Hmm... Il bastone è un cimelio di famiglia, è probabile che sia una delle gemme di cui stai parlando.", true, true));
            d.Add(new Dialogo("MOSTRA", "", "Gemma_Acqua", true, true));
            d.Add(new Dialogo("Nova", "Nova", "È proprio la gemma dell'acqua. Ora ce ne manca solo una.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Dirigiamoci verso la Federazione dell'Aria, l'ultima gemma deve essere lì.", false, true));
           
        }

        else if(mapNum == 4 && mapCompleted == false)
        {
            GameObject pg = Instantiate(Resources.Load<GameObject>("Characters/Nova"), new Vector3(-2.506786f, -0.3836665f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Thera"), new Vector3(-3.226575f, -0.3836665f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Granius"), new Vector3(-1.813266f, -0.3836665f, 0), Quaternion.identity);
            pgs.Add(pg);


            pg = Instantiate(Resources.Load<GameObject>("Characters/Sear"), new Vector3(0.714f, -0.3836665f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Hydris"), new Vector3(2.353106f, -0.3836665f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Acquira"), new Vector3(1.512476f, -0.3836665f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);

            GameObject box = new GameObject();
            box.transform.position = new Vector3(0, 0.5f, 0);
            
            pg = Instantiate(Resources.Load<GameObject>("Characters/Aeria"), box.transform);
            pgs.Add(pg);
            pg.transform.localPosition = new Vector3(1.38f, -0.195f, 0);
            
            pg = Instantiate(Resources.Load<GameObject>("Characters/Skye"), box.transform);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg.transform.localPosition = new Vector3(0.528f, 0.194f,0);

            pg = Instantiate(Resources.Load<GameObject>("Characters/volante1"), box.transform);
            pgs.Add(pg);
            pg.transform.localPosition = new Vector3(-0.677f, -0.602f, 0);

            pg = Instantiate(Resources.Load<GameObject>("Characters/volante2"), box.transform);
            pgs.Add(pg);
            pg.transform.localPosition = new Vector3(2.623f,0,0);


            box.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            music = "A Beacon of Light Awakens";

            d.Add(new Dialogo("Nova", "Nova", "Abbiamo appena varcato il confine della Federazione dell'Aria.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "State in guardia, non sappiamo cosa aspettarci.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Con questa nebbia sarà difficile avvistare i nemici...", false, true));
            d.Add(new Dialogo("CAMBIAMUSICA", "", "Stalwart Preparations", false, true)); //cambio musica 
            d.Add(new Dialogo("Granius", "Granius", "Hmm... Guardate laggiù! Oltre il ponte. Non sono Aeria e Skye?", true, true));
            d.Add(new Dialogo("Hydris", "Hydris", "Hai ragione, sembra stiano combattendo qualcosa... Potrebbero essere in pericolo.", false, true));
            d.Add(new Dialogo("Thera", "Thera", "Hydris ha ragione, dobbiamo aiutarli!", true, true));
            d.Add(new Dialogo("Acquira", "Acquira", "Con questa nebbia non ci vedranno arrivare, possiamo prenderli di sorpresa.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Andiamo a salvarli!", true, true));


        }
        else if (mapNum == 4 && mapCompleted == true)
        {
            GameObject pg = Instantiate(Resources.Load<GameObject>("Characters/Nova"), new Vector3(0.78f, -0.3f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pg = Instantiate(Resources.Load<GameObject>("Characters/Thera"), new Vector3(1.405f, -0.3f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pg = Instantiate(Resources.Load<GameObject>("Characters/Granius"), new Vector3(-1.262866f, -0.3f, 0), Quaternion.identity);
            pgs.Add(pg);


            pg = Instantiate(Resources.Load<GameObject>("Characters/Sear"), new Vector3(2.177f, -0.3f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Hydris"), new Vector3(3.049407f, -0.3f, 0), Quaternion.identity);
            pg.transform.Rotate(new Vector3(0, 180, 0));
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Acquira"), new Vector3(-3.007282f, -0.3f, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Aeria"), new Vector3(-2.231479f, 0, 0), Quaternion.identity);
            pgs.Add(pg);
            pg = Instantiate(Resources.Load<GameObject>("Characters/Skye"), new Vector3(-0.3962449f, 0.12f, 0), Quaternion.identity);
            pgs.Add(pg);


            d.Add(new Dialogo("Aeria", "Aeria", "Per fortuna siete arrivati in tempo, non ce la saremo cavata senza di voi.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Non preoccupatevi, è stato un piacere.", false, true));
            d.Add(new Dialogo("Sear", "Sear", "Come avete fatto a resistere così a lungo all'inquinamento?", false, true));
            d.Add(new Dialogo("Skye", "Skye", "L'altitudine ci è stata di aiuto ma...", true, true));
            d.Add(new Dialogo("Skye", "Skye", "Ora che ci penso, quando eravamo in difficoltà, il mio bastone ha iniziato a splendere ed a scacciare i mostri.", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Potrebbe essere proprio l'ultima gemma elementare che ci manca, posso vedere?", false, true));
            d.Add(new Dialogo("MOSTRA", "", "Gemma_Aria", true, true));
            d.Add(new Dialogo("Nova", "Nova", "È proprio la gemma dell'aria! Ora che le abbiamo tutte, abbiamo una speranza contro Morgrath!", false, true));
            d.Add(new Dialogo("Aeria", "Aeria", "Non abbiamo tempo per festeggiare, dobbiamo correre subito ad Eirene prima che la situazione peggiori.", true, true));
            d.Add(new Dialogo("Aeria", "Aeria", "Da qui in alto riesco già a vedere del fumo...", true, true));
            d.Add(new Dialogo("Thera", "Thera", "Non porta nulla di buono... Dobbiamo sbrigarci!", false, true));
            

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
            d.Add(new Dialogo("Granius", "Granius", "Morgrath la pagherà! Come ha potuto radere al suolo la nostra capitale?", false, true));
            d.Add(new Dialogo("Skye", "Skye", "Calmatevi tutti! Abbiamo bisogno di un piano per sconfiggere Morgrath.", true, true));
            d.Add(new Dialogo("Aeria", "Aeria", "Skye ha ragione, calmiamoci tutti e sentiamo il suo piano.", false, true));
            d.Add(new Dialogo("Skye", "Skye", "La città ha due entrate principali, una a destra e una a sinistra.", true, true));
            d.Add(new Dialogo("Skye", "Skye", "Conviene sfruttarle tutte e due a nostro vantaggio dividendoci in due gruppi, in modo da ripulire tutti i mostri velocemente.", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Per poi riunirsi al centro e attaccare Morgrath tutti insieme?", false, true));
            d.Add(new Dialogo("Skye", "Skye", "Esatto, mi hai proprio letto nel pensiero.", true, true));
            d.Add(new Dialogo("Acquira", "Acquira", "Mi piace, veloci e dritti all'obbiettivo. Dovrebbe andare.", false, true));
            d.Add(new Dialogo("Nova", "Nova", "Questa è la nostra ultima battaglia, mettiamocela tutta e prepariamoci a qualsiasi imprevisto.", true, true));
        }
        else if (mapNum == 5 && mapCompleted == true)
        {
            music = "Mirrored Engage";

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



            d.Add(new Dialogo("Nova", "Nova", "Ce l'abbiamo fatta!", true, true));
            d.Add(new Dialogo("Nova", "Nova", "Finalmente siamo riusciti a sconfiggerlo, il mondo è salvo dall' inquinamento ora", true, true));
            d.Add(new Dialogo("Sear", "Sear", "Guardate, tutti i mostri inquinanti stanno sparendo.", false, true));
            d.Add(new Dialogo("Hydris", "Hydris", "Eldoria è finalmente salva...", true, true));
            d.Add(new Dialogo("Granius", "Granius", "Adesso l'unica cosa che ci rimane da fare è ricostruire Eirene...", false, true));
            d.Add(new Dialogo("Skye", "Skye", "Se lavoriamo tutti insieme, Eirene tornerà come prima in un battibaleno.", true, true));
            d.Add(new Dialogo("Aeria", "Aeria", "Mettiamoci tutti all' opera allora... Prima iniziamo, prima finiamo!", false, true));


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
                GameObject.Find("Sfondo").transform.localScale = new Vector3(0.7244f, 0.9409f, 0);
                if (dial.texture == "despawn")
                {
                    foreach (GameObject g in pgs) Destroy(g);
                    pgs.Clear();
                }
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
            if(dial.nome == "CAMBIAMUSICA")
            {
                musicPlayer.GetComponent<musicScript>().ChangeMusic(dial.text);
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
            else deez.transform.GetChild(0).GetChild(index).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("playerNameBanner");
            deez.transform.GetChild(0).GetChild(index).GetChild(0).GetComponent<SpriteRenderer>().sprite = dial.sprite;
            deez.transform.GetChild(0).GetChild(index).GetChild(1).GetComponent<TextMeshProUGUI>().text = dial.nome;

            deez.transform.GetChild(0).GetChild(index).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            deez.transform.GetChild(0).GetChild(index).GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

            if (dial.nome == "") deez.transform.GetChild(0).GetChild(0).localScale = Vector3.zero;

            if (dial.latoSX && !sx && dial.nome != "")
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


        if (!(mapNum == 5 && mapCompleted == true)) musicPlayer.GetComponent<musicScript>().Rimuovi();
        GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(1);
        yield return new WaitForSeconds(1f);


        if (mapNum == 5 && mapCompleted == true)
        {
            GameObject crediti = Instantiate(Resources.Load<GameObject>("crediti"));
            for (float i = 0; i <= 100; i++)
            {
                crediti.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 100);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(5);
            musicPlayer.GetComponent<musicScript>().Rimuovi();

            for (float i = 100; i >= 0; i--)
            {
                crediti.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i / 100);
                yield return new WaitForEndOfFrame();
            }
        }

        AsyncOperation async;
        if (mapCompleted ) async = SceneManager.LoadSceneAsync("Menu");
        else async = SceneManager.LoadSceneAsync("MainScene");

        while (!async.isDone)
        {

            yield return new WaitForEndOfFrame();

        }
    }

}
