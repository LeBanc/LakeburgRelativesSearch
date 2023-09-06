using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagicalBookThemeChange : MonoBehaviour
{
    public Image background;
    public Image titleBack;
    public TMP_Text title;
    public TMP_Text year;
    public Image cross;
    public Image dropzone;

    public Image goodMatches;
    public Image neutralMatches;
    public Image badMatches;

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
            if (dropzone != null) dropzone.sprite = ThemeManager._registeredTheme.dropZone;

            if (goodMatches != null) goodMatches.color = ThemeManager._registeredTheme.backgroundColor;
            if (neutralMatches != null) neutralMatches.color = ThemeManager._registeredTheme.backgroundColor;
            if (badMatches != null) badMatches.color = ThemeManager._registeredTheme.backgroundColor;
        }
    }
    private void OnDestroy()
    {
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }
}
