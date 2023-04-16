using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> Decisions = null;
    public AIState NextState;

    public void SetUp(AIBrain brain)
    {
        Decisions = new List<AIDecision>();
        GetComponents<AIDecision>(Decisions);

        Decisions.ForEach(d => d.SetUp(brain));
    }

    public bool CheckTransition()
    {
        bool result = false;

        foreach (AIDecision d in Decisions)
        {
            result = d.MakeADecision();
            if (d.IsReverse) result = !result;
            if (result == false) break;
        }

        //여기까지 왔는 데 true면 모든 조건이 전부 true 이다
        return result;
    }
}
