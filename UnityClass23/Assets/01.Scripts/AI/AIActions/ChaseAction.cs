using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    private Vector3Int _beforePos = Vector3Int.zero;
    private SpriteRenderer _spriteRenderer;

    private readonly int _Normalhash = Shader.PropertyToID("_NormalState");

    public override void SetUp(AIBrain brain)
    {
        base.SetUp(brain);
        //_spriteRenderer = brain.GetComponent<SpriteRenderer>(); // 나는 셰이더가 없음 ㅋㅋㄹ
    }

    public override void TakeAction()
    {
        // 아무것도 안하기.
        Vector3Int nextPos = TileMapManager.Instance.GetTilePos(_brain.StateInfoCompo.LastEnemyPosition);

        if (nextPos != _beforePos)
        {
            // 플레이어가 타일 단위로 이동을 했다고 판단하고 움직임을 시작
            _brain.NavAgentCompo.Destination = nextPos;
            _beforePos = nextPos;
        }

        //Material mat = _spriteRenderer.material;
        //mat.SetInt(_Normalhash, 0);
        // 나는 셰이더가 없음!


    }
}
