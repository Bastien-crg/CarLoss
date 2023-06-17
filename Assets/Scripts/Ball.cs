using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class Ball : MonoBehaviour
{
    [SerializeField] LayerMask m_ColorizableLayerMask;
    private int ball_damage = 1;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetType() == typeof(SphereCollider))
        {
            EventManager.Instance.Raise(new EnemyHasBeenHitEvent() { eEnemy = collision.gameObject, damage = ball_damage * 10 });
        }
        else
        {
            EventManager.Instance.Raise(new EnemyHasBeenHitEvent() { eEnemy = collision.gameObject, damage = ball_damage });
        }
        
    }
}
