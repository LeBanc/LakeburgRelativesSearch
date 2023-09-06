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

    public VillagersContener match6Contener;
    public VillagersContener match5Contener;
    public VillagersContener match4Contener;
    public VillagersContener match3Contener;
    public VillagersContener match2Contener;
    public VillagersContener match1Contener;
    public VillagersContener match0Contener;
    public VillagersContener match_1Contener;
    public VillagersContener match_2Contener;
    public VillagersContener match_3Contener;
    public VillagersContener match_4Contener;
    public VillagersContener match_5Contener;
    public VillagersContener match_6Contener;
    
    private bool isInit = false;
    private VillagersManager villagersManager;

    private bool onlyRemoveBloodRelated = false;
    private int ageThreshold = 25;
    private int ageThresholdChild = 3;

    private void Start()
    {
        villagersManager = FindFirstObjectByType<VillagersManager>();
    }

    private void Init()
    {
        match0Contener.ClearVillagers();
        match6Contener.ClearVillagers();
        match5Contener.ClearVillagers();
        match4Contener.ClearVillagers();
        match3Contener.ClearVillagers();
        match2Contener.ClearVillagers();
        match1Contener.ClearVillagers();
        match_1Contener.ClearVillagers();
        match_2Contener.ClearVillagers();
        match_3Contener.ClearVillagers();
        match_4Contener.ClearVillagers();
        match_5Contener.ClearVillagers();
        match_6Contener.ClearVillagers();
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

    public void UpdateBloodRelated(int value)
    {
        if(value > 0)
        {
            onlyRemoveBloodRelated = true;
        }
        else
        {
            onlyRemoveBloodRelated = false;
        }
        UpdateMatches(selectedVillager.villager);
    }

    public void UpdateAgeThreshold(float value)
    {
        ageThreshold = (int)value;
        UpdateMatches(selectedVillager.villager);
    }

    public void UpdateMatches(VillagerData v)
    {
        Init();

        if (selectedVillager.villager != v)
        {
            selectedVillager.SetVillager(v);
            selectedVillager.SetClickable(true);
            if(v._partner != null)
            {
                selectedVillager.ShowRing();
            }
            else
            {
                selectedVillager.ShowCelib();
            }
        }

        int actualAgeThreshold = ageThreshold;
        if ((v._deathYear - v._birthYear) < 18) actualAgeThreshold = Mathf.Min(actualAgeThreshold, ageThresholdChild);

        List<VillagerData> match6 = new List<VillagerData>();
        List<VillagerData> match5 = new List<VillagerData>();
        List<VillagerData> match4 = new List<VillagerData>();
        List<VillagerData> match3 = new List<VillagerData>();
        List<VillagerData> match2 = new List<VillagerData>();
        List<VillagerData> match1 = new List<VillagerData>();
        List<VillagerData> match0 = new List<VillagerData>();
        List<VillagerData> match_1 = new List<VillagerData>();
        List<VillagerData> match_2 = new List<VillagerData>();
        List<VillagerData> match_3 = new List<VillagerData>();
        List<VillagerData> match_4 = new List<VillagerData>();
        List<VillagerData> match_5 = new List<VillagerData>();
        List<VillagerData> match_6 = new List<VillagerData>();

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
            float tempAgeThreshold = actualAgeThreshold;
            if ((v1._deathYear - v1._birthYear) < 18) tempAgeThreshold = Mathf.Min(tempAgeThreshold, ageThresholdChild);
            if (Mathf.Abs(v1._birthYear - v._birthYear) > tempAgeThreshold) continue;

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
                case 6:
                    match6.Add(v1);
                    break;
                case 5:
                    match5.Add(v1);
                    break;
                case 4:
                    match4.Add(v1);
                    break;
                case 3:
                    match3.Add(v1);
                    break;
                case 2:
                    match2.Add(v1);
                    break;
                case 1:
                    match1.Add(v1);
                    break;
                case 0:
                    match0.Add(v1);
                    break;
                case -1:
                    match_1.Add(v1);
                    break;
                case -2:
                    match_2.Add(v1);
                    break;
                case -3:
                    match_3.Add(v1);
                    break;
                case -4:
                    match_4.Add(v1);
                    break;
                case -5:
                    match_5.Add(v1);
                    break;
                case -6:
                    match_6.Add(v1);
                    break;

                default:
                    match0.Add(v1);
                    break;
            }
        }

        // Villager Name
        title.text = v._firstName + " " + v._lastName + " (" + v._birthYear.ToString() + " - " + (v._isDead ? v._deathYear.ToString() : (v._isExiled ? "?" : "")) + ")";

        foreach (VillagerData v1 in match6) match6Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match5) match5Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match4) match4Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match3) match3Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match2) match2Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match1) match1Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match0) match0Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match_1) match_1Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match_2) match_2Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match_3) match_3Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match_4) match_4Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match_5) match_5Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
        foreach (VillagerData v1 in match_6) match_6Contener.AddVillager(v1, v._partner == v1, bloodRelated.Contains(v1), v1._partner == null, true);
    }
}
