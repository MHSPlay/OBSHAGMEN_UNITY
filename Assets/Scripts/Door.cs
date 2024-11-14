using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
[RequireComponent(typeof(SceneSwitcher))]
public class Door : MonoBehaviour
{
    [SerializeField] private int lockIndex = 0;
    public bool isLocked = true;

    SceneSwitcher _sceneSwitcher;

    private void Start()
    {
        _sceneSwitcher = GetComponent<SceneSwitcher>();
    }

    public void OpenDoor()
    {
        if (Character.Instance.inventory.keys.Any(x => x == lockIndex))
        {
            isLocked = false;
        }
    }

    public void OpenDoorWithSceneSwitcher( string sceneName ) 
    {

        if (Character.Instance.inventory.keys.Any(x => x == lockIndex))
        {
            isLocked = false;
            _sceneSwitcher.LoadSceneByName( sceneName );
        }

    }


}
