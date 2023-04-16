using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EndOfAttackDecision : AIDecision
{
    public bool IsMelee;

    public override bool MakeADecision()
    {
        if (IsMelee)
        {
            return _brain.StateInfoCompo.IsMelee == false;
        }
        else
        {
            return _brain.StateInfoCompo.IsRange == false;
        }
    }
}
