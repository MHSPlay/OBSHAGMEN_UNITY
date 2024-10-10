using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class TriggerArea : MonoBehaviour
{
    private void OnValidate()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public UnityEvent onEnter;
    public UnityEvent onExit;
    public UnityEvent onStay;

    private void OnTriggerEnter(Collider other)
    {
        onEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        onExit.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        onStay.Invoke();
    }
}
