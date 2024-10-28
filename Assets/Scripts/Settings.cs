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
        GetBeenInObshaga( );




        // . . .
    }

    #region menu
    
    static float titleDisplayDuration = 5f;
    public float GetTitleDisplayDuration( ) => titleDisplayDuration;

    static int beenInObshaga = 0;
    public int HasBeenInObshaga( ) => beenInObshaga;

    public void SetBeenInObshaga( int value ) {
        beenInObshaga = value;
        PlayerPrefs.SetInt( "beenInObshaga", beenInObshaga );
        PlayerPrefs.Save( );
    }

    private void GetBeenInObshaga( ) {
         if ( PlayerPrefs.HasKey( "beenInObshaga" ) )
            beenInObshaga = PlayerPrefs.GetInt( "beenInObshaga" );
         else
            beenInObshaga = 0;
    }

    #endregion

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
