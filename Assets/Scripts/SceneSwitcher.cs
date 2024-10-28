using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour {
    
    public static SceneSwitcher instance;
    public bool titleWasShown = false;
    public GameObject titleScreen;
    public CanvasGroup titleImage;

    private void Awake( ) {

        if ( instance == null ) 
            instance = this;
       
    }

    private void Start( ) {
        if ( !titleWasShown )
            TitleScreen( );
    }

    // i think this is bullshit
    private IEnumerator ShowTitleScreen( ) {

        float timeElapsed = 0f;
        float fadeDuration = 1f;

        while ( timeElapsed < Settings.instance.GetTitleDisplayDuration( ) ) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        AsyncOperation asyncLoad = Settings.instance.HasBeenInObshaga( ) >= 1 
            ? SceneManager.LoadSceneAsync( "menu_scene" )
            : SceneManager.LoadSceneAsync( "menu_prologue" );
        asyncLoad.allowSceneActivation = false;

        float fadeElapsed = 0f;

        while ( fadeElapsed < fadeDuration ) {
            fadeElapsed += Time.deltaTime;
            titleImage.alpha = 1 - ( fadeElapsed / fadeDuration );
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;

        titleWasShown = true;

        Destroy( titleScreen );
    }

    public void LoadScene( string sceneName ) => SceneManager.LoadScene( sceneName );

    public void TitleScreen( ) => StartCoroutine( ShowTitleScreen( ) );


}
