using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DropContener : MonoBehaviour, IDropHandler
{
    public Villager villagerPrefab;
    public CanvasScaler mainScaler;
    private float ratio;
    private Image dropzone;

    private void Start()
    {
        ratio = Screen.width / mainScaler.referenceResolution.x;
        dropzone = GetComponent<Image>();
        ThemeManager.ThemeChangeAddListener(ChangeTheme);
        ChangeTheme();
    }

    private void ChangeTheme()
    {
        if (dropzone != null && ThemeManager._registeredTheme != null)
        {
            dropzone.sprite = ThemeManager._registeredTheme.dropZone;
        }
    }

    private void OnDestroy()
    {
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }


    public void OnDrop(PointerEventData eventData)
    {
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
