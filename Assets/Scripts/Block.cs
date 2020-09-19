using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BlockColor { Red, Green, Blue, Yellow, Magenta, Gray }

[System.Serializable]
class BlockType
{
    public BlockColor Color;
    public Sprite sprite;
}


public class Block : MonoBehaviour
{

    [SerializeField]
    BlockType[] BlockTypes;

    public int X { get; private set; }
    public int Y { get; private set; }

    public BlockColor Color { get; private set; }

    public bool IsConnected; 

    private Vector3 TargetPosition;
    private Board Board;
    private BlockConnection BlockConnection;

    private SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        Board = FindObjectOfType<Board>();
        BlockConnection = FindObjectOfType<BlockConnection>();

        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        Color = GetRandomColor();
        SetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        UpdateScale();
        UpdateColor();
    }

    private void UpdatePosition()
    {
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            TargetPosition,
            Time.deltaTime * 5f);
    }

    private void UpdateScale()
    {
        var targetScale = IsConnected ? 0.8f : 1f;
        targetScale *= Board.BlockSize;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale * Vector3.one, Time.deltaTime * 5f);
    }

    private void UpdateColor()
    {
        var targetColor = IsConnected ? new Color(1f, 1f, 1f, 0.5f) : UnityEngine.Color.white;
        SpriteRenderer.color = UnityEngine.Color.Lerp(SpriteRenderer.color, targetColor, Time.deltaTime * 5);
    }

    public void Configure(int x, int y)
    {
        X = x;
        Y = y;

        TargetPosition = Board.GetBlockPosition(x, y);
        IsConnected = false;
    }
    public static BlockColor GetRandomColor()
    {
        var values = System.Enum.GetValues(typeof(BlockColor));
        var index = Random.Range(0, values.Length);

        return (BlockColor)values.GetValue(index);
    }

    private void SetSprite()
    {
        var sprite = BlockTypes.First(type => type.Color == Color).sprite;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void ResetColor()
    {
        Color = GetRandomColor();
        SetSprite();
    }

    public bool IsNeighbour(Block other)
    {
        if (Mathf.Abs(X - other.X) > 1f)
            return false;

        if (Mathf.Abs(Y - other.Y) > 1f)
            return false;

        return true;
    }

    private void OnMouseEnter()
    {
        ConnectBlock();
    }
    private void OnMouseDown()
    {
        ConnectBlock();
    }
    private void ConnectBlock()
    {
        BlockConnection.Connect(this);
    }
}
