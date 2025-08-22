using UnityEngine;
using UnityEngine.UIElements;

public class ThemeController : MonoBehaviour
{
    [Header("Style Sheets")]
    public StyleSheet lightTheme;
    public StyleSheet darkTheme;
    public StyleSheet typography;
    public StyleSheet utilities;
    
    private VisualElement root;
    private Button themeToggle;
    private bool isDarkMode = false;
    
    void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
        
        // Загружаем базовые стили
        root.styleSheets.Add(typography);
        root.styleSheets.Add(utilities);
        
        // Устанавливаем светлую тему по умолчанию
        ApplyTheme(false);
        
        // Находим кнопку переключения темы
        themeToggle = root.Q<Button>("theme-toggle");
        if (themeToggle != null)
        {
            themeToggle.clicked += ToggleTheme;
        }
    }
    
    void ToggleTheme()
    {
        isDarkMode = !isDarkMode;
        ApplyTheme(isDarkMode);
    }
    
    void ApplyTheme(bool darkMode)
    {
        // Удаляем текущую тему
        if (root.styleSheets.Contains(lightTheme))
            root.styleSheets.Remove(lightTheme);
        if (root.styleSheets.Contains(darkTheme))
            root.styleSheets.Remove(darkTheme);
        
        // Применяем новую тему
        if (darkMode)
        {
            root.styleSheets.Add(darkTheme);
            if (themeToggle != null)
                themeToggle.text = "☀️ Switch to Light Theme";
        }
        else
        {
            root.styleSheets.Add(lightTheme);
            if (themeToggle != null)
                themeToggle.text = "🌙 Switch to Dark Theme";
        }
        
        Debug.Log($"Applied {(darkMode ? "Dark" : "Light")} theme");
    }
}