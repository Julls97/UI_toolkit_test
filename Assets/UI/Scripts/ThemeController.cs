using UnityEngine;
using UnityEngine.UIElements;

public class ThemeController : MonoBehaviour
{
    [Header("Style Sheets")]
    public StyleSheet lightTheme;
    public StyleSheet darkTheme;
    
    [Header("References")]
    public UIDocument uiDocument;
    
    private VisualElement root;
    private Button themeToggle;
    private bool isDarkTheme = false;
    
    void Start()
    {
        InitializeUI();
        ApplyLightTheme(); // По умолчанию светлая тема
    }
    
    void InitializeUI()
    {
        if (uiDocument == null)
        {
            uiDocument = GetComponent<UIDocument>();
        }
        
        if (uiDocument != null)
        {
            root = uiDocument.rootVisualElement;
            if (root != null)
            {
                themeToggle = root.Q<Button>("theme-toggle");
                if (themeToggle != null)
                {
                    themeToggle.clicked += ToggleTheme;
                    Debug.Log("Theme toggle button найден и настроен");
                }
                else
                {
                    Debug.LogWarning("Theme toggle button не найден!");
                }
            }
            else
            {
                Debug.LogError("rootVisualElement не найден!");
            }
        }
        else
        {
            Debug.LogError("UIDocument не найден!");
        }
    }

    public void ToggleTheme()
    {
        if (isDarkTheme)
        {
            ApplyLightTheme();
        }
        else
        {
            ApplyDarkTheme();
        }
        
        // Обновляем текст кнопки
        if (themeToggle != null)
        {
            themeToggle.text = isDarkTheme ? "☀️ Switch to Light Theme" : "🌙 Switch to Dark Theme";
        }
    }
    
    void ApplyLightTheme()
    {
        if (root != null && lightTheme != null)
        {
            // Убираем темную тему
            if (root.styleSheets.Contains(darkTheme))
            {
                root.styleSheets.Remove(darkTheme);
            }
            
            // Добавляем светлую тему
            if (!root.styleSheets.Contains(lightTheme))
            {
                root.styleSheets.Add(lightTheme);
            }
            
            isDarkTheme = false;
            Debug.Log("Применена светлая тема");
        }
    }
    
    void ApplyDarkTheme()
    {
        if (root != null && darkTheme != null)
        {
            // Убираем светлую тему
            if (root.styleSheets.Contains(lightTheme))
            {
                root.styleSheets.Remove(lightTheme);
            }
            
            // Добавляем темную тему
            if (!root.styleSheets.Contains(darkTheme))
            {
                root.styleSheets.Add(darkTheme);
            }
            
            isDarkTheme = true;
            Debug.Log("Применена темная тема");
        }
    }
    
    [ContextMenu("Apply Light Theme")]
    void ApplyLightThemeFromContext()
    {
        ApplyLightTheme();
    }
    
    [ContextMenu("Apply Dark Theme")]
    void ApplyDarkThemeFromContext()
    {
        ApplyDarkTheme();
    }
    
    [ContextMenu("Toggle Theme")]
    void ToggleThemeFromContext()
    {
        ToggleTheme();
    }
}
