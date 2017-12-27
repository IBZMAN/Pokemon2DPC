using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5;
    public int collectable = 0;
    Vector2 force;
    Rigidbody2D body;
   
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        body.velocity = new Vector2(moveVertical * speed, body.velocity.y);
        float moveHoriontal = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(moveHoriontal * speed, body.velocity.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tag = collision.gameObject.tag;

        if (tag == "collectable")
        {
            Destroy(collision.gameObject);
            collectable++;
        }
    }
}