using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class GenericOption
{
    private string name, value;
    public Dictionary<string, System.Object> valueDictionary;

    public GenericOption()
    {
        valueDictionary = new Dictionary<string, object>();
        value = "";
    }

    public string GetName()
    {
        return name;
    }

    protected string GetValue()
    {
        return value;
    }

    protected void SetValue(string value)
    {
        this.value = value;
    }

    protected void SetName(string name)
    {
        this.name = name;
    }

    //  Get a list<string> of the choices for this option -- used in filling dropdown options
    public List<string> GetChoices()
    {
        List<string> temp = new List<string>();
        
        foreach(KeyValuePair<string, System.Object> pair in valueDictionary)
        {
            temp.Add(pair.Key);
        }

        return temp;
    }

    public override string ToString()
    {
        return (name + "=" + value);
    }

    protected abstract string GetStringValue(object obj);

    public abstract object GetEnumValue(string value);

    public abstract void Apply();
}
