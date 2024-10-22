﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockConnection : MonoBehaviour
{
    List<Block> ConnectedBlocks = new List<Block>();

    private BlockColor? CurrentColor; 
    private Board Board;
    private LineRenderer LineRenderer;

    public event Action<int> OnConnection;

    private void Awake()
    {
        Board = FindObjectOfType<Board>();
    }
    void Start()
    {
        if (Input.GetMouseButtonUp(0))
            FinishConnection();
    }

    void Update()
    {
        
    }

    public void Connect(Block block)
    {
        if (!Input.GetMouseButton(0))
            return;

        if(ConnectedBlocks.Contains(block))
            return;

        if (!CurrentColor.HasValue)
            CurrentColor = block.Color;

        if (CurrentColor != block.Color)
            return;

        if (ConnectedBlocks.Count() >= 1 && ConnectedBlocks.Last().IsNeighbour(block))
            return;

        block.IsConnected = true;
        ConnectedBlocks.Add(block);

        RefreshConnector();
    }
    private void FinishConnection()
    {
        ConnectedBlocks.ForEach(block => block.IsConnected = false);

        if(ConnectedBlocks.Count >=3)
        {
            if (OnConnection != null)
                OnConnection.Invoke(ConnectedBlocks.Count);

            Board.RemoveBlocks(ConnectedBlocks);
            Board.RefreshBlocks();
        }
        
        ConnectedBlocks.Clear();

        CurrentColor = null;
        RefreshConnector();
    }
    private void RefreshConnector()
    {
        var points = ConnectedBlocks
            .Select(block => Board.GetBlockPosition(block.X, block.Y))
            .Select(position => (Vector3)position + Vector3.back * 2f)
            .ToArray();

        LineRenderer.positionCount = points.Length;
        LineRenderer.SetPositions(points);
    }
}
