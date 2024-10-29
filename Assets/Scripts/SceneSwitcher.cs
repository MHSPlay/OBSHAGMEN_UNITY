using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    public static SceneSwitcher instance;

    private void Awake( ) 
    {
        if ( instance == null ) 
            instance = this;
    }

    public void LoadSceneByName( string sceneName ) => SceneManager.LoadScene( sceneName );
    public void LoadSceneByID( int sceneID ) => SceneManager.LoadScene( sceneID );

    public AsyncOperation LoadSceneAsyncByName( string sceneName ) => SceneManager.LoadSceneAsync( sceneName );
    public AsyncOperation LoadSceneAsyncByID( int sceneID ) => SceneManager.LoadSceneAsync( sceneID );
}
