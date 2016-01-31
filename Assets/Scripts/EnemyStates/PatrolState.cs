using UnityEngine;
using System.Collections;
using System;

public class PatrolState : IEnemyState
{
    private MyEnemy enemy;

    private float patrolTimer;

    private float patrolDuration = 5;

    public void Enter(MyEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Debug.Log("patrolling");
        Patrol();
        enemy.Move();
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
