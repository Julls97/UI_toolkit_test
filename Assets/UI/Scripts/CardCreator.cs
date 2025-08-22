using UnityEngine;
using UnityEngine.UIElements;

public class CardCreator : MonoBehaviour
{
    [Header("Card Settings")]
    public string cardTitle = "Primary Card";
    public string cardDescription = "Это пример карточки с основным стилем. Текст хорошо читается на светлом фоне.";
    public string buttonText = "Primary Action";
    
    [Header("References")]
    public UIDocument uiDocument;
    
    private VisualElement root;
    private VisualElement cardsContainer;
    
    void Start()
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
                cardsContainer = root.Q("cards-container");
                
                if (cardsContainer != null)
                {
                    CreateLightCard();
                    CreateAdditionalCards();
                }
                else
                {
                    Debug.LogError("Cards container не найден!");
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
    
    void CreateLightCard()
    {
        // Создаем основную карточку
        var lightCard = CreateCard(
            "light-card",
            cardTitle,
            cardDescription,
            buttonText,
            "primary"
        );
        
        // Добавляем карточку в контейнер
        cardsContainer.Add(lightCard);
        
        Debug.Log("Light card создана из кода");
    }
    
    void CreateAdditionalCards()
    {
        // Создаем дополнительные карточки для демонстрации
        var card2 = CreateCard(
            "brand-card",
            "Brand Warm Card",
            "Карточка с тёплыми фирменными цветами. Отлично подходит для акцентов.",
            "Brand Action",
            "brand-warm-600"
        );
        
        // Применяем специальные стили для brand карточки
        card2.style.backgroundColor = new Color(0.996f, 0.843f, 0.667f, 1f); // rgb(254, 215, 170)
        
        // Настраиваем заголовок brand карточки
        var brandTitle = card2.Q<Label>("card-title");
        if (brandTitle != null)
        {
            brandTitle.style.color = new Color(0.604f, 0.204f, 0.071f, 1f); // rgb(154, 52, 18)
        }
        
        // Настраиваем описание brand карточки
        var brandDescription = card2.Q<Label>("card-description");
        if (brandDescription != null)
        {
            brandDescription.style.color = new Color(0.486f, 0.176f, 0.071f, 1f); // rgb(124, 45, 18)
        }
        
        // Настраиваем кнопку brand карточки
        var brandButton = card2.Q<Button>("card-button");
        if (brandButton != null)
        {
            brandButton.style.color = Color.white;
        }
        
        cardsContainer.Add(card2);
        
        Debug.Log("Brand card создана из кода");
    }
    
    VisualElement CreateCard(string cardName, string title, string description, string buttonText, string buttonClass)
    {
        // Создаем основную карточку
        var card = new VisualElement();
        card.name = cardName;
        card.AddToClassList("card");
        
        // Создаем заголовок
        var titleLabel = new Label(title);
        titleLabel.name = "card-title";
        titleLabel.AddToClassList("foreground");
        titleLabel.AddToClassList("card-title");
        card.Add(titleLabel);
        
        // Создаем описание
        var descriptionLabel = new Label(description);
        descriptionLabel.name = "card-description";
        descriptionLabel.AddToClassList("foreground");
        descriptionLabel.AddToClassList("card-description");
        card.Add(descriptionLabel);
        
        // Создаем кнопку
        var button = new Button();
        button.name = "card-button";
        button.text = buttonText;
        button.AddToClassList("card-button");
        button.AddToClassList(buttonClass);
        
        // Добавляем обработчик клика
        button.clicked += () => OnCardButtonClicked(cardName, buttonText);
        
        card.Add(button);
        
        return card;
    }
    
    void OnCardButtonClicked(string cardName, string buttonText)
    {
        Debug.Log($"Кнопка '{buttonText}' на карточке '{cardName}' была нажата!");
        
        // Здесь можно добавить любую логику при нажатии на кнопку
        // Например, показать уведомление, открыть модальное окно и т.д.
    }
    
    [ContextMenu("Create New Card")]
    void CreateNewCardFromContext()
    {
        if (cardsContainer != null)
        {
            var newCard = CreateCard(
                "dynamic-card-" + Random.Range(1000, 9999),
                "Dynamic Card " + Random.Range(1, 100),
                "Это динамически созданная карточка с случайным номером.",
                "Dynamic Action",
                "primary"
            );
            
            cardsContainer.Add(newCard);
            Debug.Log("Новая динамическая карточка создана!");
        }
    }
    
    [ContextMenu("Clear All Cards")]
    void ClearAllCards()
    {
        if (cardsContainer != null)
        {
            cardsContainer.Clear();
            Debug.Log("Все карточки удалены!");
        }
    }
}
