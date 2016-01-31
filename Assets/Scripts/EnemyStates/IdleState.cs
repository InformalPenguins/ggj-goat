using UnityEngine;
using System.Collections;
using System;

public class IdleState : IEnemyState
{
    private MyEnemy enemy;

    public void Enter(MyEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("Idling");
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
    }

    private void Idle()
    {
        // enemy;
    }
}
