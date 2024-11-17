using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    void Damage(int Health);

    virtual void Heal(int Health)
    {

    }
}
