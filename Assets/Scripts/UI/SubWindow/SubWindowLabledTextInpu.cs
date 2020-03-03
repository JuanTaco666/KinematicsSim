using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubWindowLabledTextInput : SubWindowComponent
{
    public Text text;
    public InputField textInput;
    public delegate string GetNewValue();
    GetNewValue updateValue;

    void Update()
    {
        if(updateValue != null && !textInput.isFocused)
        {
            textInput.text = updateValue();
        }
    }

    public void setText(string text)
    {
        name = text;
        this.text.text = text;
        textInput.text = text;
    }
    
    public void AddListener(UnityEngine.Events.UnityAction<string> call, GetNewValue updateValue)
    {
        textInput.onEndEdit.AddListener(call);
        this.updateValue = updateValue;
    }

    public string GetValue()
    {
        return textInput.text;
    }

}
