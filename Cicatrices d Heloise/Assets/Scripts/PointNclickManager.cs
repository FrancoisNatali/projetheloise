using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class PointNclickManager : MonoBehaviour
{
    //--------------------------------------------------------   A REMPLIR A LA MAIN SUR L'INSPECTOR   --------------------------------------------------------

    //Stocker le nom des Gameobjects dans une list pour lier leurs numéros(Index) à l'array "descriptionStockage")
    public List<string> listInteractivObjects;

    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT   -----------------------------------------------------------

    //Stocké les description qui aparaissent lorsqu'on passe la souris dessus (dans l'ordre de listInteractivObjects)
    private string[] desciptionStockage;
    private string[] desciptionStockageBureauOuvert = new string[] { "C'est un Moniteur", "C'est un Disque Dur", "C'est un Nodus", "C'est un Post It", "C'est une Imprimante", "Ce sont des Dossiers", "C'est une Boite" };
    private string[] desciptionStockageExterieurCommissariat = new string[] { "Porte d'entrée du commissariat" };
    private string[] desciptionStockageHallPolice = new string[] { "Une porte", "Un Papier collé au mur", "Surrement le bureau d'accueil", "Un bureau", "Une Trappe", "Une Cyber Boite aux lettres", "Vue sur la ville", "Il y a un bureau dans cette pièce", "un boitier" };
    private string[] desciptionStockageSalleDePause = new string[] { "Des journaux", "Une délicieuse banane", "Un cendrier encore fumant", "Les casiers de la brigade", "Des étagères", "Un tableau liege", "Un sac plein", "Une plante", "Un vieux canapé", "Une boîte", "Un pendu" };
    private string[] desciptionStockageLabo = new string[] { "Une porte fermée", "Une petite machine", " Un conteneur", "Une armoire", "Une imprimante 3D", "Un écran techno-Veleda", "Une lampe", "Des tuyaux suspendus", "Des documents", "Un Nodus", "Un Nodus" };
    //private string[] desciptionStockageBureauChef = new string[] { "" };


    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT   -----------------------------------------------------------

    //Pour la description qu'Heloise va dire
    private string[] findIndexDescription;
    private string[] findIndexDescriptionBureauOuvert = new string[] { "Moniteur", "DisqueDur", "Nodus", "PostIt", "Imprimante", "Dossiers", "Boite" };
    private string[] findIndexDescriptionExterieurCommissariat = new string[] { "PorteEntreeCom" };
    private string[] findIndexDescriptionHallPolice = new string[] { "PorteDuFond", "PapierSurLeMur", "BureauAccueil", "Bureau", "TrapeAuSol", "CyberBoiteAuxLettres", "PaysageFenetre", "BureauAtraversVitre", "BoitierSurLeMurDuBureau" };
    private string[] findIndexDescriptionSalleDePause = new string[] { "Journaux", "Banane", "Cendrier", "Casiers", "Etageres", "TableauLiege", "Sac", "Plante", "Canape", "Boites", "Pendu" };
    private string[] findIndexDescriptionLabo = new string[] { "PorteFermee", "PetiteMachineAuSol", "Conteneur", "Armoire", "Imprimante3D", "EcranTechnoVeleda", "Lampe", "TuyauxEnLair", "DocsAuxmurs", "NodusLab1", "NodusLab2" };
    //private string[] findIndexDescriptionBureauChef = new string[] { "" };

    //Définit s'il a déjà vu cet objet ou non
    private bool[] boolIndexDescription;
    private bool[] boolIndexDescriptionBureauOuvert = new bool[] { false, false, false, false, false, false, false };
    private bool[] boolIndexDescriptionExterieurCommisariat = new bool[] { false };
    private bool[] boolIndexDescriptionHallPolice = new bool[] { false, false, false, false, false, false, false, false, false };
    private bool[] boolIndexDescriptionSalleDePause = new bool[] { false, false, false, false, false, false, false, false, false, false, false };
    private bool[] boolIndexDescriptionLabo = new bool[] { false, false, false, false, false, false, false, false, false, false, false };
    //private bool[] boolIndexDescriptionBureauChef = new bool[] { };


    //--------------------------------------------------------   A REMPLIR A LA MAIN SUR L'INSPECTOR   --------------------------------------------------------

    //Les Descriptions d'Heloise
    public TextAsset[] descriptionHeloise;

    //Les Descriptions d'Heloise en résumé
    public TextAsset[] descriptionHeloiseResume;

    //L'index de description d'Heloise
    private int indexDescriptionHeloise;


    //Les couleurs du curseur sachant que ce sera un swap de sprite avec les assets (fonctionne pareil)
    public Sprite CurseurBasic;
    public Sprite CurseurOnObject;

    public Color curseurBasicColor;
    public Color curseurOnObjectColor;

    //Le sprite renderer du curseur
    SpriteRenderer spriteRend;


    //Nom du gameObject qui le collisionne
    private string nameOfCollision;
    public GameObject objetCollision;

    //Les Menus
    public GameObject MenuPause;
    [SerializeField]
    private GameObject MenuDeplacements;
    [SerializeField]
    private GameObject MenuScarlettDico;
    [SerializeField]
    private GameObject MenuScarlettCodec;
    [SerializeField]
    private GameObject MenuObjet;
    [SerializeField]
    private GameObject MenuPersonnages;
    [SerializeField]
    private GameObject MenuCarnetDeRoute;
    [SerializeField]
    private GameObject MenuParametres;
    [SerializeField]
    private GameObject MenuSave;
    [SerializeField]
    private GameObject YesNoMenu;
    
    //Le nom du bouton du Menu sur lequel il appuit
    private string nameButton;

    //Le Text de description de l'objet
    [SerializeField]
    private Text desciptionText;

    //Pour le script boite de dialogue
    [SerializeField]
    private GameObject BoiteDeDialogueManager;
    private BoiteDeDialogue bdd;
    [SerializeField]
    private GameObject ObjetBoiteDialogue;

    //Pour gérer l'inventaire
    private Inventaire ivt;

    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT   -----------------------------------------------------------
    //Pour le ramassage d'un objet
    public Text ramassageObjetText;

    //Pour le load
    private AsyncOperation async;

    //Pour connaitre le nom de la destination
    private string nomDestination;

    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT   -----------------------------------------------------------
    //Pour choisir la Scène désirée
    private int choiceScene;

    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT   -----------------------------------------------------------
    //Pour les annonces en tout genre
    public Text anonceText;

    //Le clavier Virtuel
    [SerializeField]
    private GameObject ClavierVirtuel;

    //Pour bloquer les controles du joueur
    public bool FreezePlayer = false;

    //Pour prendre le script Dictionnaire
    private Dictionnaire dico;
    [SerializeField]
    private GameObject DicoManager;

    //Le panel pour le menu pause
    [SerializeField]
    private GameObject PausePanel;

    //Pour savoir lorsqu'il dévérouille la suite grâce à un objet clé associé au bon PNJ ou à un autre objet
    public int matchObject = -1;

    //Pour le script Inventaire
    [SerializeField]
    private GameObject InventaireMangers;

    //Pour savoir quel scene est active
    private string sceneName;


    void Awake()
    {
        //Changer les tableau selon la scène
        sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case ("BureauOuvert"):
                desciptionStockage = desciptionStockageBureauOuvert;
                findIndexDescription = findIndexDescriptionBureauOuvert;
                boolIndexDescription = boolIndexDescriptionBureauOuvert;
                break;
            case ("ExterieurCommissariat"):
                desciptionStockage = desciptionStockageExterieurCommissariat;
                findIndexDescription = findIndexDescriptionExterieurCommissariat;
                boolIndexDescription = boolIndexDescriptionExterieurCommisariat;
                break;
            case ("HallPolice"):
                desciptionStockage = desciptionStockageHallPolice;
                findIndexDescription = findIndexDescriptionHallPolice;
                boolIndexDescription = boolIndexDescriptionHallPolice;
                break;
            case ("SalleDePause"):
                desciptionStockage = desciptionStockageSalleDePause;
                findIndexDescription = findIndexDescriptionSalleDePause;
                boolIndexDescription = boolIndexDescriptionSalleDePause;
                break;
            case ("Labo"):
                desciptionStockage = desciptionStockageLabo;
                findIndexDescription = findIndexDescriptionLabo;
                boolIndexDescription = boolIndexDescriptionLabo;
                break;
            case ("BureauChef"):
                desciptionStockage = desciptionStockageLabo;
                findIndexDescription = findIndexDescriptionLabo;
                boolIndexDescription = boolIndexDescriptionLabo;
                break;
        }


        //Importer le sprite renderer du curseur (pour changer forme et couleur)
        spriteRend = GetComponent<SpriteRenderer>();

        //Importer les scripts
        bdd = BoiteDeDialogueManager.GetComponent<BoiteDeDialogue>();
        ivt = InventaireMangers.GetComponent<Inventaire>();
        dico = DicoManager.GetComponent<Dictionnaire>();
    }
    
    void Update()
    {
        //Faire bouger le curseur(GameObject) avec la souris en permanence
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        transform.position = new Vector3(mouse.x, mouse.y, 1);

        //Pour enlever l'effet du trigger lorsqu'il ouvre le menu pause
        if (FreezePlayer == true && (nameOfCollision != null && objetCollision != null))
        {
            spriteRend.sprite = CurseurBasic;
            spriteRend.color = curseurBasicColor;
            nameOfCollision = null;
            objetCollision = null;
        }
        
        //Afficher le text de description à côté de l'objet
        if ((nameOfCollision != null) && !MenuPause.activeSelf)
        {
            desciptionText.text = desciptionStockage[listInteractivObjects.IndexOf(nameOfCollision)];
        }
        else
        {
            desciptionText.text = null;
        }


        //--------------------------------------------------------   INPUTS   --------------------------------------------------------
        
        if (Input.GetKeyDown(KeyCode.RightShift) && !ClavierVirtuel.activeSelf && (MenuScarlettDico.activeSelf || MenuScarlettCodec.activeSelf))
        {
            ClavierVirtuel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightShift) && ClavierVirtuel.activeSelf && (MenuScarlettDico.activeSelf || MenuScarlettCodec.activeSelf))
        {
            ClavierVirtuel.SetActive(false);
        }

        //Pour Freeze les controls (pendant le menu pause)
        if (FreezePlayer == true)
        {
            //La touche Retour pour tous les menus
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (MenuPause.activeSelf)
                {
                    MenuPause.SetActive(false);
                    PausePanel.SetActive(false);
                    FreezePlayer = false;
                }
                if (MenuDeplacements.activeSelf)
                {
                    MenuDeplacements.SetActive(false);
                    MenuPause.SetActive(true);
                }
                if (MenuScarlettDico.activeSelf)
                {
                    MenuScarlettDico.SetActive(false);
                    ClavierVirtuel.SetActive(false);
                    MenuPause.SetActive(true);
                }
                if (MenuScarlettCodec.activeSelf && (!ObjetBoiteDialogue.activeSelf))
                {
                    MenuScarlettCodec.SetActive(false);
                    ClavierVirtuel.SetActive(false);
                    MenuPause.SetActive(true);
                }
                if (MenuObjet.activeSelf)
                {
                    MenuObjet.SetActive(false);
                    MenuPause.SetActive(true);
                }
                if (MenuPersonnages.activeSelf)
                {
                    MenuPersonnages.SetActive(false);
                    MenuPause.SetActive(true);
                }
                if (MenuCarnetDeRoute.activeSelf)
                {
                    MenuCarnetDeRoute.SetActive(false);
                    MenuPause.SetActive(true);
                }
                if (MenuParametres.activeSelf)
                {
                    MenuParametres.SetActive(false);
                    MenuPause.SetActive(true);
                }
                if (MenuSave.activeSelf)
                {
                    MenuSave.SetActive(false);
                    MenuPause.SetActive(true);
                }
                if (YesNoMenu.activeSelf)
                {
                    YesNoMenu.SetActive(false);
                    MenuPause.SetActive(true);
                }
            }

        }
        //S'il est sur la scène principal
        else if (FreezePlayer == false)
        {
            //La touche Retour
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuPause.SetActive(true);
                PausePanel.SetActive(true);
                FreezePlayer = true;
            }

            //Lancer la fonction lorsqu'il click sur l'objet ou le PNJ
            if (Input.GetMouseButtonDown(0) && (nameOfCollision != null))
            {
                OnActionPointClick();
            }
        }
    }

    IEnumerator RamassageObjetAnimation()
    {
        //Affichage rapide de d'objet ramassé
        ramassageObjetText.text = objetCollision.name + " Found !";
        
        yield return new WaitForSeconds(1f);
        ramassageObjetText.text = null;
    }

    //Lorsque le curseur trouve une collision, il change de sprite et garde le nom du collider
    void OnTriggerEnter2D(Collider2D col)
    {
        if (FreezePlayer == false && !ObjetBoiteDialogue.activeSelf)
        {
            spriteRend.sprite = CurseurOnObject;
            spriteRend.color = curseurOnObjectColor;
            nameOfCollision = col.gameObject.name;
            objetCollision = col.gameObject;
        }
    }

    //Lorsque le curseur n'est plus en contact, il enlève le nom du collider et change de sprite
    void OnTriggerExit2D(Collider2D ex)
    {
        spriteRend.sprite = CurseurBasic;
        spriteRend.color = curseurBasicColor;
        objetCollision = null;
        nameOfCollision = null;
    }

    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT   -----------------------------------------------------------
    //Activation du Menu ou Etat corespondant au bouton préssé (nameButton)
    public void OnMenuAction()
    {
        //Déclarer variable nom du bouton
        nameButton = EventSystem.current.currentSelectedGameObject.name;

        //Désactiver le menu pause pour faire place au nouveau menu
        MenuPause.SetActive(false);
        switch (nameButton)
        {
            case ("MoveButton"):
                MenuDeplacements.SetActive(true);
                break;
            case ("DicoButton"):
                ClavierVirtuel.SetActive(true);
                MenuScarlettDico.SetActive(true);
                break;
            case ("CodecButton"):
                ClavierVirtuel.SetActive(true);
                MenuScarlettCodec.SetActive(true);
                break;
            case ("ObjetButton"):
                MenuObjet.SetActive(true);
                break;
            case ("PersonnageButton"):
                MenuPersonnages.SetActive(true);
                break;
            case ("CarnetDeRouteButton"):
                MenuCarnetDeRoute.SetActive(true);
                break;
            case ("ParametresButton"):
                MenuParametres.SetActive(true);
                break;
            case ("SaveButton"):
                MenuSave.SetActive(true);
                break;
            case ("QuitterButton"):
                YesNoMenu.SetActive(true);
                break;
        }
        nameButton = null;
    }

    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT   -----------------------------------------------------------
    //Définit toutes les actions qui vont s'effectuer après avoir cliquer
    void OnActionPointClick()
    {
        ivt.matchObjetCollisionName = objetCollision.name;
        Debug.Log(ivt.matchObjetCollisionName);
        ivt.UnlockObjet();
        if (objetCollision.tag == "PNJ")
        {
            //Vérifie s'il possède l'objet de la condition spécial
            ivt.CombinaisonItems();
            if (matchObject == 0)
            {
                //Démarrer la condition spécial qui dévérouille la suite du jeu
                Debug.Log("Confidence d'un PNJ");
                matchObject = -1;
            }
            else if (matchObject == -1)
            {
                //Démarer le dialogue et le script BoiteDeDialogues normalement
                bdd.TextActive = true;
                bdd.EnableTextBox();
                bdd.autoriseReloadDocument = true;
                Debug.Log("PNJ is talking");
            }
        }
        //Démarer la description de l'objet
        else if (objetCollision.tag == "Ramassable")
        {
            //Ajouter l'objet dans l'inventaire et dans sa List puis le déplacer
            StartCoroutine(RamassageObjetAnimation());
            ivt.InventaireList.Add(objetCollision);
            objetCollision.transform.SetParent(MenuObjet.transform);
        }
        else if ((objetCollision.tag == "Objet"))
        {
            indexDescriptionHeloise = System.Array.IndexOf(findIndexDescription, nameOfCollision);
            bdd.EnableTextBoxWithoutPNJ();
            if (boolIndexDescription[indexDescriptionHeloise] == false)
            {
                bdd.ReloadScript(descriptionHeloise[indexDescriptionHeloise]);
                boolIndexDescription[indexDescriptionHeloise] = true;
            }
            else if (boolIndexDescription[indexDescriptionHeloise] == true)
            {
                bdd.ReloadScript(descriptionHeloiseResume[indexDescriptionHeloise]);
            }
        }
        nameOfCollision = null;
    }
    
    //---------------------------------------------------   Charger une nouvelle scène en arrière plan   ---------------------------------------------------
    IEnumerator LoadLaScene()
    {
        if (choiceScene > 0)
        {
            //AFFICHER LOAD SCENE (anim?) + Panel pour bloquer controles
            //yield return new WaitForSeconds(3);

            //Ceci représente le choix del'épisode Si on en ajoute un MAIS egalement ce qui va servir avec le Load() du SaveLoadManager
            switch (choiceScene)
            {
                case 1:
                    async = SceneManager.LoadSceneAsync("PointNclick");
                    break;
                case 2:
                    async = SceneManager.LoadSceneAsync("MainMenu");
                    break;
                case 3:
                    async = SceneManager.LoadSceneAsync("Scene3");
                    break;
            }

            // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
            while (!async.isDone)
            {
                //AFFICHER LOAD SCENE (anim?) + Panel pour bloquer controles
                yield return null;
            }
        }
    }
    public void OnDestinationButton()
    {
        //Définir le nom du bouton qui est aussi le nom de la destination
        nomDestination = EventSystem.current.currentSelectedGameObject.name;
        YesNoMenu.SetActive(true);
    }
    public void OnYesQuitterButton()
    {
        choiceScene = 2;
        StartCoroutine(LoadLaScene());
    }


    public void OnNoQuitterButton()
    {
        choiceScene = 0;
        YesNoMenu.SetActive(false);
        if (!MenuDeplacements.activeSelf)
        {
            MenuPause.SetActive(true);
        }
    }
    public void OnCancelSaveButton()
    {
        MenuSave.SetActive(false);
        MenuPause.SetActive(true);
    }
}