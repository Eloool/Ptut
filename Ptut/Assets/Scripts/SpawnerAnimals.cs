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

    
    public Terrain terrain; // Assurez-vous que cet objet est assigné dans l'inspecteur
    // Start is called before the first frame update
    void Start()
    {
        // Récupérer la taille du terrain
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
            // SampleHeight retourne la hauteur du terrain pour une position donnée
            return terrain.SampleHeight(position);
        }
        else
        {
            Debug.LogError("Terrain non assigné !");
            return 0f; // Retourner une hauteur par défaut si aucun terrain n'est assigné
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
                if (rand < 9f / 64f) // En moyenne 9 ours seront générés parmi les 64 chunks
                {
                    MinZMap = j * TailleChunk;
                    MaxZMap = j * TailleChunk + TailleChunk;
                    float spawnX = Random.Range(MinXMap, MaxXMap);
                    float spawnZ = Random.Range(MinZMap, MaxZMap);

                    // Calculer la hauteur du terrain à cette position
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
                if (rand < 10f / 64f) // En moyenne 10 chèvres seront générées parmi les 64 chunks
                {
                    MinZMap = j * TailleChunk;
                    MaxZMap = j * TailleChunk + TailleChunk;
                    float spawnX = Random.Range(MinXMap, MaxXMap);
                    float spawnZ = Random.Range(MinZMap, MaxZMap);

                    // Calculer la hauteur du terrain à cette position
                    float spawnY = GetTerrainHeightAtPosition(new Vector3(spawnX, 0, spawnZ));
                    Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);

                    Instantiate(Goat, spawnPos, Quaternion.identity);
                }
            }
        }
    }

}
