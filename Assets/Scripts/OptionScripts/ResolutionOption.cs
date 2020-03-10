using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResolutionOption : GenericOption
{
    private string[] strChoices = {"320x240", "480x360", "640x480", "960x720" };
    private enum choices {_240p, _360p, _480p, _720p };

    public ResolutionOption()
    {
        base.SetName("Resolution");

        for(int i = 0; i < strChoices.Length; ++i)
        {
            valueDictionary.Add(strChoices[i], (choices)i);
        }
    }

    public override void Apply()
    {
        // Apply resolution change

    }

    public override object GetEnumValue(string value)
    {
        for(int i = 0; i < strChoices.Length; ++i)
        {
            if(value.CompareTo(strChoices[i]) == 0)
            {
                return (choices)i;
            }
        }

        return null;
    }

    //  Internal class use to make code a bit more readable
    //  Uses the dictionary to convert from passed value to string so that we can compare using our enums without using suboptimal parsing
    protected override string GetStringValue(object obj)
    {
        throw new System.NotImplementedException();
    }
}
