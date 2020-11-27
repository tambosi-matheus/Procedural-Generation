using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGameObjectGeneration : MonoBehaviour
{
    [SerializeField] Transform player;
    private Transform parent;
    [SerializeField] private GameObject prefab;
    private Vector2 min = new Vector2(), max = new Vector2();

    [SerializeField] [Range(0.01f, 0.25f)] private float precision = 0.1f;
    [SerializeField] [Range(0.1f, 20f)] private float scale = 8f;
    [SerializeField] private int gridSize = 10;
    [SerializeField] private bool autoMode = false;

    public float minDist, maxDist;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponent<Transform>();
        prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GenerateTerrain();       
    }

    // Update is called once per frame
    void Update()
    {
        minDist = Mathf.Abs(player.position.x - min.x);
        maxDist = Mathf.Abs(player.position.x - max.x);

        if (maxDist < 10)
        {
            Debug.Log("aa");
            CreateObjects(false, new Vector3(max.x, 0, player.position.x - 25), 50);
        }
    }

    private void GenerateTerrain()
    {
        var noise = 0f;

        for (int x = 0; x < gridSize; x++)
        {
            CreateObjects(false, new Vector3(x, 0, 0), gridSize);
        }
    }

    private void CreateObject(Vector3 pos)
    {
        var obj = Instantiate(prefab, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
        obj.transform.SetParent(parent);
        IsNewEdge(obj.transform.position);
    }

    private void CreateObjects(bool inX, Vector3 startPos, int length)
    {
        var noise = 0f;

        if(inX)
        {
            int z = (int)startPos.z;
            for (int x = 0; x < length; x++)
            {
                noise = Mathf.PerlinNoise(x * precision, z * precision);
                var pos = new Vector3(x, noise * scale, z);
                CreateObject(pos);
            }
        }
        else
        {
            int x = (int) startPos.x;
            for (int z = 0; z < length; z++)
            {
                noise = Mathf.PerlinNoise(x * precision, z * precision);
                var pos = new Vector3(x, noise * scale, z);
                CreateObject(pos);
            }
        }
    }

    private void IsNewEdge(Vector3 pos)
    {
        if (pos.x > max.x)
        {
            Debug.Log("big pos x");
            max.x = pos.x;
        }
        else if (pos.x < min.x)
            min.x = pos.x;
        if (pos.y > max.y)
            max.y = pos.y;
        else if (pos.y < min.y)
            min.y = pos.y;
    }
}
