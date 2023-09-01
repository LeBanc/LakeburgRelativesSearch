using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class MagicalBookManager : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text year;
    public Villager selectedVillager;

    public VillagersContener perfectContener;
    public VillagersContener awesomeContener;
    public VillagersContener goodContener;
    public VillagersContener neutralContener;
    public VillagersContener badContener;
    public VillagersContener horribleContener;
    public VillagersContener awefulContener;

    private bool isInit = false;
    private VillagersManager villagersManager;

    private bool onlyRemoveBloodRelated = false;

    private void Start()
    {
        villagersManager = FindFirstObjectByType<VillagersManager>();
    }

    private void Init()
    {
        perfectContener.ClearVillagers();
        awesomeContener.ClearVillagers();
        goodContener.ClearVillagers();
        neutralContener.ClearVillagers();
        badContener.ClearVillagers();
        horribleContener.ClearVillagers();
        awefulContener.ClearVillagers();
    }

    private void Update()
    {
        if (!isInit)
        {
            Init();
            selectedVillager.SetDraggable(false);
            selectedVillager.SetClickable(false);
            isInit = true;
        }
    }

    public void UpdateMatches(VillagerData v)
    {
        Init();

        selectedVillager.SetVillager(v);

        List<VillagerData> perfectMatches = new List<VillagerData>();
        List<VillagerData> awesomeMatches = new List<VillagerData>();
        List<VillagerData> goodMatches = new List<VillagerData>();
        List<VillagerData> neutralMatches = new List<VillagerData>();
        List<VillagerData> badMatches = new List<VillagerData>();
        List<VillagerData> horribleMatches = new List<VillagerData>();
        List<VillagerData> awefulMatches = new List<VillagerData>();

        List<VillagerData> bloodRelated = villagersManager.FindAllBloodRelatedVillagers(v);
        List<VillagerData> otherRelatives = new List<VillagerData>();
        otherRelatives.AddRange(v._greatGrandParents);
        otherRelatives.AddRange(v._stepGrandParents);
        otherRelatives.AddRange(v._grandParentsInLaw);
        otherRelatives.AddRange(v._grandPiblings);
        otherRelatives.AddRange(v._stepParents);
        otherRelatives.AddRange(v._parentsInLaw);
        otherRelatives.AddRange(v._piblings);
        otherRelatives.AddRange(v._stepSiblings);
        otherRelatives.AddRange(v._siblingsInLaw);
        otherRelatives.AddRange(v._cousins);
        otherRelatives.AddRange(v._coParentsInLaw);
        otherRelatives.AddRange(v._stepChildren);
        otherRelatives.AddRange(v._childrenInLaw);
        otherRelatives.AddRange(v._niblings);
        otherRelatives.AddRange(v._stepGrandChildren);
        otherRelatives.AddRange(v._grandChildrenInLaw);
        otherRelatives.AddRange(v._grandNiblings);
        foreach (VillagerData v1 in bloodRelated)
        {
            if (otherRelatives.Contains(v1)) otherRelatives.Remove(v1);
        }

        foreach (VillagerData v1 in villagersManager.villagers)
        {
            if (v == v1) continue;
            if (v1._isDead) continue;
            if (v1._isExiled) continue;
            if (bloodRelated.Contains(v1)) continue;
            if(!onlyRemoveBloodRelated)
            {
                if (otherRelatives.Contains(v1)) continue;
            }

            int loveMeter = 0;
            foreach(string topic in v1._likedTopics)
            { 
                if (v._likedTopics.Contains(topic)) loveMeter++;
                if (v._dislikedTopics.Contains(topic)) loveMeter--;
            }
            foreach (string topic in v1._dislikedTopics)
            {
                if (v._likedTopics.Contains(topic)) loveMeter--;
                if (v._dislikedTopics.Contains(topic)) loveMeter++;
            }

            switch(loveMeter)
            {
                case 3:
                    perfectMatches.Add(v1);
                    break;
                case 2:
                    awesomeMatches.Add(v1);
                    break;
                case 1:
                    goodMatches.Add(v1);
                    break;
                case 0:
                    neutralMatches.Add(v1);
                    break;
                case -1:
                    badMatches.Add(v1);
                    break;
                case -2:
                    horribleMatches.Add(v1);
                    break;
                case -3:
                    awefulMatches.Add(v1);
                    break;
                default:
                    neutralMatches.Add(v1);
                    break;
            }
        }

        // Villager Name
        title.text = v._firstName + " " + v._lastName + " (" + v._birthYear.ToString() + " - " + (v._isDead ? v._deathYear.ToString() : (v._isExiled ? "?" : "")) + ")";

        foreach (VillagerData v1 in perfectMatches) perfectContener.AddVillager(v1);

        foreach (VillagerData v1 in awesomeMatches) awesomeContener.AddVillager(v1);

        foreach (VillagerData v1 in goodMatches) goodContener.AddVillager(v1);

        foreach (VillagerData v1 in neutralMatches) neutralContener.AddVillager(v1);

        foreach (VillagerData v1 in badMatches) badContener.AddVillager(v1);

        foreach (VillagerData v1 in horribleMatches) horribleContener.AddVillager(v1);

        foreach (VillagerData v1 in awefulMatches) awefulContener.AddVillager(v1);
    }
}
