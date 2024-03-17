using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataEnemies : MonoBehaviour
{
  
    public BoxCollider2D collider;
    public static float width_enemies;
    public static float height_enemies;
    public float Health_enemies;
    public GameObject bullet;
    public Transform spawn;
     private float speed = 0.5f;
    public float minFireRate = 2f;
    public float maxFireRate = 5f;
    private float nextFireTime;
    public static bool CanShootEnemies; 
    bool check;
    bool Check;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
         width_enemies = collider.size.x;
         height_enemies = collider.size.y;
         Check = true;
          check = true;
         Health_enemies = 5f;
         CanShootEnemies = false;
          nextFireTime = Time.time + Random.Range(minFireRate, maxFireRate);
    }
    void Update(){
        Move();
        if (Time.time >= nextFireTime && CanShootEnemies)
        {
            Shoot();
            nextFireTime = Time.time + Random.Range(minFireRate, maxFireRate);
        }
        
       
      
    }
    void Shoot()
    {
        if (Check)
        {
            CreateClone();
            Check = false;
            StartCoroutine(Clone());
        }
    }

    void CreateClone()
    {
        GameObject clone = Instantiate(bullet, spawn.position, Quaternion.identity);
        Destroy(clone, 2f); 
    }
    IEnumerator Clone()
    {
        yield return new WaitForSeconds(4f);
        Check = true;
    }

    void Move(){
        if(check){
            for (int i=0; i<4; i++){
                for (int j =0; j<4; j++){
                transform.Translate(Vector2.down *speed *Time.deltaTime);
             if (Movement.listEnemies[i,j].transform.position.y < -(3.6f)){
                check = false;
               
            }
                }
            }
        
        }
      
    } 


    public void GetDame(float damage){
        Health_enemies -=damage;
        if (Health_enemies <=0){
            gameObject.SetActive(false);
            Movement.score +=1f; 
        }
    }
  
}
