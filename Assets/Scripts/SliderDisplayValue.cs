using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class SliderDisplayValue : MonoBehaviour
{
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void UpdateValue(float value)
    {
        text.text = ((int)value).ToString();
    }
}
