using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    public float ViewAngle;
    public float ViewRange;

    public Transform PlayerTrm;

    [SerializeField] private AIState _currentState; // ���� �� ����

    public NavAgent NavAgentCompo { get; private set; }
    public AIStateInfo StateInfoCompo { get; private set; }

    private void Awake()
    {
        NavAgentCompo = GetComponent<NavAgent>();
        StateInfoCompo = transform.Find("AI").GetComponent<AIStateInfo>();
    }

    private void Update()
    {
        _currentState.UpdateState();
    }

    public void ChangeState(AIState nextState)
    {
        _currentState = nextState;
    }

    public void Attack(bool isMelee)
    {
        NavAgentCompo.StopImmediately();
        StateInfoCompo.IsAttack = true;
        if (isMelee)
        {
            StateInfoCompo.IsMelee = true;
            StateInfoCompo.MeleeCool = 1f;
            Debug.Log("�и� ����");
            StartCoroutine(DelayCo(0.3f, () => StateInfoCompo.IsMelee = false));
        }
        else
        {
            StateInfoCompo.IsRange = true;
            StateInfoCompo.RangeCool = 3f;
            Debug.Log("���Ÿ� ����");
            StartCoroutine(DelayCo(0.8f, () => StateInfoCompo.IsRange = false));
        }
    }

    private IEnumerator DelayCo(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        StateInfoCompo.IsAttack = false;
        callback?.Invoke();
    }
}
