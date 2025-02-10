using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAnimals : MonoBehaviour
{
    public GameObject Bear;
    public GameObject Goat;
    public GameObject Pig;
    public GameObject Horse;
    public GameObject Cow;
    public GameObject Sheep;

    public GameObject RockSword;

    public float nbChunkInLine = 8; // Nombre de chunks sur une ligne
    private float TailleMap;
    private float TailleChunk;

    public Terrain terrain; // Assurez-vous que cet objet est assign� dans l'inspecteur

    public GameObject player; // Assigne le joueur dans l'inspecteur

    void Start()
    {
        // V�rification que le terrain est bien assign�
        if (terrain == null)
        {
            Debug.LogError("Le terrain n'est pas assign� !");
            return;
        }

        float terrainWidth = terrain.terrainData.size.x;
        TailleMap = terrainWidth;
        TailleChunk = TailleMap / nbChunkInLine;

        // D�placer le joueur � une position al�atoire
        if (player != null)
        {
            MovePlayerToRandomPosition();
        }
        else
        {
            Debug.LogError("Aucun joueur assign� !");
        }

        // Spawner les entit�s
        SpawnEntities(Sheep, 25f / 64f);
        SpawnEntities(Bear, 40f / 64f);
        SpawnEntities(Goat, 30f / 64f);
        SpawnEntities(Pig, 25f / 64f);
        SpawnEntities(Horse, 23f / 64f);
        SpawnEntities(Cow, 29f / 64f);

        // Spawner une seule RockSword
        SpawnSingleEntity(RockSword);
    }

    void MovePlayerToRandomPosition()
    {
        float spawnX = Random.Range(0, TailleMap);
        float spawnZ = Random.Range(0, TailleMap);
        float spawnY = GetTerrainHeightAtPosition(new Vector3(spawnX, 0, spawnZ));

        Vector3 randomPosition = new Vector3(spawnX, spawnY, spawnZ);
        player.transform.position = randomPosition;

        Debug.Log($"Joueur d�plac� � {randomPosition}");
    }


    float GetTerrainHeightAtPosition(Vector3 position)
    {
        if (terrain != null)
        {
            return terrain.SampleHeight(position);
        }
        else
        {
            Debug.LogError("Terrain non assign� !");
            return 0f; // Retourner une hauteur par d�faut si aucun terrain n'est assign�
        }
    }

    void SpawnEntities(GameObject entityPrefab, float spawnProbability)
    {
        if (entityPrefab == null)
        {
            Debug.LogError("Prefab non assign� !");
            return;
        }

        // Parcourir les chunks pour g�n�rer les entit�s
        for (int i = 0; i < nbChunkInLine; i++)
        {
            float MinXMap = i * TailleChunk;
            float MaxXMap = MinXMap + TailleChunk;

            for (int j = 0; j < nbChunkInLine; j++)
            {
                float MinZMap = j * TailleChunk;
                float MaxZMap = MinZMap + TailleChunk;

                float rand = Random.value;
                if (rand < spawnProbability) // V�rifier la probabilit� de spawn
                {
                    float spawnX = Random.Range(MinXMap, MaxXMap);
                    float spawnZ = Random.Range(MinZMap, MaxZMap);
                    float spawnY = GetTerrainHeightAtPosition(new Vector3(spawnX, 0, spawnZ));
                    Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);

                    Instantiate(entityPrefab, spawnPos, Quaternion.identity);
                }
            }
        }
    }

    void SpawnSingleEntity(GameObject entityPrefab)
    {
        if (entityPrefab == null)
        {
            Debug.LogError("Prefab non assign� !");
            return;
        }

        // G�n�rer une position al�atoire sur le terrain
        float spawnX = Random.Range(0, TailleMap);
        float spawnZ = Random.Range(0, TailleMap);
        float spawnY = GetTerrainHeightAtPosition(new Vector3(spawnX, 0, spawnZ));
        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);

        // Instancier l'entit�
        Instantiate(entityPrefab, spawnPos, Quaternion.identity);
    }
}
