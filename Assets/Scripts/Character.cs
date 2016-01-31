using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {

    protected Animator myAnimator;

    [SerializeField]
    protected float movementSpeed;

    protected bool facingLeft;

    // Use this for initialization
    public virtual void Start () {
        myAnimator = GetComponent<Animator>();
        facingLeft = true;
    }

    // Update is called once per frame
    void Update () {
	
	}

    protected void changeDirection()
    {
        facingLeft = !facingLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }
}
