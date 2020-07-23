using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Factory to create concrete option objects while returning our abstract GenericOption object
public class OptionFactory
{
    public GenericOption GetOption(in string name)
    {
        if (name == ResolutionOption.Name())
            return new ResolutionOption(OptionUI.GetDisplayInterfaceByOptionName(in name, ResolutionOption.OptionType()));
        else if (name == BossSyncOption.Name())
            return new BossSyncOption(OptionUI.GetDisplayInterfaceByOptionName(in name, BossSyncOption.OptionType()));
        else if (name == RSVersionOption.Name())
            return new RSVersionOption(OptionUI.GetDisplayInterfaceByOptionName(in name, RSVersionOption.OptionType()));

        return null;
    }
}
