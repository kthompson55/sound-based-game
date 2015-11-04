using UnityEngine;
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
        float yLimit = transform.position.y;
        Vector3[] vertices = new Vector3[mesh.vertexCount];
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            if(mesh.vertices[i].y > 0)
            {
                Vector3 vertex = mesh.vertices[i];
                vertices[i] = new Vector3(vertex.x, -vertex.y, vertex.z);
            }
            else
            {
                vertices[i] = mesh.vertices[i];
            }
        }
        mesh.vertices = vertices;
	}
}
