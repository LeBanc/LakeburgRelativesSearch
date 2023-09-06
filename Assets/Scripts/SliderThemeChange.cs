using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderThemeChange : MonoBehaviour
{
    private Slider slider;
    public Image background;
    public Image fill;
    
    public Image handle;
    public Image handleDisplay;
    public TMP_Text handleText;
    public TMP_Text sliderText;

    void Start()
    {
        slider = GetComponent<Slider>();
        ThemeManager.ThemeChangeAddListener(ChangeTheme);
        ChangeTheme();
    }

    private void ChangeTheme()
    {
        if (ThemeManager._registeredTheme != null)
        {
            if(slider != null)
            {
                ColorBlock colorBlock = ColorBlock.defaultColorBlock;
                colorBlock.normalColor = ThemeManager._registeredTheme.sliderHandleColor;
                colorBlock.highlightedColor = ThemeManager._registeredTheme.sliderHandleColor;
                colorBlock.selectedColor = ThemeManager._registeredTheme.sliderHandleColor;
                colorBlock.pressedColor = ThemeManager._registeredTheme.sliderPressedColor;
                slider.colors = colorBlock;
            }
            if (background != null) background.color = ThemeManager._registeredTheme.sliderFillColor;
            if (fill != null) fill.color = ThemeManager._registeredTheme.sliderFillColor;
            if (handle != null) handle.sprite = ThemeManager._registeredTheme.sliderHandle;
            if (handleDisplay != null) handleDisplay.sprite = ThemeManager._registeredTheme.sliderDisplay;
            if (handleText != null) handleText.color = ThemeManager._registeredTheme.buttonFontColor;
            if (sliderText != null) sliderText.color = ThemeManager._registeredTheme.villagerFontColor;            
        }
    }
    private void OnDestroy()
    {
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }
}
