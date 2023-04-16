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
        //_spriteRenderer = brain.GetComponent<SpriteRenderer>(); // ���� ���̴��� ���� ������
    }

    public override void TakeAction()
    {
        // �ƹ��͵� ���ϱ�.
        Vector3Int nextPos = TileMapManager.Instance.GetTilePos(_brain.StateInfoCompo.LastEnemyPosition);

        if (nextPos != _beforePos)
        {
            // �÷��̾ Ÿ�� ������ �̵��� �ߴٰ� �Ǵ��ϰ� �������� ����
            _brain.NavAgentCompo.Destination = nextPos;
            _beforePos = nextPos;
        }

        //Material mat = _spriteRenderer.material;
        //mat.SetInt(_Normalhash, 0);
        // ���� ���̴��� ����!


    }
}
