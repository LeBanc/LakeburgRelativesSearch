using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelativesThemeChange : MonoBehaviour
{
    public Image background;
    public Image titleBack;
    public TMP_Text title;
    public TMP_Text year;
    public Image cross;

    public Image g_3;
    public Image g_3Panel;
    public TMP_Text g_3PanelText;
    public Image g_2;
    public Image g_2Panel;
    public TMP_Text g_2PanelText;
    public Image g_1;
    public Image g_1Panel;
    public TMP_Text g_1PanelText;
    public Image g0;
    public Image g0Panel;
    public TMP_Text g0PanelText;
    public Image g1;
    public Image g1Panel;
    public TMP_Text g1PanelText;
    public Image g2;
    public Image g2Panel;
    public TMP_Text g2PanelText;
    public Image g3;
    public Image g3Panel;
    public TMP_Text g3PanelText;

    private RelativesManager relativesManager;

    void Start()
    {
        relativesManager = FindObjectOfType<RelativesManager>();
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

            if (g_3 != null) g_3.color = ThemeManager._registeredTheme.backgroundColor;
            if (g_3Panel != null) g_3Panel.color = ThemeManager._registeredTheme.backgroundColor;
            if (g_3PanelText != null) g_3PanelText.color = ThemeManager._registeredTheme.villagerFontColor;
            if (g_2 != null) g_2.color = ThemeManager._registeredTheme.backgroundColor;
            if (g_2Panel != null) g_2Panel.color = ThemeManager._registeredTheme.backgroundColor;
            if (g_2PanelText != null) g_2PanelText.color = ThemeManager._registeredTheme.villagerFontColor;
            if (g_1 != null) g_1.color = ThemeManager._registeredTheme.backgroundColor;
            if (g_1Panel != null) g_1Panel.color = ThemeManager._registeredTheme.backgroundColor;
            if (g_1PanelText != null) g_1PanelText.color = ThemeManager._registeredTheme.villagerFontColor;
            if (g0 != null) g0.color = ThemeManager._registeredTheme.backgroundColor;
            if (g0Panel != null) g0Panel.color = ThemeManager._registeredTheme.backgroundColor;
            if (g0PanelText != null) g0PanelText.color = ThemeManager._registeredTheme.villagerFontColor;
            if (g1 != null) g1.color = ThemeManager._registeredTheme.backgroundColor;
            if (g1Panel != null) g1Panel.color = ThemeManager._registeredTheme.backgroundColor;
            if (g1PanelText != null) g1PanelText.color = ThemeManager._registeredTheme.villagerFontColor;
            if (g2 != null) g2.color = ThemeManager._registeredTheme.backgroundColor;
            if (g2Panel != null) g2Panel.color = ThemeManager._registeredTheme.backgroundColor;
            if (g2PanelText != null) g2PanelText.color = ThemeManager._registeredTheme.villagerFontColor;
            if (g3 != null) g3.color = ThemeManager._registeredTheme.backgroundColor;
            if (g3Panel != null) g3Panel.color = ThemeManager._registeredTheme.backgroundColor;
            if (g3PanelText != null) g3PanelText.color = ThemeManager._registeredTheme.villagerFontColor;

            if (relativesManager != null) relativesManager.SetOriginImages(ThemeManager._registeredTheme.originTown, ThemeManager._registeredTheme.originMarriage, ThemeManager._registeredTheme.originNeighbour);
        }
    }
    private void OnDestroy()
    {
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }
}
