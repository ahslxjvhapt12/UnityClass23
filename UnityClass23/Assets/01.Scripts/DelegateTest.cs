using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate int MyDelegate(int a, int b);

public class Student
{
    public int Plus(int a, int b)
    {
        return a + b;
    }
    public int Minus(int a, int b)
    {
        return a - b;
    }
    public void SetUp(MyDelegate action)
    {
        int result = action(3, 4);
        Debug.Log(result);
    }
}
public class DelegateTest : MonoBehaviour
{
    private int dummy = 4;
    private void Start()
    {
        ABC();
    }
    public void ABC()
    {
        Student s = new Student();
        MyDelegate a = delegate (int a, int b)
        {
            return a + b + dummy;
        };

        s.SetUp(a);

        Debug.Log(a?.Invoke(3, 4));
    }
}
