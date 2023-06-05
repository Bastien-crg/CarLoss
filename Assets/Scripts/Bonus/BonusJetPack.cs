using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class BonusJetPack : MonoBehaviour, IDestroyable
{
    public int m_Fuel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EventManager.Instance.Raise(new JetPackTriggerEvent() { fuel = m_Fuel });
            Kill();
        }
    }



    public void Kill()
    {
        gameObject.SetActive(false);
    }

    public void Damage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
