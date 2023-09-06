using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraveyardManager : MonoBehaviour
{

    private VillagersManager villagersManager;
    public TMP_Text year;
    public Transform contener;
    public Villager villagerPrefab;
    public CanvasHandler graveyardCanvas;

    // Start is called before the first frame update
    void Start()
    {
        villagersManager = FindObjectOfType<VillagersManager>();
    }

    public void UpdateGraveyard()
    {
        graveyardCanvas.ShowCanvas();
        // Clear the graveyard
        for (int i = contener.childCount - 1; i >= 0; i--)
        {
            Transform child = contener.GetChild(i);
            DestroyImmediate(child.gameObject);
        }
        
        // Add dead villagers to the list of dead people and sort them by year of death
        List<VillagerData> deadVillagers = new List<VillagerData>();
        foreach(VillagerData v in villagersManager.villagers)
        {
            if(v._isDead) deadVillagers.Add(v);
        }
        deadVillagers.Sort(VillagersManager.CompareByDeathYear);

        // Add dead villagers to graveyard
        foreach(VillagerData v in deadVillagers)
        {
            AddVillager(v);
        }

        // Hide canvas after init
        graveyardCanvas.HideCanvas();
    }

    public void AddVillager(VillagerData v)
    {
        if (contener != null && villagerPrefab != null)
        {
            Villager newVillager = Instantiate(villagerPrefab, contener);
            newVillager.SetVillager(v);
            newVillager.SetDraggable(false);
            newVillager.SetClickable(true);
        }
    }
}
