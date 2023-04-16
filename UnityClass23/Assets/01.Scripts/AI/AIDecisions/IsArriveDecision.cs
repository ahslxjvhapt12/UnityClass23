using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsArriveDecision : AIDecision
{
    public override bool MakeADecision()
    {
        _brain.StateInfoCompo.IsArrived = _brain.NavAgentCompo.IsArrived;

        return _brain.NavAgentCompo.IsArrived;
    }
}
