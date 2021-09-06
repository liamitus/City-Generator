using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCity : MonoBehaviour {

    public GameObject[] buildings;
    public GameObject xStreets;
    public GameObject zStreets;
    public GameObject crossroads;

    public int mapWidth = 200;
    public int mapHeight = 200;

    private int buildingFootprint = 2;
    private int[,] mapgrid;

    // Start is called before the first frame update
    void Start() {
        // int seed = 481;
        int seed = Random.Range(0, 1000);
        Debug.Log($"Seed: {seed}");
        Random.InitState(seed);

        GenerateMapData(seed);

        // Generate city
        for (int h = 0; h < mapHeight; h++) {
            for (int w = 0; w < mapWidth; w++) {
                int type = mapgrid[w, h];
                GameObject model;

                if (type < -2) {
                    model = crossroads;
                } else if (type < -1) {
                    model = xStreets;
                } else if (type < 0) {
                    model = zStreets;
                } else {
                    model = buildings[type];
                }

                Vector3 pos = new Vector3(w * buildingFootprint, 0, h * buildingFootprint);
                Instantiate(model, pos, Quaternion.identity);
            }

        }

    }

    // Update is called once per frame
    void Update() {

    }

    void GenerateMapData(int seed) {
        mapgrid = new int[mapWidth, mapHeight];

        // Generate map data
        for (int h = 0; h < mapHeight; h++) {
            for (int w = 0; w < mapWidth; w++) {
                mapgrid[w, h] = (int)(Mathf.PerlinNoise(w / 12.0f + seed, h / 12.0f + seed) * (buildings.Length - 1));
            }

        }

        // Build Z streets
        for (int x = 0, n = 0; n < 50; n++) {
            for (int h = 0; h < mapHeight; h++) {
                mapgrid[x, h] = -1;
            }
            x += Random.Range(3, 3);
            if (x >= mapWidth) {
                break;
            }

        }

        // Build X streets
        for (int z = 0, n = 0; n < 10; n++) {
            for (int w = 0; w < mapWidth; w++) {
                if (mapgrid[w, z] == -1) {
                    mapgrid[w, z] = -3;
                } else {
                    mapgrid[w, z] = -2;
                }
            }
            z += Random.Range(2, 20);
            if (z >= mapHeight) {
                break;
            }
        }

    }
}
