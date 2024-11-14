using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int keyId;
    string description;
    public void pickUp()
    {
        Character.Instance.inventory.keys.Add( keyId );
        Destroy( gameObject );
    }

}
