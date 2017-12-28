using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Vector2 direction;

    protected Rigidbody2D myRigidBody;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }
}
