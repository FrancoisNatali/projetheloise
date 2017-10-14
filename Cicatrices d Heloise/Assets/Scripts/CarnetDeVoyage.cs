using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarnetDeVoyage : MonoBehaviour
{
    //Le premier text qui affiche les aventures d'Heloise
    [SerializeField]
    private Text text1Carnet;

    [SerializeField]
    private GameObject inventaire;
    private Inventaire ivt;

    private bool[] avanceHistoire = new bool [1];


	void Awake()
    {
        //Importer tous les scripts pour condition
        ivt = inventaire.GetComponent<Inventaire>();
    }

	void OnEnable ()
    {
        //ICI Ajouter une condition pour ajouter à chaque fois un morceau du text en plus
		if (ivt.ListDeTousLesObjets[0].activeSelf && avanceHistoire[0] == false)
        {
            text1Carnet.text += "Voila la suite de l'histoire";
            avanceHistoire[0] = true;
        }
	}
}
