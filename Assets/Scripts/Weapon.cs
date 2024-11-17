using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour, IPickUp
{
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private string m_name;

    public void Hit()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            IHealth component = hit.collider.GetComponent<IHealth>();
            if (component != null)
            {
                component.Damage(damage);
            }
        }
    }

    public void PickUp()
    {
        Character.Instance.inventory.weapons.Add(this);
        transform.parent = Character.Instance.weaponHolder.transform;
        transform.position = Character.Instance.weaponHolder.transform.position;
        gameObject.SetActive(false);
    }
} 
