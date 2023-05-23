using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class WorldGenerationConstants
{
    public static int chunkSize = 540; // width and height of a chunk in world units. multiple of roadLength.
    public static float roadLength = 90; // distance between the center of 2 intersections in world units
    public static float roadWidth = 20; // width of a road in world units

    public static int buildingsPerRoad = 2;
    public static float buildingWidth = (roadLength - roadWidth) / buildingsPerRoad; // width (and length) of a building in world units
    public static int chunkBuildingWidth = (chunkSize / (int)roadLength) * buildingsPerRoad;
    public static int buildingPermSize = chunkBuildingWidth + 1;
    public static int intersectionPermSize = (chunkSize / (int)roadLength) + 1;
}