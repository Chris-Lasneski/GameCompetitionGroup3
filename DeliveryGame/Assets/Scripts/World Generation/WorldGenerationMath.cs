using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class WorldGenerationMath {

    public static int mod(int n, int m) {
        return ((n % m) + m) % m;
    }

    public static int[][] generatePerm(int seedX, int seedY, int size) {
        int[][] perm = new int[size][];

        Random rX = new Random(seedX);
        Random rY = new Random(seedY);
        for (int i = 0; i < size; i++) {
            perm[i] = new int[size];

            for (int j = 0; j < size; j++) {
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
