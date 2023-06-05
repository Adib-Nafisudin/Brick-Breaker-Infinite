using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickLoader : MonoBehaviour
{
    //🅱️rick Setup
    public GameObject[] BrickPrefabs;
    public float SizeX = 2; 
    public float SizeY = 1;
    public float RowSpawn = 5;
    public int ColumnLength = 8;
    private int BrickCounter = 0;
    public bool SpaceAround = false;

    //UI
    public Text ScoreText;
    private float Score = 0;
    
    //Win Condition UI
    public GameObject MenuManager;
    private GameObject WasteBrick;

    // Brick Loader Function
    public void BrickSpawner(float Column, float Row){

            float ColumnSpawn = Column * -1;
            while (ColumnSpawn <= Column)
            {
                GameObject Brick = GameObject.Instantiate<GameObject> (BrickPrefabs[0]);
                Brick.transform.SetParent(this.transform);
                Brick.transform.position = new Vector2 (ColumnSpawn,Row);
                BrickCounter++;
                ColumnSpawn += SizeX;
            }
    }
    // Score Function
    public void ScoreCounter()
    {
        float BrickScore = PointRounder(BrickCounter);
        Score += BrickScore;
        ScoreText.text = ""+ Score;
    }
    public float PointRounder(int Floorer){
        float Point = Mathf.Floor(100/Floorer);
        return Point;
    }
    // Start is called before the first frame update
    void Start()
    {
        while (RowSpawn >= 0)
        {
            int RandomColumn = Random.Range(1,ColumnLength);
            BrickSpawner(RandomColumn, RowSpawn);
            RowSpawn -= SizeY;
        }
    }
    public void NextStart(){
        RowSpawn = 5;
        while (RowSpawn >= 0)
        {
            int RandomColumn = Random.Range(1,ColumnLength + 1);
            BrickSpawner(RandomColumn, RowSpawn);
            RowSpawn -= SizeY;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            Debug.Log("Level Done!");
            Win();
        }
    }
    public void Win(){
        MenuManager.gameObject.GetComponent<MenuScript>().WinGame();
        BrickCounter = 0;

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
