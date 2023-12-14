using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Villager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public TMP_Text villagerName;
    public TMP_Text villagerYears;
    public TMP_Text villagerJob;
    public Image villagerOrigin;
    public Image warning;
    public Image ring;
    public Image bloodDrop;
    public Image celib;
    public Image background;
    public Image jobBack;
    public Image backMask;
    public Image backColor;
    public VillagerPortrait portrait;

    public VillagerData villager;

    private Sprite fromLakeburg;
    private Sprite fromTindra;
    private Sprite fromNeighbourhood;

    private Image _image;
    private Button _button;
    private RelativesManager _relativesManager;
    private MagicalBookManager _magicalBookManager;
    private MainPageManager _mainPageManager;

    private Vector3 beginDragPosition;
    private bool _isDraggable = true;
    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _relativesManager = FindFirstObjectByType<RelativesManager>();
        _magicalBookManager = FindFirstObjectByType<MagicalBookManager>();
        _mainPageManager = FindFirstObjectByType<MainPageManager>();
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
        HideRing();
        HideBloodDrop();
        HideCelib();
    }

    public void SetDraggable(bool drag)
    {
        _isDraggable = drag;
    }

    public void SetClickable(bool click, bool book = false)
    {
        if(click)
        {
            if(_button == null) { Debug.Log("Button not found"); return; }
            _button.interactable = true;
            
            if(book)
            {
                _button.onClick.AddListener(() => _mainPageManager.magicalBookCanvas.ShowCanvas());
                _button.onClick.AddListener(() => _magicalBookManager.UpdateMatches(villager));
            }
            else
            {
                _button.onClick.AddListener(() => _mainPageManager.relativesCanvas.ShowCanvas());
                _button.onClick.AddListener(() => _relativesManager.UpdateRelatives(villager));
            }            
        }
        else
        {
            _button.interactable = false;
            _button.onClick.RemoveAllListeners();
        }
    }

    private void UpdateGraphics()
    {
        portrait.SetPortraitLib(PortraitManager.GetPortraitLib(villager._portraitLib));
        portrait.ChangePortrait(villager._face, villager._hair, villager._hairBehind, villager._beard, villager._eyeBrows, villager._eyes, villager._nose, villager._mouth, villager._wrinkles, villager._skinColor, villager._hairColor, villager._eyeColor);

        string nameText = villager._firstName + " " + villager._lastName;
        string yearText = villager._birthYear.ToString() + (villager._isDead?(" - " + villager._deathYear + " (" + (villager._deathYear - villager._birthYear) + ")") :(villager._isExiled?" - ?":" (" + (villager._deathYear - villager._birthYear) + ")"));
        villagerName.text = nameText;
        villagerYears.text = yearText;
        villagerJob.text = villager._job;
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

    public void ShowRing()
    {
        if (ring != null) ring.gameObject.SetActive(true);
    }
    public void HideRing()
    {
        if (ring != null) ring.gameObject.SetActive(false);
    }

    public void ShowBloodDrop()
    {
        if (bloodDrop != null) bloodDrop.gameObject.SetActive(true);
    }
    public void HideBloodDrop()
    {
        if (bloodDrop != null) bloodDrop.gameObject.SetActive(false);
    }

    public void ShowCelib()
    {
        if (celib != null) celib.gameObject.SetActive(true);
    }
    public void HideCelib()
    {
        if (celib != null) celib.gameObject.SetActive(false);
    }

    public void ShowJob()
    {
        if (villagerJob != null) villagerJob.enabled = true;
    }
    public void HideJob()
    {
        if (villagerJob != null) villagerJob.enabled = false;
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
            if (warning != null) warning.sprite = ThemeManager._registeredTheme.interrogationMark;
            if (ring != null) ring.sprite = ThemeManager._registeredTheme.ring;
            if (bloodDrop != null) bloodDrop.sprite = ThemeManager._registeredTheme.blood;
            if (celib != null) celib.sprite = ThemeManager._registeredTheme.single;
            fromLakeburg = ThemeManager._registeredTheme.originTown;
            fromNeighbourhood = ThemeManager._registeredTheme.originNeighbour;
            fromTindra = ThemeManager._registeredTheme.originMarriage;
            if (villagerName != null) villagerName.color = ThemeManager._registeredTheme.villagerFontColor;
            if (villagerYears != null) villagerYears.color = ThemeManager._registeredTheme.villagerFontColor;
            if (villagerJob != null) villagerJob.color = ThemeManager._registeredTheme.villagerFontColor;
            if (jobBack != null) jobBack.sprite = ThemeManager._registeredTheme.button;
            if (background != null) background.sprite = ThemeManager._registeredTheme.villagerBorder;
            if (backColor != null) backColor.color = ThemeManager._registeredTheme.sliderHandleColor;
            if (backMask != null) backMask.sprite = ThemeManager._registeredTheme.villagerMask;
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
