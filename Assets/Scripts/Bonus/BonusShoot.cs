using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class BonusShoot : MonoBehaviour, IDestroyable
{
    public float m_shoot_cooldown;
    public float m_bonus_time;
    public void Damage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EventManager.Instance.Raise(new ShootBonusTriggerEvent() { bonus_time = m_bonus_time, cooldown = m_shoot_cooldown });
            Kill();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}