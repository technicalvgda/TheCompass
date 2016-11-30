using UnityEngine;
using System.Collections;
using System;

public class MenuTransitionSingle : MenuTransition
{
    public Menu nextSingle;

    public override Menu next { get { return nextSingle; } }
}
