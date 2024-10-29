using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public static TitleScreen instance;

    [ SerializeField ] private CanvasGroup titleImage;
    [ SerializeField ] private GameObject titleScreen;
    [ SerializeField ] private GameObject gameMenu;

    #region Unity Methods
    private void Awake( )
    {
        if ( instance == null )
            instance = this;
    }

    #endregion

    private IEnumerator ShowTitleScreen( ) 
    {
        float timeElapsed = 0f;
        const float fadeDuration = 1f;

        while ( timeElapsed < 5 ) 
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        AsyncOperation asyncLoad = ( bool )Config.instance.GetSettings( "bPrologueEnd" ) == true 
            ? SceneSwitcher.instance.LoadSceneAsyncByName( "menu_scene" )
            : SceneSwitcher.instance.LoadSceneAsyncByName( "menu_prologue" );

        asyncLoad.allowSceneActivation = false;

        float fadeElapsed = 0f;

        while ( fadeElapsed < fadeDuration ) 
        {
            fadeElapsed += Time.deltaTime;
            titleImage.alpha = 1 - ( fadeElapsed / fadeDuration );
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;

        gameMenu.SetActive( true );

        // suicide ;)
        Destroy( titleScreen );
    }

    public void StartTitle( ) => StartCoroutine( ShowTitleScreen( ) );

}
