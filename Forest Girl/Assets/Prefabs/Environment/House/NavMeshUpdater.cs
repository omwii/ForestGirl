using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshUpdater : MonoBehaviour
{
    private NavMeshSurface _meshSurface;

    private void Awake()
    {
        _meshSurface = GetComponent<NavMeshSurface>();
    }

    private void BakeNavMesh()
    {
        _meshSurface.BuildNavMesh();
    }

    public void OnDoorOpened()
    {
        Invoke("BakeNavMesh", 1);
    }
}
