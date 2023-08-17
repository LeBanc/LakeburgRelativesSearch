using UnityEngine;
using UnityEngine.Events;

public class ThemeManager : MonoBehaviour
{
    public Theme currentTheme;
    public static Theme _registeredTheme;
    private static UnityEvent _themeChanged = new UnityEvent();

    // Start is called before the first frame update
    void Awake()
    {
        _registeredTheme = currentTheme;
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
