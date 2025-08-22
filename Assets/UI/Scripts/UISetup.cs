using UnityEngine;
using UnityEngine.UIElements;

public class UISetup : MonoBehaviour
{
    [Header("UI Components")]
    public UIDocument uiDocument;
    public ThemeController themeController;
    public CardManager cardManager;
    public CardCreator cardCreator;
    
    [Header("Style Sheets")]
    public StyleSheet lightTheme;
    public StyleSheet darkTheme;
    
    void Start()
    {
        SetupUI();
    }
    
    void SetupUI()
    {
        // Проверяем UIDocument
        if (uiDocument == null)
        {
            uiDocument = GetComponent<UIDocument>();
            if (uiDocument == null)
            {
                Debug.LogError("UIDocument не найден! Создайте UIDocument на этом GameObject.");
                return;
            }
        }
        
        // Проверяем ThemeController
        if (themeController == null)
        {
            themeController = GetComponent<ThemeController>();
            if (themeController == null)
            {
                themeController = gameObject.AddComponent<ThemeController>();
                Debug.Log("ThemeController добавлен автоматически");
            }
        }
        
        // Проверяем CardManager
        if (cardManager == null)
        {
            cardManager = GetComponent<CardManager>();
            if (cardManager == null)
            {
                cardManager = gameObject.AddComponent<CardManager>();
                Debug.Log("CardManager добавлен автоматически");
            }
        }
        
        // Проверяем CardCreator
        if (cardCreator == null)
        {
            cardCreator = GetComponent<CardCreator>();
            if (cardCreator == null)
            {
                cardCreator = gameObject.AddComponent<CardCreator>();
                Debug.Log("CardCreator добавлен автоматически");
            }
        }
        
        // Настраиваем ThemeController
        if (themeController != null)
        {
            themeController.uiDocument = uiDocument;
            if (lightTheme != null) themeController.lightTheme = lightTheme;
            if (darkTheme != null) themeController.darkTheme = darkTheme;
            Debug.Log("ThemeController настроен");
        }
        
        // Настраиваем CardManager
        if (cardManager != null)
        {
            cardManager.uiDocument = uiDocument;
            Debug.Log("CardManager настроен");
        }
        
        // Настраиваем CardCreator
        if (cardCreator != null)
        {
            cardCreator.uiDocument = uiDocument;
            Debug.Log("CardCreator настроен");
        }
        
        // Проверяем Panel Settings
        if (uiDocument.panelSettings == null)
        {
            Debug.LogWarning("Panel Settings не назначен в UIDocument! Это может вызвать проблемы с отображением.");
        }
        
        Debug.Log("UI Setup завершён. Проверьте Console для дополнительной информации.");
    }
    
    [ContextMenu("Setup UI")]
    void SetupUIFromContext()
    {
        SetupUI();
    }
    
    [ContextMenu("Test UI Elements")]
    void TestUIElements()
    {
        if (uiDocument != null && uiDocument.rootVisualElement != null)
        {
            var root = uiDocument.rootVisualElement;
            Debug.Log($"Root element найден: {root.name}, visible: {root.visible}");
            
            var cardsContainer = root.Q("cards-container");
            if (cardsContainer != null)
            {
                Debug.Log($"Cards container найден, visible: {cardsContainer.visible}");
            }
            else
            {
                Debug.LogWarning("Cards container не найден!");
            }
            
            var themeToggle = root.Q<Button>("theme-toggle");
            if (themeToggle != null)
            {
                Debug.Log($"Theme toggle найден, text: {themeToggle.text}");
            }
            else
            {
                Debug.LogWarning("Theme toggle не найден!");
            }
        }
        else
        {
            Debug.LogError("UIDocument или rootVisualElement недоступны!");
        }
    }
    
    [ContextMenu("Test Theme Switching")]
    void TestThemeSwitching()
    {
        if (themeController != null)
        {
            Debug.Log("Тестируем переключение тем...");
            themeController.ToggleTheme();
        }
        else
        {
            Debug.LogWarning("ThemeController не найден!");
        }
    }
}
