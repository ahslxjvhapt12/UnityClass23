using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class InnerDistanceDecision : AIDecision
{

    [SerializeField][Range(0.1f, 30f)] private float _distance = 5f;


    public override bool MakeADecision()
    {
        float calc = Vector2.Distance(_brain.PlayerTrm.position, transform.position);

        if (calc < _distance)
        {
            // ���� �߰��ߴ� => ��Ʈ ������ ����
            _brain.StateInfoCompo.LastEnemyPosition = _brain.PlayerTrm.position;
            return true;
        }
        else
        {

            return false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _distance);
            Gizmos.color = Color.white;
        }
    }
#endif

}
