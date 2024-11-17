using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour, IHealth
{
    [SerializeField] private int HP = 5;

    public void Damage(int Health)
    {
        HP -= Health;

        if (HP <= 0)
            Death();
    }

    private void Death()
    {
        Destroy(gameObject);
    }

}
