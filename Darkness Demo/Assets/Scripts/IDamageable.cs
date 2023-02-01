using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int takedDamageValue, float WaitTime);
    float Health { get; set; }
}

public interface IAttackable
{
   bool contactEnemy { get; set; }
    Transform enemyTransform { get; set; }
}
