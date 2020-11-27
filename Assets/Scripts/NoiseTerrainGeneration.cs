using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseTerrainGeneration : MonoBehaviour
{
    private Terrain target;
    private float[,] heightMap;

    [SerializeField] private float precision = 0.1f;
    [SerializeField] private float scale = 0.1f;
    [SerializeField] private int gridSize = 100;
    private float noise = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Terrain>();
        var noise = 0f;
        gridSize = target.terrainData.heightmapResolution;
        heightMap = new float[gridSize, gridSize];

        for(int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                noise = Mathf.PerlinNoise(x * precision, y * precision);
                heightMap[x, y] = noise * scale;
            }
        }

        target.terrainData.SetHeights(0, 0, heightMap);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
