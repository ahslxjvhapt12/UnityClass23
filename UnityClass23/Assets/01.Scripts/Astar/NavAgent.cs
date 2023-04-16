using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class NavAgent : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private bool _cornerCheck = false;

    private Vector3 _nextPos;
    private int _moveIndex = 0;
    private bool _isMoving = false; //�̵�������

    public bool IsArrived = false; //�����ߴ°�

    [SerializeField] private LineRenderer _lineRenderer;

    private PriorityQueue<Node> _openList;
    private List<Node> _closeList; //�Դ���
    //���� ����� ���� _closeList�� Dictionary �� �����ص� �ȴ�


    private List<Vector3Int> _routePath; //���� ���
    private Vector3Int _currentPosition; //���� Ÿ�� ��ġ
    private Vector3Int _destination; //������ ��ġ
    public Vector3Int Destination
    {
        get => _destination;
        set
        {
            //���⿡ ���� �ϱ�
            SetCurrentPos();
            _destination = value;
            _isMoving = CalcRoute(); // ��ΰ� ������� _isMoving = true �̴�
            _moveIndex = 0;
            if (_routePath.Count > 0)
                _nextPos = TileMapManager.Instance.GetWorldPos(_routePath[0]);
            DrawRoutePath();
            IsArrived = false;
        }
    }

    // �� ������ǥ�� Ÿ�� ��ǥ�� �ٲ㼭 �����ϴ� ��
    private void SetCurrentPos()
    {
        _currentPosition = TileMapManager.Instance.GetTilePos(transform.position);
    }

    private void Awake()
    {
        _openList = new PriorityQueue<Node>();
        _closeList = new List<Node>();
        _routePath = new List<Vector3Int>();
    }

    private void Start()
    {
        SetCurrentPos();
        transform.position = TileMapManager.Instance.GetWorldPos(_currentPosition);
    }

    private void DrawRoutePath()
    {
        //routhPath�� ����Ǿ� �ִ� �Ϳ� �������� �� ���� _currentPosition���� �ϰ�
        //���η������� Ȱ��ȭ
        //����� �������� ���� ���� TileMapManager�� ���� world��ǥ�� �����ؼ� �־�� �Ѵ�.
        //����� ������ _lineRenderer�� SetPosition�� �̿��� �־��ش�
        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = _routePath.Count;
        //for (int i = 0; i <= _routePath.Count; i++)
        //{
        //    _lineRenderer.SetPosition(i,TileMapManager.Instance.GetWorldPos(_routePath[i]));
        //}
        _lineRenderer.SetPositions(_routePath.Select(p => TileMapManager.Instance.GetWorldPos(p)).ToArray());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            Vector3Int cellPos = TileMapManager.Instance.GetTilePos(pos);
            Destination = cellPos;
        }

        if (_isMoving)
        {
            Vector2 dir = (_nextPos - transform.position).normalized;
            transform.Translate(dir * _speed * Time.deltaTime, Space.World);

            if (Vector2.Distance(_nextPos, transform.position) < 0.1f)
            {
                if (GetNextTarget() == false) //�����ع�����
                {
                    IsArrived = true;
                    _isMoving = false;
                }
            }
        }
    }

    private bool GetNextTarget()
    {
        ++_moveIndex;
        if (_moveIndex >= _routePath.Count)
        {
            return false;
        }
        _nextPos = TileMapManager.Instance.GetWorldPos(_routePath[_moveIndex]);
        return true;
    }

    public void StopImmediately()
    {
        _isMoving = false;
    }

    #region ASTAR �˰��� ����

    public bool CalcRoute()
    {
        _openList.Clear(); // ������ �� ����� ����
        _closeList.Clear();

        _openList.Push(new Node
        {
            CellPos = _currentPosition,
            Parent = null,
            G = 0,
            F = CalcH(_currentPosition)
        }); // �� ó�� ���¸���Ʈ�� ���� �ִ� ���� �ִ´�.

        bool result = false; // ���� ã�ҳ�

        int cnt = 0;
        while (_openList.Count > 0)
        {

            Node n = _openList.Pop(); // ���� ������ �� �� �ִ� �༮�� �̴´�.

            FindOpenList(n);
            _closeList.Add(n);

            if (n.CellPos == Destination)
            {
                result = true;
                break;
            }

            cnt++;
            if (cnt >= 100000)
            {
                Debug.Log("1���� �Ѱ� ����!");
                break;
            }
        }

        if (result)
        {
            _routePath.Clear();
            Node last = _closeList[_closeList.Count - 1]; //������ ���
            while (last.Parent != null)
            {
                _routePath.Add(last.CellPos);
                last = last.Parent;
            }

            //_routePath.Add(_currentPosition);
            _routePath.Reverse();

            foreach (Vector3Int pos in _routePath)
            {
                Debug.Log(pos);
            }
        }

        return result;
    }

    //��� n��忡 ����� �� �� �ִ� ��� ���� ã�Ƴ���.
    private void FindOpenList(Node n)
    {
        /* O O O
         * O X O
         * O O O */


        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x == y && x == 0) continue;

                if (_cornerCheck && (Mathf.Abs(x) + Mathf.Abs(y)) == 2)
                {
                    Vector3Int corner = n.CellPos + new Vector3Int(x, 0);
                    if (TileMapManager.Instance.CanMove(corner) == false) continue;
                    corner = n.CellPos + new Vector3Int(0, y);
                    if (TileMapManager.Instance.CanMove(corner) == false) continue;
                }

                Vector3Int nextPos = n.CellPos + new Vector3Int(x, y, 0);

                Node temp = _closeList.Find(x => x.CellPos == nextPos);

                if (temp != null) continue; // �̹� �湮�� ���̸� ����

                if (TileMapManager.Instance.CanMove(nextPos))
                {
                    float g = (n.CellPos - nextPos).magnitude + n.G;

                    Node nextOpenNode = new Node
                    {
                        CellPos = nextPos,
                        Parent = n,
                        G = g,
                        F = g + CalcH(nextPos)
                    };


                    Node exist = _openList.Contains(nextOpenNode);
                    if (exist == null)
                    {
                        _openList.Push(nextOpenNode);
                    }
                }

                //if (TileMapManager.Instance.CanMove(nextPos))
                //{
                //    float g = (n.CellPos - nextPos).magnitude + n.G;

                //    Node nextOpenNode = new Node
                //    {
                //        CellPos = nextPos,
                //        Parent = n,
                //        G = g,
                //        F = g + CalcH(nextPos)
                //    };

                //    Node exist = _openList.Contains(nextOpenNode);
                //    if (exist == null)
                //    {
                //        _openList.Push(nextOpenNode);
                //    }
                //}

            }
        }
    }

    private float CalcH(Vector3Int pos)
    {
        Vector3Int distance = _destination - pos;
        return distance.magnitude;
    }
    #endregion
}