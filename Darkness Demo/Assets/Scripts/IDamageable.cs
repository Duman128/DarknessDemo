using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int takedDamageValue, float WaitTime);
    float Health { get; set; }
}
