using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

    private const int InitialRockCount = 1000;
    private const int Border = 2;
    
    [SerializeField]
    private GameObject rockPrefab;
    // Maybe we dont need this.. can get the root from the script?
    [SerializeField]
    private GameObject rockParent;

    private Bounds floorBounds;
    private float nextRockTime;

    void Start()
    {
        if(rockPrefab != null && rockParent != null)
        {
            MeshCollider floorCollider = GameObject.Find("Floor").GetComponent<MeshCollider>();
            floorBounds = floorCollider.bounds;
            for(int i = 0;i<InitialRockCount;i++)
            {
                CreateNewRock();
            }
            nextRockTime = Time.time + Random.Range(6, 60);
        }
        else
        {
            Debug.LogError("You forgot to set the rock prefab or parent in the RockSpawner");
            // Exit with error?
        }
    }

    void Update()
    {
        if(Time.time > nextRockTime)
        {
            CreateNewRock();
            nextRockTime = Time.time + Random.Range(6, 60);
        }
    }

    private void CreateNewRock()
    {
        var x = Random.Range(floorBounds.center.x - floorBounds.extents.x + Border, floorBounds.center.x + floorBounds.extents.x - Border);
        var z = Random.Range(floorBounds.center.z - floorBounds.extents.z + Border, floorBounds.center.z + floorBounds.extents.z - Border);
        var position = new Vector3(x, rockPrefab.transform.position.y, z);
        Instantiate(rockPrefab, position, rockPrefab.transform.rotation, rockParent.transform);
    }
}
