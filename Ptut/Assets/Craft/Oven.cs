using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class Oven : InteractibleGameObject
{
    public Item Combustible;
    public Item Bruler;
    public Item Sortie;
    private int HealthBruler = 20;
    private int HealthItem = 0;
    private float timeBetweenTick = 1.0f;
    public bool iscooking;
    public bool isOpen =false;
    private float currentTimeBetweenTicks=0.0f;
    private FuelStat fuel;
    private int FuelRemaining;

    private CookingData CookingData;

    public override bool Interact()
    {
        CanvasOven.instance.ToggleOven(this);
        return true;
    }

    private void Update()
    {
        if (iscooking) Cooking();
    }

    public void StartCooking()
    {
        bool canBeginCooking = false;
        if (Bruler != null && FuelRemaining>=0)
        {
            CookingData = CanvasOven.instance.GetCookingData(Bruler.ItemData);
            if (Sortie == null )
            {
                if (Bruler.amount >= CookingData.ItemsNeededforCooking.amount)
                {
                    canBeginCooking = true;
                }
            }
            else
            {
                if (Sortie.ItemData == CookingData.ItemGotbyCooking.requiredItem &&
                    CookingData.ItemGotbyCooking.amount + Sortie.amount <= Sortie.ItemData.amountStockableMax
                    )
                {
                    canBeginCooking = true;
                }
            }
            if (Bruler.amount <= 0)
            {
                canBeginCooking = false;
                Bruler.parent.GetComponent<InventoryItem>().item = null;
                Bruler.parent = null;
                Destroy(Bruler.gameObject);
                Bruler = null;
            }
            if(Combustible == null)
            {
                canBeginCooking = false;
            }
            else
            {
                fuel = Combustible.GetStat<FuelStat>();
            }
        }
        if (canBeginCooking)
        {
            if (CookingData != null && fuel !=null)
            {
                if (CookingData.ItemsNeededforCooking.requiredItem == Bruler.ItemData)
                {
                    iscooking = true;
                    HealthItem = HealthBruler;
                    currentTimeBetweenTicks = 0.0f;
                    if (FuelRemaining <= 0)
                    {
                        Combustible.MinusOne();
                        FuelRemaining = fuel.maxFuel;
                    }
                }
            }
        }
    }

    private void Cooking()
    {
        currentTimeBetweenTicks += Time.unscaledDeltaTime;
        while (currentTimeBetweenTicks > timeBetweenTick)
        {
            if (Combustible == null)
            {
                StopCooking(false);
                return;
            }
            if (FuelRemaining <= 0)
            {
                FuelRemaining = fuel.maxFuel;
                Combustible.MinusOne();
            }

            currentTimeBetweenTicks -= timeBetweenTick;
            HealthItem -= fuel.fuelParTick;
            FuelRemaining -= fuel.fuelParTick;
            if (HealthItem <= 0)
            {
                StopCooking(true);
            }
        }
      }

    public void StopCooking(bool hasfinished)
    {
        iscooking = false;
        if (hasfinished)
        { 
            Bruler.amount -= CookingData.ItemsNeededforCooking.amount;
            Bruler.UpdateTextAmount();
            if (Sortie ==null)
            {
                GameObject sortie = CanvasOven.instance.ListInventoryItem[2].gameObject;
                GameObject GameObjectCooked = Instantiate(ListAllItems.instance.listeallItems[CookingData.ItemGotbyCooking.requiredItem.id].prefabIcon);
                GameObjectCooked.GetComponent<Item>().amount = CookingData.ItemGotbyCooking.amount;
                GameObjectCooked.GetComponent<Image>().sprite = CookingData.ItemGotbyCooking.requiredItem.iconImage;
                GameObjectCooked.GetComponent<Item>().CreateTextAmount();
                GameObjectCooked.transform.SetParent(sortie.transform);
                sortie.GetComponent<OvenInventoryItem>().DropItemDirect(GameObjectCooked.GetComponent<Item>());
                GameObjectCooked.GetComponent<RectTransform>().localPosition = Vector3.zero;
                GameObjectCooked.GetComponent<RectTransform>().localScale = Vector3.one;
                Sortie = sortie.GetComponent<OvenInventoryItem>().item;
                Sortie.candragitem = true;
                Sortie.UpdateTextAmount();
                if (!isOpen)
                {
                    GameObject notOpenObject = sortie.GetComponent<OvenInventoryItem>().DetachItem();
                    notOpenObject.transform.SetParent(gameObject.transform);
                }
            }
            else
            {
                Sortie.amount += CookingData.ItemGotbyCooking.amount;
                Sortie.UpdateTextAmount();
            }
            
        }
        StartCooking();
    }

    public void SetCombustible(Item Combustible)
    {
        this.Combustible= Combustible;
    }
    
    public void SetBruler(Item bruler)
    {
        this.Bruler = bruler;
    }

    public void SetSortie(Item Sortie)
    {
        this.Sortie= Sortie;
    }
}
