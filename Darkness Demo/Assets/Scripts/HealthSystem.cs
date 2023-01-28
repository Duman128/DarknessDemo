using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    public float _health;
    public float Health { get; set; }

    private float time = 0;

    void Start()
    {
        Health = _health;
    }

    public void TakeDamage(int takedDamageValue, float waitTime)
    {
        if (Time.time > time)
        {
            time = Time.time + waitTime;
            Health -= takedDamageValue;

            if (Health <= 0)
            {
                if (gameObject.tag == "WalkEnemies")
                    StartCoroutine(DeathEnemy());
            }
        }
    }

    IEnumerator DeathEnemy()
    {
        GetComponent<PatrolAndDetect>().enabled = false;
        GetComponent<EnemyAnimation>().State = EnemyAnimation.AnimStates.Death;
        GetComponent<SpriteRenderer>().DOFade(0, 2);
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2.2f);
        Destroy(gameObject);
    }
}
