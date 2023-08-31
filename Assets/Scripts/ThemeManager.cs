using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ThemeManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public static Theme _registeredTheme;
    private Theme currentTheme;
    private static UnityEvent _themeChanged = new UnityEvent();
    public Theme lakeburg;
    public Theme handdrawn;

    void Awake()
    {
        _registeredTheme = currentTheme;
    }

    private void Start()
    {
        if (dropdown != null)
        {
            dropdown.onValueChanged.AddListener(delegate {
                ChangeTheme(dropdown.value);
            });
            ChangeTheme(dropdown.value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_registeredTheme != currentTheme)
        {
            _registeredTheme = currentTheme;
            _themeChanged.Invoke();            
        }
    }

    public void ChangeTheme(int index)
    {
        switch (index)
        {
            case 0:
                currentTheme = handdrawn;
                break;
            case 1:
                currentTheme = lakeburg;
                break;
            default:
                currentTheme = handdrawn;
                break;
        }
    }

    public static void ThemeChangeAddListener(UnityAction action)
    {
        _themeChanged.AddListener(action);
    }

    public static void ThemeChangeRemoveListener(UnityAction action)
    {
        _themeChanged.RemoveListener(action);
    }

    private void OnDestroy()
    {
        _themeChanged.RemoveAllListeners();
    }
}
