using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DropContener : MonoBehaviour, IDropHandler
{
    public Villager villagerPrefab;
    public CanvasScaler mainScaler;
    private float ratio;

    private void Start()
    {
        // ratio = Mathf.Max(Screen.width / mainScaler.referenceResolution.x, Screen.height / mainScaler.referenceResolution.y);
        ratio = Screen.width / mainScaler.referenceResolution.x;
    }


    public void OnDrop(PointerEventData eventData)
    {
        // ratio = Mathf.Max(Screen.width / mainScaler.referenceResolution.x, Screen.height / mainScaler.referenceResolution.y);
        ratio = Screen.width / mainScaler.referenceResolution.x;
        ClearAllChildren();
        Villager newVillager = Instantiate<Villager>(villagerPrefab, transform);
        newVillager.SetVillager(eventData.pointerDrag.GetComponent<Villager>().villager);
        newVillager.GetComponent<RectTransform>().position = newVillager.GetComponent<RectTransform>().position + ratio * new Vector3(GetComponent<RectTransform>().rect.width/2.0f, GetComponent<RectTransform>().rect.height/2.0f, 0f);
        newVillager.SetDraggable(false);
    }

    public void ClearAllChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
}
