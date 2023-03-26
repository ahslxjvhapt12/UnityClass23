using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandle : IEnumerator
{
    public object Current { get; }

    public bool IsDone { get; private set; }

    public bool MoveNext()
    {
        return !IsDone;
    }

    public void Reset()
    {

    }

    public CoroutineHandle(MonoBehaviour owner, IEnumerator coroutine)
    {
        Current = owner.StartCoroutine(Wrap(coroutine));
    }

    private IEnumerator Wrap(IEnumerator coroutine)
    {
        yield return coroutine;
        IsDone = true;
    }
}
