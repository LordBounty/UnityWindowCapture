using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreatenewMeshHelper 
{
    public static GameObject CreateMeshBy4Point(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4,Material mat,Vector2[] uvs=null)
    {
        //vertex array
        Vector3[] _vertices =
        {
            p1,
            p2,
            p3,
            p4
        };
        int[] _triangles =
        {
            0, 1, 2,
            2, 3, 0,
        };
        if (uvs==null)
        {
            uvs = new Vector2[]{
                new Vector2(0f, 0f),
                new Vector2(0.0f, 1.0f),
                new Vector2(1f, 1f),
                new Vector2(1.0f, 0f)
            };
        }
        //UV array
        //Vector2[] 

        Mesh mesh = new Mesh()
        {
            vertices = _vertices,
            uv = uvs,
            triangles = _triangles,
        };
        mesh.RecalculateNormals();
        
        GameObject go = new GameObject("plan");
        go.transform.position = Vector3.zero;
        go.AddComponent<MeshFilter>().mesh = mesh;
        go.AddComponent<MeshRenderer>().material = mat;

        return go;
    }
}
