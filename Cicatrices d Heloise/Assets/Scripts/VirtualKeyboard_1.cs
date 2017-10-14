using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualKeyboard_1 : MonoBehaviour
{
    private Text searchText;
    [SerializeField]
    private GameObject LeSearchDicoText;
    [SerializeField]
    private GameObject LeSearchCodecText;

    //Nombre actuel de lettre
    private int countKeys;
    //Nombre MAX de lettre
    private int maxKeys = 10;

    //Variable qui stock la valeur écrite
    public string visualText; 
    
    //Pour prendre le script Dictionnaire
    private Dictionnaire dico;
    [SerializeField]
    private GameObject DicoManager;

    //Importer pour le freezControls
    private PointNclickManager pnc;
    [SerializeField]
    private GameObject PointNclickManager;


    void Awake()
    {
        //Importer les scripts
        pnc = PointNclickManager.GetComponent<PointNclickManager>();
        dico = DicoManager.GetComponent<Dictionnaire>();
    }

    //Lorsque le Script se désactive, remet les conteurs à 0
    void OnDisable()
    {
        countKeys = 0;
    }

    void Update()
    {
        //Importer le Text selon quel morceau de Scarlett est démarré
        if (LeSearchDicoText.activeInHierarchy)
        {
            searchText = LeSearchDicoText.GetComponent<Text>();
        }
        else if (LeSearchCodecText.activeInHierarchy)
        {
            searchText = LeSearchCodecText.GetComponent<Text>();
        }
        

        //--------------------------------------------------------------   Les Inputs du clavier   --------------------------------------------------------------
        foreach (char c in Input.inputString)
        {
            if (c == '\b') //Backspace et Delete
            {
                if (searchText.text.Length != 0)
                {
                    searchText.text = searchText.text.Substring(0, searchText.text.Length - 1);
                    countKeys -= 1;
                }
            }
            else if ((c == '\n') || (c == '\r')) //Les 2 touches Entrée
            {
                Debug.Log("Voici ce que j'attendais: " + searchText.text);
                dico.OnEnterButton();
            }
            //Ce sont les lettres normales (genre a,z,e,r,t, etc...)
            else if (countKeys < maxKeys)
            {
                searchText.text += c;
                countKeys += 1;
            }
        }
        //--------------------------------------------------------------   Les Inputs du clavier   --------------------------------------------------------------
    }
    
    //Correspond aux Boutons du clavier Virtuel
    public void OnButtonLetter()
    {
        //Déclarer variable nom du bouton
        string nameButton = EventSystem.current.currentSelectedGameObject.name;

        //Si le nombre de lettre n'est pas max, ajouter la lettre
        if (countKeys < maxKeys)
        {
            searchText.text += nameButton;
            countKeys += 1;
        }
    }

    //Touche BackSpace (Retour)
    public void OnBackSpaceButton()
    {
        searchText.text = searchText.text.Substring(0, searchText.text.Length - 1);
        countKeys -= 1;
    }

    //Touche Espace
    public void OnEspaceButton()
    {
        searchText.text += " ";
        countKeys += 1;
    }

    //Touche Reset
    public void OnResetButton()
    {
        searchText.text = null;
        countKeys = 0;
    }
}
