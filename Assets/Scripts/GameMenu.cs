using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class GameMenu : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;

    public AudioMixer audioMixer;

    // UI settings
    private float mouseSensitivity;
    public Slider sensitivitySlider;
    public TMP_Text sensitivityText;

    private float currentMasterVolume;
    public Slider MasterVolumeSlider;
    public TMP_Text MasterVolumeText;

    private float currentMusicVolume;
    public Slider MusicVolumeSlider;
    public TMP_Text MusicVolumeText;






    static bool InSettings = false;

    #region Unity Methods

    private void Awake( ) {

    }

    private void Start( ) {

        mouseSensitivity = ( float )Config.instance.GetSettings( "fMouseSensitivity" );
        currentMasterVolume = ( float )Config.instance.GetSettings( "MasterVolume" );
        currentMusicVolume = ( float )Config.instance.GetSettings( "MusicVolume" );

        UpdateSensitivityText( );

        if ( sensitivitySlider != null ) 
        {
            sensitivitySlider.value = mouseSensitivity;
            sensitivitySlider.onValueChanged.AddListener( OnSensitivityChanged );
        }

        if ( MasterVolumeSlider != null )
        {
            MasterVolumeSlider.value = currentMasterVolume;
            MasterVolumeText.text = "MasterVolume: 1";
            MasterVolumeSlider.onValueChanged.AddListener( volume => OnVolumeChanged( "MasterVolume", "Master", volume, MasterVolumeText ) );
        }

        if ( MusicVolumeSlider != null )
        {
            MusicVolumeSlider.value = currentMusicVolume;
            MusicVolumeText.text = "MusicVolume: 1";
            MusicVolumeSlider.onValueChanged.AddListener( volume => OnVolumeChanged( "MusicVolume", "Music", volume, MusicVolumeText ) );
        }

        // reset by default
        MainMenuPanel.SetActive( true );
        SettingsPanel.SetActive( false );
    }

    private void Update( ) {
        OnSettings( );
    }
    #endregion

    void OnSettings( ) {

        if ( !InSettings ) 
            return;

        // toggle
        InSettings =!InSettings;

        // hide menu while in settings
        MainMenuPanel.SetActive( InSettings );  
        SettingsPanel.SetActive( !InSettings );

    }

    public void OnBack( ) {
        MainMenuPanel.SetActive( !InSettings );
        SettingsPanel.SetActive( InSettings );
    }

    void OnSensitivityChanged( float newSensitivity ) {
        Config.instance.SetSettings( "fMouseSensitivity", newSensitivity );
        UpdateSensitivityText( );
    }

    void UpdateSensitivityText( ) {
        mouseSensitivity = ( float )Config.instance.GetSettings( "fMouseSensitivity" );
        if ( sensitivityText != null )
            sensitivityText.text = "sensitivity: " + mouseSensitivity.ToString( "F1" );
    }

    void OnVolumeChanged( string settingName, string mixerName, float volume, TMP_Text text ) {
        Config.instance.SetSettings( settingName, volume );
        UpdateSliderText( settingName, mixerName, text );
    }

    void UpdateSliderText( string settingName, string mixerName, TMP_Text text ) 
    {
        float volume = ( float )Config.instance.GetSettings( settingName );
        float volumeInDb = Mathf.Log10( volume ) * 20;
        volumeInDb = Mathf.Clamp( volumeInDb, -80f, 0f );
        audioMixer.SetFloat( mixerName, volumeInDb );

        if ( text != null )
            text.text = $"{settingName}: {volume:F2}";
    }

    public void PressedOnSettings( ) => InSettings = true;
    public void OnExit( ) => Application.Quit( );
}
