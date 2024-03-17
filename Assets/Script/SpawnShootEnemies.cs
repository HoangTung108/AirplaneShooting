using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShootEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 0.5f;
    public static GameObject clone;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        rb.velocity = Vector3.down * speed ;
        

    }
    void OnTriggerEnter2D(Collider2D other){
        Movement player = other.GetComponent<Movement>();
        if (player !=null){
            player.TakeDame(1);
            Destroy(gameObject);
        
        }
    
           
    }
}
