using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // Permet au script de s'ex�cuter dans l'�diteur
public class TreeToGameObjectConverter : MonoBehaviour
{
    [System.Serializable]
    public class TerrainElement
    {
        public GameObject prefab; // Prefab correspondant � cet �l�ment
        public string name; // Nom de l'�l�ment (facultatif, pour l'organisation)
    }

    public TerrainElement[] terrainElements; // Liste des prefabs pour chaque prototype du terrain

    [ContextMenu("Convert Terrain Elements")] // Option pour convertir les �l�ments
    public void ConvertTerrainElementsToGameObjects()
    {
        Terrain terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            Debug.LogError("Aucun terrain actif trouv� !");
            return;
        }

        TerrainData terrainData = terrain.terrainData;

        // R�cup�re tous les arbres/�l�ments peints sur le terrain
        TreeInstance[] terrainTrees = terrainData.treeInstances;

        if (terrainElements.Length == 0)
        {
            Debug.LogError("Aucun prefab assign� dans la liste des �l�ments !");
            return;
        }

        foreach (TreeInstance tree in terrainTrees)
        {
            // Position dans le monde
            Vector3 worldPosition = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            // Rotation
            Quaternion worldRotation = Quaternion.Euler(0f, 0f, 0f);

            // �chelle
            Vector3 worldScale = (tree.widthScale + 100) * Vector3.one;

            // D�termine le prefab correspondant au prototype
            int prototypeIndex = tree.prototypeIndex; // Prototype utilis� pour cet arbre/�l�ment
            if (prototypeIndex < 0 || prototypeIndex >= terrainElements.Length)
            {
                Debug.LogWarning($"Aucun prefab assign� pour le prototype {prototypeIndex}.");
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

        // Supprime les �l�ments peints du terrain
        terrainData.treeInstances = new TreeInstance[0];

        Debug.Log("Conversion termin�e : " + terrainTrees.Length + " �l�ments remplac�s.");
    }
}
