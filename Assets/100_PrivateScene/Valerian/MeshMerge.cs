using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshMerge : MonoBehaviour
{
    [SerializeField] MeshFilter[] meshFilters;
    // Start is called before the first frame update
    void Start()
    {
        meshFilters = GetComponentsInChildren<MeshFilter>().Skip(1).ToArray();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        gameObject.SetActive(true);
    }
}
