using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Villager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public TMP_Text villagerName;
    public TMP_Text villagerYears;
    public Image villagerOrigin;
    public Image warning;
    public Image background;
    public VillagerData villager;

    private Sprite fromLakeburg;
    private Sprite fromTindra;
    private Sprite fromNeighbourhood;

    private Image _image;
    private Button _button;
    private RelativesManager _relativesManager;

    private Vector3 beginDragPosition;
    private bool _isDraggable = true;
    private void Awake()
    {
        _image = GetComponent<Image>();
        _relativesManager = FindFirstObjectByType<RelativesManager>();
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        ThemeManager.ThemeChangeAddListener(ChangeTheme);
        ChangeTheme();
        if (villager != null) UpdateGraphics();
    }

    public void SetVillager(VillagerData v)
    {
        villager = v;
        name = villager.name;
        UpdateGraphics();
        HideWarning();
    }

    public void SetDraggable(bool drag)
    {
        _isDraggable = drag;
    }

    public void SetClickable(bool click)
    {
        if(click)
        {
            _button.interactable = true;
            _button.onClick.AddListener(() => _relativesManager.UpdateRelatives(villager));
        }
        else
        {
            _button.interactable = false;
            _button.onClick.RemoveAllListeners();
        }
    }

    private void UpdateGraphics()
    {
        string nameText = villager._firstName + " " + villager._lastName;
        string yearText = villager._birthYear.ToString() + (villager._isDead?(" - " + villager._deathYear):(villager._isExiled?" - ?":""));
        villagerName.text = nameText;
        villagerYears.text = yearText;
        switch (villager._villagerOrigin)
        {
            case VillagerData.OriginEnum.Lakeburg:
                villagerOrigin.sprite = fromLakeburg;
                break;
            case VillagerData.OriginEnum.Tindra:
                villagerOrigin.sprite = fromTindra;
                break;
            case VillagerData.OriginEnum.Neighbourhood:
                villagerOrigin.sprite = fromNeighbourhood;
                break;
            default:
                villagerOrigin.sprite = fromLakeburg;
                break;
        }
    }

    public void ShowWarning()
    {
        if (warning != null) warning.enabled = true;
    }
    public void HideWarning()
    {
        if (warning != null) warning.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_isDraggable)
        {
            transform.position = eventData.position;
        }        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isDraggable)
        {
            beginDragPosition = transform.position;
            _image.raycastTarget = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isDraggable)
        {
            transform.position = beginDragPosition;
            _image.raycastTarget = true;
        }
    }

    private void ChangeTheme()
    {
        if (ThemeManager._registeredTheme != null)
        {
            if(warning != null) warning.sprite = ThemeManager._registeredTheme.interrogationMark;
            fromLakeburg = ThemeManager._registeredTheme.originTown;
            fromNeighbourhood = ThemeManager._registeredTheme.originNeighbour;
            fromTindra = ThemeManager._registeredTheme.originMarriage;
            if (villagerName != null) villagerName.color = ThemeManager._registeredTheme.villagerFontColor;
            if (villagerYears != null) villagerYears.color = ThemeManager._registeredTheme.villagerFontColor;
            if (background != null) background.sprite = ThemeManager._registeredTheme.villagerBorder;
            if (villager != null)
            {
                switch (villager._villagerOrigin)
                {
                    case VillagerData.OriginEnum.Lakeburg:
                        villagerOrigin.sprite = fromLakeburg;
                        break;
                    case VillagerData.OriginEnum.Tindra:
                        villagerOrigin.sprite = fromTindra;
                        break;
                    case VillagerData.OriginEnum.Neighbourhood:
                        villagerOrigin.sprite = fromNeighbourhood;
                        break;
                    default:
                        villagerOrigin.sprite = fromLakeburg;
                        break;
                }
            }
            else
            {
                villagerOrigin.sprite = fromLakeburg;
            }
        }
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }
}
