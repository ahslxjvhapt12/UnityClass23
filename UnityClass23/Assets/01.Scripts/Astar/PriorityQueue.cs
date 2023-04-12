using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditorInternal;
using Unity.VisualScripting;

public class PriorityQueue<T> where T : IComparable<T>
{
    // MinHeap
    public List<T> _heap = new List<T>();
    public int Count => _heap.Count;

    public T Contains(T t)
    {
        int idx = _heap.IndexOf(t);
        if (idx < 0) return default(T);
        return _heap[idx];
    }

    public void Push(T data)
    {
        // 새로운걸 넣을때에는 맨 밑에다가 넣고 올려가며 자리를 찾는다.
        _heap.Add(data);
        int now = _heap.Count - 1; // 맨 마지막번째의 인덱스를 가져온다.

        while (now > 0) // 끝 조상까지 올라갈때까지 부모님이 누구시니?
        {
            int next = (now - 1) / 2;
            if (_heap[now].CompareTo(_heap[next]) < 0) //부모가 나보다 작다 == 여기가 내자리다
            {
                break;
            }
            T temp = _heap[now];
            _heap[now] = _heap[next];
            _heap[next] = temp;

            now = next;
        }
    }

    public T Pop()
    {
        T ret = _heap[0];

        int lastIndex = _heap.Count - 1;
        _heap[0] = _heap[lastIndex];
        _heap.RemoveAt(lastIndex); //마지막 원소를 지우면 리스트를 재구성하지 않는다.

        lastIndex--;

        int now = 0; //루트부터 내려간다


        while (true)
        {
            int left = 2 * now + 1;
            int right = 2 * now + 2;

            int next = now;
            if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                next = left;

            if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) < 0)
                next = right;

            if (next == now) break;

            T temp = _heap[now];
            _heap[now] = _heap[next];
            _heap[next] = temp;

            now = next;
        }
        return ret;
    }

    public T Peek()
    {
        return _heap.Count == 0 ? default(T) : _heap[0];
    }

    public void Clear()
    {
        _heap.Clear(); // 힙 클리어
    }
}
