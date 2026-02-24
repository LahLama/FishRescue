using UnityEditor;
using UnityEngine;

public class TankItem : MonoBehaviour
{
    SelectHotbarItem selectHotbarItem;
    GameObject currentSlot;
    public PrefabAssetType prefabItem;

    void Awake()
    {
        selectHotbarItem = GameObject.FindAnyObjectByType<SelectHotbarItem>();
        currentSlot = selectHotbarItem.slot;
    }
    void MakeTankItem()
    {

    }
}
