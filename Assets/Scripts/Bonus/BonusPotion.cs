using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPotion : MonoBehaviour,IDestroyable
{
    public float m_life;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EventManager.Instance.Raise(new PotionTriggerEvent() { life = m_life });
            Kill();
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }

    public void Damage()
    {
        throw new System.NotImplementedException();
    }
}
