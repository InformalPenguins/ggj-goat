using UnityEngine;
using System.Collections;

public class MyEnemy : Character
{

    private IEnemyState currentState;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        ChangeState(new IdleState());
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentState.Execute();
	}

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }
}
