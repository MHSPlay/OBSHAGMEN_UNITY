using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int damage;
    private int range;
    private string name;

    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

    public void Hit()
    {
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            IHealth component = hit.collider.GetComponent<IHealth>();
            if (component != null)
            {
                component.Damage(damage);
            }
        }

    }
} 
