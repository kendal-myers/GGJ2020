using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIntVal : MonoBehaviour
{
    public IntVal value;
    public TMPro.TextMeshPro text;
    private void OnEnable()
    {
        value.ValueChanged += Value_ValueChanged;
        Value_ValueChanged();
    }

    private void Value_ValueChanged()
    {
        text.text = value.Value.ToString();
    }

    private void OnDisable()
    {
        value.ValueChanged -= Value_ValueChanged;
    }
}
