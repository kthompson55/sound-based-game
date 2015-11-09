using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[RequireComponent(typeof(MeshFilter))]
public class EchoTunnelCulling : MonoBehaviour 
{
    private Mesh mesh;

	// Use this for initialization
	void Awake() 
    {
        mesh = GetComponent<MeshFilter>().mesh;
        float yLimit = transform.localPosition.y;
        List<int> newTriangles = new List<int>();

        for(int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 currentVector = transform.rotation * mesh.vertices[mesh.triangles[i]];
            if(currentVector.y < yLimit)
            {
                newTriangles.Add(mesh.triangles[i]);
                newTriangles.Add(mesh.triangles[i + 1]);
                newTriangles.Add(mesh.triangles[i + 2]);
            }
        }

        mesh.triangles = newTriangles.ToArray();
	}
}
