using System.IO;
using UnityEngine;

public class Config : MonoBehaviour {
    public static Config instance;

    private string filePath;
    private GameSettings settings;
    
    #region Unity Methods
    private void Awake( )
    {
        if ( instance == null )
        {
            instance = this;

            filePath = Path.Combine( Application.persistentDataPath, "gameSettings.json" );
            Debug.Log(filePath);
            Load( );
            Save( );

            // save this object
            DontDestroyOnLoad( gameObject );
        }
    }
    #endregion

    public object GetSettings( string settingName )
    {
        var field = typeof( GameSettings ).GetField( settingName );
        return field != null ? field.GetValue( settings ) : null;
    }

    public void SetSettings( string settingName, object value )
    {
        var field = typeof( GameSettings ).GetField( settingName );
        if ( field != null && field.FieldType == value.GetType( ) )
        {
            field.SetValue( settings, value );
            Save( );
        }
    }

    public void Save( ) => File.WriteAllText( filePath, JsonUtility.ToJson( settings, true ) );

    public void Load( ) => settings = File.Exists( filePath )
        ? JsonUtility.FromJson< GameSettings >( File.ReadAllText( filePath ) )
        : new GameSettings( );


    // why inspector such... such... i wanna cry ;(
    public void SetFloat( string settingName, float value ) => SetSettings( settingName, value );
    public void SetInt( string settingName, int value ) => SetSettings( settingName, value );
    public void SetBool( string settingName, bool value ) => SetSettings( settingName, value );


    // if the inspector such crap, I'll shit-hardcore it. )))))
    // inspector doesn't accept more than one argument
    // let's hardcode )))
    public void SetBoolFalse( string settingName ) => SetSettings( settingName, false );
    public void SetBoolTrue( string settingName ) => SetSettings( settingName, true );


}

[ System.Serializable ]
public class GameSettings
{

    // settings game, like graphics and etc...
    public int iResolutionWidth = 1920;
    public int iResolutionHeight = 1080;
    public bool bIsFullscreen = true;

    public float iMasterVolume = 100;
    public float iMusicVolume = 100;

    public float fMouseSensitivity = 1.0f;




    // progress of game, like key's or something...
    public bool bPrologueEnd = false;

}