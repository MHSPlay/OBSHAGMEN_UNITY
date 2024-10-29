using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public GameObject esc_menu_panel;
    public GameObject settings_menu_panel;
    public Slider sensitivitySlider;
    public TMP_Text sensitivityText;

    private float mouseSensitivity;
    private bool isPaused = false;
    private enum MenuState { none, esc, settings }
    private MenuState currentMenuState = MenuState.esc;

    void Start( ) {
        mouseSensitivity = ( float )Config.instance.GetSettings( "fMouseSensitivity" );
        UpdateSensitivityText( );

        if ( sensitivitySlider != null ) {
            sensitivitySlider.value = mouseSensitivity;
            sensitivitySlider.onValueChanged.AddListener( OnSensitivityChanged );
        }

        // hide the cannabis <3
        esc_menu_panel.SetActive( false );
        settings_menu_panel.SetActive( false );
    }

    void Update( ) {
        if ( Input.GetKeyDown( KeyCode.Escape ) )
            ToggleMenu( );
    }

    public void ToggleMenu( ) {
        isPaused = !isPaused;

        if ( isPaused )
            UpdateMenuState( MenuState.esc );
        else
            UpdateMenuState( MenuState.none );
        
        // faceless void put the phronosphere on us
        Time.timeScale = isPaused ? 0 : 1;

        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    private void UpdateMenuState( MenuState newState ) {
        esc_menu_panel.SetActive( false );
        settings_menu_panel.SetActive( false );

        switch ( newState ) {
            case MenuState.esc:
                esc_menu_panel.SetActive( true );
                break;
            case MenuState.settings:
                settings_menu_panel.SetActive( true );
                break;
            case MenuState.none:
                isPaused = false;
                break;
        }

        currentMenuState = newState;
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

    public void OnSettingPressed( ) => UpdateMenuState( MenuState.settings );
    public void BackToMainMenu( ) => UpdateMenuState( MenuState.esc );
    public bool IsPaused( ) => isPaused;
    public void OnExit( ) => Application.Quit( );
}
