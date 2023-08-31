using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Dropdown))]
public class DropdownThemeChange : MonoBehaviour
{
    public Image background;
    public Image arrow;
    public TMP_Text mainText;
    public Image template;
    public Image itemBackground;
    public TMP_Text itemText;

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
            if (background != null) background.sprite = ThemeManager._registeredTheme.button;
            if (arrow != null) arrow.sprite = ThemeManager._registeredTheme.dropdownArrow;
            if (mainText != null) mainText.color = ThemeManager._registeredTheme.buttonFontColor;
            if (template != null) template.color = ThemeManager._registeredTheme.backgroundColor;
            if (itemBackground != null) itemBackground.color = ThemeManager._registeredTheme.backgroundColor;
            if (itemText != null) itemText.color = ThemeManager._registeredTheme.buttonFontColor;
        }
    }
    private void OnDestroy()
    {
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }
}
