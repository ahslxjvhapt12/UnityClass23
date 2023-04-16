using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    private SpriteRenderer _spriteRenderer;
    private readonly int _Normalhash = Shader.PropertyToID("_NormalState");

    public override void SetUp(AIBrain brain)
    {
        base.SetUp(brain);
        //_spriteRenderer = brain.GetComponent<SpriteRenderer>(); // 나는 셰이더가 없음 ㅋㅋㄹ
    }
    public override void TakeAction()
    {
        _brain.NavAgentCompo.StopImmediately();   
        
        //Material mat = _spriteRenderer.material;
        //mat.SetInt(_Normalhash, 0);
        // 나는 셰이더가 없음!
    }
}
