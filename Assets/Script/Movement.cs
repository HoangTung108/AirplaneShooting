using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{  
    public Text win;
    public Text lose;
    public GameObject Win;
    public GameObject Lose;
    public Text heath;
    public  Text point;
    public static float score;
    public Rigidbody2D rb;
    public GameObject armor;
    public GameObject Enemy;
    Vector3 SpawnEnemy;
    public BoxCollider2D collider;
    public Transform spawn;
    public static GameObject[,] listEnemies = new GameObject[4, 4];
 
    bool Check;
    [SerializeField] private float Speed;
    Vector3 Pos;
    Vector3[,] listPos = new Vector3[9,5];
    float move =0.004f;
    Vector3 pointB;
    Vector3 normalY ;
    Vector3 normalX;
     Vector3 pointA ;
    bool check1;
    bool check2;
    bool CanShoot;
    float Health_Player =10f;
    Vector3[]positions = new Vector3[16];
    Vector3[] positionSquare = new Vector3[16];


    void Transition()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * Speed * Time.deltaTime);
        }
        CheckPosition();
      
       
    }

    void CheckPosition()
    {

        if (transform.position.x < -4f)
        {
            transform.position = new Vector3(-4f, transform.position.y, 0);
        }
        if (transform.position.x > 4f)
        {
            transform.position = new Vector3(4f, transform.position.y, 0);
        }
        if (transform.position.y < -8.5f)
        {
            transform.position = new Vector3(transform.position.x, -8.5f, 0);
        }
        if (transform.position.y > -1f)
        {
            transform.position = new Vector3(transform.position.x, -1f, 0);
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && Check)
        {
            CreateClone();
            Check = false;
            StartCoroutine(Clone());
        }
    }

    void CreateClone()
    {
        GameObject clone = Instantiate(armor, spawn.position, Quaternion.identity);
        Destroy(clone, 2f); 
    }
    IEnumerator Clone()
    {
        yield return new WaitForSeconds(0.1f);
        Check = true;
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Pos = new Vector3(i, j, 0);
                listEnemies[i, j] = Instantiate(Enemy, SpawnEnemy + Pos , transform.rotation);
            }

        }
     
    }
     void DrawEmptyTriangle()
    {
        float a =0.65f;
        float b = 6f;
        for(int i = 0; i < positions.Length; i++){
                    positions[i] = new Vector3(-4+i,Vector3.Reflect(pointB,normalY).y,0);
                    if (i >8 && i<12){
                    positions[i] = new Vector3(i-b,Vector3.Reflect(pointB,normalY).y +a,0);
                    a = a +0.9f;
                    b = b +2f;
                    }if(i>=12) {
                        positions[i] = new Vector3(i-b,Vector3.Reflect(pointB,normalY).y +a,0);
                            a = a - 0.9f;
                            b = b +2f;
                    }
        }
    }
    IEnumerator Create(Vector3[] pos){
        int index=0;
        yield return new WaitForSeconds(5);
        for(int i=0;i<4;i++){
            for(int j =0; j <4;j++){
                Vector3 Pos = new Vector3(listEnemies[i,j].transform.position.x,listEnemies[i,j].transform.position.y,0);
                listEnemies[i,j].transform.position = Vector3.Slerp(Pos,pos[index],move);
                index ++;
            }
        }
        DataEnemies.CanShootEnemies = true;
        
          check2=false;
    }
    IEnumerator Goto(Vector3[] pos){
        int index=0;
        yield return new WaitForSeconds(5);
        for(int i=0;i<4;i++){
            for(int j =0; j <4;j++){
                Vector3 Pos = new Vector3(listEnemies[i,j].transform.position.x,listEnemies[i,j].transform.position.y,0);
                listEnemies[i,j].transform.position = Vector3.Slerp(Pos,pos[index],move);
                index ++;
            }
        }
          CanShoot = true;
    }
    void ShapeSquare(){
        float a =0.65f;
        float b = 1f;
        for(int i = 0; i < positionSquare.Length; i++){
                positionSquare[i] = new Vector3(-3+i,Vector3.Reflect(pointB,normalY).y+a,0);
                if(i >6 && i <=13){
                    positionSquare[i] = new Vector3(-3+i-b,Vector3.Reflect(pointB,normalY).y+a+1.8f,0);
                    b+=2;
                }
                else if (i >13){
                    positionSquare[i] = new Vector3(-3+i+1-b,Vector3.Reflect(pointB,normalY).y+a+0.9f,0);
                    b -=5;
                }
        }
    }

    void ShapeMaking()
    {
        pointA = new Vector3(listEnemies[0, 2].transform.position.x - DataEnemies.width_enemies, listEnemies[0, 2].transform.position.y, 0);
        normalX = new Vector3(1f,0,0);
        normalY = new Vector3(0,pointA.y-1.45f,0);
        pointB = new Vector3(0,listEnemies[0, 3].transform.position.y+ 0.8f ,0);
        listEnemies[0,0].transform.position = Vector3.Slerp(new Vector3(listEnemies[0,0].transform.position.x,listEnemies[0,0].transform.position.y,0), pointA, move);
        listEnemies[1,0].transform.position = Vector3.Slerp(new Vector3(listEnemies[1,0].transform.position.x,listEnemies[1,0].transform.position.y,0), Vector3.Reflect(pointA,normalX), move);
        listEnemies[2,0].transform.position = Vector3.Slerp(new Vector3(listEnemies[2,0].transform.position.x,listEnemies[2,0].transform.position.y,0), pointB, move);
        listEnemies[3,0].transform.position = Vector3.Slerp(new Vector3(listEnemies[3,0].transform.position.x,listEnemies[3,0].transform.position.y,0), Vector3.Reflect(pointB,normalY), move);
        check1 = false;
        DrawEmptyTriangle();
        ShapeSquare();
      
    
    }
    void GotoSquare(){
        StartCoroutine(Goto(positionSquare));
    }
    void GotoTriangle(){
        StartCoroutine(Create(positions));
    }
    void ShowText(){
        heath.text = "Health: "+ ((int)Health_Player).ToString();
        point.text = "Score: "+ ((int)score).ToString();
        win.text = "YOU WIN \n" + "Your Score: "+((int)score).ToString();
        lose.text = "YOU LOSE \n" + "Your Score: "+((int)score).ToString();
    }

    void Start()
    {
         transform.position = new Vector3(0, -8.5f, 0);
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        SpawnEnemy = new Vector3(-1.5f,10, 0);
        Check = true;
        check1 = true;
        CanShoot = false;
        score = 0;
        Transfer.check = true;
        SpawnEnemies();
           Win.SetActive(false);
        Lose.SetActive(false);
    }

    void Update()
    {
        WinGame();
        ShowText();
        if(CanShoot){
             Shoot();
        }
        Transition();
        if(check1){
            Invoke("ShapeMaking",5);
            check2 = true;
        }
       if (check2){

        Invoke("GotoTriangle",5);
      
       }
       else{
        Invoke("GotoSquare",5);
    }
    }
    void WinGame(){
        if(score >=16){
            Win.SetActive(true);
              Time.timeScale = 0f;
        }
    }
    public void TakeDame(float dame){
        Health_Player -=dame;
        if (Health_Player <=0){
            Lose.SetActive(true);
            Destroy(gameObject);
        
              Time.timeScale = 0f;
        }
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag =="Enemies"){
            Health_Player -=1;
            if (Health_Player <=0){
            Lose.SetActive(true);
            Destroy(gameObject);
        
              Time.timeScale = 0f;
        }
        }
    }
}

