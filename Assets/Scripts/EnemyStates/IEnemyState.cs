using UnityEngine;

public interface IEnemyState
{

    void Execute();

    void Enter(MyEnemy enemy);

    void Exit();

    void OnTriggerEnter(Collider2D other);

}
