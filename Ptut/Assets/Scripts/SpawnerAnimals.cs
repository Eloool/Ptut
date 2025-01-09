using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnerAnimals : MonoBehaviour
{
    public GameObject Bear;
    public GameObject Goat;


    public int MaxHighOfMap = 0;
    private int MinXMap;
    private int MinZMap;
    private int MaxXMap;
    private int MaxZMap;
    private int nbChunkInLine = 8;
    private int TailleMap = 2048;
    private int TailleChunk;

    public GameObject Plane;
    // Start is called before the first frame update
    void Start()
    {
        BearSpawner();
        GoatSpawner();
    }

    void BearSpawner()
    {
        TailleChunk = TailleMap / nbChunkInLine;
        for (int i = 0; i < nbChunkInLine; i++)
        {
            MinXMap = i * TailleChunk;
            MaxXMap = i * TailleChunk + TailleChunk;
            for (int j = 0; j < nbChunkInLine; j++)
            {
                float rand = Random.value;
                //Debug.Log("valeur aléat générée : " +  rand + " pour le chunk : " + i + ", "+j);
                if (rand < 9f / 64f)//en moyenne 9 ours seront générés parmis les 64 chunks (8 par ligne et 8 par colonne)
                {
                    MinZMap = j * TailleChunk;
                    MaxZMap = j * TailleChunk + TailleChunk;
                    int spawnX = Random.Range(MinXMap, MaxXMap);
                    int spawnZ = Random.Range(MinZMap, MaxZMap);
                    Vector3 spawnPos = new Vector3(spawnX, MaxHighOfMap, spawnZ);
                    Instantiate(Bear, spawnPos, Quaternion.identity);
                    //Debug.Log("L'ours spawn à " + spawnX + ", " + spawnZ);
                }
            }
        }
    }

    void GoatSpawner()
    {
        TailleChunk = TailleMap / nbChunkInLine;
        for (int i = 0; i < nbChunkInLine; i++)
        {
            MinXMap = i * TailleChunk;
            MaxXMap = i * TailleChunk + TailleChunk;
            for (int j = 0; j < nbChunkInLine; j++)
            {
                float rand = Random.value;
                if (rand < 10f / 64f)//en moyenne 10 chevre seront générés parmis les 64 chunks (8 par ligne et 8 par colonne)
                {
                    MinZMap = j * TailleChunk;
                    MaxZMap = j * TailleChunk + TailleChunk;
                    int spawnX = Random.Range(MinXMap, MaxXMap);
                    int spawnZ = Random.Range(MinZMap, MaxZMap);
                    Vector3 spawnPos = new Vector3(spawnX, MaxHighOfMap, spawnZ);
                    Instantiate(Goat, spawnPos, Quaternion.identity);
                }
            }
        }
    }

}
