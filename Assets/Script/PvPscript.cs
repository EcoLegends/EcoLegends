
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class PvPscript : MonoBehaviour
{

    
    GameObject sprite;
    Vector3 oldPos;

    public bool finitoLvlUp = false;

    bool skip = false;


    public void iniziaPvPvero(GameObject e, GameObject p, int[] output, GameObject spritePlayer, GameObject spriteEnemy, GameObject playerParent, GameObject enemyParent, Scene activeScene, GameObject temp, string initial_turn, bool cura)
    {
        StartCoroutine(iniziaPvP(e, p, output, spritePlayer, spriteEnemy, playerParent, enemyParent,activeScene, temp, initial_turn, cura));

    }



    public IEnumerator iniziaPvP(GameObject e, GameObject p, int[] output, GameObject spritePlayer, GameObject spriteEnemy, GameObject playerParent, GameObject enemyParent, Scene activeScene, GameObject temp, string initial_turn, bool cura)
    {

        playerScript player = p.GetComponent<playerScript>();
        GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN + "_pvp");
        GameObject.Find("Davanti").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN + "_pvpDavanti");


        if (cura==false)
        {
            


            int playerAS = output[3];
            int enemyAS = output[7];
            bool move = false;

            List<string> turns = new List<string>();
            

            enemyScript enemy = e.GetComponent<enemyScript>();
            if (!battleManager.dialoghiFatti.Contains(player.nome + "-" + enemy.nome))
            {
                battleManager.dialoghiFatti.Add(player.nome + "-" + enemy.nome);
                string dialogo = player.nome + "-" + enemy.nome;
                
                if (enemy.boss)
                {
                    if(mapScript.mapN == 5 && GameObject.Find("Music").GetComponent<musicScript>().music != enemy.musica) GameObject.Find("Music").GetComponent<musicScript>().ChangeMusic("Goddess in Shadow");
                    else if(GameObject.Find("Music").GetComponent<musicScript>().music != enemy.musica) GameObject.Find("Music").GetComponent<musicScript>().ChangeMusic("Boss Intro");
                    List<Dialogo> d = new List<Dialogo>();

                    Debug.Log(dialogo);

                    switch (dialogo)
                    {

                        //mappa 2
                        case "Nova-Granius?":
                            {
                                d.Add(new Dialogo("Nova", "Nova", "Granius! Cosa diavolo sta succedendo qui?", true, true));
                                d.Add(new Dialogo("Nova", "Nova", "Sono arrivata appena in tempo per vederti assediato da questi mostri!", true, true));
                                d.Add(new Dialogo("Granius?",enemy.textureFile, "Nova... finalmente sei arrivata. Ma non hai idea di cosa sia successo.", false,false));
                                d.Add(new Dialogo("Granius?",enemy.textureFile, "Il nostro impero è stato invaso e distrutto dall'inquinamento che avanza nella nostra terra.", false, false));
                                d.Add(new Dialogo("Granius?", enemy.textureFile, "Morgrath, lui è l'unico che ci può salvare. Non posso più fidarmi di te o di nessun altro.", false, false));
                                d.Add(new Dialogo("Nova", "Nova", "Cosa stai dicendo, Granius? Tu e Morgrath?", true, true));
                                d.Add(new Dialogo("Nova", "Nova", "Sei sempre stato un difensore della natura, un nostro alleato! E ora ti allei col Grande Inquinatore?", true, true));
                                d.Add(new Dialogo("Granius?", enemy.textureFile, "Le cose sono cambiate. Ora devo affrontarti, Nova. Non c'è altra scelta.", false, false));
                                d.Add(new Dialogo("Nova", "Nova", "Non posso crederci... ma non importa cosa sia successo.", true, true));
                                d.Add(new Dialogo("Nova", "Nova", "Granius, io ti salverà e non mi arrenderò senza lottare. Se è così che deve essere, allora che lo sia.", true, true));
                                break;
                            }
                        case "Sear-Granius?":
                            {
                                d.Add(new Dialogo("Sear", "Sear", "Granius! Resisti! Arrivo subito! Lascia che ti aiuti!", true, true));
                                d.Add(new Dialogo("Granius?", enemy.textureFile, "Ah, Sear... finalmente sei arrivato. Peccato che sia troppo tardi per te.", false, false));
                                d.Add(new Dialogo("Sear", "Sear", "Cosa stai dicendo, Granius? Cosa ti è successo?", true, true));
                                d.Add(new Dialogo("Granius?", enemy.textureFile, "Nulla che tu possa capire, Sear. Sono diventato più potente di quanto avrei mai immaginato.", false, false));
                                d.Add(new Dialogo("Granius?", enemy.textureFile, "Morgrath mi ha aperto gli occhi. Ora vedo la vera natura del mondo.", false, false));
                                d.Add(new Dialogo("Sear", "Sear", "Morgrath? Il Grande Inquinatore? No, Granius, non puoi credere a quello che ti ha detto. ", true, true));
                                d.Add(new Dialogo("Sear", "Sear", "È solo un inganno, e tu sei stato trascinato nella sua oscurità.", true, true));
                                d.Add(new Dialogo("Granius?", enemy.textureFile, "Non c'è ritorno per me, Sear. Sono diventato ciò che dovevo essere.", false, false));
                                d.Add(new Dialogo("Granius?", enemy.textureFile, "E ora devo eliminare chiunque cerchi di ostacolarmi.", false, false)); 
                                d.Add(new Dialogo("Sear", "Sear", "Allora sarà una battaglia. Granius, prometto che ti salverò!", true, true));
                                break;
                            }
                        case "Nova-Thera?":
                            {
                                d.Add(new Dialogo("Nova", "Nova", "Thera! Vengo subito! Prendi la mia mano e scappiamo da qui!", true, true));
                                d.Add(new Dialogo("Thera?", enemy.textureFile, "Nova... è troppo tardi. Non posso scappare. E non ho bisogno del tuo aiuto.", false, false));
                                d.Add(new Dialogo("Nova", "Nova", "Cosa intendi, Thera? Cosa sta succedendo qui?", true, true));
                                d.Add(new Dialogo("Thera?", enemy.textureFile, "Sono cambiata, Nova.", false, false));
                                d.Add(new Dialogo("Thera?", enemy.textureFile, "L'inquinamento ha rivelato la vera natura delle cose, e ho visto di non avere nessuna speranza contro di loro.", false, false));
                                d.Add(new Dialogo("Thera?", enemy.textureFile, "Ora sono con Morgrath.", false, false));
                                d.Add(new Dialogo("Nova", "Nova", "No, Thera, non puoi arrenderti così! Cosa ti ha fatto Morgrath?", true, true));
                                d.Add(new Dialogo("Thera?", enemy.textureFile, "Morgrath non mi ha fatto nulla. Mi ha solo aperto gli occhi sulla realtà.", false, false));
                                d.Add(new Dialogo("Thera?", enemy.textureFile, "E ora devo eliminare qualsiasi ostacolo si frapponga sulla nostra strada.", false, false)); 
                                d.Add(new Dialogo("Nova", "Nova", "Allora sono obbligata a fermarti, Thera. Non vi lascerò avere la meglio senza lottare.", true, true));
                                    
                                break;
                            }
                        case "Sear-Thera?":
                            {
                                d.Add(new Dialogo("Sear", "Sear", "Thera! Ti raggiungo subito! Tieniti forte, arrivo!", true, true));
                                d.Add(new Dialogo("Thera?", enemy.textureFile, "Sear... non hai bisogno di preoccuparti per me. La mia situazione è sotto controllo.", false, false));
                                d.Add(new Dialogo("Sear", "Sear", "Cosa stai dicendo, Thera? Cosa sta succedendo qui?", true, true));
                                d.Add(new Dialogo("Thera?", enemy.textureFile, "Non importa. Quello che devi sapere è che sono ora dalla parte di Morgrath.", false, false));
                                d.Add(new Dialogo("Thera?", enemy.textureFile, "E non ci sarà pietà per chiunque cerchi di fermarci.", false, false));
                                d.Add(new Dialogo("Sear", "Sear", "Ma che stai dicendo? Thera, non puoi lasciare che Morgrath ti manipoli in questo modo! Non sei tu!", true, true));
                                d.Add(new Dialogo("Thera?", enemy.textureFile, "È inutile, Sear. Il mio destino è stato scritto, e ora devo seguirlo fino in fondo.", false, false));
                                d.Add(new Dialogo("Sear", "Sear", "Allora sarà una battaglia, Thera. Non posso permetterti di vincere.", true, true));


                                break;
                            }

                        //mappa 3

                        case "Nova-Acquira?":
                            {
                                d.Add(new Dialogo("Nova", "Nova", "Acquira, come hai potuto? Ci fidavamo!", true, true));
                                d.Add(new Dialogo("Acquira?", enemy.textureFile, "Nova... Morgrath offre potere, qualcosa che non possiamo ignorare.", false, false));
                                d.Add(new Dialogo("Nova", "Nova", "Il potere? A che prezzo? Hai tradito tutto ciò in cui credevamo!", true, true));
                                d.Add(new Dialogo("Acquira?", enemy.textureFile, "Non c'è tempo per le tue lamentele, Nova.", false, false));
                                d.Add(new Dialogo("Acquira?", enemy.textureFile, "Morgrath comanda ora, e tu... sei solo un ostacolo da eliminare.", false, false));

                                break;
                            }
                        case "Sear-Acquira?":
                            {
                                d.Add(new Dialogo("Sear", "Sear", "Acquira, come hai potuto? Ti consideravamo un alleato fidato e soprattutto un amico!", true, true));
                                d.Add(new Dialogo("Acquira?", enemy.textureFile, "Le cose cambiano, Sear. Morgrath mi ha offerto potere e opportunità che voi non potevate darmi.", false, false));
                                d.Add(new Dialogo("Sear", "Sear", "Tradire per il potere? Non credevo che saresti caduto così in basso.", true, true));
                                d.Add(new Dialogo("Acquira?", enemy.textureFile, "Il potere è tutto ciò che conta. E ora sarai tu a capirlo quando ti sconfiggerò.", false, false));
                                d.Add(new Dialogo("Sear", "Sear", "Non mi arrenderò mai a te, Acquira.", true, true));
                                d.Add(new Dialogo("Sear", "Sear", "Sei solo una pedina nelle mani di Morgrath, e io non permetterò che distrugga tutto ciò che amiamo.", true, true));
                                d.Add(new Dialogo("Acquira?", enemy.textureFile, "Vedremo quanto ne sarai sicuro dopo il nostro duello...", false, false));

                                break;
                            }
                        case "Granius-Acquira?":
                            {
                                d.Add(new Dialogo("Granius", "Granius", "Acquira... perché? Come hai potuto tradirci così?", true, true));
                                d.Add(new Dialogo("Acquira?", enemy.textureFile, "Granius, non è ciò che sembra. Morgrath... ha promesso potere, dominio... cose che non avrei mai ottenuto con voi.", false, false));
                                d.Add(new Dialogo("Granius", "Granius", "Non capisci? Ti ha ingannato! Sei diventato uno schiavo delle sue menzogne, Acquira.", true, true));
                                d.Add(new Dialogo("Acquira?", enemy.textureFile, "Non c'è tempo per le tue parole, Granius. Se vuoi fermarmi, dovrai farlo con la forza.", false, false));
                                
                                break;
                            }
                        case "Thera-Acquira?":
                            {
                                d.Add(new Dialogo("Thera", "Thera", " Acquira... Non riesco a credere che tu abbia fatto questo.", true, true));
                                d.Add(new Dialogo("Acquira?", enemy.textureFile, "Thera, non c'è tempo per i rimpianti. Ho preso la mia decisione.", false, false));
                                d.Add(new Dialogo("Thera", "Thera", "Una decisione che ci ha traditi tutti! Come hai potuto?", true, true));
                                d.Add(new Dialogo("Acquira?", enemy.textureFile, "Morgrath mi ha aperto gli occhi. Finalmente vedo la verità.", false, false));
                                d.Add(new Dialogo("Thera", "Thera", "La verità? Non c'è verità nel tradimento e nell'oscurità che hai scelto di seguire.", true, true));
                                d.Add(new Dialogo("Acquira?", enemy.textureFile, "Forse un giorno capirai e ti unirai a noi, Thera. Ma ora... è troppo tardi per tornare indietro.", false, false));

                                break;
                            }


                        case "Nova-Hydris?":
                            {
                                d.Add(new Dialogo("Nova", "Nova", "Hydris, come hai potuto tradirci così? Eravamo una squadra, una famiglia.", true, true));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "Le cose sono cambiate, Nova. Morgrath mi ha aperto gli occhi, mostrandomi il vero potere.", false, false));
                                d.Add(new Dialogo("Nova", "Nova", "Ma a che prezzo? Hai abbandonato tutto ciò in cui credevi per seguirlo.", true, true));
                                d.Add(new Dialogo("Nova", "Nova", "Come hai potuto farti ingannare così?", true, true));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "Non sei in grado di capire, Nova. Il potere di Morgrath è irresistibile.", false, false));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "È solo questione di tempo prima che tu anche tu ti unisca a noi.", false, false));
                                d.Add(new Dialogo("Nova", "Nova", "Non accadrà mai, continuerò a lottare contro l'inquinamento.", true, true));
                                d.Add(new Dialogo("Nova", "Nova", "E se devo fermarti, lo farò per riportarti alla ragione, anche se dovrò combattere contro di te.", true, true));


                                break;
                            }
                        case "Sear-Hydris?":
                            {
                                d.Add(new Dialogo("Sear", "Sear", "Hydris, come hai potuto? Noi ci fidavamo di te!", true, true));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "Sear, le cose non sono sempre come sembrano.", false, false));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "Morgrath mi ha offerto potere, un potere che non potevo ignorare.", false, false));
                                d.Add(new Dialogo("Sear", "Sear", "Il potere? Preferisci abbandonare tutto per il potere?", true, true));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "Non c'è tempo per spiegazioni. Ora sono dalla parte di Morgrath, e tu sei il mio avversario.", false, false));
                                d.Add(new Dialogo("Sear", "Sear", "Non posso credere che tu sia arrivata a questo ma non mi arrenderò facilmente.", true, true));


                                break;
                            }
                        case "Granius-Hydris?":
                            {
                                d.Add(new Dialogo("Granius", "Granius", "Hydris, come hai potuto tradirci così? Eravamo una squadra!", true, true));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "Granius, le cose sono cambiate. Morgrath offre potere e dominio, qualcosa che tu non sembri capire.", false, false));
                                d.Add(new Dialogo("Granius", "Granius", "Il potere non giustifica il tradimento degli amici! Sei diventata una pedina nelle sue mani oscure.", true, true));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "Forse tu non hai ancora capito, Granius. Morgrath è l'unica via verso la vittoria.", false, false));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "E tu sei solo un ostacolo sul nostro cammino.", false, false));
                                d.Add(new Dialogo("Granius", "Granius", "Non lascerò che Morgrath ti usi così. Sei sempre stata più forte di questo.", true, true));
                                d.Add(new Dialogo("Granius", "Granius", "Ma se è una battaglia che vuoi, la otterrai.", true, true));


                                break;
                            }
                        case "Thera-Hydris?":
                            {
                                d.Add(new Dialogo("Thera", "Thera", "Hydris... come hai potuto? Mi fidavo di te... Eravamo amiche...", true, true));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "Thera, capirai presto che non avevo altra scelta.", false, false));
                                d.Add(new Dialogo("Thera", "Thera", "Non ci credo! Eravamo una squadra, una famiglia!", true, true));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "Le cose cambiano, Thera. E io ho scelto il lato che garantirà la mia sopravvivenza.", false, false));
                                d.Add(new Dialogo("Thera", "Thera", "La tua sopravvivenza? Non hai idea di cosa stai facendo!", true, true));
                                d.Add(new Dialogo("Thera", "Thera", "Ti stai arrendendo alla corruzione di Morgrath!", true, true));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "Forse è meglio di stare dalla tua parte, Thera.", false, false));
                                d.Add(new Dialogo("Hydris?", enemy.textureFile, "Morgrath mi offre potere e controllo, cosa che voi non potreste mai darmi.", false, false));
                                d.Add(new Dialogo("Thera", "Thera", "Preferisci il potere alla nostra amicizia?", true, true));
                                d.Add(new Dialogo("Thera", "Thera", "Allora preparati, perché questa non sarà una battaglia facile per te, Hydris.", true, true));

                                break;
                            }

                        //mappa 5
                        case "Nova-Morgrath":
                            {
                                d.Add(new Dialogo("Nova", "Nova", "È arrivata la tua fine Morgrath!", true, true));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Pensi davvero di potermi fermare? Sei proprio patetica.", false, false));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Ho impiegato gli ultimi cento anni a rompere il sigillo magico che mi imprigionava.", false, false));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "E adesso finalmente sono libero e posso vendicarmi del vostro lurido mondo!", false, false));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Non hai speranze contro il mio potere.", false, false));
                                d.Add(new Dialogo("Nova", "Nova", "Questo lo vedremo. Non ti lascerò mai distruggere Eldoria!", true, true));

                                break;
                            }
                        case "Sear-Morgrath":
                            {
                                d.Add(new Dialogo("Sear", "Sear", "Come hai potuto distruggere Eirene!", true, true));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Questo è solo l'inizio del mio regno... Presto tutta Eldoria verrà rasa al suolo!", false, false));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "E voi non avete nessun potere per fermarmi!", false, false));
                                d.Add(new Dialogo("Sear", "Sear", "Te la farò pagare Morgrath, non ti permetterò di fare ulteriori danni.", true, true));
                                break;
                            }
                        case "Granius-Morgrath":
                            {
                                d.Add(new Dialogo("Granius", "Granius", "Come hai osato usarci come burattini! Te la farò pagare.", true, true));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Guarda chi si rivede... Distruggere il vostro impero è stato un piacere.", false, false));
                                d.Add(new Dialogo("Granius", "Granius", "Rivendicherò la mia terra... Stanne sicuro Morgrath!", true, true));

                                break;
                            }
                        case "Thera-Morgrath":
                            {
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Guarda un po' chi si rivede... Sei migliorata molto dal nostro ultimo incontro.", false, false));
                                d.Add(new Dialogo("Thera", "Thera", "Questa volta riuscirai a prenderci di sopresa, Morgath.", true, true));
                                d.Add(new Dialogo("Thera", "Thera", "Ti pentirai di aver inquinato il nostro impero e di averci usato come burattini.", true, true));

                                break;
                            }
                        case "Acquira-Morgrath":
                            {

                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Ah... Sei tu... Uno dei miei più grandi fallimenti...", false, false));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Come hai osato far fallire un piano così perfetto!", false, false));
                                d.Add(new Dialogo("Acquira", "Acquira", "Come hai osato te controllarci ed a farci tradire i nostri amici!", true, true));
                                d.Add(new Dialogo("Acquira", "Acquira", "Te la farò pagare Morgrath, nessuno mi può mettere contro i miei amici!", true, true));


                                break;
                            }
                        case "Hydris-Morgrath":
                            {
                                d.Add(new Dialogo("Hydris", "Hydris", "Come hai osato farmi combattere contro i miei amici!", true, true));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "È la parte più divertente, cara Hydris.", false, false));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Seminare caos e inquinamento ovunque è la mia specialità.", false, false));
                                d.Add(new Dialogo("Hydris", "Hydris", "Io ti fermerò Morgrath, non ti permetterò di distruggere più nulla.", true, true));

                                break;
                            }
                        case "Aeria-Morgrath":
                            {

                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Siete stati bravi a contenere il mio inquinamento, ma i vostri sforzi non basteranno.", false, false));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Quando vi renderò miei schiavi non ci sarà più nessuno a proteggere la vostra gente!", false, false));
                                d.Add(new Dialogo("Aeria", "Aeria", "Non succederà mai. Io ti fermerò.", true, true));
                                


                                break;
                            }
                        case "Skye-Morgrath":
                            {
                                d.Add(new Dialogo("Skye", "Skye", "Bruciare un'intera citta? Ma sei forse impazzito?", true, true));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Questo è solo l'inizio...", false, false));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "Appena avrò finito con voi pesti, il mondo finirà sotto il mio controllo.", false, false));
                                d.Add(new Dialogo("Morgrath", enemy.textureFile, "E non rimarrà neanche una briciola dei vostri regni.", false, false));
                                d.Add(new Dialogo("Skye", "Skye", "Questo lo vedremo, riusciremo a sconfiggerti prima noi. Stanne certo.", true, true));
                                break;
                            }

                    }


                    GameObject deez = (GameObject)Instantiate(Resources.Load("Dialogo", typeof(GameObject)), new Vector3(0, 0, 0), Quaternion.identity);

                    bool dx=false, sx=false;
                    deez.transform.GetChild(0).GetChild(0).localScale = Vector3.zero;
                    deez.transform.GetChild(0).GetChild(1).localScale = Vector3.zero;
                    deez.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

                        
                        

                    yield return new WaitForSeconds(1);
                    for (float i=0; i <= 17; i++)
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
                        if(!dial.player) deez.transform.GetChild(0).GetChild(index).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("enemyNameBanner");
                        deez.transform.GetChild(0).GetChild(index).GetChild(0).GetComponent<SpriteRenderer>().sprite = dial.sprite;
                        deez.transform.GetChild(0).GetChild(index).GetChild(1).GetComponent<TextMeshProUGUI>().text = dial.nome;

                        deez.transform.GetChild(0).GetChild(index).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                        deez.transform.GetChild(0).GetChild(index).GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

                        if (dial.latoSX && !sx)
                        {
                            sx = true;
                            for(float i = 0; i <= 20; i++)
                            {
                                deez.transform.GetChild(0).GetChild(0).localScale = new Vector3(9.259258f* (i/20), 5.185184f * (i/20), 9.259258f * (i/20));
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
                        for (int i = 0; i < 30; i++)
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


                    if (enemy.textureFile == "Granius" || enemy.textureFile == "Thera")
                    {
                        enemy.textureFile = enemy.textureFile + "_evil";
                        Destroy(enemy.gameObject.transform.GetChild(0).gameObject);
                        var texture = Resources.Load<GameObject>("Characters/" + enemy.textureFile);   //carica la texture del personaggio
                        GameObject sprite = Instantiate(texture, new Vector3(0, 0, 0), Quaternion.identity);
                        sprite.transform.parent = enemy.gameObject.transform;
                        sprite.transform.localPosition = new Vector3(0, 0, 0);
                        sprite.transform.SetAsFirstSibling();
                        sprite.transform.Rotate(0, 180, 0);

                        Destroy(spriteEnemy);

                        spriteEnemy = Instantiate(Resources.Load<GameObject>("Characters/" + enemy.textureFile), Vector3.zero, Quaternion.identity);


                        spriteEnemy.transform.parent = enemyParent.transform;
                        spriteEnemy.transform.localEulerAngles = new Vector3(0,0,0);
                        spriteEnemy.transform.localPosition = new Vector3(0, 0, 0);
                        enemyParent.transform.position = new Vector3(2, 0, 0);
                            
                    }

                }
                
            }

            int distance = (int)Mathf.Abs(player.x - enemy.x) + (int)Mathf.Abs(player.y - enemy.y);

            if(temp.transform.GetChild(4).GetComponent<mapScript>().mapTiles[player.x, player.y].GetComponent<tileScript>().avoBonus > 0 && player.nome=="Granius")
            {
                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                skill.GetComponent<skillGUIScript>().Setup("Istinto Selvaggio");
            }

            if (player.haFattoQualcosa==false  && player.nome == "Skye")
            {
                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                skill.GetComponent<skillGUIScript>().Setup("In Guardia");
            }

            if (initial_turn=="enemy" && player.nome == "Sear" && (float)player.hp/ (float)player.maxHp <= 0.25f)
            {
                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                skill.GetComponent<skillGUIScript>().Setup("Vantaggio");
                initial_turn = "player";
            }
            turns.Add(initial_turn);

            if (player.nome == "Aeria" && player.weaponMaxRange==3)
            {
                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                skill.GetComponent<skillGUIScript>().Setup("Dio del Vento");
            }
            
            

            float playerInitialHP = player.hp / (float)player.maxHp*100;

            if (initial_turn=="player"){
                if (enemy.weaponMinRange <= distance && distance <= enemy.weaponMaxRange)
            {
                turns.Add("enemy");
                if (enemyAS >= playerAS + 4) { turns.Add("enemy"); }
            }
            if (playerAS >= enemyAS + 4) { turns.Add("player"); }
            }else{
                if (player.weaponMinRange <= distance && distance <= player.weaponMaxRange)
            {
                turns.Add("player");
                if (playerAS >= enemyAS + 4) { turns.Add("player"); }
            }
            if (enemyAS >= playerAS + 4) { turns.Add("enemy"); }
            }

            if (enemy.boss) GameObject.Find("Music").GetComponent<musicScript>().ChangeMusic(enemy.musica);

            bool playerCanCounter = turns.Contains("player");
            bool enemyCanCounter = turns.Contains("enemy");

            GameObject pvpForecast = Instantiate(Resources.Load<GameObject>("pvpForecastCombat"),GameObject.Find("CombatCamera").transform);
            
            if (playerAS >= enemyAS + 4) pvpForecast.GetComponent<pvpForecastScript>().SetupPlayer(player.hp,player.maxHp,output[0],output[1],output[2],true, playerCanCounter);
            else pvpForecast.GetComponent<pvpForecastScript>().SetupPlayer(player.hp, player.maxHp, output[0], output[1], output[2], false, playerCanCounter);
            if (enemyAS >= playerAS + 4) pvpForecast.GetComponent<pvpForecastScript>().SetupEnemy(enemy.hp, enemy.maxHp, output[4], output[5], output[6], true, enemyCanCounter);
            else pvpForecast.GetComponent<pvpForecastScript>().SetupEnemy(enemy.hp, enemy.maxHp, output[4], output[5], output[6], false, enemyCanCounter);


            yield return new WaitForSeconds(1);
            UnityEngine.Object mov_tile_prefab = Resources.Load("movTileEnemyAllPrefab", typeof(GameObject));
            GameObject mov_tile = (GameObject)Instantiate(mov_tile_prefab, new Vector3(88, 88, -2), Quaternion.identity);
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("tileViola")) Destroy(g);

            int damageDealt = 0;

            yield return new WaitForSeconds(1);


            foreach(string turn in turns){


                if (player.hp <= 0||enemy.hp<=0) break;

                if(turn=="player"){
                    int playerHit = UnityEngine.Random.Range(1, 100); //player hit
                    if (playerHit <= output[1])
                    {

                        int playerCrit = UnityEngine.Random.Range(1, 100);  //se critta

                        Debug.Log("inizio");

                        sprite = GameObject.Find("Info Canvas").transform.GetChild(1).gameObject;
                        GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.localScale = Vector3.zero;
                        sprite.transform.localScale = Vector3.zero;
                        GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.SetActive(true);
                        
                        
                        if(move == false && player.weaponMaxRange==1){

                            for (float i = 0; i < 20; i++)
                            {
                            playerParent.transform.position += new Vector3(0.035f*5, 0, 0);
                            yield return new WaitForEndOfFrame();

                            }

                            
                            GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position.z);
                            move = true;
                        }
                        int dmg;
                    
                        if (playerCrit <= output[2])
                        {
                            GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("crit");
                            if (move) spritePlayer.GetComponent<Animator>().Play("Crit");
                            else spritePlayer.GetComponent<Animator>().Play("Crit2");
                            GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=(output[0]*3).ToString();
                            enemy.hp = Mathf.Clamp(enemy.hp -= output[0] * 3, 0, enemy.maxHp);
                            GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.SetActive(true);
                            damageDealt += output[0] * 3;
                            dmg = output[0] * 3;
                        }
                        else
                        {
                            if (move) spritePlayer.GetComponent<Animator>().Play("Attack");
                            else spritePlayer.GetComponent<Animator>().Play("Attack2");
                            GameObject.Find("Info Canvas").transform.GetChild(1).GetComponent<TextMeshProUGUI>().text=output[0].ToString();
                            enemy.hp = Mathf.Clamp(enemy.hp -= output[0], 0, enemy.maxHp);
                            damageDealt += output[0];
                            dmg = output[0];
                        }
                        
                        if(enemy.hp <= 0 && player.nome=="Thera" && UnityEngine.Random.Range(1, 100) < player.dex)
                        {
                            GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                            skill.GetComponent<skillGUIScript>().Setup("Giardino Fiorito");
                            player.hp = Mathf.Clamp(player.hp += dmg / 2, 0, player.maxHp);
                            
                            
                            GameObject.Find("Info Canvas").transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = (dmg/2).ToString();
                            GameObject.Find("Info Canvas").transform.GetChild(6).gameObject.SetActive(true);
                            GameObject.Find("Info Canvas").transform.GetChild(6).position = new Vector3(playerParent.transform.position.x -0.3f, GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.y, GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(6).localScale = Vector3.zero;
                        }
                        

                        yield return new WaitForEndOfFrame();
                        

                        AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                        AnimationClip firstClip = clipInfos[0].clip;
                        float duration = firstClip.length + Time.time;
                        Debug.Log(firstClip.length);

                        

                        while (Time.time < duration)
                        {
                            yield return new WaitForEndOfFrame();
                        }

                        oldPos = sprite.transform.position;

                        GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("dmg");
                        spriteEnemy.GetComponent<Animator>().Play("Hurt");
                        enemy.healthbar.SetHealth(enemy.hp);
                        pvpForecast.GetComponent<pvpForecastScript>().DamageEnemy(enemy.hp, enemy.maxHp);
                        pvpForecast.GetComponent<pvpForecastScript>().DamagePlayer(player.hp, player.maxHp);
                        yield return new WaitForEndOfFrame();
                        clipInfos = spriteEnemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                        firstClip = clipInfos[0].clip;
                        duration = firstClip.length + Time.time;
                        if(GameObject.Find("Info Canvas").transform.GetChild(6).gameObject.activeInHierarchy) GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("heal");

                        for (float i = 0; i < 20; i++)
                        {
                            sprite.transform.localScale = new Vector3(Mathf.Clamp(i/2,0,1), Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1));
                            GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.localScale = new Vector3(Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1));
                            GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position += new Vector3(0, 0.01f*5, 0);

                            GameObject.Find("Info Canvas").transform.GetChild(6).position += new Vector3(0, 0.01f*5, 0);
                            GameObject.Find("Info Canvas").transform.GetChild(6).localScale = new Vector3(Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1));

                            sprite.transform.position += new Vector3(0, 0.01f * 5, 0);
                            yield return new WaitForEndOfFrame();

                        }

                        
                        
                        Debug.Log(firstClip.length);
                        while (Time.time < duration)
                        {
                            yield return new WaitForEndOfFrame();
                        }
                        yield return new WaitForSeconds(1);
                        sprite.SetActive(false);
                        sprite.transform.position = oldPos;
                        GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.SetActive(false);
                        GameObject.Find("Info Canvas").transform.GetChild(6).gameObject.SetActive(false);
                        GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position -= new Vector3(0, 1, 0);
                        Debug.Log("fine");
                        player.healthbar.SetHealth(player.hp);

                    }
                    else         //player miss
                    {
                        Debug.Log("inizio");

                        if(move == false && player.weaponMaxRange==1){

                            for (float i = 0; i < 20; i++)
                            {
                            playerParent.transform.position += new Vector3(0.035f*5, 0, 0);
                            yield return new WaitForEndOfFrame();

                            }

                            GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position = new Vector3(playerParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.transform.position.z);

                            move = true;
                        }

                        if (move) spritePlayer.GetComponent<Animator>().Play("Attack");
                        else spritePlayer.GetComponent<Animator>().Play("Attack2");

                        sprite = GameObject.Find("Info Canvas").transform.GetChild(0).gameObject;
                        sprite.transform.localScale = Vector3.zero;
                        GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.SetActive(true);

                        yield return new WaitForEndOfFrame();
                        AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                        AnimationClip firstClip = clipInfos[0].clip;
                        float duration = firstClip.length + Time.time;
                        float half = firstClip.length / 2 + Time.time;
                        bool enemyMissStarted = false;
                        Debug.Log(firstClip.length);



                        

                        while (Time.time < duration)
                        {
                            if(Time.time > half && !enemyMissStarted)
                            {
                                enemyMissStarted = true;
                                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("miss");
                                spriteEnemy.GetComponent<Animator>().Play("Miss");
                            }
                            yield return new WaitForEndOfFrame();
                        }

                        oldPos = sprite.transform.position;


                        for (float i = 0; i < 20; i++)
                        {
                            sprite.transform.localScale = new Vector3(Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1));
                            sprite.transform.position += new Vector3(0, 0.01f * 5, 0);
                            yield return new WaitForEndOfFrame();

                        }
                        yield return new WaitForSeconds(1);
                        sprite.SetActive(false);
                        sprite.transform.position = oldPos;
                        Debug.Log("fine");



                    }
                }
                else{
                    int enemyHit = UnityEngine.Random.Range(1, 100);

                    if (enemyHit <= output[5])             //enemy hit
                    {

                        int enemyCrit = UnityEngine.Random.Range(1, 100);  //se critta
                        Debug.Log("inizio");

                        GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.localScale = Vector3.zero;
                        sprite = GameObject.Find("Info Canvas").transform.GetChild(2).gameObject;
                        sprite.transform.localScale = Vector3.zero;
                        GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.SetActive(true);    

                        if(move == false && enemy.weaponMaxRange==1){

                            for (float i = 0; i < 20; i++)
                            {
                            enemyParent.transform.position -= new Vector3(0.035f * 5, 0, 0);
                            GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position.z);
                            yield return new WaitForEndOfFrame();

                            }

                            move = true;
                        }

                        int hpPrec = player.hp;
                        int dmgPrec = output[4];
                        if (player.nome == "Acquira" && UnityEngine.Random.Range(1, 100) < player.dex)
                        {
                            GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                            skill.GetComponent<skillGUIScript>().Setup("Acqua Cristallina");
                            output[4] = output[4] / 2;
                        }
                        if (enemyCrit <= output[6]){
                            GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("crit");
                            if (move) spriteEnemy.GetComponent<Animator>().Play("Crit");
                            else spriteEnemy.GetComponent<Animator>().Play("Crit2");
                            GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=(output[4]*3).ToString();
                            player.hp = Mathf.Clamp(player.hp -= output[4] * 3, 0, player.maxHp);
                            if(player.hp==0 && player.nome=="Nova" && playerInitialHP >= 30)
                            {
                                GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (hpPrec - 1).ToString();
                                player.hp = 1;
                                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                                skill.GetComponent<skillGUIScript>().Setup("Persistenza");
                            }
                            GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.SetActive(true);
                        }
                        else{
                            if (move) spriteEnemy.GetComponent<Animator>().Play("Attack");
                            else spriteEnemy.GetComponent<Animator>().Play("Attack2");
                            GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text=output[4].ToString();
                            player.hp = Mathf.Clamp(player.hp -= output[4], 0, player.maxHp);
                            if (player.hp == 0 && player.nome == "Nova" && playerInitialHP >= 30)
                            {
                                GameObject.Find("Info Canvas").transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = (hpPrec - 1).ToString();
                                player.hp = 1;
                                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                                skill.GetComponent<skillGUIScript>().Setup("Persistenza");
                            }
                        }
                        output[4] = dmgPrec;


                        yield return new WaitForEndOfFrame();
                        
                        AnimatorClipInfo[] clipInfos = spriteEnemy.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                        AnimationClip firstClip = clipInfos[0].clip;
                        float duration = firstClip.length + Time.time;
                        Debug.Log(firstClip.length);

                        GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.localScale = Vector3.zero;
                        sprite.transform.localScale = Vector3.zero;

                        while (Time.time < duration)
                        {
                            yield return new WaitForEndOfFrame();
                        }

                        oldPos = sprite.transform.position;

                        GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("dmg");
                        spritePlayer.GetComponent<Animator>().Play("Hurt");
                        player.healthbar.SetHealth(player.hp);
                        pvpForecast.GetComponent<pvpForecastScript>().DamagePlayer(player.hp, player.maxHp);
                        yield return new WaitForEndOfFrame();
                        clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                        firstClip = clipInfos[0].clip;
                        duration = firstClip.length + Time.time;
                        for (float i = 0; i < 20; i++)
                        {
                            sprite.transform.localScale = new Vector3(Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1));
                            GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.localScale = new Vector3(Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1));
                            GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position += new Vector3(0, 0.01f*5, 0);
                            sprite.transform.position += new Vector3(0, 0.01f * 5, 0);
                            yield return new WaitForEndOfFrame();

                        }

                        


                        
                        Debug.Log(firstClip.length);
                        while (Time.time < duration)
                        {
                            yield return new WaitForEndOfFrame();
                        }
                        yield return new WaitForSeconds(1);
                        sprite.SetActive(false);
                        sprite.transform.position = oldPos;
                        GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.SetActive(false);
                        GameObject.Find("Info Canvas").transform.GetChild(5).gameObject.transform.position -= new Vector3(0, 1, 0);
                        Debug.Log("fine");
                        
                    }
                    else               //enemy miss
                    {
                        Debug.Log("inizio");

                        if(move == false && enemy.weaponMaxRange==1){

                            for (float i = 0; i < 20; i++)
                            {
                            enemyParent.transform.position -= new Vector3(0.035f*5, 0, 0);
                            yield return new WaitForEndOfFrame();

                            }
                            GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(1).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(4).gameObject.transform.position.z);
                            GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position = new Vector3(enemyParent.transform.position.x,GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position.y,GameObject.Find("Info Canvas").transform.GetChild(0).gameObject.transform.position.z);
                            move = true;
                        } 
                        
                        if(move) spriteEnemy.GetComponent<Animator>().Play("Attack");
                        else spriteEnemy.GetComponent<Animator>().Play("Attack2");

                        sprite = GameObject.Find("Info Canvas").transform.GetChild(3).gameObject;
                        sprite.transform.localScale = Vector3.zero;
                        GameObject.Find("Info Canvas").transform.GetChild(3).gameObject.SetActive(true);
                        
                        yield return new WaitForEndOfFrame();
                        AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
                        AnimationClip firstClip = clipInfos[0].clip;
                        float duration = firstClip.length + Time.time;
                        float half = firstClip.length / 2 + Time.time;
                        bool enemyMissStarted = false;
                        Debug.Log(firstClip.length);



                        

                        while (Time.time < duration)
                        {
                            if (Time.time > half && !enemyMissStarted)
                            {
                                enemyMissStarted = true;
                                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("miss");
                                spritePlayer.GetComponent<Animator>().Play("Miss");
                            }
                            yield return new WaitForEndOfFrame();
                        }

                        oldPos = sprite.transform.position;


                        for (float i = 0; i < 20; i++)
                        {
                            sprite.transform.localScale = new Vector3(Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1));
                            sprite.transform.position += new Vector3(0, 0.01f * 5, 0);
                            yield return new WaitForEndOfFrame();

                        }
                        
                        yield return new WaitForSeconds(1);
                        sprite.SetActive(false);
                        sprite.transform.position = oldPos;
                        Debug.Log("fine");
                    }

                }
            

            }

            

            if (!battleManager.canMoveEnemy) battleManager.canMoveEnemy = true;
            if (enemy.hp == 0)
            {

                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("defeat");
                GameObject particle = Instantiate(Resources.Load<GameObject>("Petals Prefab 1"));
                particle.transform.position = new Vector3(enemyParent.transform.position.x-0.5f, enemyParent.transform.position.y+0.5f, -3.5f);

                particle.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

                if (enemy.textureFile.Contains("evil"))
                {
                    enemy.textureFile = enemy.textureFile.Replace("_evil", "");
                    Destroy(enemy.gameObject.transform.GetChild(0).gameObject);
                    var texture = Resources.Load<GameObject>("Characters/" + enemy.textureFile);   //carica la texture del personaggio
                    GameObject sprite = Instantiate(texture, new Vector3(0, 0, 0), Quaternion.identity);
                    sprite.transform.parent = enemy.gameObject.transform;
                    sprite.transform.localPosition = new Vector3(0, 0, 0);
                    sprite.transform.SetAsFirstSibling();
                    sprite.transform.Rotate(0, 180, 0);

                    Destroy(spriteEnemy);

                    spriteEnemy = Instantiate(Resources.Load<GameObject>("Characters/" + enemy.textureFile), Vector3.zero, Quaternion.identity);


                    spriteEnemy.transform.parent = enemyParent.transform;
                    spriteEnemy.transform.localEulerAngles = new Vector3(0, 0, 0);
                    spriteEnemy.transform.localPosition = new Vector3(0, 0, 0);
                    for (float i = 0; i <= 20; i++)
                    {
                        yield return new WaitForEndOfFrame();
                    }


                }
                else
                {
                    for (float i = 0; i <= 20; i++)
                    {

                        enemyParent.transform.localScale = new Vector3(1 - i / 20, 1 - i / 20, 1 - i / 20);
                        yield return new WaitForEndOfFrame();
                    }
                }


            }
            if (player.hp >= 0 && player.nome == "Hydris")
            {
                GameObject skill = Instantiate(Resources.Load<GameObject>("Skill"));
                skill.GetComponent<skillGUIScript>().Setup("Guarigione");
                player.hp = Mathf.Clamp(player.hp += 5 / 2, 0, player.maxHp);
                player.healthbar.SetHealth(player.hp);
                pvpForecast.GetComponent<pvpForecastScript>().DamagePlayer(player.hp, player.maxHp);
                
                GameObject.Find("Info Canvas").transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = (5).ToString();
                GameObject.Find("Info Canvas").transform.GetChild(6).gameObject.SetActive(true);
                GameObject.Find("Info Canvas").transform.GetChild(6).position = new Vector3(playerParent.transform.position.x - 0.3f, GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.y, GameObject.Find("Info Canvas").transform.GetChild(2).gameObject.transform.position.z);
                GameObject.Find("Info Canvas").transform.GetChild(6).localScale = Vector3.zero;
                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("heal");
                for (float i = 0; i < 20; i++)
                {

                    GameObject.Find("Info Canvas").transform.GetChild(6).position += new Vector3(0, 0.01f*5, 0);
                    GameObject.Find("Info Canvas").transform.GetChild(6).localScale = new Vector3(Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1));

                    yield return new WaitForEndOfFrame();

                }


                yield return new WaitForSeconds(1);

                GameObject.Find("Info Canvas").transform.GetChild(6).gameObject.SetActive(false);
            }

            pvpForecast.GetComponent<pvpForecastScript>().Rimuovi();
            yield return new WaitForSeconds(0.5f);

            int expGained = Mathf.Clamp(damageDealt, 0, 20);
            if (player.hp == 0) expGained = 0;
            if (expGained != 0)
            {


                int expNeeded = (int)Mathf.Round(100 * Mathf.Pow(1.1f, player.lvl - 2));
                if (enemy.hp == 0)
                {
                    expGained = Mathf.Clamp(30 + (enemy.lvl - 1), 0, 100);
                    if (enemy.boss) expGained += 20;
                }


                GameObject expGUI = (GameObject)Instantiate(Resources.Load("ExpCanvas", typeof(GameObject)), Camera.main.transform.position, Quaternion.identity);
                expGUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player.nome;
                expGUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (expNeeded - player.exp).ToString();
                expGUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "+" + (expGained).ToString();
                expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetMaxHealth(expNeeded);
                expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetHealth(player.exp);

                float y = expGUI.transform.position.y * -1;
                for (int i = 0; i < 20; i++)
                {
                    y += 0.01516f *5;
                    expGUI.transform.position = new Vector3(expGUI.transform.position.x, y, -1);
                    yield return new WaitForEndOfFrame();
                }

                yield return new WaitForSeconds(0.5f);
                bool lvlup = false;
                AudioClip expSfx = (AudioClip)Resources.Load("Sounds/SFX/Exp");
                
                for (int i = 0; i < expGained; i++)
                {
                    yield return new WaitForSeconds(1/(float)expGained);
                    player.exp++;
                    if(i%2==0 || expGained < 15) expGUI.transform.GetChild(7).GetComponent<AudioSource>().PlayOneShot(expSfx);
                    if (player.exp >= expNeeded)
                    {
                        player.exp = 0;
                        expNeeded = (int)Mathf.Round(100 * Mathf.Pow(1.1f, player.lvl - 1));
                        expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetMaxHealth(expNeeded);
                        expGUI.transform.GetChild(6).GetChild(1).GetComponent<UnityEngine.UI.Image>().color = new Color(0, 186, 50);
                        lvlup = true;
                    }
                    expGUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (expNeeded - (player.exp)).ToString();
                    expGUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "+" + (expGained - i).ToString();

                    expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetHealth(player.exp);

                }
                expGUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "0";

                yield return new WaitForSeconds(2);

                for (int i = 0; i < 20; i++)
                {
                    y -= 0.01516f * 5;
                    expGUI.transform.position = new Vector3(expGUI.transform.position.x, y, expGUI.transform.position.z);
                    yield return new WaitForEndOfFrame();
                }

                if (lvlup)
                {
                    finitoLvlUp = false;
                    LevelUp(player);
                    while (!finitoLvlUp) yield return new WaitForEndOfFrame();
                }

                List<string> nomi = new List<string> { "Nova", "Sear", "Granius", "Thera", "Acquira", "Hydris", "Aeria", "Skye" };
                try
                {
                    string[] arrLine = System.IO.File.ReadAllLines(Application.streamingAssetsPath+"/dati.txt");
                    arrLine[nomi.IndexOf(player.nome)] = player.nome + "," + player.textureFile + "," + player.lvl + "," + player.exp + "," + player.movement + "," + player.weaponMinRange + "," + player.weaponMaxRange + "," + player.weaponWt + "," + player.weaponMt + "," + player.weaponHit + "," + player.weaponCrit + "," + player.unitType + "," + player.weaponType + "," + player.weaponIsMagic + "," + player.maxHp + "," + player.str + "," + player.mag + "," + player.dex + "," + player.spd + "," + player.lck + "," + player.def + "," + player.res + "," + player.hpGrowth + "," + player.strGrowth + "," + player.magGrowth + "," + player.dexGrowth + "," + player.spdGrowth + "," + player.lckGrowth + "," + player.defGrowth + "," + player.resGrowth + "," + player.heal;
                    System.IO.File.WriteAllLines(Application.streamingAssetsPath+"/dati.txt", arrLine);
                }
                catch (System.Exception) { }
                
            }

            if (enemy.hp == 0)
            {
                List<Dialogo> d = new List<Dialogo>();
                //dialogo sconfitta nemico

                if (enemy.textureFile == "Granius")
                {
                    d.Add(new Dialogo(enemy.textureFile, enemy.textureFile, "Cosa è successo?", false, true));
                    d.Add(new Dialogo(enemy.textureFile, enemy.textureFile, "Nova? Sear? Che ci fate voi qui? E cosa sono questi mostri!?", false, true));
                    d.Add(new Dialogo(player.textureFile, player.textureFile, "Eri sotto il controllo di Morgrath, ora corri al riparo finchè non sconfiggiamo tutti i mostri!", true, true));
                    d.Add(new Dialogo(enemy.textureFile, enemy.textureFile, "Mi avete salvato? Grazie... Spero di non aver causato problemi...", false, true));
                }
                if (enemy.textureFile == "Thera")
                {
                    d.Add(new Dialogo(enemy.textureFile, enemy.textureFile, player.textureFile+"? Cosa sta succedendo?", false, true));
                    d.Add(new Dialogo(player.textureFile, player.textureFile, "Morgrath ti stava manipolando, per fortuna siamo riusciti a salvarti...", true, true));
                    d.Add(new Dialogo(enemy.textureFile, enemy.textureFile, "Cosa? Ma è terribile... Grazie... Spero di non avervi fatto del male...", false, true));

                }
                if (enemy.textureFile == "Acquira")
                {
                    d.Add(new Dialogo(enemy.textureFile, enemy.textureFile, "Ma che... "+player.textureFile + "? Perché stiamo combattendo?", false, true));
                    d.Add(new Dialogo(player.textureFile, player.textureFile, "Stai fingendo di nuovo? Ci hai tradito per unirti a Morgrath!", true, true));
                    d.Add(new Dialogo(enemy.textureFile, enemy.textureFile, "Io e Morgrath? Ma di che stai parlando?", false, true));
                    d.Add(new Dialogo(player.textureFile, player.textureFile, "Ci hai fatto imboscare dai suoi mostri! Non ti ricordi più niente?", true, true));
                    d.Add(new Dialogo(enemy.textureFile, enemy.textureFile, player.textureFile+", io non mi unirei mai con Morgrath!", false, true));
                    d.Add(new Dialogo(player.textureFile, player.textureFile, "Deve aver manipolato anche te allora. Vai al riparo mentre sistemiamo le cose qui.", true, true));
                }
                if (enemy.textureFile == "Hydris")
                {
                    d.Add(new Dialogo(enemy.textureFile, enemy.textureFile, "Cosa sta succedendo? " + player.textureFile + "? Che ci fai te qui?", false, true));
                    d.Add(new Dialogo(player.textureFile, player.textureFile, "Non fare finta di non sapere! Ci hai tradito per unirti a Morgrath!", true, true));
                    d.Add(new Dialogo(enemy.textureFile, enemy.textureFile, "Unirmi a Morgrath? Non ha senso, lui è il nostro nemico, perché dovrei mai unirmi a lui?", false, true));
                    d.Add(new Dialogo(player.textureFile, player.textureFile, "Eppure ci hai condotti dritti in un'imboscata dei suoi mostri! Non puoi negare i fatti.", true, true));
                    d.Add(new Dialogo(enemy.textureFile, enemy.textureFile, "Non capisco, non ricordo nulla di questo. Mi credi davvero capace di un tradimento del genere?", false, true));
                    d.Add(new Dialogo(player.textureFile, player.textureFile, "Non so cosa pensare. Forse sei stata manipolata anche tu. Vai al sicuro, risolveremo questa situazione noi.", true, true));

                }
                if (enemy.textureFile == "Morgrath")
                {
                    d.Add(new Dialogo("Morgrath", enemy.textureFile, "Ma come... Come ho potuto perdere contro creature inferiori come voi...", false, false));

                }
                if (d.Count > 0)
                {
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
                        for (int i = 0; i < 30; i++)
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
                }


                battleManager.canMoveEnemy = true;
                if (battleManager.enemies.Contains(e))
                    battleManager.enemies.Remove(e);
                enemy.cancInfo();
                GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(2);
                yield return new WaitForSeconds(0.5f);
                temp.SetActive(true);
                temp.transform.DetachChildren();
                Destroy(temp);
                Destroy(e);
                yield return new WaitForEndOfFrame();
                
                SceneManager.UnloadSceneAsync(2);
                SceneManager.SetActiveScene(activeScene);

                    
                player.endPvp();

                Debug.Log("fine");
                
                yield break;
            }

            if (player.hp <= 0)
            {
                
                List<Dialogo> d = new List<Dialogo>();
                //dialogo sconfitta player

                if (player.textureFile == "Nova")
                {
                    d.Add(new Dialogo("Nova", "Nova", "È evidente che questo è il mio limite... Mi dispiace, amici, ma devo arrendermi e ritirarmi...", true, true));

                }
                if (player.textureFile == "Sear")
                {
                    d.Add(new Dialogo("Sear", "Sear", "È stato un duro scontro, ma devo riconoscere che non posso proseguire... Mi dispiace, ma devo ritirarmi...", true, true));

                }
                if (player.textureFile == "Granius")
                {
                    d.Add(new Dialogo("Granius", "Granius", "Ho combattuto con tutte le mie forze, ma è giunto il momento di ammettere la sconfitta... Mi ritiro...", true, true));

                }
                if (player.textureFile == "Thera")
                {
                    d.Add(new Dialogo("Thera", "Thera", "Ho dato il massimo, ma purtroppo non è stato abbastanza. Mi dispiace ma devo ritirarmi...", true, true));

                }
                if (player.textureFile == "Acquira")
                {
                    d.Add(new Dialogo("Acquira", "Acquira", "Ho sottovalutato la situazione... e ora ne sto subendo le conseguenze... Vi prego di perdonarmi...", true, true));

                }
                if (player.textureFile == "Hydris")
                {
                    d.Add(new Dialogo("Hydris", "Hydris", "Mi sembra che le mie capacità siano giunte al limite... Vi chiedo scusa ma devo accettare la sconfitta...", true, true));

                }
                if (player.textureFile == "Aeria")
                {
                    d.Add(new Dialogo("Aeria", "Aeria", "Ho lottato con tutte le mie forze, ma questa volta la vittoria non è stata dalla mia parte. Perdonatemi, amici...", true, true));

                }
                if (player.textureFile == "Skye")
                {
                    d.Add(new Dialogo("Skye", "Skye", "Le mie abilità non sono all'altezza della situazione. Perdonatemi...", true, true));

                }



                if (d.Count > 0)
                {
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
                        for (int i = 0; i < 30; i++)
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
                }

                GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("defeat");
                GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(2);
                yield return new WaitForSeconds(0.5f);
                temp.SetActive(true);
                temp.transform.DetachChildren();
                player.endPvp();
                if (battleManager.units.Contains(p))
                    battleManager.units.Remove(p);
                if (battleManager.unmovedUnits.Contains(p))
                    battleManager.unmovedUnits.Remove(p);

                
                Destroy(temp);
                yield return new WaitForEndOfFrame();
                Destroy(p);
                
                SceneManager.UnloadSceneAsync(2);
                SceneManager.SetActiveScene(activeScene);

                
                Destroy(p);
                Debug.Log("fine");
                
                yield break;
            }
        }
        else{

            GameObject.Find("Sfondo").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN+"_pvp");
            GameObject.Find("Davanti").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mappe/Mappa" + mapScript.mapN + "_pvpDavanti");

            playerScript player2 = e.GetComponent<playerScript>();

            GameObject pvpForecast = Instantiate(Resources.Load<GameObject>("pvpForecastCombat"), GameObject.Find("CombatCamera").transform);
            pvpForecast.transform.GetChild(0).GetChild(13).GetChild(2).GetComponent<Image>().color = new Color(0.1607843f, 0.572549f, 0.9607843f);

            pvpForecast.GetComponent<pvpForecastScript>().SetupPlayer(player.hp, player.maxHp, (8 + player.mag / 3), 100, 0, false, true);
            pvpForecast.GetComponent<pvpForecastScript>().SetupEnemy(player2.hp, player2.maxHp, 0, 0, 0, false, false);

            pvpForecast.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Rec";

            yield return new WaitForSeconds(1);
            sprite = GameObject.Find("Info Canvas").transform.GetChild(6).gameObject;
            sprite.transform.localScale = Vector3.zero;
            sprite.SetActive(true);

            spritePlayer.GetComponent<Animator>().Play("Heal");
            GameObject.Find("Info Canvas").transform.GetChild(6).GetComponent<TextMeshProUGUI>().text=((8 + player.mag / 3)).ToString();
            player2.hp = Mathf.Clamp(player2.hp += (8+player.mag/3), 0, player2.maxHp);

            AnimatorClipInfo[] clipInfos = spritePlayer.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
            AnimationClip firstClip = clipInfos[0].clip;
            float duration = firstClip.length + Time.time;
            float half = firstClip.length / 2 + Time.time;
            
            Debug.Log(firstClip.length);

            while (Time.time < duration)
            {
                yield return new WaitForEndOfFrame();
            }

            oldPos = sprite.transform.position;
            spriteEnemy.GetComponent<Animator>().Play("Select");
            player2.healthbar.SetHealth(player2.hp);
            pvpForecast.GetComponent<pvpForecastScript>().DamageEnemy(player2.hp, player2.maxHp);
            GameObject.Find("SFX").GetComponent<sfxScript>().playSFX("heal");
            for (float i = 0; i < 20; i++)
            {
                sprite.transform.localScale = new Vector3(Mathf.Clamp(i/2,0,1), Mathf.Clamp(i / 2, 0, 1), Mathf.Clamp(i / 2, 0, 1));
                sprite.transform.position += new Vector3(0, 0.01f*5, 0);
                yield return new WaitForEndOfFrame();

            }

            yield return new WaitForSeconds(1);
            sprite.SetActive(false);
            sprite.transform.position = oldPos;

            pvpForecast.GetComponent<pvpForecastScript>().Rimuovi();
            yield return new WaitForSeconds(0.5f);

            int expGained = (player.lvl + 9);
            if (player.hp == 0) expGained = 0;
            if (expGained != 0)
            {


                int expNeeded = (int)Mathf.Round(100 * Mathf.Pow(1.1f, player.lvl - 2));
                


                GameObject expGUI = (GameObject)Instantiate(Resources.Load("ExpCanvas", typeof(GameObject)), Camera.main.transform.position, Quaternion.identity);
                expGUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player.nome;
                expGUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (expNeeded - player.exp).ToString();
                expGUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "+" + (expGained).ToString();
                expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetMaxHealth(expNeeded);
                expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetHealth(player.exp);

                float y = expGUI.transform.position.y * -1;
                for (int i = 0; i < 20; i++)
                {
                    y += 0.01516f * 5;
                    expGUI.transform.position = new Vector3(expGUI.transform.position.x, y, -1);
                    yield return new WaitForEndOfFrame();
                }

                yield return new WaitForSeconds(0.5f);
                bool lvlup = false;
                AudioClip expSfx = (AudioClip)Resources.Load("Sounds/SFX/Exp");

                for (int i = 0; i < expGained; i++)
                {
                    yield return new WaitForSeconds(1 / (float)expGained);
                    player.exp++;
                    if (i % 2 == 0 || expGained < 15) expGUI.transform.GetChild(7).GetComponent<AudioSource>().PlayOneShot(expSfx);
                    if (player.exp >= expNeeded)
                    {
                        player.exp = 0;
                        expNeeded = (int)Mathf.Round(100 * Mathf.Pow(1.1f, player.lvl - 1));
                        expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetMaxHealth(expNeeded);
                        expGUI.transform.GetChild(6).GetChild(1).GetComponent<UnityEngine.UI.Image>().color = new Color(0, 186, 50);
                        lvlup = true;
                    }
                    expGUI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = (expNeeded - (player.exp)).ToString();
                    expGUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "+" + (expGained - i).ToString();

                    expGUI.transform.GetChild(6).GetComponent<heathBarScript>().SetHealth(player.exp);

                }
                expGUI.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "0";

                yield return new WaitForSeconds(2);

                for (int i = 0; i < 20; i++)
                {
                    y -= 0.01516f*5;
                    expGUI.transform.position = new Vector3(expGUI.transform.position.x, y, expGUI.transform.position.z);
                    yield return new WaitForEndOfFrame();
                }

                if (lvlup)
                {
                    finitoLvlUp = false;
                    LevelUp(player);
                    while (!finitoLvlUp) yield return new WaitForEndOfFrame();
                }

                List<string> nomi = new List<string> { "Nova", "Sear", "Granius", "Thera", "Acquira", "Hydris", "Aeria", "Skye" };
                try
                {
                    string[] arrLine = System.IO.File.ReadAllLines(Application.streamingAssetsPath + "/dati.txt");
                    arrLine[nomi.IndexOf(player.nome)] = player.nome + "," + player.textureFile + "," + player.lvl + "," + player.exp + "," + player.movement + "," + player.weaponMinRange + "," + player.weaponMaxRange + "," + player.weaponWt + "," + player.weaponMt + "," + player.weaponHit + "," + player.weaponCrit + "," + player.unitType + "," + player.weaponType + "," + player.weaponIsMagic + "," + player.maxHp + "," + player.str + "," + player.mag + "," + player.dex + "," + player.spd + "," + player.lck + "," + player.def + "," + player.res + "," + player.hpGrowth + "," + player.strGrowth + "," + player.magGrowth + "," + player.dexGrowth + "," + player.spdGrowth + "," + player.lckGrowth + "," + player.defGrowth + "," + player.resGrowth + "," + player.heal;
                    System.IO.File.WriteAllLines(Application.streamingAssetsPath + "/dati.txt", arrLine);
                }
                catch (System.Exception) { }

            }


        }

        GameObject.Find("LevelLoader").GetComponent<LevelLoad>().LoadNextLevel(2);
        yield return new WaitForSeconds(0.5f);

        temp.SetActive(true);
        temp.transform.DetachChildren();
        Destroy(temp);
        SceneManager.UnloadSceneAsync(2);
        SceneManager.SetActiveScene(activeScene);

        player.endPvp();
        
        Debug.Log("fine");
        

    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) skip = true;

    }
    public void LevelUp(playerScript player)
    {


        int[] increments = { 0, 0, 0, 0, 0, 0, 0, 0 }; //incrementi iniziali

        int n1 = UnityEngine.Random.Range(0, 8);        //garantisce 1o incremento
        int n2;

        do
        {                                   //garantisce 2o incremento
            n2 = UnityEngine.Random.Range(0, 8);
        } while (n1 == n2);

        increments[n1] = 1;
        increments[n2] = 1;

        int[] incrementPercentage = { player.hpGrowth, player.strGrowth, player.magGrowth, player.dexGrowth, player.spdGrowth, player.lckGrowth, player.defGrowth, player.resGrowth };


        for (int i = 0; i < 8; i++)
        {
            if (UnityEngine.Random.Range(1, 100) <= incrementPercentage[i]) increments[i] = 1;        //se il numero e' minore della % di crescita allora stat incrementa
        }

        Object canvas = Resources.Load("Level Up Canvas", typeof(GameObject));

        GameObject levelUpCanvas = (GameObject)Instantiate(canvas, new Vector3(0.159606f, 0.9915071f, 0), Quaternion.identity);
        levelUpCanvas.GetComponent<levelUpScript>().Setup(player.nome, player.lvl, player.maxHp, player.str, player.mag, player.dex, player.spd, player.lck, player.def, player.res, increments);

        player.lvl++;
        player.maxHp += increments[0];            //incrementa i valori
        player.str += increments[1];
        player.mag += increments[2];
        player.dex += increments[3];
        player.spd += increments[4];
        player.lck += increments[5];
        player.def += increments[6];
        player.res += increments[7];

        player.healthbar.SetMaxHealth(player.maxHp);

    }

}
