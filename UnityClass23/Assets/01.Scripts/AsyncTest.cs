using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncTest : MonoBehaviour
{
    private bool _start = false;
    private ulong _number = 0;

    private void Awake()
    {
        Debug.Log($"Awake : {Thread.CurrentThread.ManagedThreadId}");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("����!");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            JobSequence();
        }
    }

    private async void JobSequence()
    {
        Debug.Log($"Sequence : {Thread.CurrentThread.ManagedThreadId}");
        Debug.Log("JobStart");
        await Task.Run(() => MyJob(1)); //�� ����
        await Task.Run(() => MyJob(2)); //�� ����
        await Task.Run(() => MyJob(3)); //�� ����
        await Task.Run(() => MyJob(4)); //�� ����
        Debug.Log("JobComplete");
    }

    private void MyJob(int num)
    {
        Debug.Log($"Job {num} : {Thread.CurrentThread.ManagedThreadId}");
        for (int i = 0; i < 500000; i++)
        {
            _number--;
        }
        Debug.Log($"MyJob End : { num }");
    }
}
