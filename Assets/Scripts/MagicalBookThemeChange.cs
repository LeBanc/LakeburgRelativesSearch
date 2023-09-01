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

    public Image match3;
    public Image match2;
    public Image match1;
    public Image match0;
    public Image match_1;
    public Image match_2;
    public Image match_3;

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
            if (background != null) background.sprite = ThemeManager._registeredTheme.relativesBackground;
            if (titleBack != null) titleBack.sprite = ThemeManager._registeredTheme.titleBackground;
            if (title != null) title.color = ThemeManager._registeredTheme.relativesTitleFontColor;
            if (year != null) year.color = ThemeManager._registeredTheme.yearFontColor;
            if (cross != null) cross.sprite = ThemeManager._registeredTheme.cross;
            if (dropzone != null) dropzone.sprite = ThemeManager._registeredTheme.dropZone;

            if (match3 != null) match3.color = ThemeManager._registeredTheme.backgroundColor;
            if (match2 != null) match2.color = ThemeManager._registeredTheme.backgroundColor;
            if (match1 != null) match1.color = ThemeManager._registeredTheme.backgroundColor;
            if (match0 != null) match0.color = ThemeManager._registeredTheme.backgroundColor;
            if (match_1 != null) match_1.color = ThemeManager._registeredTheme.backgroundColor;
            if (match_2 != null) match_2.color = ThemeManager._registeredTheme.backgroundColor;
            if (match_3 != null) match_3.color = ThemeManager._registeredTheme.backgroundColor;
        }
    }
    private void OnDestroy()
    {
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }
}
