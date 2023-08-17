using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPageThemeChange : MonoBehaviour
{
    public Image background;
    public TMP_Text relativeCountLabel;
    public TMP_Text relativeCountText;
    public Image scrollView;
    public Image dropDownButton;
    public Image dropDownArrow;
    public TMP_Text dropDownText;
    public Image dropDownTemplate;
    public Image dropDownItemBack;
    public TMP_Text dropDownItemText;

    // Start is called before the first frame update
    void Start()
    {
        ThemeManager.ThemeChangeAddListener(ChangeTheme);
        ChangeTheme();
    }

    private void ChangeTheme()
    {
        if (ThemeManager._registeredTheme != null)
        {
            if(background != null) background.color = ThemeManager._registeredTheme.backgroundColor;
            if (relativeCountLabel != null) relativeCountLabel.color = ThemeManager._registeredTheme.relativesTitleFontColor;
            if (relativeCountText != null) relativeCountText.color = ThemeManager._registeredTheme.relativesTitleFontColor;
            if (scrollView != null) scrollView.color = ThemeManager._registeredTheme.backgroundColor;
            if (dropDownButton != null) dropDownButton.sprite = ThemeManager._registeredTheme.button;
            if (dropDownArrow != null) dropDownArrow.sprite = ThemeManager._registeredTheme.dropdownArrow;
            if (dropDownText != null) dropDownText.color = ThemeManager._registeredTheme.buttonFontColor;
            if (dropDownTemplate != null) dropDownTemplate.color = ThemeManager._registeredTheme.backgroundColor;
            if (dropDownItemBack != null) dropDownItemBack.color = ThemeManager._registeredTheme.backgroundColor;
            if (dropDownItemText != null) dropDownItemText.color = ThemeManager._registeredTheme.buttonFontColor;
        }
    }
    private void OnDestroy()
    {
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }
}
