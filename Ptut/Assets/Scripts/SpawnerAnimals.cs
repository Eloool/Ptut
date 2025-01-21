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
        SpawnEntities(Sheep, 14f / 64f); // Moutons, Nb entité moyens
        SpawnEntities(Bear, 20f / 64f); // Ours, Nb entité moyens
        SpawnEntities(Goat, 14f / 64f); // Chèvres, Nb entité moyens
        SpawnEntities(Pig, 13f / 64f); // Cochons, Nb entité moyens
        SpawnEntities(Horse, 15f / 64f); // Chevaux, Nb entité moyens
        SpawnEntities(Cow, 14f / 64f); // Vaches, Nb entité moyens

        // Appeler la fonction pour spawner une seule RockSword
        SpawnSingleEntity(RockSword);
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

    void SpawnSingleEntity(GameObject entityPrefab)
    {
        if (entityPrefab == null)
        {
            Debug.LogError("Prefab non assigné !");
            return;
        }

        // Générer une position aléatoire sur le terrain
        float spawnX = Random.Range(0, TailleMap);
        float spawnZ = Random.Range(0, TailleMap);
        float spawnY = GetTerrainHeightAtPosition(new Vector3(spawnX, 0, spawnZ));
        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);

        // Instancier l'entité
        Instantiate(entityPrefab, spawnPos, Quaternion.identity);
    }
}
