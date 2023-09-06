using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraveyardThemeChange : MonoBehaviour
{
    public Image background;
    public Image titleBack;
    public TMP_Text title;
    public TMP_Text year;
    public Image cross;

    void Start()
    {
        ThemeManager.ThemeChangeAddListener(ChangeTheme);
        ChangeTheme();
    }

    private void ChangeTheme()
    {
        if (ThemeManager._registeredTheme != null)
        {
            if (background != null) background.sprite = ThemeManager._registeredTheme.relativesBackground;
            if (titleBack != null) titleBack.sprite = ThemeManager._registeredTheme.titleBackground;
            if (title != null) title.color = ThemeManager._registeredTheme.relativesTitleFontColor;
            if (year != null) year.color = ThemeManager._registeredTheme.yearFontColor;
            if (cross != null) cross.sprite = ThemeManager._registeredTheme.cross;
        }
    }
    private void OnDestroy()
    {
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }
}
