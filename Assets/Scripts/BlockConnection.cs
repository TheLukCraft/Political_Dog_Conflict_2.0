using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConnection : MonoBehaviour
{
    bool IsConnecting = false;
    List<Block> ConnectedBlocks = new List<Block>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartConnection(Block block)
    {
        IsConnecting = true;
    }
    public void Connect(Block block)
    {
        if (!IsConnecting)
            return;

        if(ConnectedBlocks.Contains(block))
            return;

        block.IsConnected = true;
        ConnectedBlocks.Add(block);
    }
    public void FinishConnection()
    {
        ConnectedBlocks.
            ForEach(block => block.IsConnected = false);

        ConnectedBlocks.Clear();
        IsConnecting = false;
    }
}
