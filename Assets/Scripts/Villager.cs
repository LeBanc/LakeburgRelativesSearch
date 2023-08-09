using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Villager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public TMP_Text villagerName;
    public TMP_Text villagerYears;
    public VillagerData villager;

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
        if (villager != null) UpdateGraphics();
    }

    public void SetVillager(VillagerData v)
    {
        villager = v;
        name = villager.name;
        UpdateGraphics();
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
        string yearText = villager._birthYear.ToString() + (villager._isDead?(" - " + villager._deathYear):"");
        villagerName.text = nameText;
        villagerYears.text = yearText;
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

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
