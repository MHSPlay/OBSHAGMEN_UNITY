using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[ RequireComponent( typeof( Config ) ) ]
[ RequireComponent( typeof( SceneSwitcher ) ) ]
public class GameLoop : MonoBehaviour {
    public static GameLoop instance;

    #region Unity Methods
    private void Awake( )
    {
        if ( instance == null ) 
        { 
            instance = this;

            // save this object on next scene
            DontDestroyOnLoad( gameObject );
        }
        else
            Destroy( gameObject );
    }

    private void Start( )
    {
        // start title when game ready
        TitleScreen.instance.StartTitle( );

    }

    private void Update( )
    {
       



    }

    private void FixedUpdate( ) 
    {
        
    }

    private void LateUpdate( )
    {
        
    }
    #endregion

}
