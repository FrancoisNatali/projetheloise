using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Le Menu load
    [SerializeField]
    private GameObject MenuLoad;

    //Pour le load
    private AsyncOperation async;

    //Pour choisir la Scène désirée
    private int choiceScene;

    //Image qui stock tous les infos et voir si on ajoute des bonus
    [SerializeField]
    private GameObject PlusInfosImage;

	
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnCancelButton();
        }
	}

    //Charger la scène en arrière plan
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
                    async = SceneManager.LoadSceneAsync("Scene2");
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

    //Lancer une nouvelle partie
    public void OnNewGameButton()
    {
        choiceScene = 1;
        StartCoroutine(LoadLaScene());
    }

    //Démarrer le menu Load
    public void OnLoadButton()
    {
        MenuLoad.SetActive(true);
    }

    //Pour voir nom des développeurs et Bonus
    public void OnPlusInfosButton()
    {
        PlusInfosImage.SetActive(true);
    }

    //Bouton retour
    public void OnCancelButton()
    {
        PlusInfosImage.SetActive(false);
        MenuLoad.SetActive(false);
    }

    //Pour quitter le jeu
    public void OnQuitterButton()
    {
        Application.Quit();
    }
}
