using System.Collections.Generic;
using UnityEngine;

public class orderManagement : MonoBehaviour, IInteractable
{
    Dishes dishes;
    AllowedItems allowedItems;
    SetFoodMaterials setFoodMaterials;
    public angerManagement angerManagement;
    List<Dishes.Dish> orderedMeals = new List<Dishes.Dish>();

    
    public string Interact(Collider col)
    {

        if (!this.enabled)
        {
            return null;
        }
        dishes = GetComponent<Dishes>();
        if (dishes == null)
        {
            Debug.LogError("Dishes component not found on " + gameObject.name);
            return null;
        }
        allowedItems = GetComponent<AllowedItems>();
        if (allowedItems == null)
        {
            Debug.LogError("AllowedItems component not found on " + gameObject.name);
            return null;
        }
        setFoodMaterials = FindAnyObjectByType<SetFoodMaterials>();
        if (setFoodMaterials == null)
        {
            Debug.LogError("SetFoodMaterials component not");
            return null;
        }
        int attempts = 2;
        int startPoint = 3;
        // int maxVal = dishes.allDishes.Length;
        int maxVal = 7 + 1;





        for (var i = 0; i < Random.Range(1, attempts + 1); i++)
        {
            allowedItems.onlyAllowedItems.Clear();
            dishes.currentDish = dishes.getDish(Random.Range(startPoint, maxVal));
            allowedItems.onlyAllowedItems.Add(dishes.currentDish);
        }

        // assumes no more than 2 orders 
        for (int j = 0; j < allowedItems.onlyAllowedItems.Count; j++)
        {
            transform.GetChild(j).gameObject.SetActive(true);
            dishes = transform.GetChild(j).GetComponent<Dishes>();
            dishes.currentDish = allowedItems.onlyAllowedItems[j];
            setFoodMaterials.SetCustomerPreview(transform.GetChild(j).gameObject, allowedItems.onlyAllowedItems[j]);
        }

        string msg = name + " has ordered " + allowedItems.listDishes();
        Debug.Log(msg);

        this.enabled = false;
        return null;
    }


    void OnDisable()
    {
        if (angerManagement != null)
        {
            angerManagement.startAnger();
        }
    }



}
