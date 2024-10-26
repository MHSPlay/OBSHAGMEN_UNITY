using UnityEngine;

public class Settings : MonoBehaviour {

    public static Settings instance;

    private void Awake( ) {
        if ( instance == null ) {
            instance = this;
            LoadSettings( );
        }
    }
    
    private void LoadSettings( ) {
        LoadSensitivity( );





        // . . .
    }

#region sensitivity
    static float defaultSensitivity = 1f;
    float mouseSensitivity = defaultSensitivity;

    public void SetMouseSensitivity( float sensitivity ) {
        mouseSensitivity = sensitivity;
        PlayerPrefs.SetFloat( "MouseSensitivity", mouseSensitivity );
        PlayerPrefs.Save( );
    }

    private void LoadSensitivity( ) {
        if ( PlayerPrefs.HasKey( "MouseSensitivity" ) ) 
            mouseSensitivity = PlayerPrefs.GetFloat( "MouseSensitivity" );
         else 
            mouseSensitivity = defaultSensitivity;
    }

    public float GetMouseSensitivity( ) => mouseSensitivity;
    public float GetDefaultMouseSensitivity() => defaultSensitivity;
#endregion



}
