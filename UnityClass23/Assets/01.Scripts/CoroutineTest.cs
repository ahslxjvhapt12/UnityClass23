using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CoroutineTest : MonoBehaviour
{
    private List<string> list = new List<string>();

    IEnumerator Start()
    {
        var t1 = this.RunCoroitine(CoA());
        var t2 = this.RunCoroitine(CoB());
        while (!t1.IsDone && !t2.IsDone)
        {
            yield return null;
        }
        Debug.Log("complete");
    }

    private void Update()
    {
        bool ground = transform.IsInGround(0.2f);
        if (ground)
        {
            Debug.Log("¶¥¹Ù´Ú!");
        }
    }

    IEnumerator CoA()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Co A Complete");
    }

    IEnumerator CoB()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Co B Complete");
    }

    IEnumerator DelayCall(float time)
    {
        ulong number = 0;
        while (true)
        {
            number++;
            if (number >= 900000000000) break;
        }

        yield return new WaitForSeconds(time);
        Debug.Log("ÄÝ!");
    }
}
