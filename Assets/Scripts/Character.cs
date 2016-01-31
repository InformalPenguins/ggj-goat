using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour {
    [SerializeField]
    protected float movementSpeed;

    protected bool facingLeft;

    public Animator MyAnimator { get; set; }

    // Use this for initialization
    public virtual void Start () {
        MyAnimator = GetComponent<Animator>();
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
