using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBehaviour : MonoBehaviour
{
    public enum PartieCorps{
        Helmet,
        Chest,
        Pants,
        Shoes
    }
    public BodyAllGameObjects BodyPlayer;

    public List<BodyAllGameObjects> objects =new List<BodyAllGameObjects>();

    public static ArmorBehaviour instance;

    private void Awake()
    {
        instance = this;
    }

    public void LoadArmor(int id)
    {
        foreach (BodyAllGameObjects obj in objects)
        {
            if(obj.Helmet.id == id)
            {
                obj.Helmet.part.SetActive(true);
                return;
            }
            else if(obj.Chest.id == id)
            {
                obj.Chest.part.SetActive(true);
                BodyPlayer.Chest.part.SetActive(false);
                return;
            }
            else if (obj.Pants.id == id)
            {
                obj.Pants.part.SetActive(true);
                BodyPlayer.Pants.part.SetActive(false);
                return;
            }
            else if (obj.Shoes.id == id)
            {
                obj.Shoes.part.SetActive(true);
                BodyPlayer.Shoes.part.SetActive(false);
                return;
            }
        }
    }
    
    public void DeloadAllArmor()
    {
        foreach (var item in objects)
        {
            if(item.Chest.part !=null)
                item.Chest.part.SetActive(false);
            if (item.Pants.part != null)
                item.Pants.part.SetActive(false);
            if (item.Helmet.part != null)
                item.Helmet.part.SetActive(false);
            if (item.Shoes.part != null)
                item.Shoes.part.SetActive(false);
        }
        BodyPlayer.Chest.part.SetActive(true);
        BodyPlayer.Pants.part.SetActive(true);
        BodyPlayer.Shoes.part.SetActive(true);
    }
}

[System.Serializable]
public class BodyAllGameObjects
{
    public PartWithId Helmet;
    public PartWithId Chest;
    public PartWithId Pants;
    public PartWithId Shoes;
}

[System.Serializable]
public class PartWithId
{
    public GameObject part;
    public int id;
}