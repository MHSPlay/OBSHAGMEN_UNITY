using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour {
    public GameObject menuPanel;
    public Slider sensitivitySlider;
    public TMP_Text sensitivityText;
    public Character _characterController;
    public float defaultSensitivity = 1f;

    private float mouseSensitivity;
    private bool isPaused = false;

    void Start( ) {
        mouseSensitivity = defaultSensitivity;
        UpdateSensitivityText( );

        if ( sensitivitySlider != null ) {
            sensitivitySlider.value = mouseSensitivity;
            sensitivitySlider.onValueChanged.AddListener( OnSensitivityChanged );
        }

        // we don't need menu now
        menuPanel.SetActive( false );
    }

    void Update( ) {
        if ( Input.GetKeyDown( KeyCode.Escape ) )
            ToggleMenu( );
    }

    void ToggleMenu( ) {
        isPaused = !isPaused;
        
        menuPanel.SetActive( isPaused );

        // Faceless Void put the chronosphere on us 
        Time.timeScale = isPaused ? 0 : 1;
        
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    void OnSensitivityChanged( float newSensitivity ) {
        mouseSensitivity = newSensitivity;
        UpdateSensitivityText( );

        if ( _characterController != null )
            _characterController.SetMouseSensitivity( mouseSensitivity );
    }

    void UpdateSensitivityText( ) {
        if ( sensitivityText != null ) 
            sensitivityText.text = "sensitivity: " + mouseSensitivity.ToString( "F1" );
    }

    public float GetMouseSensitivity( ) => mouseSensitivity;
    public bool IsPaused( ) => isPaused;
}
