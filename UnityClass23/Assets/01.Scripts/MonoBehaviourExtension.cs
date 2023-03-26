using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtension
{
    public static CoroutineHandle RunCoroitine(this MonoBehaviour owner, IEnumerator co)
    {
        return new CoroutineHandle(owner, co);
    }
}
