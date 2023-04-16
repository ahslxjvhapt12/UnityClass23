using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRenderer : MonoBehaviour
{
    NavAgent _navAgent = null;

    private void Awake()
    {
        _navAgent = GetComponent<NavAgent>();
    }

    private void Update()
    {
        Vector3 worldPos = TileMapManager.Instance.GetWorldPos(_navAgent.Destination);

        Vector3 dir = worldPos - transform.position;
        float degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0, 0, degree);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 4f);


    }
}
