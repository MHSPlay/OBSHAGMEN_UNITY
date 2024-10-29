using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] public UnityEvent onInteract;
    [SerializeField] public UnityEvent onCursorEnter;
    [SerializeField] public UnityEvent onCursorExit;

    private void Start()
    {
        gameObject.layer = 3;
    }
}
