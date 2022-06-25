using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Support : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;

    private int[] indexes =
    {
        0, 3, 5, 7, 9, 11, 13, 14, 15, 18, 21, 23, 25, 27, 29, 31, 32, 33
        /*0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
        19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 39, 42, 43,
        44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
        67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 87*/
    };

    private int j = 0;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        foreach (var i in indexes)
        {
            vertices[i].z -= .05f;
        }
        mesh.vertices = vertices;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
