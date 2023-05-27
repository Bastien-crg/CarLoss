using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class BonusJetPack : MonoBehaviour, IDestroyable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EventManager.Instance.Raise(new JetPackTriggerEvent());
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