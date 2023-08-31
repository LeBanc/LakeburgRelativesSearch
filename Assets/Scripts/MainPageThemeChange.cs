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
        }
    }
    private void OnDestroy()
    {
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }
}
