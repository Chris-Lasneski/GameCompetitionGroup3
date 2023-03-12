using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class WorldGenerationConstants
{
    public static int chunkSize = 640; // width and height of a chunk in world units. multiple of roadLength.
    public static float roadLength = 80; // distance between the center of 2 intersections in world units
    public static float roadWidth = 10; // width of a road in world units

    public static int buildingsPerRoad = 2;
    public static float buildingWidth = (roadLength - roadWidth) / buildingsPerRoad; // width (and length) of a building in world units
    public static int chunkBuildingWidth = (chunkSize / (int)roadLength) * buildingsPerRoad;
    public static int buildingPermSize = chunkBuildingWidth + 1;
}

public class Math {

    public static int mod(int n, int m) {
        return ((n % m) + m) % m;
    }

    public static int[][] generatePerm(int seedX, int seedY, int size) {
        int[][] perm = new int[size][];

        Random rX = new Random(seedX);
        Random rY = new Random(seedY);
        for (int i = 0; i < size; i++) {
            perm[i] = new int[size];

            for(int j = 0; j < size; j++) {
                perm[i][j] = i * size + j;
            }
        }
        for (int i = 0; i < size - 1; i++) {
            for (int j = 0; j < size; j++) {
                int x = mod((seedX + rX.Next(0, size)), size);
                int y = mod((seedY + rY.Next(0, size)), size);
                int temp = perm[i][j];
                perm[i][j] = perm[x][y];
                perm[x][y] = temp;
            }
        }
        return perm;
    }
}