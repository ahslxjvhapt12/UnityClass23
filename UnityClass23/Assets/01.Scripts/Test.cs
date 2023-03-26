using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Collision2D _col;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _col = collision;
        Debug.Log("Ãæµ¹");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log(_col.gameObject.name);
        }
    }
}
