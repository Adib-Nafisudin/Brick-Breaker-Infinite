using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    // Variable Initiation
    public int Force;
    Rigidbody2D BallPhysics;
    public GameObject Paddle;
    public GameObject Brick;
    private Vector2 BallStart;
    private bool Magnet;

    //Ball Movement Function
    public void BallVector(float X, float Y){
        Vector2 direction = new Vector2 (X,Y).normalized;
        if (Magnet)
        {
            // BallPhysics.velocity = Vector2.MoveTowards(transform.position, direction, Mathf.Abs(Vector2.SqrMagnitude(direction)) * Time.deltaTime);
            BallPhysics.velocity = new Vector2(0,0);
            BallPhysics.AddForce (Vector2.MoveTowards(transform.position, direction, Mathf.Abs(Vector2.SqrMagnitude(direction))*Time.deltaTime)* 2);
        }
        else
        {
            BallPhysics.velocity = new Vector2(0,0);
            BallPhysics.AddForce (direction * Force * 2);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        BallStart = new Vector2 (transform.position.x, transform.position.y);
        BallPhysics = GetComponent<Rigidbody2D>();
        BallBehaviour();
    }
    // Collision
    private void OnCollisionEnter2D(Collision2D hit) {
        if (hit.gameObject.name == "Paddle")
        {
            float Angle = (transform.position.x - hit.transform.position.x)*5f;
            BallVector(Angle, BallPhysics.velocity.y);
        }
        if (hit.gameObject.tag == "Brick")
        {
            Brick.gameObject.GetComponent<BrickLoader>().ScoreCounter();
            Destroy(hit.gameObject);
        }
        if (hit.gameObject.name =="BottomWall")
        {
            Paddle.gameObject.GetComponent<PaddleController>().LoseHP();
            transform.position = BallStart;
            float RandomDirection = Random.Range(-1.5f , 1.5f);
            BallVector(RandomDirection,2);
        }
    }
    public void Restart(){
        transform.position = BallStart;
        BallBehaviour();
    }
    private void OnMouseDown()
    {
        BallBehaviour();
    }
    public Vector2 DirectionRandomizer(){
        float BallDirectionX = Random.Range(-4f,4f);
        float BallDirectionY = Random.Range(-4f,4f);
        Vector2 BallDirection = new Vector2(BallDirectionX,BallDirectionY);
        return BallDirection;
    }
    public void BallBehaviour(){
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                Vector2 StartDirection = DirectionRandomizer();
                BallVector(StartDirection.x,StartDirection.y);
                break;
            case 1:
                BallVector(0,-2);
                break;
            default:
                break;
        }
    }
    public void AddSpeed()
    {
        Force += 50;
        Debug.Log(Force);
    }
    // Update is called once per frame
    void Update()
    {
        PaddleController PaddleScript = Paddle.gameObject.GetComponent<PaddleController>();
        Vector2 PaddlePosition = PaddleScript.GetPaddlePosition();
        Vector2 BallToPaddle = new Vector2(PaddlePosition.x - transform.position.x, PaddlePosition.y - transform.position.y);
        Magnet = false;
        if (Input.GetMouseButton(0))
        {
            // Debug.Log("Mouse Button triggered");
            Magnet = true;
            BallVector(BallToPaddle.x,BallToPaddle.y);
        }
    }
}
