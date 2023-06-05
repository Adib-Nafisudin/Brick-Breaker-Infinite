// using System.Net.Mime;
// using System;
// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaddleController : MonoBehaviour
{
    //Paddle Position
    public float PaddleY = -4.25f;
    
    //Movement Speed
    public float MoveSpeed = 10;

    //Limiter
    public float LeftLimit;
    public float RightLimit;
    
    //HP Counter
    public GameObject[] HPrefab;
    private GameObject HPobject;
    public GameObject Ball;

    //Menu UI
    public GameObject MenuManager;

    private int Life = 3;
    // Start is called before the first frame update
    void Start()
    {
        HealthPoint();
        HPobjectAssign(0);
    }
    public void HealthPoint(){
        float HPposition = -0.5f;
        for (var i = 0; i < Life; i++)
        {
            GameObject HP = GameObject.Instantiate<GameObject> (HPrefab[0]);
            HP.transform.SetParent(this.transform);
            HP.transform.position = new Vector2 (HPposition,PaddleY);
            HPposition += 0.5f;
        }
    }
    public void HPobjectAssign(int index){
        HPobject = this.gameObject.transform.GetChild(index).gameObject;
    }

    // Update is called once per frame
    // Paddle Move based on Mouse Position
    void Update()
    {
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float PaddleX = transform.position.x;
        Vector2 PaddleTargetPosition = new Vector2 (MousePosition.x, PaddleY);
        Vector2 PaddleCurrentPosition = new Vector2 (PaddleX, PaddleY);
        Vector2 direction = (PaddleTargetPosition - PaddleCurrentPosition);
        if (MousePosition.x < LeftLimit || MousePosition.x > RightLimit || Time.timeScale == 0)
        {
            if (MousePosition.x < LeftLimit )
            {
                // transform.position = new Vector2 (LeftLimit, PaddleY);
                transform.position = Vector2.MoveTowards(PaddleCurrentPosition, new Vector2(LeftLimit,PaddleY), Mathf.Abs(direction.x) * Time.deltaTime);
                // Debug.Log("Left Limit Reached");
            }
            if (MousePosition.x > RightLimit )
            {
                // transform.position = new Vector2 (RightLimit, PaddleY);
                transform.position = Vector2.MoveTowards(PaddleCurrentPosition, new Vector2(RightLimit,PaddleY), Mathf.Abs(direction.x) * Time.deltaTime);
                // Debug.Log("Right Limit Reached");
            }
            if (Time.timeScale == 0)
            {
                transform.position = PaddleCurrentPosition;
            }
        }else
        {
            // transform.position = new Vector2 (MousePosition.x, PaddleY);
            transform.position = Vector2.MoveTowards(PaddleCurrentPosition, PaddleTargetPosition, Mathf.Abs(direction.x) * Time.deltaTime);
        }


        // Smoothie
        // Debug.Log(Mathf.Abs(direction.x));

        
        //Life
        if (Life > 0)
        {
            if (Life == 2)
            {
                HPobjectAssign(1);
            }else
            {
                HPobjectAssign(0);
            }
        }
    }
    public void Restart(){
        transform.position = new Vector2 (0, PaddleY);
    }
    public void LoseHP(){
        Life --;
        Destroy(HPobject);
        if (Life <= 0)
        {
            Destroy(Ball);
            // Debug.Log("Game Over");
            MenuManager.gameObject.GetComponent<MenuScript>().GameOver();
        }
    }
    public Vector2 GetPaddlePosition(){
        Vector2 PaddlePosition = transform.position;
        return PaddlePosition;
    }
}
