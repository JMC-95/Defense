using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPathPoint : MonoBehaviour
{
    public Color _color = Color.red;
    public float _radius = 0.6f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
