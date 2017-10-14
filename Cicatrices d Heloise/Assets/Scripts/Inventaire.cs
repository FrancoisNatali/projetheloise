using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventaire : MonoBehaviour
{
    //Pour le DontDestroyOnLoad
    public static Inventaire invManager;

    //L'inventaire
    [SerializeField]
    public List<GameObject> InventaireList;

    //L'endroit ou sont stocké les combianaisons, c'est à dire les pnj ou les objets qui ont une condition spécial, un dialogue spécial pour continuer l'histoire
    private string[] ListDesComparaisons1 = new string[] { "PNJ" };

    //L'endroit ou sont stocké les bons objets qui valide les bonnes combinaisons
    [SerializeField]
    private GameObject[] ListDesComparaisons2;

    //La List de tous les objet à dévérouiller
    private string[] ListDeTousLesObjetsName = new string[] { "Objet", "PNJ" };
    //Cette list contient tous ce que les objets doivent contenir (text+IMG, etc...)
    public GameObject[] ListDeTousLesObjets;

    //Faire le lien avec PointNClick
    private PointNclickManager pnc;

    [SerializeField]
    private GameObject PointNClickManagers;
    
    //Stocker les objets pour voir si la combinaison match
    public int IndexComparaison;
    private GameObject ObjectComparaison2;

    //Pour déverouiller dans la list de tous les objets
    private int indexObj = -1;


    public string matchObjetCollisionName;


    void Awake ()
    {
        //Pour qu'il n'y est pas 2 Inventaire et qu'il soit conservé
        if (invManager == null)
        {
            DontDestroyOnLoad(gameObject);
            invManager = this;
        }
        else if (invManager != null)
        {
            Destroy(gameObject);
        }
        
        //Si cela ne marche pas mettre pnc = GameObject.FindOfType<PointNclickManager>()
        pnc = PointNClickManagers.GetComponent<PointNclickManager>();
    }

    public void UnlockObjet()
    {
        //Lorsqu'il découvre un objet, ça dévérouille sa définition
        Debug.Log("Nouvel Objet dévérouillé");
        indexObj = System.Array.IndexOf(ListDeTousLesObjetsName, matchObjetCollisionName);
        if (indexObj >= 0)
        {
            ListDeTousLesObjets[indexObj].SetActive(true);
        }
    }

    //Maintenant la combianaison se fait automatiquement par script
    public void CombinaisonItems()
    {
        IndexComparaison = System.Array.IndexOf(ListDesComparaisons1, matchObjetCollisionName);
        Debug.Log("IndexComparaison is : " + IndexComparaison);
        if (IndexComparaison >= 0)
        {
            ObjectComparaison2 = ListDesComparaisons2[IndexComparaison];
            if (InventaireList.Contains(ObjectComparaison2) == true)
            {
                pnc.matchObject = 0;
            }
            //else if (InventaireList.Contains(ObjectComparaison2) == true)
            //{
            //    pnc.matchObject = 1;
            //}
        }
    }
}
