using UnityEngine;
using TMPro;

public class MainPageManager : MonoBehaviour
{
    public DropContener relativesDrop;
    public DropContener magicalBookDrop;
    public DropContener comparisonDrop1;
    public DropContener comparisonDrop2;
    public TMP_Text relationCount;

    public CanvasHandler mainPageCanvas;
    public CanvasHandler relativesCanvas;
    public CanvasHandler graveyardCanvas;
    public CanvasHandler magicalBookCanvas;

    private RelativesManager relativesManager;
    private VillagersManager villagersManager;
    private MagicalBookManager magicalBookManager;

    private void Awake()
    {
        relativesManager = GetComponent<RelativesManager>();
        villagersManager = GetComponent<VillagersManager>();
        magicalBookManager = GetComponent<MagicalBookManager>();
    }

    private void Start()
    {
        BackToMainPage();
    }

    public void BackToMainPage()
    {
        magicalBookCanvas.HideCanvas();
        graveyardCanvas.HideCanvas();
        relativesCanvas.HideCanvas();
        mainPageCanvas.ShowCanvas();
    }

    public void ComputeRelatives()
    {
        if(relativesDrop != null) {
            if(relativesDrop.transform.childCount > 0)
            {
                Villager v = relativesDrop.transform.GetChild(0).GetComponent<Villager>();
                if (v != null)
                {
                    relativesCanvas.ShowCanvas();
                    relativesManager.UpdateRelatives(v.villager);
                    mainPageCanvas.HideCanvas();
                }
            }

        }
    }

    public void ComputeRelationsLevel()
    {
        string result = "";
        if (comparisonDrop1 != null && comparisonDrop2 != null)
        {
            if (comparisonDrop1.transform.childCount > 0 && comparisonDrop2.transform.childCount > 0)
            {
                Villager v1 = comparisonDrop1.transform.GetChild(0).GetComponent<Villager>();
                Villager v2 = comparisonDrop2.transform.GetChild(0).GetComponent<Villager>();

                if (v1 != null && v2 != null)
                {
                    result = villagersManager.FindSmallestConnection(v1.villager, v2.villager);

                }
            }

        }
        relationCount.text = result;
    }

    public void OpenMagicalBook()
    {
        if (magicalBookDrop != null)
        {
            if (magicalBookDrop.transform.childCount > 0)
            {
                Villager v = magicalBookDrop.transform.GetChild(0).GetComponent<Villager>();
                if (v != null)
                {
                    if(!v.villager._isDead)
                    {
                        magicalBookCanvas.ShowCanvas();
                        magicalBookManager.UpdateMatches(v.villager);
                        mainPageCanvas.HideCanvas();
                    }
                    else
                    {
                        v.ShowWarning();
                    }                    
                }
            }

        }
    }

    public void ClearDropConteners()
    {
        relativesDrop.ClearAllChildren();
        magicalBookDrop.ClearAllChildren();
        comparisonDrop1.ClearAllChildren();
        comparisonDrop2.ClearAllChildren();
        relationCount.text = ""; ;
}

    public void QuitApplication()
    {
        Application.Quit();
    }
}
