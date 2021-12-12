using System.Collections;
using System.Collections.Generic;
using TargetSystem;
using UnityEngine;

public class Tree : MonoBehaviour, IInteractionObject
{
    private float HP = 50;
    public void Fell(float strong)
    {
        HP -= strong;
        if (HP < 0)
        {
            Destroy(gameObject);
        }
    }

    public void IdleUse()
    {
        Debug.Log("Idle");
    }
}
