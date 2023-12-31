using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelativesManager : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text year;
    public Image origin;

    public Transform gen_3Contener;
    public VillagersContener greatGrandParentsContener;

    public Transform gen_2Contener;
    public VillagersContener grandParentsContener;
    public VillagersContener stepGrandParentsContener;
    public VillagersContener grandParentsInLawContener;
    public VillagersContener grandPiblingsContener;

    public Transform gen_1Contener;
    public VillagersContener parentsContener;
    public VillagersContener stepParentsContener;
    public VillagersContener parentsInLawContener;
    public VillagersContener piblingsContener;

    public Transform gen0Contener;
    public VillagersContener partnerContener;
    public VillagersContener siblingsContener;
    public VillagersContener halfSiblingsContener;
    public VillagersContener stepSiblingsContener;
    public VillagersContener cousinsContener;
    public VillagersContener siblingsInLawContener;
    public VillagersContener exesContener;
    public VillagersContener partnerExesContener;
    public VillagersContener coParentsInLawContener;

    public Transform gen1Contener;
    public VillagersContener childrenContener;
    public VillagersContener stepChildrenContener;
    public VillagersContener childrenInLawContener;
    public VillagersContener niblingsContener;

    public Transform gen2Contener;
    public VillagersContener grandChildrenContener;
    public VillagersContener stepGrandChildrenContener;
    public VillagersContener grandChildrenInLawContener;
    public VillagersContener grandNiblingsContener;

    public Transform gen3Contener;
    public VillagersContener greatGrandChildrenContener;

    private bool isInit = false;
    private VillagersManager villagersManager;
    private Sprite fromLakeburg;
    private Sprite fromTindra;
    private Sprite fromNeighbourhood;

    private void Start()
    {
        villagersManager = FindFirstObjectByType<VillagersManager>();
    }

    public void SetOriginImages(Sprite lakeburg, Sprite tindra, Sprite neighbor)
    {
        fromLakeburg = lakeburg;
        fromTindra = tindra;
        fromNeighbourhood = neighbor;
    }

    private void Init()
    {
        // Gen-3
        greatGrandParentsContener.ClearVillagers();
        gen_3Contener.gameObject.SetActive(false);

        // Gen-2
        grandParentsContener.ClearVillagers();
        stepGrandParentsContener.ClearVillagers();
        grandParentsInLawContener.ClearVillagers();
        grandPiblingsContener.ClearVillagers();
        gen_2Contener.gameObject.SetActive(false);

        // Gen-1
        parentsContener.ClearVillagers();
        stepParentsContener.ClearVillagers();
        parentsInLawContener.ClearVillagers();
        piblingsContener.ClearVillagers();
        gen_1Contener.gameObject.SetActive(false);

        // Gen0
        partnerContener.ClearVillagers();
        siblingsContener.ClearVillagers();
        halfSiblingsContener.ClearVillagers();
        stepSiblingsContener.ClearVillagers();
        cousinsContener.ClearVillagers();
        siblingsInLawContener.ClearVillagers();
        exesContener.ClearVillagers();
        partnerExesContener.ClearVillagers();
        coParentsInLawContener.ClearVillagers() ;
        gen0Contener.gameObject.SetActive(false);

        // Gen1
        childrenContener.ClearVillagers();
        stepChildrenContener.ClearVillagers();
        childrenInLawContener.ClearVillagers();
        niblingsContener.ClearVillagers();
        gen1Contener.gameObject.SetActive(false);

        // Gen2
        grandChildrenContener.ClearVillagers();
        stepGrandChildrenContener.ClearVillagers();
        grandChildrenInLawContener.ClearVillagers();
        grandNiblingsContener.ClearVillagers();
        gen2Contener.gameObject.SetActive(false);

        // Gen3
        greatGrandChildrenContener.ClearVillagers();
        gen3Contener.gameObject.SetActive(false);

        // StartCoroutine(UpdateRelativesDisplay());
    }

    private void Update()
    {
        if(!isInit)
        {
            Init();
            isInit = true;
        }
    }


    public void UpdateRelatives(VillagerData v)
    {
        Init();

        // Villager Name
        title.text = v._firstName + " " + v._lastName + " (" + v._birthYear.ToString() + " - " +  (v._isDead?v._deathYear.ToString():(v._isExiled?"?":"")) + ")";
        switch (v._villagerOrigin)
        {
            case VillagerData.OriginEnum.Lakeburg:
                origin.sprite = fromLakeburg;
                break;
            case VillagerData.OriginEnum.Tindra:
                origin.sprite = fromTindra;
                break;
            case VillagerData.OriginEnum.Neighbourhood:
                origin.sprite = fromNeighbourhood;
                break;
            default:
                origin.sprite = fromLakeburg;
                break;
        }

        List<VillagerData> bloodRelated = villagersManager.FindAllBloodRelatedVillagers(v);

        // Gen-3
        gen_3Contener.gameObject.SetActive(true);
        foreach (VillagerData v1 in v._greatGrandParents) greatGrandParentsContener.AddVillager(v1, false, bloodRelated.Contains(v1));

        // Gen -2
        gen_2Contener.gameObject.SetActive(true);
        foreach (VillagerData v1 in v._grandParents) grandParentsContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._stepGrandParents) stepGrandParentsContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._grandParentsInLaw) grandParentsInLawContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._grandPiblings) grandPiblingsContener.AddVillager(v1, false, bloodRelated.Contains(v1));

        // Gen -1
        gen_1Contener.gameObject.SetActive(true);
        foreach (VillagerData v1 in v._parents) parentsContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._stepParents) stepParentsContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._parentsInLaw) parentsInLawContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._piblings) piblingsContener.AddVillager(v1, false, bloodRelated.Contains(v1));

        // Gen 0
        gen0Contener.gameObject.SetActive(true);
        if (v._partner != null) partnerContener.AddVillager(v._partner, true, bloodRelated.Contains(v._partner));
        foreach (VillagerData v1 in v._siblings) siblingsContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._halfSiblings) halfSiblingsContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._stepSiblings) stepSiblingsContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._cousins) cousinsContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._siblingsInLaw) siblingsInLawContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._exes) exesContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._partnerExes) partnerExesContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._coParentsInLaw) coParentsInLawContener.AddVillager(v1, false, bloodRelated.Contains(v1));

        // Gen 1
        gen1Contener.gameObject.SetActive(true);

        foreach (VillagerData v1 in v._children) childrenContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._stepChildren) stepChildrenContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._childrenInLaw) childrenInLawContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._niblings) niblingsContener.AddVillager(v1, false, bloodRelated.Contains(v1));

        // Gen 2
        gen2Contener.gameObject.SetActive(true);
        foreach (VillagerData v1 in v._grandChildren) grandChildrenContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._stepGrandChildren) stepGrandChildrenContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._grandChildrenInLaw) grandChildrenInLawContener.AddVillager(v1, false, bloodRelated.Contains(v1));
        foreach (VillagerData v1 in v._grandNiblings) grandNiblingsContener.AddVillager(v1, false, bloodRelated.Contains(v1));

        // Gen 3
        gen3Contener.gameObject.SetActive(true);
        foreach (VillagerData v1 in v._greatGrandChildren) greatGrandChildrenContener.AddVillager(v1, false, bloodRelated.Contains(v1));

        StartCoroutine(UpdateRelativesDisplay());
    }

    private IEnumerator UpdateRelativesDisplay()
    {
        yield return new WaitForFixedUpdate();
        if(greatGrandParentsContener.CountVillagers() <1 ) gen_3Contener.gameObject.SetActive(false);
        if ((grandParentsContener.CountVillagers() + stepGrandParentsContener.CountVillagers() +
            grandParentsInLawContener.CountVillagers() + grandPiblingsContener.CountVillagers()) < 1) gen_2Contener.gameObject.SetActive(false);
        if ((parentsContener.CountVillagers() + stepParentsContener.CountVillagers() + parentsInLawContener.CountVillagers() +
            piblingsContener.CountVillagers()) < 1) gen_1Contener.gameObject.SetActive(false);
        if ((partnerContener.CountVillagers() + siblingsContener.CountVillagers() + halfSiblingsContener.CountVillagers() + stepSiblingsContener.CountVillagers() +
            cousinsContener.CountVillagers() + siblingsInLawContener.CountVillagers() + exesContener.CountVillagers() +
            partnerExesContener.CountVillagers() + coParentsInLawContener.CountVillagers()) < 1) gen0Contener.gameObject.SetActive(false);
        if ((childrenContener.CountVillagers() + stepChildrenContener.CountVillagers() + childrenInLawContener.CountVillagers() +
            niblingsContener.CountVillagers()) < 1) gen1Contener.gameObject.SetActive(false);
        if ((grandChildrenContener.CountVillagers() + stepGrandChildrenContener.CountVillagers() +
            grandChildrenInLawContener.CountVillagers() + grandNiblingsContener.CountVillagers()) < 1) gen2Contener.gameObject.SetActive(false);
        if (greatGrandChildrenContener.CountVillagers() < 1) gen3Contener.gameObject.SetActive(false);
    }

}
