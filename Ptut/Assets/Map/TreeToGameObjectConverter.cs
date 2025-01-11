using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // Permet au script de s'ex�cuter dans l'�diteur
public class TreeToGameObjectConverter : MonoBehaviour
{
    public GameObject treePrefab; // Ton prefab d'arbre r�coltable avec collider et script

    [ContextMenu("Convert Terrain Trees")] // Ajoute une option dans le menu contextuel (clic droit)
    public void ConvertTerrainTreesToGameObjects()
    {
        Terrain terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            Debug.LogError("Aucun terrain actif trouv� !");
            return;
        }

        TerrainData terrainData = terrain.terrainData;

        // R�cup�re tous les arbres peints
        TreeInstance[] terrainTrees = terrainData.treeInstances;

        foreach (TreeInstance tree in terrainTrees)
        {
            // Convertir la position relative de l'arbre en position dans le monde
            Vector3 worldPosition = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            // Instancier un GameObject pour chaque arbre
            GameObject newTree = Instantiate(treePrefab, worldPosition, Quaternion.identity, this.transform);
            newTree.name = "Tree_" + Random.Range(0, 10000); // Renomme chaque arbre pour les identifier facilement
        }

        // Supprime les arbres peints du terrain
        terrainData.treeInstances = new TreeInstance[0];

        Debug.Log("Conversion termin�e : " + terrainTrees.Length + " arbres remplac�s.");
    }
}
