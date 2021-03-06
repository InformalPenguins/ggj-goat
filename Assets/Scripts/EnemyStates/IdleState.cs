﻿using UnityEngine;

public class IdleState : IEnemyState
{

    private MyEnemy enemy;

    private float idleTimer;

    private float idleDuration = 5;

    public void Enter(MyEnemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Idle();
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
    }

    private void Idle()
    {
        enemy.MyAnimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

}
