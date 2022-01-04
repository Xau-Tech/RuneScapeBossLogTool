using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Factory for building options at runtime
/// </summary>
public class OptionFactory
{
    public GenericOption BuildOption(string name)
    {
        if (name == ResolutionOption.NAME)
            return new ResolutionOption(OptionsView.GetDisplayInterfaceByOptionName(name, ResolutionOption.OPTIONTYPE));
        else if (name == BossSyncOption.NAME)
            return new BossSyncOption(OptionsView.GetDisplayInterfaceByOptionName(name, BossSyncOption.OPTIONTYPE));
        else if (name == RSVersionOption.NAME)
            return new RSVersionOption(OptionsView.GetDisplayInterfaceByOptionName(name, RSVersionOption.OPTIONTYPE));

        return null;
    }
}
