using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class board : MonoBehaviour
{
    public int Width;
    public int Height;
    public GameObject[] dots;
    public BackgroundTile[,] AllTiles;
    public GameObject TilePrefab;
    public GameObject[,] AllDots;

    // Start is called before the first frame update
    void Start()
    {
        //AllTiles = new BackgroundTile[Width,Height];
        AllDots = new GameObject[Width,Height];
        Setup();
    }

    void Setup()
    {
        
        for(int i= 0; i< Width; i++)
        {
            for(int j = 0; j< Height; j++)
            {
                int DotToUse = Random.Range(0, dots.Length);
                Vector2 TempPosition = new Vector2(i, j);
                GameObject tiles = Instantiate(TilePrefab, TempPosition, Quaternion.identity);
                GameObject dot = Instantiate(dots[DotToUse], TempPosition, Quaternion.identity);
                dot.transform.parent = this.transform;
                dot.name = "( " + i + " , " + j + " ) ";
                tiles.transform.parent = this.transform;
                tiles.name = " T " + "( " + i + " , " + j + " ) ";
                AllDots[i, j] = dot;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
