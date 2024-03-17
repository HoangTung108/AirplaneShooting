using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transfer : MonoBehaviour
{
    public BoxCollider2D collider;
    public Rigidbody2D rb;
    public static float width_size;
    public static float height_size;
    private float height;
    public static bool check;
    private float scrollspeed = -5f;
    
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        height = collider.size.y * 1.1f;
        width_size = collider.size.x;
        height_size = collider.size.y;
 
    }

    void Move()
    {
        rb.velocity = new Vector2(0, scrollspeed);
        Vector3 resetPosition = new Vector3(0, 0, 0);
        if (transform.position.y < -height)
        {
            resetPosition.y = height * 1.8f;
        }
        transform.position += resetPosition;
        
    }

    void Update()
    {
        if (check)
        {
            Move();
        }
    }
}
