using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dictionnaire : MonoBehaviour
{
    //Les menus Scarlett
    [SerializeField]
    private GameObject MenuScarlettDico;
    [SerializeField]
    private GameObject MenuScarlettCodec;

    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT ET SUR L'INsPECTOR !!!!   -----------------------------------------------------------

    //Composé d'un avec les mots clefs du Codec et l'autre avec les doc txt qui lance les dialogues
    private string[] DictionnaireMotsClefsCodec = new string[2] { "bi", "ki" };
    //C'est lui qui a les doc txt pour le Codec
    public TextAsset[] DictionnaireCodec;

    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT ET SUR L'INsPECTOR !!!!   -----------------------------------------------------------

    //Composé du mot clé du Dictionnaire, puis du Text de Scarlett : DictionnaireDeRecherche [String MotClé, String AffichageTextScarlett.text];
    private string[] DictionnaireMotsClefs = new string[2] { "ba", "ka" };
    public TextAsset[] DictionnaireDeRecherche;

    //--------------------------------------------------------   A REMPLIR A LA MAIN SUR L'INSPECTOR   ---------------------------------------------------------

    //Ici ce sont les images liées à la recherche (Dictionnaire) : Pour faire le lien, il faut utilisé l'index de DictionnaireDeRecherche
    public Sprite[] DictionnaireDeRechercheImages;//Remettre en 2dimensions pour 2 images à chaque fois OU 2 Array

    //Les images de descriptions
    public GameObject ImageScarlett1;
    public GameObject ImageScarlett2;
    //Et leurs spriteRenderer
    private Sprite ImgSpr1;
    private Sprite ImgSpr2;

    //La description de Scarlett et ce que le joueur écrit
    public Text affichageTextScarlett;
    public Text affichageSearchDico;
    public Text affichageSearchCodec;
    //public Text affichageSearchReseau;

    //L'index du mot découvert et mettre l'index du dictionnaire à -1 par défault pour qu'il ne trouve rien
    private int indexDictionnaire = -1;

    //Le clavier virtuel
    [SerializeField]
    private GameObject ClavierVirtuel;
    private VirtualKeyboard_1 vkb;

    //Importer pour le script
    private PointNclickManager pnc;
    [SerializeField]
    private GameObject PointNclickManager;

    //La boite de dialogue
    [SerializeField]
    private GameObject BoiteDeDialogues;
    [SerializeField]
    private Text BoiteDeDialogueText;

    //Pour le Menu Codec
    [SerializeField]
    private GameObject ImageAppel;
    private Image ImageAppelSpr;
    public Text CodecText;

    //Pour le Menu Reseau
    public GameObject ObjetPointDeConnexion;

    //--------------------------------------------------------   A REMPLIR A LA MAIN SUR L'INSPECTOR   ---------------------------------------------------------
    //Lorsqu'il n'a pas trouvé la bonne combianison
    [SerializeField]
    private TextAsset MauvaisChoixReseau;

    //Pour le script boite de dialogue
    [SerializeField]
    private GameObject BoiteDeDialogueManager;
    private BoiteDeDialogue bdd;

    //Pour enregistrer les contact déjà contacté
    private GameObject[] ListDesContactSave;


    void Awake()
    {
        //Pour changer le sprite de l'image
        ImageAppelSpr = ImageAppel.GetComponent<Image>();

        //Les scripts à importer
        pnc = PointNclickManager.GetComponent<PointNclickManager>();
        vkb = ClavierVirtuel.GetComponent<VirtualKeyboard_1>();
        bdd = BoiteDeDialogueManager.GetComponent<BoiteDeDialogue>();

        //Réinitialiser la recherche à chaque démarrage de Scarlett
        affichageTextScarlett.text = "";
    }

    void Update()
    {
        //Il appuie sur Enter pour lancer la recherche
        if (Input.GetKeyDown(KeyCode.Return) && !BoiteDeDialogues.activeSelf)
        {
            OnEnterButton();
        }
    }
    
    //Lancement de la recherche
    public void OnEnterButton()
    {
        indexDictionnaire = -1;
        //-------------------------------------------------------------   CODEC   ---------------------------------------------------------------
        if (MenuScarlettCodec.activeInHierarchy)
        {
            indexDictionnaire = System.Array.IndexOf(DictionnaireMotsClefsCodec, affichageSearchCodec.text);

            //Si on ajoute un dialogue différent selon la scène, il faut mettre la condition en plus en dessous  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //Si l'index existe
            if (indexDictionnaire >= 0)
            {
                //Désactiver le clavier
                ClavierVirtuel.SetActive(false);

                //Enregistrer le contact
                ListDesContactSave[indexDictionnaire].SetActive(true);

                //Lancer le dialogue
                bdd.EnableTextBoxWithoutPNJ();
                bdd.ReloadScript(DictionnaireCodec[indexDictionnaire]);

                //Afficher L'image de l'appelé et son nom
                ImageAppel.SetActive(true);
                ImageAppelSpr.sprite = bdd.ImageTextBox.GetComponent<Image>().sprite;
                CodecText.text = "";
            }
            //Sinon désactiver le tout et afficher un message d'erreur
            else
            {
                //Un léger son d'erreur doit être ajouté pour plus de clareté
                CodecText.text = "Cette Personne n'existe pas";
            }
        }

        //-----------------------------------------------------------   DICTIONNAIRE   ----------------------------------------------------------
        if (MenuScarlettDico.activeInHierarchy)
        {
            //Chercher l'index du mot clé
            indexDictionnaire = System.Array.IndexOf(DictionnaireMotsClefs, affichageSearchDico.text);

            //Si l'index existe
            if (indexDictionnaire >= 0)
            {
                //Désactiver le clavier
                ClavierVirtuel.SetActive(false);

                //Afficher la description affilié à l'index du mot clé
                affichageTextScarlett.text = DictionnaireDeRecherche[indexDictionnaire].text;

                //Afficher les images
                ImageScarlett1.SetActive(true);
                ImageScarlett2.SetActive(true);

                //Récupérer les spriteRenderer des Images et mettre les sprite voulus du Dico (doit être en 2 dimensions mais du coup ne s'affiche plus sur l'inspector... A VOIR)
                ImageScarlett1.GetComponent<Image>().sprite = DictionnaireDeRechercheImages[indexDictionnaire];
                ImageScarlett2.GetComponent<Image>().sprite = DictionnaireDeRechercheImages[indexDictionnaire];
            }
            //Sinon désactiver le tout et afficher un message d'erreur
            else
            {
                //Un léger son d'erreur doit être ajouté pour plus de clareté
                affichageTextScarlett.text = "Nothing On this";
                ImageScarlett1.SetActive(false);
                ImageScarlett2.SetActive(false);
            }
        }
    }

    public void OnScarlettCancelButton()
    {
        MenuScarlettDico.SetActive(false);
        MenuScarlettCodec.SetActive(false);
        pnc.MenuPause.SetActive(true);
    }
}
