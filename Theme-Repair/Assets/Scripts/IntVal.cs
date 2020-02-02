using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/IntVal")]
public class IntVal : ScriptableObject
{
    public event System.Action ValueChanged;
    [System.NonSerialized]
    private int _value;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (_value != value)
            {
                _value = value;
                ValueChanged?.Invoke();
            }
        }
    }
}
