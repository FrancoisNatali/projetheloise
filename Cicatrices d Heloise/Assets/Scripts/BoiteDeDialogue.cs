using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class BoiteDeDialogue : MonoBehaviour
{
    //C'est le Manager des dialogues simples ou avec choix. Il peut les passer avec Enter. Il fait également le lien avec les scripts "DialoguePNJ" pour changer les doc txt
    
    //--------------------------------------------------------   A REMPLIR A LA MAIN SUR L'INSPECTOR   ---------------------------------------------------------
    //Pour considérer que le fichier Text est une Array
    public TextAsset textFile;
    //L'Array du fichier Text
    private string[] textLines;

    //Vérifier le nombre de choix avec la première ligne des doc txt et le convertir en int
    private string choiceFirstLine;
    private int choiceFirstLineInt;

    //Vérifier la seconde ligne pour savoir quel image il faut mettre
    private string choiceSecondLine;
    private int choiceSecondLineInt;

    //--------------------------------------------------------   A REMPLIR A LA MAIN SUR L'INSPECTOR   ---------------------------------------------------------
    //Le tableau qui contient les Images
    public Sprite[] listDesImagesInterlocuteur;

    //La boite de dialogue physique et son image
    public GameObject textBox;
    public GameObject ImageTextBox;
    //Pour que l'image soit de l'autre coté
    [SerializeField]
    private GameObject ImageTextBoxOppose;

    //Le bouton choix et l'int qui définit quel choix de dialogue a fait le joueur
    [SerializeField]
    private GameObject ChoixButtons;
    private int choix;
    //Les 3 boutons de choix
    [SerializeField]
    private GameObject ChoixButton1;
    [SerializeField]
    private GameObject ChoixButton2;
    [SerializeField]
    private GameObject ChoixButton3;

    //Le Text qui va s'afficher
    public Text dialogue;

    //La ligne actuel et la dernière ligne
    public int currentLine = 2;
    public int endAtLine;

    //-------------------------------------------------------------------   Ajout d'un affichage lettres par lettres   -------------------------------------------------------------------

    //Si le text est en train de s'écrire et cancel pour le stopper
    private bool isTyping = false;
    private bool cancelTyping = false;

    //Vitesse d'affichage lettre par lettre
    private float textSpeed = 0.1f;

    //-------------------------------------------------------------------   Ajout d'un affichage lettres par lettres   -------------------------------------------------------------------

    //Boolean qui définit si le dialogue a démarré ou non
    public bool TextActive = false;

    //Connaitre le nom du bouton pressé
    private string nameButtonChoix;

    //Chopper le PointNClickManager
    private PointNclickManager pcm;
    [SerializeField]
    private GameObject PointNclickManager;

    //C'est le numéro du doc text dans l'array de chaque PNJ
    public int listDocNum = 0;

    //Autorise un Reload du doc txt dans le script PNJ
    public bool autoriseReloadDocument;

    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT   -----------------------------------------------------------
    //Pour bloquer les waitForPress (est utile pour le ResetLesWaitForPress(); )
    private int nombreDePNJsurLaMap = 3;

    //Accelérer les dialogues
    private float speedDialogue;
    private bool stopSpeedDialogue;

    void Awake()
    {
        //Importer scripts
        pcm = PointNclickManager.GetComponent<PointNclickManager>();
    }

    void Start()
    {
        //Au start, s'initialise avec un faux document text (placeHolder à mettre sur lui dans l'inspector)
        if (TextActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));

            if (endAtLine == 0)
            {
                endAtLine = textLines.Length - 1;
                Debug.Log("V4");
            }
        }
    }

    //GetComponent<Image>().sprite = DictionnaireDeRechercheImages[indexDictionnaire];
    void OnDisable()
    {
        speedDialogue = 0;
        textSpeed = 0.1f;
    }

    void Update()
    {
        Debug.Log("speedDialogue is :" + speedDialogue);
        Debug.Log("textSpeed id :" + textSpeed);
        Debug.Log("JeLisL'update");
        if (!TextActive)
        {
            Debug.Log("ICI?");
            return;
        }
        Debug.Log("TropLoin");

        //Définit si le joueur a un choix de réponse
        if ((choix != 0) && (currentLine > endAtLine))
        {
            ChoixButtons.SetActive(true);
            switch (choix)
            {
                case (1):
                    ChoixButton1.SetActive(true);
                    break;
                case (2):
                    ChoixButton1.SetActive(true);
                    ChoixButton2.SetActive(true);
                    break;
                case (3):
                    ChoixButton1.SetActive(true);
                    ChoixButton2.SetActive(true);
                    ChoixButton3.SetActive(true);
                    break;
            }
        }

        //Si tous les texts ont été lu, il désactive la box (il réactive le curseur)
        if ((choix == 0) && (currentLine > endAtLine))
        {
            //Désactiver la box de dialogue et la reset
            DisableTextBox();
            listDocNum = 0;
            currentLine = 2;

            //Reset les waitForPress des DialoguePNJ
            ResetLesWaitForPress();
            Debug.Log("C'est Fini !");
        }

        //Si le joueur a appuyé sur un bouton choix, lance la coroutine de choix
        if (nameButtonChoix != null)
        {
            StartCoroutine(ChoixEffectue());
        }
        
        //Configurer l'avance rapide
        if (Input.GetKey(KeyCode.S))
        {
            speedDialogue += 1;
            if (speedDialogue > 10)
            {
                textSpeed = 0.01f;
                if (stopSpeedDialogue == false)
                {
                    currentLine += 1;
                    //Si la ligne actuel est la dernière, il stop la box de dialogue. Sinon il lance le dialogue lettre par lettre
                    if (currentLine <= endAtLine)
                    {
                        StartCoroutine(TextScroll(textLines[currentLine]));
                    }
                    stopSpeedDialogue = true;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            OnDisable();
        }

        //Il appuit sur Enter pour continuer le dialogue
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (!isTyping)
            {
                currentLine += 1;

                //Si la ligne actuel est la dernière, il stop la box de dialogue. Sinon il lance le dialogue lettre par lettre
                if (currentLine <= endAtLine)
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));
                }
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
        Debug.Log("Fin de L'update");
    }

    //Pour Reset tous les waitforpress des DialoguePNJ
    void ResetLesWaitForPress()
    {
        for (int i = 0; i < nombreDePNJsurLaMap; i++)
        {
            DialoguePNJ.waitForPress[i] = false;
            Debug.Log("WaitForPress Reset !");
        }
    }

    //Lire le dialogue lettre par lettre
    IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        dialogue.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            dialogue.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(textSpeed);
        }
        dialogue.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
        Debug.Log("Incroyable Coroutine !");

        //Pour bloquer l'avance rapide
        if (letter >= lineOfText.Length - 1)
        {
            stopSpeedDialogue = false;
            Debug.Log("J'ai stoppé l'avance rapide le temps du changement");
        }
    }

    //Fonction qui active la boite de dialogue
    public void EnableTextBox()
    {
        //Afficher Box Dialogue, Freeze controls
        TextActive = true;
        textBox.SetActive(true);
        Debug.Log("Enable 1");
        if (autoriseReloadDocument == true)
        {
            StartCoroutine(TextScroll(textLines[currentLine]));
        }
        Debug.Log("Enable 2");
    }

    public void EnableTextBoxWithoutPNJ()
    {
        //Afficher Box Dialogue
        TextActive = true;
        textBox.SetActive(true);
        StartCoroutine(TextScroll(textLines[currentLine]));
    }

    //Fonction qui désactive la boite de dialogue
    void DisableTextBox()
    {
        //Cacher Box Dialogue
        OnDisable();
        TextActive = false;
        textBox.SetActive(false);
    }


    public void ReloadScript(TextAsset dialogues)
    {
        //allowWaitForPressReset = false;
        Debug.Log("Marche 7");
        //Reset les Images
        ImageTextBoxOppose.SetActive(false);
        ImageTextBox.SetActive(false);
        //ImageTextBox = null;
        //ImageTextBoxOppose = null;

        //Réinitialise la currentLine et le enAtLine et resplit le doc txt en Array
        currentLine = 2;
        textLines = new string[1];
        textLines = (dialogues.text.Split('\n'));
        Debug.Log("Marche 8");

        //Je récupère la première valeur
        choiceFirstLine = textLines[0];
        Debug.Log("Marche 9");

        //Je le transforme en Int et j'applique sa valeur au nombre de choix
        int.TryParse(choiceFirstLine, out choiceFirstLineInt);
        choix = choiceFirstLineInt;

        //Je récupère la seconde valeur
        choiceSecondLine = textLines[1];
        Debug.Log("Marche 9.5 !");

        //Je le transforme en Int et j'applique sa valeur au nombre de choix
        int.TryParse(choiceSecondLine, out choiceSecondLineInt);

        //if (choiceSecondLineInt == 0)
        //{
        //    ImageTextBoxOppose.GetComponent<Image>().sprite = listDesImagesInterlocuteur[choiceSecondLineInt];
        //    ImageTextBoxOppose.SetActive(true);
        //    Debug.Log("Marche Opposé");
        //}
        /*else*/ if ((choiceSecondLineInt >= 0))
        {
            //Affiche l'image de l'interlocuteur
            ImageTextBox.GetComponent<Image>().sprite = listDesImagesInterlocuteur[choiceSecondLineInt];
            ImageTextBox.SetActive(true);
            Debug.Log("Marche 9.9 !");
        }
        

        //Définir le nombre de ligne
        endAtLine = textLines.Length - 1;
        Debug.Log("Marche 10");
        StartCoroutine(TextScroll(textLines[currentLine]));

    }
    
    public void OnChoixButton()
    {
        //Déclarer variable nom du bouton
        nameButtonChoix = EventSystem.current.currentSelectedGameObject.name;
    }

    IEnumerator ChoixEffectue()
    {
        switch (nameButtonChoix)
        {
            case "1":
                {
                    listDocNum += 1;
                    Debug.Log("Marche 1");
                    break;
                }
            case "2":
                {
                    listDocNum += 10;
                    break;
                }
            case "3":
                {
                    listDocNum += 20;
                    break;
                }
        }
        //Il relance le script du PNJ
        nameButtonChoix = null;
        autoriseReloadDocument = true;
        Debug.Log("Marche 2");
        
        while (autoriseReloadDocument == true)
        {
            yield return null;
        }
        //Désactiver les choix buttons
        ChoixButton1.SetActive(false);
        ChoixButton2.SetActive(false);
        ChoixButton3.SetActive(false);
        ChoixButtons.SetActive(false);

        Debug.Log("Marche 3");
    }
}
