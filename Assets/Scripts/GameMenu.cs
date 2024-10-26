using UnityEngine;

public class GameMenu : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject SettingsPanel;

    static bool InSettings = false;

    #region Unity Methods
    private void Start( ) {
        

        

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
    
    public void PressedOnSettings( ) => InSettings = true;

}
