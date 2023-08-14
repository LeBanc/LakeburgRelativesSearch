using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VillagersManager : MonoBehaviour
{
    public Villager villagerPrefab;
    public RectTransform scrollContent;
    public Scrollbar scrollBar;
    public List<VillagerData> villagers;
    public TMP_Dropdown dropdownSort;

    private void Start()
    {
        if(dropdownSort != null)
        {
            dropdownSort.onValueChanged.AddListener(delegate {
                SortVillagers(dropdownSort.value);
            });
        }
    }

    public void SetScrollBarLeft()
    {
        StartCoroutine(InitScrollBarLeft());
    }

    private IEnumerator InitScrollBarLeft()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        scrollBar.value = 0;
    }

    public void ClearAll()
    {
        ClearVillagerObjects();

        // Clear VillagerData
        villagers.Clear();
    }

    private void ClearVillagerObjects()
    {
        // Clear Villagers in scroll bar
        if (scrollContent != null)
        {
            for (int i = scrollContent.childCount - 1; i >= 0; i--)
            {
                Transform child = scrollContent.GetChild(i);
                DestroyImmediate(child.gameObject);
            }
        }
    }

    #region VillagerData
    public void UpdateVillager(VillagerData v)
    {
        // Debug.Log(v._id);
        v._mother = this.villagers.Find(p => p._id == v._motherId);
        v._father = this.villagers.Find(p => p._id == v._fatherId);

        if (v._partnerId != null)
        {
            v._partner = this.villagers.Find(p => p._id == v._partnerId);
            if(v._partner != null) { v._partner._partner = v; }
        }

        foreach (string childId in v._childrenId)
        {
            VillagerData c = this.villagers.Find(p => p._id == childId);
            if (c != null)
            {
                v._children.Add(c);
            }
        }

        foreach (string exId in v._exesId)
        {
            VillagerData c = this.villagers.Find(p => p._id == exId);
            if (c != null)
            {
                v._exes.Add(c);
            }
        }
    }

    public void UpdateAllVillagers()
    {
        foreach (VillagerData v in villagers)
        {
            UpdateVillager(v);
        }
    }

    public void SortVillagers(int order)
    {
        ClearVillagerObjects();
        switch (order)
        {
            case 0:
                SortVillagersByOldest();
                break;
            case 1:
                SortVillagersByYoungest();
                break;
            case 2:
                SortVillagersByFirstNamesAZ();
                break;
            case 3:
                SortVillagersByFirstNamesZA();
                break;
            case 4:
                SortVillagersByLastNamesAZ();
                break;
            case 5:
                SortVillagersByLastNamesZA();
                break;
            default:
                SortVillagersByYoungest();
                break;
        }
        CreateVillagerObjects();
        SetScrollBarLeft();
    }

    public void SortVillagersByYoungest()
    {
        villagers.Sort(CompareByYoungest);
    }

    public void SortVillagersByOldest()
    {
        villagers.Sort(CompareByOldest);
    }
    public void SortVillagersByFirstNamesAZ()
    {
        villagers.Sort(CompareByAlphabeticalFirstNames);
    }
    public void SortVillagersByLastNamesAZ()
    {
        villagers.Sort(CompareByAlphabeticalLastNames);
    }
    public void SortVillagersByFirstNamesZA()
    {
        villagers.Sort(CompareByReverseAlphabeticalFirstNames);
    }
    public void SortVillagersByLastNamesZA()
    {
        villagers.Sort(CompareByReverseAlphabeticalLastNames);
    }

    public static int CompareByYoungest(VillagerData v1, VillagerData v2)
    {
        if (v1 == null) return -1;
        if (v2 == null) return 1;
        if (v1._birthYear < v2._birthYear) return 1;
        if (v1._birthYear == v2._birthYear) return 0;
        return -1;
    }
    public static int CompareByOldest(VillagerData v1, VillagerData v2)
    {
        if (v1 == null) return 1;
        if (v2 == null) return -1;
        if (v1._birthYear < v2._birthYear) return -1;
        if (v1._birthYear == v2._birthYear) return 0;
        return 1;
    }

    public static int CompareByAlphabeticalFirstNames(VillagerData v1, VillagerData v2)
    {
        if (v1 == null) return -1;
        if (v2 == null) return 1;
        return v1._firstName.CompareTo(v2._firstName);
    }
    public static int CompareByAlphabeticalLastNames(VillagerData v1, VillagerData v2)
    {
        if (v1 == null) return -1;
        if (v2 == null) return 1;
        return v1._lastName.CompareTo(v2._lastName);
    }

    public static int CompareByReverseAlphabeticalFirstNames(VillagerData v1, VillagerData v2)
    {
        if (v1 == null) return 1;
        if (v2 == null) return -1;
        return -v1._firstName.CompareTo(v2._firstName);
    }
    public static int CompareByReverseAlphabeticalLastNames(VillagerData v1, VillagerData v2)
    {
        if (v1 == null) return 1;
        if (v2 == null) return -1;
        return -v1._lastName.CompareTo(v2._lastName);
    }

    #endregion

    #region Villager
    public void CreateVillagerObjects()
    {
        foreach(VillagerData v in villagers)
        {
            Villager newVillager = Instantiate<Villager>(villagerPrefab, scrollContent);
            newVillager.SetVillager(v);
        }
    }
    #endregion

    #region VillagerData advanced
    // Level 1 Relationships (Parents / Children(already done) / Partener(already done) / Exes(already done))
    private void SearchParents(VillagerData v)
    {
        // Set parents
        if (v._mother != null) v._parents.Add(v._mother);
        if (v._father != null) v._parents.Add(v._father);
    }

    // Level 2 Relationships (GrandParents / Siblings / Half-siblings / Parents in Law / Exes(already done) / Children in Law / GrandChildren / StepParents)
    private void SearchGrandParents(VillagerData v)
    {
        // Set Grand-parents
        foreach (VillagerData v1 in v._parents)
        {
            foreach (VillagerData v2 in v1._parents)
            {
                v._grandParents.Add(v2);
            }
        }
    }
    private void SearchSiblings(VillagerData v)
    {
        // Set siblings & half_siblings
        foreach (VillagerData v1 in v._parents)
        {
            foreach (VillagerData v2 in v1._children)
            {
                if (v2._id.Equals(v._id)) continue;
                if ((v2._mother.Equals(v._mother) || v2._mother.Equals(v._father)) && (v2._father.Equals(v._mother) || v2._father.Equals(v._father)))
                {
                    if(!v._siblings.Contains(v2)) v._siblings.Add(v2);
                }
                else
                {
                    if (!v._halfSiblings.Contains(v2)) v._halfSiblings.Add(v2);
                }
            }
        }        
    }
    private void SearchParentsInLaw(VillagerData v)
    {
        if(v._partner != null)
        {
            foreach(VillagerData v1 in v._partner._parents)
            {
                v._parentsInLaw.Add(v1);
            }
        }
    }
    private void SearchChildrenInLaw(VillagerData v)
    {
        foreach (VillagerData child in v._children)
        {
            if (child._partner != null) { v._childrenInLaw.Add(child._partner); }
        }
    }
    private void SearchGrandChildren(VillagerData v)
    {
        foreach (VillagerData v1 in v._children)
        {
            foreach (VillagerData v2 in v1._children)
            {
                v._grandChildren.Add(v2);
            }
        }
    }
    private void SearchStepParents(VillagerData v)
    {
        foreach (VillagerData v1 in v._parents)
        {
            if(v1._partner != null)
            {
                if(!v._parents.Contains(v1._partner)) v._stepParents.Add(v1._partner);
            }
            foreach(VillagerData v2 in v1._exes)
            {
                if (!v._parents.Contains(v2)) v._stepParents.Add(v2);
            }
        }
    }
    private void SearchStepChildren(VillagerData v)
    {
        if (v._partner != null)
        {
            // Set step childrens
            foreach (VillagerData v1 in v._partner._children)
            {
                if (!v._children.Contains(v1)) v._stepChildren.Add(v1);
            }
        }
        foreach (VillagerData v1 in v._exes)
        {
            foreach (VillagerData v2 in v1._children)
            {
                if (!v._children.Contains(v2)) v._stepChildren.Add(v2);
            }
        }
        
    }

    // Level 3 Relationships (GreatGrandParents / StepGrandParents / GrandParentsInLaw / Piblings
    // StepSiblings / SiblingsInLaw / Niblings / GrandChildInLaw / StepGrandChild / GreatGrandChild / SiblingInLaw
    // HalfSiblingInLaw / StepChildInLaw / StepGrandChild / PartnerExes / StepParentsInLaw)
    private void SearchStepGrandParents(VillagerData v)
    {
        // Set step Grand-parents for Parents of Step-parents
        foreach (VillagerData v1 in v._stepParents)
        {
            foreach (VillagerData v2 in v1._parents)
            {
                v._stepGrandParents.Add(v2);
            }
        }
        // Set step Grand-parents for StepParents of parents
        foreach (VillagerData v1 in v._parents)
        {
            foreach (VillagerData v2 in v1._stepParents)
            {
                if (!v._stepGrandParents.Contains(v2)) v._stepGrandParents.Add(v2);
            }
        }
        // Set Step Grand-parents for partner of Grand-Parents that are not Grand-Parents => should be redundant of previous step
        foreach (VillagerData v1 in v._grandParents)
        {
            if (v1._partner != null)
            {
                if (!v._grandParents.Contains(v1._partner)) { v._stepGrandParents.Add(v1._partner); }
            }
        }
    }
    private void SearchGreatGrandParents(VillagerData v)
    {
        foreach(VillagerData v1 in v._grandParents)
        {
            foreach( VillagerData v2 in v1._parents)
            {
                if (!v._greatGrandParents.Contains(v2)) v._greatGrandParents.Add(v2);
            }
            foreach (VillagerData v2 in v1._stepParents)
            {
                if(!v._greatGrandParents.Contains(v2)) v._greatGrandParents.Add(v2);
            }
        }
        foreach (VillagerData v1 in v._stepGrandParents)
        {
            foreach (VillagerData v2 in v1._parents)
            {
                if (!v._greatGrandParents.Contains(v2)) v._greatGrandParents.Add(v2);
            }
            foreach (VillagerData v2 in v1._stepParents)
            {
                if (!v._greatGrandParents.Contains(v2)) v._greatGrandParents.Add(v2);
            }
        }
    }    
    private void SearchGrandParentsInLaw(VillagerData v)
    {
        if(v._partner != null)
        {
            foreach (VillagerData v1 in v._partner._grandParents)
            {
                v._grandParentsInLaw.Add(v1);
            }
            foreach (VillagerData v1 in v._partner._stepGrandParents)
            {
                v._grandParentsInLaw.Add(v1);
            }
        }        
    }
    private void SearchPiblings(VillagerData v)
    {
        // Set piblings (uncles, aunts)
        foreach (VillagerData v1 in v._parents)
        {
            foreach (VillagerData v2 in v1._siblings)
            {
                if (!v._piblings.Contains(v2)) v._piblings.Add(v2);
            }
            foreach (VillagerData v2 in v1._halfSiblings)
            {
                if (!v._piblings.Contains(v2)) v._piblings.Add(v2);
            }
            foreach (VillagerData v2 in v1._stepSiblings)
            {
                if (!v._piblings.Contains(v2)) v._piblings.Add(v2);
            }
        }
        // Set siblings of step-parents as piblings too
        foreach (VillagerData v1 in v._stepParents)
        {
            foreach (VillagerData v2 in v1._siblings)
            {
                if (!v._piblings.Contains(v2)) v._piblings.Add(v2);
            }
            foreach (VillagerData v2 in v1._halfSiblings)
            {
                if (!v._piblings.Contains(v2)) v._piblings.Add(v2);
            }
        }
        // Set partners of piblings as piblings
        List<VillagerData> _pib = new List<VillagerData>();
        foreach (VillagerData v1 in v._piblings)
        {
            if (v1._partner != null) _pib.Add(v1._partner);
        }
        foreach (VillagerData v1 in _pib)
        {
            if(!v._piblings.Contains(v1)) v._piblings.Add(v1);
        }
    }
    private void SearchStepSiblings(VillagerData v)
    {
        foreach(VillagerData v1 in v._stepParents)
        {
            foreach (VillagerData v2 in v1._children)
            {
                if (!v._stepSiblings.Contains(v2) && !v._halfSiblings.Contains(v2)) v._stepSiblings.Add(v2);
            }
        }
    }
    private void SearchSiblingsInLaw(VillagerData v)
    {
        foreach (VillagerData v1 in v._siblings)
        {
            if(v1._partner != null) v._siblingsInLaw.Add(v1._partner);
        }
        foreach (VillagerData v1 in v._halfSiblings)
        {
            if (v1._partner != null) v._siblingsInLaw.Add(v1._partner);
        }
        foreach (VillagerData v1 in v._stepSiblings)
        {
            if (v1._partner != null)
            {
                if(!v._siblingsInLaw.Contains(v1._partner)) v._siblingsInLaw.Add(v1._partner);
            }
        }

        if (v._partner != null)
        {
            foreach(VillagerData v1 in v._partner._siblings)
            {
                v._siblingsInLaw.Add(v1);
            }
            foreach (VillagerData v1 in v._partner._halfSiblings)
            {
                v._siblingsInLaw.Add(v1);
            }
        }
        List<VillagerData> _sib = new List<VillagerData> ();
        foreach (VillagerData v1 in v._siblingsInLaw)
        {
            if (v1._partner != null) _sib.Add(v1._partner);
        }
        foreach (VillagerData v1 in _sib)
        {
            if (!v._siblings.Contains(v1) && !v._halfSiblings.Contains(v1) && !v._stepSiblings.Contains(v1) && !v._siblingsInLaw.Contains(v1)) v._siblingsInLaw.Add(v1);
        }
    }
    private void SearchNiblings(VillagerData v)
    {
        // Set niblings (nephews, nieces)
        foreach (VillagerData v1 in v._siblings)
        {
            foreach (VillagerData v2 in v1._children)
            {
                if (!v._niblings.Contains(v2)) v._niblings.Add(v2);
            }
            foreach (VillagerData v2 in v1._stepChildren)
            {
                if (!v._niblings.Contains(v2)) v._niblings.Add(v2);
            }
        }
        foreach (VillagerData v1 in v._halfSiblings)
        {
            foreach (VillagerData v2 in v1._children)
            {
                if (!v._niblings.Contains(v2)) v._niblings.Add(v2);
            }
            foreach (VillagerData v2 in v1._stepChildren)
            {
                if (!v._niblings.Contains(v2)) v._niblings.Add(v2);
            }
        }
        foreach (VillagerData v1 in v._stepSiblings)
        {
            foreach (VillagerData v2 in v1._children)
            {
                if (!v._niblings.Contains(v2)) v._niblings.Add(v2);
            }
            foreach (VillagerData v2 in v1._stepChildren)
            {
                if (!v._niblings.Contains(v2)) v._niblings.Add(v2);
            }
        }
        foreach (VillagerData v1 in v._siblingsInLaw)
        {
            foreach (VillagerData v2 in v1._children)
            {
                if (!v._niblings.Contains(v2)) v._niblings.Add(v2);
            }
            foreach (VillagerData v2 in v1._stepChildren)
            {
                if (!v._niblings.Contains(v2)) v._niblings.Add(v2);
            }
        }
    }
    private void SearchGrandChildInLaw(VillagerData v)
    {
        foreach(VillagerData v1 in v._grandChildren)
        {
            if(v1._partner != null) v._grandChildrenInLaw.Add(v1._partner);
        }
    }

    private void SearchStepGrandChildren(VillagerData v)
    {
        // Step Grand Children as Step Sibling of GrandChild
        foreach(VillagerData v1 in v._childrenInLaw)
        {
            foreach(VillagerData v2 in v1._children)
            {
                if (!v._grandChildren.Contains(v2) && !v._stepGrandChildren.Contains(v2)) v._stepGrandChildren.Add(v2);
            }
        }
        // Step Grand Children as Children of the StepChild
        foreach(VillagerData v1 in v._stepChildren)
        {
            foreach(VillagerData v2 in v1._children)
            {
                v._stepGrandChildren.Add(v2);
            }
        }
    }
    private void SearchGreatGrandChildren(VillagerData v)
    {
        foreach (VillagerData v1 in v._grandChildren)
        {
            foreach (VillagerData v2 in v1._children)
            {
                if (!v._greatGrandChildren.Contains(v2)) v._greatGrandChildren.Add(v2);
            }
        }
    }
    private void SearchStepChildInLaw(VillagerData v)
    {
        foreach(VillagerData v1 in v._stepChildren)
        {
            if(v1._partner != null) v._childrenInLaw.Add(v1._partner);
        }
    }
    private void SearchForPartnerExes(VillagerData v)
    {
        if(v._partner != null)
        {
            foreach(VillagerData v1 in v._partner._exes) v._partnerExes.Add(v1);
        }
    }
    private void SearchCoParentsInLaw(VillagerData v)
    {
        foreach(VillagerData v1 in v._childrenInLaw)
        {
            foreach (VillagerData v2 in v1._parents) v._coParentsInLaw.Add(v2);
            foreach (VillagerData v2 in v1._stepParents)
            {
                if(!v._coParentsInLaw.Contains(v2)) v._coParentsInLaw.Add(v2);
            }
        }
        List<VillagerData> _co = new List<VillagerData> ();
        foreach (VillagerData v1 in v._coParentsInLaw)
        {
            if(v1._partner != null) _co.Add(v1._partner);
        }
        foreach (VillagerData v1 in _co)
        {
            if (!v._coParentsInLaw.Contains(v1)) v._coParentsInLaw.Add(v1);
        }
    }

    // Level 4 Relationships (Cousins / GrandPiblings / GrandNiblings)
    private void SearchCousins(VillagerData v)
    {
        // Set cousins
        foreach (VillagerData v1 in v._piblings)
        {
            foreach (VillagerData v2 in v1._children)
            {
                if (!v._cousins.Contains(v2)) v._cousins.Add(v2);
            }                    
        }
    }
    private void SearchGrandPiblings(VillagerData v)
    {
        // Siblings of grand parents (as children of great grand parents)
        foreach(VillagerData v1 in v._greatGrandParents)
        {
            foreach(VillagerData v2 in v1._children)
            {
                if(!v._grandParents.Contains(v2) && !v._stepGrandParents.Contains(v2) && !v._grandPiblings.Contains(v2)) v._grandPiblings.Add(v2);
            }
        }
        // Set partner of Grand Pibling as Grand Pibling too
        List<VillagerData> _gp = new List<VillagerData>();
        foreach (VillagerData v1 in v._grandPiblings)
        {
            if (v1._partner != null) _gp.Add(v1._partner);
        }
        foreach (VillagerData v1 in _gp)
        {
            if (!v._grandPiblings.Contains(v1)) v._grandPiblings.Add(v1);
        }
    }
    private void SearchGrandNiblings(VillagerData v)
    {
        foreach (VillagerData v1 in v._niblings)
        {
            foreach (VillagerData v2 in v1._children)
            {
                if(!v._grandChildren.Contains(v2) && !v._grandNiblings.Contains(v2)) v._grandNiblings.Add(v2);
            }
        }
    }
    private void SearchStepParentsInLaw(VillagerData v)
    {
        List<VillagerData> _step = new List<VillagerData> ();
        foreach (VillagerData v1 in v._parentsInLaw)
        {
            if (v1._partner != null) _step.Add(v1._partner);
        }
        foreach (VillagerData v2 in _step)
        {
            if(!v._parents.Contains(v2) && !v._parentsInLaw.Contains(v2)) v._parentsInLaw.Add(v2);
        }
    }

    // Search all villager relatives by batches (to allow step(n) relatives in step(n+1) search)
    public void SearchAllConnections()
    {
        foreach(VillagerData v in villagers)
        {
            SearchParents(v);
        }
        foreach (VillagerData v in villagers)
        {
            SearchGrandParents(v);
            SearchSiblings(v);
            SearchParentsInLaw(v);
            SearchChildrenInLaw(v);
            SearchGrandChildren(v);
            SearchStepParents(v);
            SearchStepChildren(v);
        }
        foreach (VillagerData v in villagers)
        {
            SearchStepGrandParents(v);
            SearchGreatGrandParents(v);            
            SearchGrandParentsInLaw(v);
            SearchPiblings(v);
            SearchStepSiblings(v);
            SearchSiblingsInLaw(v);
            SearchNiblings(v);
            SearchGrandChildInLaw(v);
            SearchStepGrandChildren(v);
            SearchGreatGrandChildren(v);
            SearchStepChildInLaw(v);
            SearchForPartnerExes(v);
            SearchCoParentsInLaw(v);
            SearchStepParentsInLaw(v);
        }
        foreach (VillagerData v in villagers)
        {
            SearchCousins(v);
            SearchGrandPiblings(v);
            SearchGrandNiblings(v);
        }
    }
    #endregion

    #region CompareVillagers
    public string FindSmallestConnection(VillagerData v1, VillagerData v2)
    {
        if (v1 == null || v2 == null) return "";
        if (v1 == v2) return "NA";

        List<VillagerData> checkedVillagers = new List<VillagerData>();
        List<VillagerData> currentVillagers = new List<VillagerData>();
        List<VillagerData> currentVillagersTemp = new List<VillagerData>();
        int index = 0;

        checkedVillagers.Add(v1);
        currentVillagersTemp.AddRange(CombineLevel1Relatives(v1));
        while(currentVillagersTemp.Count > 0)
        {
            index++;
            if (index > 5) break;
            foreach(VillagerData v in currentVillagersTemp) { currentVillagers.Add(v); }
            currentVillagersTemp.Clear();
           
            foreach(VillagerData v in currentVillagers)
            {
                if (v == v2) return index.ToString();
                checkedVillagers.Add(v);
                currentVillagersTemp.AddRange(CombineLevel1Relatives(v));
            }
            List<VillagerData> toRemove = new List<VillagerData>();
            foreach (VillagerData v in currentVillagersTemp)
            {
                if (checkedVillagers.Contains(v)) toRemove.Add(v);
            }
            foreach (VillagerData v in toRemove) currentVillagersTemp.Remove(v);

        }
        return "No match";
    }

    private List<VillagerData> CombineLevel1Relatives(VillagerData v)
    {
        List<VillagerData> output = new List<VillagerData>();
        foreach(VillagerData v1 in v._parents) { output.Add(v1); }
        foreach (VillagerData v2 in v._children) { output.Add(v2); }
        if(v._partner != null) { output.Add(v._partner); }
        foreach(VillagerData v3 in v._exes) {  output.Add(v3); }
        return output;
    }
    #endregion

}
