using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
public class Door : MonoBehaviour
{
    [SerializeField] private int lockIndex = 0;
    public bool isLocked = true;

    public void OpenDoor()
    {
        if (Character.Instance.inventory.keys.Any(x => x == lockIndex))
        {
            isLocked = false;
        }
    }
}
