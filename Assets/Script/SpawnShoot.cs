using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShoot : MonoBehaviour
{
    public BoxCollider2D collider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    public static GameObject clone;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }
    void Update(){
        rb.velocity = Vector2.up * speed ;

    }
    void OnTriggerEnter2D(Collider2D other){
        DataEnemies enemy = other.GetComponent<DataEnemies>();
      if(enemy){
         enemy.GetDame(1);
        Destroy(gameObject);
           
      }
           
    }
 
}
