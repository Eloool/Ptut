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

    [ContextMenu("Convert Terrain Elements")]
    public void ConvertTerrainElementsToGameObjects()
    {
        Terrain terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            Debug.LogError("Aucun terrain actif trouv� !");
            return;
        }

        TerrainData terrainData = terrain.terrainData;

        TreeInstance[] terrainTrees = terrainData.treeInstances;

        if (terrainElements.Length == 0)
        {
            Debug.LogError("Aucun prefab assign� !");
            return;
        }

        foreach (TreeInstance tree in terrainTrees)
        {
            // Calculer la position dans le monde
            Vector3 worldPosition = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            // Obtenir le prefab correspondant
            int prototypeIndex = tree.prototypeIndex;
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

            // Instancier l'objet principal
            GameObject newElement = Instantiate(prefab, worldPosition, Quaternion.identity, this.transform);
            newElement.name = $"{terrainElements[prototypeIndex].name}_{Random.Range(0, 10000)}";

            // Localiser l'enfant contenant le renderer (LOD)
            Transform lodChild = newElement.transform.Find(prefab.name + "LOD"); // Exemple : "RockIronLOD"
            if (lodChild != null)
            {
                // Appliquer la rotation et l'�chelle sur l'enfant (LOD)
                lodChild.rotation = Quaternion.Euler(0f, tree.rotation * Mathf.Rad2Deg, 0f);
                lodChild.localScale = tree.widthScale * Vector3.one;
            }
            else
            {
                Debug.LogWarning($"Le prefab {prefab.name} n'a pas d'enfant avec le suffixe 'LOD'.");
            }
        }

        // Supprimer les arbres du terrain
        terrainData.treeInstances = new TreeInstance[0];

        Debug.Log("Conversion termin�e : " + terrainTrees.Length + " �l�ments remplac�s.");
    }
}
