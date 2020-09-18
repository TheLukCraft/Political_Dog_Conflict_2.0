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

    public BlockColor Color { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Color = GetRandomColor();
        SetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
