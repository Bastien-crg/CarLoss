using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDestroyable
{
    public NavMeshAgent agent;

    //La vie de l'ennemi
    [SerializeField] int Health = 2;

    //Le slider de vie
    public Slider helthBar;

    public Vector3 player_position;
    //public Transform player;

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player_position);

        // mise à jour de bar de vie
        helthBar.value = Health;

    }
    
    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<EmitPositionEvent>(GetPlayerPosition);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<EmitPositionEvent>(GetPlayerPosition);
    }

    void OnEnable()
    {
        SubscribeEvents();
    }
    void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void GetPlayerPosition(EmitPositionEvent e)
    {
        player_position = e.position;
    }

    public void Kill()
    {
        Destroy(gameObject);
        //throw new System.NotImplementedException();
    }

    public void Damage()
    {
        Health--;
        if (Health <= 0)
        {
            Kill();
        }
        //throw new System.NotImplementedException();
    }
}
