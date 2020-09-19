using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TextDirection { Left, Middle, Right }

public class Number : MonoBehaviour
{
    [SerializeField]
    Sprite[] Sprites;

    [SerializeField]
    float GridSize = 1f;

    [SerializeField]
    TextDirection TextDirection;

    private int _value = 0;
    public int Value
    {
        get { return _value; }
        set
        {
            _value = Value;
            RefreshNumbers();
        }
    }

    List<GameObject> DigitsObjects = new List<GameObject>();


    // Start is called before the first frame update
    

    private void RefreshNumbers()
    {

        RemoveDigits();

        var digits = Value
            .ToString()
            .Select(c => int.Parse(c.ToString()))
            .ToArray();

        for(int i=0; i<digits.Count(); i++)
        {
            var position = CalculatePosition(i, digits.Count());
            var digit = CreateDigit(position, digits[i]);
            DigitsObjects.Add(digit);
        }
    }

    private void RemoveDigits()
    {
        DigitsObjects.ForEach(number => Destroy(number));
        DigitsObjects.Clear();
    }

    private Vector3 CalculatePosition(int index, int numberOfDigits)
    {
        float result = 0;

       if(TextDirection == TextDirection.Left)
            result = index;
       else if(TextDirection == TextDirection.Middle)
            result = index - numberOfDigits / 2f + 0.5f;
       else if(TextDirection == TextDirection.Right)
            result = index - numberOfDigits + 1;

        return Vector3.right * result * GridSize;
    }

    private GameObject CreateDigit(Vector3 position, int value)
    {
        var digit = new GameObject();
        digit.transform.parent = transform;
        digit.transform.localPosition = position;

        var sprite = Sprites[value];
        digit.AddComponent<SpriteRenderer>().sprite = sprite;

        return digit;
    }
}
