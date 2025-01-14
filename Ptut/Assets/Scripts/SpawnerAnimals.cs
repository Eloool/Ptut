using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnerAnimals : MonoBehaviour
{
    public GameObject Bear;
    public GameObject Goat;


    public float MaxHighOfMap = 0;
    private float MinXMap;
    private float MinZMap;
    private float MaxXMap;
    private float MaxZMap;
    private float nbChunkInLine = 8;
    private float TailleMap;
    private float TailleChunk;

    
    public Terrain terrain; // Assurez-vous que cet objet est assign� dans l'inspecteur
    // Start is called before the first frame update
    void Start()
    {
        // R�cup�rer la taille du terrain
        float terrainWidth = terrain.terrainData.size.x;

        

        float terrainLength = terrain.terrainData.size.z;

        // Afficher les tailles dans la console
        Debug.Log("Taille de la carte: " + terrainWidth + " x " + terrainLength);


        TailleMap = terrainWidth;//Mets la TailleMap a la longueur (et largeur) du terrain.


        BearSpawner();
        GoatSpawner();
    }


    float GetTerrainHeightAtPosition(Vector3 position)
    {
        if (terrain != null)
        {
            // SampleHeight retourne la hauteur du terrain pour une position donn�e
            return terrain.SampleHeight(position);
        }
        else
        {
            Debug.LogError("Terrain non assign� !");
            return 0f; // Retourner une hauteur par d�faut si aucun terrain n'est assign�
        }
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
                if (rand < 9f / 64f) // En moyenne 9 ours seront g�n�r�s parmi les 64 chunks
                {
                    MinZMap = j * TailleChunk;
                    MaxZMap = j * TailleChunk + TailleChunk;
                    float spawnX = Random.Range(MinXMap, MaxXMap);
                    float spawnZ = Random.Range(MinZMap, MaxZMap);

                    // Calculer la hauteur du terrain � cette position
                    float spawnY = GetTerrainHeightAtPosition(new Vector3(spawnX, 0, spawnZ));
                    Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);

                    Instantiate(Bear, spawnPos, Quaternion.identity);
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
                if (rand < 10f / 64f) // En moyenne 10 ch�vres seront g�n�r�es parmi les 64 chunks
                {
                    MinZMap = j * TailleChunk;
                    MaxZMap = j * TailleChunk + TailleChunk;
                    float spawnX = Random.Range(MinXMap, MaxXMap);
                    float spawnZ = Random.Range(MinZMap, MaxZMap);

                    // Calculer la hauteur du terrain � cette position
                    float spawnY = GetTerrainHeightAtPosition(new Vector3(spawnX, 0, spawnZ));
                    Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);

                    Instantiate(Goat, spawnPos, Quaternion.identity);
                }
            }
        }
    }

}
