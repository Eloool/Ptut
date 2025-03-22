using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static sc.terrain.vegetationspawner.SpawnerBase;

[ExecuteAlways]
public class GenTree : MonoBehaviour
{
    public GameObject treePrefab; // Prefab de l'arbre
    public GameObject treeContainer; // Conteneur des arbres
    public int totalTrees = 1000; // Nombre total d'arbres
    public int columns = 100; // Nombre d'arbres par ligne
    public float treeSpacing = 2f; // Espacement entre les arbres

    // Start is called before the first frame update
    void Start()
    {
        GenerationTree();
    }

    public void GenerationTree()
    {
        // Vérifier si des arbres existent déjà
        if (treeContainer != null && treeContainer.transform.childCount > 0)
        {
            Debug.Log("Les arbres existent déjà !");
            return;
        }

        int rows = Mathf.CeilToInt((float)totalTrees / columns); // Nombre de lignes

        for (int i = 0; i < totalTrees; i++)
        {
            int row = i / columns; // Ligne actuelle
            int col = i % columns; // Colonne actuelle

            Vector3 position = new Vector3(col * treeSpacing, 0, row * treeSpacing);
            Quaternion rotation = Quaternion.identity;

            GameObject newTree = Instantiate(treePrefab, position, rotation, treeContainer != null ? treeContainer.transform : null);
            newTree.name = "Tree_" + i; // Renomme chaque arbre
        }

        Debug.Log("Génération des arbres terminée !");
    }
}
