using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TobisUtils;

public class DrawGizmo2D : MonoBehaviour
{
    [Range(3, 12)]
    public int SideCount = 4;
    [Range(1, 10)]
    public int Density = 1;

    void OnDrawGizmos()
    {
        // generate data
        Vector2[] verts = new Vector2[SideCount];
        for (int i = 0; i < SideCount; i++)
            verts[i] = TobisUtils.Math.AngToDir2D((TobisUtils.Math.TAU / SideCount) * i);

        // draw gizmos
        Gizmos.color = Color.red;
        for (int i = 0; i < SideCount; i++)
            Gizmos.DrawSphere(verts[i], 0.05f);

        Gizmos.color = Color.white;
        for (int i = 0; i < SideCount; i++)
            Gizmos.DrawLine(verts[i], verts[(i + Density) % SideCount]);


    }
}
