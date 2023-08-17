using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonThemeChange : MonoBehaviour
{
    private Image button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Image>();
        ThemeManager.ThemeChangeAddListener(ChangeTheme);
        ChangeTheme();
    }
    private void ChangeTheme()
    {
        if (button != null && ThemeManager._registeredTheme != null)
        {
            button.sprite = ThemeManager._registeredTheme.button;
        }
    }
    private void OnDestroy()
    {
        ThemeManager.ThemeChangeRemoveListener(ChangeTheme);
    }
}
