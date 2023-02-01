using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackForAllChar : MonoBehaviour, IAttackable
{
    public bool contactEnemy { get; set; }
    public Transform enemyTransform { get; set; }

    void Start()
    {
        enemyTransform = transform;
    }
}
