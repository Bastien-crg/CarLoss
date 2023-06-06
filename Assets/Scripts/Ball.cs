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
        // IDENTIFICATION PAR NAME
        //if(collision.gameObject.name.Equals("Cube"))
        //if (collision.gameObject.name.ToUpper().Contains("CUBE"))

        // IDENTIFICATION PAR TAG
        //if(collision.gameObject.CompareTag("Colorizable"))

        // IDENTIFICATION PAR LAYER
        //if((m_ColorizableLayerMask.value & (1<<collision.gameObject.layer))!=0)

        // IDENTIFICATION PAR COMPONENT TAG
        // if (null != collision.gameObject.GetComponent<ColorizableTag>())

        //IDENTIFICATION FONCTIONNELLE PAR INTERFACE
        if (collision.collider.GetType() == typeof(SphereCollider))
        {
            EventManager.Instance.Raise(new EnemyHasBeenHitEvent() { eEnemy = collision.gameObject, damage = ball_damage*10 });
        } else {
            EventManager.Instance.Raise(new EnemyHasBeenHitEvent() { eEnemy = collision.gameObject, damage = ball_damage });
        }
    }
}
