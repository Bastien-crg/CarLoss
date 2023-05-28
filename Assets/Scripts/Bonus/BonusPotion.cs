using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPotion : MonoBehaviour,IDestroyable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("potion");
            EventManager.Instance.Raise(new PotionTriggerEvent());
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
