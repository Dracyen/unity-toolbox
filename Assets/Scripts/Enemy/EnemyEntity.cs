using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    protected int Health;

    public virtual void DamageHealth(int damage) { }

    public virtual void Kill() { }
}
