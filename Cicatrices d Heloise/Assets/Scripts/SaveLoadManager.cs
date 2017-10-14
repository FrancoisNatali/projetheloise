using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveLoadManager : MonoBehaviour
{
    //Pour Save & Load
    private string nameSaveButton;

    //Pour gérer l'inventaire
    [SerializeField]
    private GameObject MenuObjet;
    private Inventaire ivt;


    void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        //Importer les scripts
        if (currentScene != "MainMenu")
        {
            ivt = MenuObjet.GetComponent<Inventaire>();
        }
    }

    public void Load()
    {
        //Load les infos du jeu
        if (File.Exists(Application.persistentDataPath + "/" + nameSaveButton + ".hdw"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + nameSaveButton + ".hdw", FileMode.Open);
            //Décrypter les données
            CicatricesData data = (CicatricesData)bf.Deserialize(file);
            

            //Ici c'est les infos à LOAD
            ivt.InventaireList = data.InventaireList;
            //L'inventaire, la position du joueur et dans quel scène il est, s'il y a des bool qui gère l'avancé dans l'histoire

            file.Close();
            Debug.Log("Load" + Application.persistentDataPath + "/" + nameSaveButton + ".hdw");
        }
    }

    public void Save()
    {
        //Enregistrer les infos dans les datas du jeu
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + nameSaveButton + ".hdw");

        CicatricesData data = new CicatricesData();

        //Ici c'est les infos à SAVE
        data.InventaireList = ivt.InventaireList;
        //L'inventaire, la position du joueur et dans quel scène il est, s'il y a des bool qui gère l'avancé dans l'histoire

        //Crypter les données
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Save Done");
    }

    public void SaveAuto()
    {
        //Enregistrer les infos dans les datas du jeu
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveAuto.hdw");

        CicatricesData data = new CicatricesData();

        //Ici c'est les infos à SAVE
        data.InventaireList = ivt.InventaireList;
        //L'inventaire, la position du joueur et dans quel scène il est, s'il y a des bool qui gère l'avancé dans l'histoire

        //Crypter les données
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("SaveAuto Done");
    }

    public void OnLoadButton()
    {
        //Les boutons devront s'appeler "Cicatrice_Save1","Cicatrice_Save2","Cicatrice_Save3", et "SaveAuto" (juste pour le load)
        nameSaveButton = EventSystem.current.currentSelectedGameObject.name;
        Load();
    }

    public void OnSaveButton()
    {
        //Les boutons devront s'appeler "Cicatrice_Save1","Cicatrice_Save2","Cicatrice_Save3", et "SaveAuto" (juste pour le load)
        nameSaveButton = EventSystem.current.currentSelectedGameObject.name;
        Save();
    }

    //Toutes les valeurs à Sauvegarder et à Load sont dans la class ci-dessous
    [Serializable]
    class CicatricesData
    {
        public List<GameObject> InventaireList;
    }
}
