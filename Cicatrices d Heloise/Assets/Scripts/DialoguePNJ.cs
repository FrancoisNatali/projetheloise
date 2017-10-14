using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePNJ : MonoBehaviour
{
    //-----------------------------------------------------------   A REMPLIR A LA MAIN SUR L'INSPECTOR   -----------------------------------------------------------
    [SerializeField]
    private TextAsset[] ListDesDocumentsText;

    //-----------------------------------------------------------   A REMPLIR A LA MAIN SUR L'INSPECTOR   -----------------------------------------------------------
    [SerializeField]
    private TextAsset[] ListDesDocumentsTextsResume;

    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT   -----------------------------------------------------------
    //Définit s'il a déjà parlé à ce PNJ ([] est le nombre de PNJ sur la map)
    public bool[] ListDejaVuPnj = new bool [3];

    //Pour chopper la boite de dialogue (le script)
    private BoiteDeDialogue theTextBoxManager;

    //Pour chopper la boite de dialogue (l'objet)
    [SerializeField]
    private GameObject BoiteDeDialogueObjet;

    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT   -----------------------------------------------------------
    //Pour que les dialogues ne se mélange pas : la list des bool
    public static bool[] waitForPress = new bool[3];
    //-----------------------------------------------------------   A REMPLIR A LA MAIN PAR SCRIPT ET SUR L'INSPECTOR !!!  -----------------------------------------------------------
    //Et la list des noms
    public string[] nomPourWaitForPress = new string[3];

    private int tailleListDocText;

    private int separateur = -1;


    void Awake()
    {
        //Pour trouver l'objet qui contient le script BoiteDeDialogue
        theTextBoxManager = FindObjectOfType<BoiteDeDialogue>();

        //Pour différentier tous les PNJs
        separateur = System.Array.IndexOf(nomPourWaitForPress, gameObject.name);

        //Pour connaitre la taille de la list principal
        tailleListDocText = ListDesDocumentsText.Length;
        Debug.Log(tailleListDocText);
    }

    void Update()
    {
        //Permet de continuer si un bouton est préssé
        if (theTextBoxManager.listDocNum > 0)
        {
            waitForPress[separateur] = true;
        }

        //Si le joueur a fait un choix, il va changer le doc txt
        if ((theTextBoxManager.autoriseReloadDocument == true) && (waitForPress[separateur] == true))//theTextBoxManager.isActiveAndEnabled
        {
            if (ListDejaVuPnj[separateur] == false)
            {
                theTextBoxManager.ReloadScript(ListDesDocumentsText[theTextBoxManager.listDocNum]);
                Debug.Log("J'ai changé le doc txt 1");
            }
            else if (ListDejaVuPnj[separateur] == true)
            {
                theTextBoxManager.ReloadScript(ListDesDocumentsTextsResume[theTextBoxManager.listDocNum]);
            }


            if (tailleListDocText == 1)
            {
                Debug.Log("CA CHANGE CEST SUPER COOL !!!!!");
                ListDejaVuPnj[separateur] = true;
            }
            else if (theTextBoxManager.listDocNum +1 == tailleListDocText)
            {
                Debug.Log("CA CHANGE CEST SUPER COOL  222222222!!!!!");
                ListDejaVuPnj[separateur] = true;
            }

            waitForPress[separateur] = false;
            theTextBoxManager.autoriseReloadDocument = false;
            Debug.Log("J'ai changé le doc txt 2");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(gameObject.name);
        if (col.tag == "Curseur")
        {
            waitForPress[separateur] = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Curseur")
        {
            waitForPress[separateur] = false;
        }
    }
}
