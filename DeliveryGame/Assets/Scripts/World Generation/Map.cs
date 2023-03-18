using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;




/*
 A road plan is a graph of nodes and edges. 
 these nodes and edges are arranged in a grid. 
 a node is an intersection and an edge is a road.
 buildings are stored in a grid, assuming 2 buildings per road.
 */
public class Map {
    public static Dictionary<Vector2Int, int> directionToExternalIndex = new Dictionary<Vector2Int, int>();
    public static Dictionary<int, Vector2Int> externalIndexToDirection = new Dictionary<int, Vector2Int>();

    private static List<int> buildingChoices = new List<int>();
    private static int buildingCount;

    private static float[] buildingOffset = new float[] {
            -(WorldGenerationConstants.roadWidth + WorldGenerationConstants.buildingWidth) / 2,
            (WorldGenerationConstants.roadWidth + WorldGenerationConstants.buildingWidth) / 2,
        };
    private static float roadLength = WorldGenerationConstants.roadLength;
    private static int buildingsPerRoad = WorldGenerationConstants.buildingsPerRoad;
    private static int chunkSize = WorldGenerationConstants.chunkSize;
    private static int permSize = WorldGenerationConstants.buildingPermSize;

    public static void initMap(int[] buildingWeights) {
        directionToExternalIndex.Clear();
        directionToExternalIndex.Add(new Vector2Int( 0,  1), 0);
        directionToExternalIndex.Add(new Vector2Int( 1,  1), 1);
        directionToExternalIndex.Add(new Vector2Int( 1,  0), 2);
        directionToExternalIndex.Add(new Vector2Int( 1, -1), 3);
        directionToExternalIndex.Add(new Vector2Int( 0, -1), 4);
        directionToExternalIndex.Add(new Vector2Int(-1, -1), 5);
        directionToExternalIndex.Add(new Vector2Int(-1,  0), 6);
        directionToExternalIndex.Add(new Vector2Int(-1,  1), 7);

        externalIndexToDirection.Clear();
        externalIndexToDirection.Add(0, new Vector2Int( 0,  1));
        externalIndexToDirection.Add(1, new Vector2Int( 1,  1));
        externalIndexToDirection.Add(2, new Vector2Int( 1,  0));
        externalIndexToDirection.Add(3, new Vector2Int( 1, -1));
        externalIndexToDirection.Add(4, new Vector2Int( 0, -1));
        externalIndexToDirection.Add(5, new Vector2Int(-1, -1));
        externalIndexToDirection.Add(6, new Vector2Int(-1,  0));
        externalIndexToDirection.Add(7, new Vector2Int(-1,  1));

        for(int i = 0; i < buildingWeights.Length; i++) {
            for(int j = 0; j < buildingWeights[i]; j++) {
                buildingChoices.Add(i);
            }
        }

        buildingCount = buildingChoices.Count;
    }

    public static ChunkData getChunk(int X, int Y) {

        List<Vector2> vertices = new List<Vector2>();
        List<RoadInfo> roads = new List<RoadInfo>();
        List<RoadInfo>[] externalRoads = new List<RoadInfo>[8];
        List<BuildingInfo> buildings = new List<BuildingInfo>();
        List<BuildingInfo>[] externalBuildings = new List<BuildingInfo>[8];
        for (int i = 0; i < 8; i++) externalRoads[i] = new List<RoadInfo>();
        for (int i = 0; i < 8; i++) externalBuildings[i] = new List<BuildingInfo>();

        addRoads(X, Y, vertices, roads, externalRoads);
        addBuildings(X, Y, buildings, externalBuildings);

        ChunkData ret = new ChunkData(
            new Vector2Int(X, Y), 
            vertices, 
            roads, 
            externalRoads, 
            buildings, 
            externalBuildings
        );

        return ret;
    }

    private static void addRoads(int X, int Y, List<Vector2> vertices, List<RoadInfo> roads, List<RoadInfo>[] externalRoads) {


        float minX = X * chunkSize;
        float minY = Y * chunkSize;
        float maxX = (X + 1) * chunkSize;
        float maxY = (Y + 1) * chunkSize;

        // add vertices in a grid, assumes chunkSize is divisible by roadLength
        // add edges below and left of the vertices when they are placed
        // edges going off the chunk are recorded as external instead of internal
        for (float x = minX; x < maxX; x += roadLength) {
            for (float y = minY; y < maxY; y += roadLength) {
                Vector2 placed = new Vector2(x, y);

                vertices.Add(placed);

                float x2 = x - roadLength;
                float y2 = y;

                if (x2 < minX) {
                    externalRoads[6].Add(new RoadInfo(placed, new Vector2(x2, y2))); //external left
                }
                else {
                    roads.Add(new RoadInfo(placed, new Vector2(x2, y2)));
                }

                x2 = x;
                y2 = y - roadLength;

                if (y2 < minY) {
                    externalRoads[4].Add(new RoadInfo(placed, new Vector2(x2, y2))); // external down
                }
                else {
                    roads.Add(new RoadInfo(placed, new Vector2(x2, y2)));
                }
            }
        }

        //add external edges to the right and above the grid. assumes chunksize is divisible by roadLength

        for (float x2 = minX; x2 < maxX; x2 += roadLength) {
            externalRoads[0].Add(new RoadInfo(new Vector2(x2, maxY), new Vector2(x2, maxY - roadLength))); // external up
        }

        for (float y2 = minY; y2 < maxY; y2 += roadLength) {
            externalRoads[2].Add(new RoadInfo(new Vector2(maxX, y2), new Vector2(maxX - roadLength, y2))); // external right
        }
    }


    private static void addBuildings(int X, int Y, List<BuildingInfo> buildings, List<BuildingInfo>[] externalBuildings) {
        int minX = X * WorldGenerationConstants.chunkBuildingWidth + 1;
        int minY = Y * WorldGenerationConstants.chunkBuildingWidth + 1;
        int maxX = (X + 1) * WorldGenerationConstants.chunkBuildingWidth - 1;
        int maxY = (Y + 1) * WorldGenerationConstants.chunkBuildingWidth - 1;

        int [][] currentPerm = Math.generatePerm(X, Y, permSize);

        for (int x = minX; x < maxX; x++) {
            for (int y = minY; y < maxY; y++) {
                BuildingInfo buildingInfo = getBuilding(x, y, currentPerm);

                buildings.Add(buildingInfo);
            }
        }

        Vector2Int displacement;
        int[][] externalPerm;

        displacement = externalIndexToDirection[0];
        externalPerm = Math.generatePerm(X + displacement.x, Y + displacement.y, permSize);
        for (int x = minX; x < maxX; x++) {
            externalBuildings[0].Add(getBuilding(x, maxY + 0, externalPerm));
            externalBuildings[0].Add(getBuilding(x, maxY + 1, externalPerm));

            externalBuildings[4].Add(getBuilding(x, minY - 1, currentPerm));
            externalBuildings[4].Add(getBuilding(x, minY - 2, currentPerm));
        }

        displacement = externalIndexToDirection[2];
        externalPerm = Math.generatePerm(X + displacement.x, Y + displacement.y, permSize);
        for (int y = minY; y < maxY; y++) {
            externalBuildings[2].Add(getBuilding(maxX, y + 0, externalPerm));
            externalBuildings[2].Add(getBuilding(maxX + 1, y, externalPerm));

            externalBuildings[6].Add(getBuilding(minX - 1, y, currentPerm));
            externalBuildings[6].Add(getBuilding(minX - 2, y, currentPerm));
        }

        displacement = externalIndexToDirection[1];
        externalPerm = Math.generatePerm(X + displacement.x, Y + displacement.y, permSize);
        externalBuildings[1].Add(getBuilding(maxX + 0, maxY + 0, externalPerm));
        externalBuildings[1].Add(getBuilding(maxX + 1, maxY + 0, externalPerm));
        externalBuildings[1].Add(getBuilding(maxX + 0, maxY + 1, externalPerm));
        externalBuildings[1].Add(getBuilding(maxX + 1, maxY + 1, externalPerm));

        displacement = externalIndexToDirection[2];
        externalPerm = Math.generatePerm(X + displacement.x, Y + displacement.y, permSize);
        externalBuildings[3].Add(getBuilding(maxX + 0, minY - 1, externalPerm));
        externalBuildings[3].Add(getBuilding(maxX + 1, minY - 1, externalPerm));
        externalBuildings[3].Add(getBuilding(maxX + 0, minY - 2, externalPerm));
        externalBuildings[3].Add(getBuilding(maxX + 1, minY - 2, externalPerm));

        externalBuildings[5].Add(getBuilding(minX - 1, minY - 1, currentPerm));
        externalBuildings[5].Add(getBuilding(minX - 2, minY - 1, currentPerm));
        externalBuildings[5].Add(getBuilding(minX - 1, minY - 2, currentPerm));
        externalBuildings[5].Add(getBuilding(minX - 2, minY - 2, currentPerm));

        displacement = externalIndexToDirection[0];
        externalPerm = Math.generatePerm(X + displacement.x, Y + displacement.y, permSize);
        externalBuildings[7].Add(getBuilding(minX - 1, maxY + 0, externalPerm));
        externalBuildings[7].Add(getBuilding(minX - 2, maxY + 0, externalPerm));
        externalBuildings[7].Add(getBuilding(minX - 1, maxY + 1, externalPerm));
        externalBuildings[7].Add(getBuilding(minX - 2, maxY + 1, externalPerm));
    }

    private static float[][] rotation = new float[][] {
        new float[] {0, 90},
        new float[] {270, 180}
    };

    private static BuildingInfo getBuilding(int x, int y, int[][] perm) {
        BuildingInfo buildingInfo = new BuildingInfo();

        int displaceX = Math.mod(x, buildingsPerRoad);
        int displaceY = Math.mod(y, buildingsPerRoad);

        int rotXInd = displaceX % 2;
        int rotYInd = displaceY % 2;

        buildingInfo.pos = new Vector2(
            ((x - displaceX) / 2) * roadLength + buildingOffset[displaceX],
            ((y - displaceY) / 2) * roadLength + buildingOffset[displaceY]
        );

        buildingInfo.buildingType = getBuildingType(x, y, perm);

        buildingInfo.rot = rotation[rotXInd][rotYInd];

        return buildingInfo;
    }

    private static int getBuildingType(int x, int y, int[][] perm) {

        int index = perm[Math.mod(x, permSize)][Math.mod(y, permSize)] % buildingCount;

        return buildingChoices[index];
    }
}