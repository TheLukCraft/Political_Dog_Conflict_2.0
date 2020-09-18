using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    GameObject BlockPrefab;

    [SerializeField]
    [Range(3, 8)]
    int Width = 5;

    [SerializeField]
    [Range(3, 10)]
    int Height = 6;

    [SerializeField]
    [Range(0.5f, 5f)]
    float GridSize = 2f;

    [SerializeField]
    [Range(0.5f, 2f)]
    float BlockSize = 1f;


    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateBoard()
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                GenerateBlock(x, y);
    }

    private void GenerateBlock(int x, int y)
    {
        var obj = Instantiate(BlockPrefab);
        obj.transform.parent = transform;
        obj.transform.localPosition = GetBlockPosition(x,y);
        obj.transform.localScale = Vector3.one * BlockSize;
    }

    public Vector2 GetBlockPosition(int x, int y)
    {
        var basePosition = new Vector2(
            x - Width /2f +0.5f,
            y - Height /2f + 0.5f);
        return basePosition * GridSize;
    }
}
