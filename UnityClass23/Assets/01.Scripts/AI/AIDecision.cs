using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    public bool IsReverse; // �� ����
    protected AIBrain _brain;
    // ������ ������ ���ؼ� �ʿ��� �ٸ� ����

    public virtual void SetUp(AIBrain brain)
    {
        _brain = brain;
    }

    public abstract bool MakeADecision(); // ������ ������ ��

}
