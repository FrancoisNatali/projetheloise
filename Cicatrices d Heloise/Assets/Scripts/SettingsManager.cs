using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    //Les variables pour les paramètres
    public bool fullscreen;
    public int resolutionIndex;
    public float VolumeMusique;
    public float VolumeSFX;

    //Les objets qui controlent les variables paramètres
    [SerializeField]
    private Toggle fullScreenToggle;
    [SerializeField]
    private Dropdown resolutionDropdown;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Slider SFXVolumeSlider;
    [SerializeField]
    private Button ApplyButton;

    //Le tableau des résolutions disponibles
    private Resolution[] resolutions;

    //Juste pour l'exemple on met un audiosource ICI mais il sera sur l'audioManager, il faudra juste y accéder dans l'Awake ou l'OnEnable
    public AudioSource testAudioSourceMUSIC;
    public AudioSource testAudioSourceSFX;

    
    void OnEnable ()
    {
        //L'équivalent de Drag & Drop dans l'inspector
        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicValue(); });
        SFXVolumeSlider.onValueChanged.AddListener(delegate { OnSFXValue(); });
        ApplyButton.onClick.AddListener(delegate { OnApplyButton(); });

        resolutions = Screen.resolutions;
        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        LoadSettings();
    }

    public void OnFullScreenToggle()
    {
        Screen.fullScreen = fullScreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        //AUTRE SCRIPT CONTENANT UNIQUEMENT LES VARIABLES.reloutionIndex = resolutionDropdown.value;
    }

    public void OnMusicValue()
    {
        testAudioSourceMUSIC.volume = musicVolumeSlider.value;
    }

    public void OnSFXValue()
    {
        testAudioSourceSFX.volume = SFXVolumeSlider.value;
    }

    void OnApplyButton()
    {
        SaveSettings();
    }

    void SaveSettings()
    {
        //Inclure le save dans un nouveau fichier (pas crypté)

        //string jsonData = JsonUtility.ToJson(AUTRE SCRIPT CONTENANT UNIQUEMENT LES VARIABLES, true);
        //File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    void LoadSettings()
    {
        //Load le doc json

        //AUTRE SCRIPT CONTENANT UNIQUEMENT LES VARIABLES = JsonUtility.FromJson< AUTRE SCRIPT CONTENANT UNIQUEMENT LES VARIABLES > (File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        //musicVolumeSlider.value = AUTRE SCRIPT CONTENANT UNIQUEMENT LES VARIABLES.musicVolume;
        //fullScreenToggle.isOn = AUTRE SCRIPT CONTENANT UNIQUEMENT LES VARIABLES.fullscreen;

        //PUIS FAIRE PAREIL POUR TOUTES LES VALEURS

        //resolutionDropdown.RefreshShownValue();
    }
}
