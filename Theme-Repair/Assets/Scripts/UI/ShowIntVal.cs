using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIntVal : MonoBehaviour
{
    public IntVal value;
    public TMPro.TextMeshPro text;
    public UnityEngine.UI.Text text2;
    private void OnEnable()
    {
        value.ValueChanged += Value_ValueChanged;
        Value_ValueChanged();
    }

    private void Value_ValueChanged()
    {
        if (text) text.text = value.Value.ToString();
        if (text2) text2.text = value.Value.ToString();
    }

    private void OnDisable()
    {
        value.ValueChanged -= Value_ValueChanged;
    }
}
