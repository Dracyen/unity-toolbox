using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class DebugGizmo : MonoBehaviour
{
    public float GizmoRadius = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, GizmoRadius);
    }
}
