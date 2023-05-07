using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDestroyable
{
    public NavMeshAgent agent;

    public Vector3 player_position;
    //public Transform player;

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player_position);
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
        throw new System.NotImplementedException();
    }

    public void Damage()
    {
        throw new System.NotImplementedException();
    }
}
