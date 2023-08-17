using UnityEngine;
using TMPro;

public class MainPageManager : MonoBehaviour
{
    public DropContener relativesDrop;
    public DropContener familyTreeDrop;
    public DropContener comparisonDrop1;
    public DropContener comparisonDrop2;
    public TMP_Text relationCount;

    public CanvasHandler mainPageCanvas;
    public CanvasHandler relativesCanvas;
    // public CanvasHandler familyTreeCanvas;

    private RelativesManager relativesManager;
    private VillagersManager villagersManager;

    private void Start()
    {
        relativesManager = GetComponent<RelativesManager>();
        villagersManager = GetComponent<VillagersManager>();
        BackToMainPage();
    }

    public void BackToMainPage()
    {
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

    public void ClearDropConteners()
    {
        relativesDrop.ClearAllChildren();
        familyTreeDrop.ClearAllChildren();
        comparisonDrop1.ClearAllChildren();
        comparisonDrop2.ClearAllChildren();
        relationCount.text = ""; ;
}

    public void QuitApplication()
    {
        Application.Quit();
    }
}
