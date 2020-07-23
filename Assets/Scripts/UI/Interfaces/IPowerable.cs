using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Interface for any script that can be turned off
public interface IPowerable
{
    void PowerOn();
    void PowerOff();
}
