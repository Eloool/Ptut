using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // Permet au script de s'exécuter dans l'éditeur
public class TreeToGameObjectConverter : MonoBehaviour
{
    [System.Serializable]
    public class TerrainElement
    {
        public GameObject prefab; // Prefab correspondant à cet élément
        public string name; // Nom de l'élément (facultatif, pour l'organisation)
    }

    public TerrainElement[] terrainElements; // Liste des prefabs pour chaque prototype du terrain

    [ContextMenu("Convert Terrain Elements")] // Option pour convertir les éléments
    public void ConvertTerrainElementsToGameObjects()
    {
        Terrain terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            Debug.LogError("Aucun terrain actif trouvé !");
            return;
        }

        TerrainData terrainData = terrain.terrainData;

        // Récupère tous les arbres/éléments peints sur le terrain
        TreeInstance[] terrainTrees = terrainData.treeInstances;

        if (terrainElements.Length == 0)
        {
            Debug.LogError("Aucun prefab assigné dans la liste des éléments !");
            return;
        }

        foreach (TreeInstance tree in terrainTrees)
        {
            // Position dans le monde
            Vector3 worldPosition = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            // Rotation
            Quaternion worldRotation = Quaternion.Euler(0f, 0f, 0f);

            // Échelle
            Vector3 worldScale = (tree.widthScale + 100) * Vector3.one;

            // Détermine le prefab correspondant au prototype
            int prototypeIndex = tree.prototypeIndex; // Prototype utilisé pour cet arbre/élément
            if (prototypeIndex < 0 || prototypeIndex >= terrainElements.Length)
            {
                Debug.LogWarning($"Aucun prefab assigné pour le prototype {prototypeIndex}.");
                continue;
            }

            GameObject prefab = terrainElements[prototypeIndex].prefab;
            if (prefab == null)
            {
                Debug.LogWarning($"Le prefab pour le prototype {prototypeIndex} est vide.");
                continue;
            }

            // Instancier le GameObject
            GameObject newElement = Instantiate(prefab, worldPosition, worldRotation, this.transform);
            newElement.name = $"{terrainElements[prototypeIndex].name}_{Random.Range(0, 10000)}";
            newElement.transform.localScale = worldScale;
        }

        // Supprime les éléments peints du terrain
        terrainData.treeInstances = new TreeInstance[0];

        Debug.Log("Conversion terminée : " + terrainTrees.Length + " éléments remplacés.");
    }
}
