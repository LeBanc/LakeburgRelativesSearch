using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VillagersContener : MonoBehaviour
{
    public Villager villagerPrefab;
    public Transform contener;
    public TMP_Text title;
    public string titleText;

    private bool _updateDisplay = false;
    private Image _image;

    public static List<Villager> relatives = new List<Villager>();

    private void Start()
    {
        title.text = titleText;
        _image = GetComponent<Image>();
        ThemeManager.ThemeChangeAddListener(ChangeTheme);
        ChangeTheme();
    }

    private IEnumerator UpdateDisplay()
    {
        yield return new WaitForFixedUpdate();
        if (CountVillagers() > 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if(_updateDisplay)
        {
            if (CountVillagers() > 0)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
            _updateDisplay = false;
        }
    }

    public void AddVillager(VillagerData v)
    {
        gameObject.SetActive(true);
        if (contener != null && villagerPrefab != null)
        {
            Villager newVillager = Instantiate(villagerPrefab, contener);
            newVillager.SetVillager(v);
            newVillager.SetDraggable(false);
            newVillager.SetClickable(true);

            foreach (Villager v1 in relatives) {
                if (v == v1.villager)
                {
                    v1.ShowWarning();
                    newVillager.ShowWarning();
                }
            }
            relatives.Add(newVillager);
        }
        _updateDisplay = true;
    }

    public void RemoveVillager(VillagerData v)
    {
        gameObject.SetActive(true);
        if (contener != null)
        {
            for(int i = 0; i < contener.childCount; i++)
            {
                Transform child = contener.GetChild(i);
                if (child.GetComponent<Villager>().villager._id.Equals(v._id))
                {
                    relatives.Remove(child.GetComponent<Villager>());
                    DestroyImmediate(child.gameObject);                    
                }
            }
        }
        _updateDisplay = true;
    }

    public void ClearVillagers()
    {
        gameObject.SetActive(true);
        if (contener != null)
        {
            for (int i = contener.childCount - 1; i >= 0; i--)
            {
                Transform child = contener.GetChild(i);
                DestroyImmediate(child.gameObject);
            }
        }
        relatives.Clear();
        _updateDisplay = true;
    }

    public int CountVillagers()
    {
        if (contener != null)
        {
            return contener.childCount;
        }
        return 0;
    }

    private void ChangeTheme()
    {
        if (ThemeManager._registeredTheme != null)
        {
            if(_image != null) _image.color = ThemeManager._registeredTheme.backgroundColor;
            if (title != null) title.color = ThemeManager._registeredTheme.buttonFontColor;
        }
    }
    private void OnDestroy()
    {
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }
}
