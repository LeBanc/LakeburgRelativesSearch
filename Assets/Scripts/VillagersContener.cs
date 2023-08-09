using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VillagersContener : MonoBehaviour
{
    public Villager villagerPrefab;
    public Transform contener;
    public TMP_Text title;
    public string titleText;

    private bool _updateDisplay = false;

    private void Start()
    {
        title.text = titleText;
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
}
