using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIBrain _brain;

    public virtual void SetUp(AIBrain brain)
    {
        _brain = brain;
    }

    public abstract void TakeAction(); // 추상메서드로 아래애들이 구현하게
}
