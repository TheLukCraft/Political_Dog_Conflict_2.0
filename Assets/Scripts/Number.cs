using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Number : MonoBehaviour
{
    [SerializeField]
    Sprite[] Sprites;

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

    
    // Start is called before the first frame update
    void Start()
    {
        Value = 123;
    }

    private void RefreshNumbers()
    {
        var digits = Value
            .ToString()
            .Select(c => int.Parse(c.ToString()))
            .ToArray();

        for(int i=0; i<digits.Count(); i++)
        {
            var position = GetPosition(i);
            CreateDigit(position, Sprites[i]);
        }
    }

    private Vector3 GetPosition(int i)
    {
        return Vector3.right * i;
    }

    private void CreateDigit(Vector3 position, Sprite sprite)
    {
        var digit = new GameObject();
        digit.transform.parent = transform;
        digit.transform.localPosition = position;

        digit.AddComponent<SpriteRenderer>().sprite = sprite;
    }
}
