using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    protected float speed = 2;

    protected Vector2 direction;

    protected Rigidbody2D myRigidBody;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        
	}

    void FixedUpdate()
    {
        
    }

}
