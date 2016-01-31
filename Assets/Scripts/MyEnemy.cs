using UnityEngine;

public class MyEnemy : Character
{

    private IEnemyState currentState;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
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

    public void Move()
    {
        MyAnimator.SetFloat("speed", 1);

        transform.Translate(GetDirection() * movementSpeed * Time.deltaTime);
    }

    public Vector2 GetDirection()
    {
        return facingLeft ? Vector2.left : Vector2.right;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
    }

}
