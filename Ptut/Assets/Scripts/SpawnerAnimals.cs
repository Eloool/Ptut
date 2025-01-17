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

    public float nbChunkInLine = 8; // Nombre de chunks sur une ligne
    private float TailleMap;
    private float TailleChunk;

    public Terrain terrain; // Assurez-vous que cet objet est assigné dans l'inspecteur

    void Start()
    {
        // Récupérer la taille du terrain
        if (terrain == null)
        {
            Debug.LogError("Le terrain n'est pas assigné !");
            return;
        }

        float terrainWidth = terrain.terrainData.size.x;
        TailleMap = terrainWidth; // Mets la TailleMap à la longueur du terrain
        TailleChunk = TailleMap / nbChunkInLine; // Calculer la taille d'un chunk

        // Appeler la fonction Spawner pour chaque type d'animal
        SpawnEntities(Sheep, 9f / 64f); // Moutons
        SpawnEntities(Bear, 9f / 64f); // Ours
        SpawnEntities(Goat, 10f / 64f); // Chèvres
        SpawnEntities(Pig, 8f / 64f); // Cochons
        SpawnEntities(Horse, 7f / 64f); // Chevaux
        SpawnEntities(Cow, 6f / 64f); // Vaches
    }

    float GetTerrainHeightAtPosition(Vector3 position)
    {
        if (terrain != null)
        {
            return terrain.SampleHeight(position);
        }
        else
        {
            Debug.LogError("Terrain non assigné !");
            return 0f; // Retourner une hauteur par défaut si aucun terrain n'est assigné
        }
    }

    void SpawnEntities(GameObject entityPrefab, float spawnProbability)
    {
        if (entityPrefab == null)
        {
            Debug.LogError("Prefab non assigné !");
            return;
        }

        // Parcourir les chunks pour générer les entités
        for (int i = 0; i < nbChunkInLine; i++)
        {
            float MinXMap = i * TailleChunk;
            float MaxXMap = MinXMap + TailleChunk;

            for (int j = 0; j < nbChunkInLine; j++)
            {
                float MinZMap = j * TailleChunk;
                float MaxZMap = MinZMap + TailleChunk;

                float rand = Random.value;
                if (rand < spawnProbability) // Vérifier la probabilité de spawn
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
}
