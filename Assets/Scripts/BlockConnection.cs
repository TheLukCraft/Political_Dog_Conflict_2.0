using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockConnection : MonoBehaviour
{
    List<Block> ConnectedBlocks = new List<Block>();
    void Start()
    {
        if(Input.GetMouseButtonUp(0))
        {

        }
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
