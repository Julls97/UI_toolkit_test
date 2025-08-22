using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class CardData
{
    public string id;
    public string title;
    public string description;
    public string buttonText;
    public string buttonClass;
    public Color backgroundColor = Color.white;
    public Color titleColor = new Color(0.102f, 0.102f, 0.102f, 1f);
    public Color descriptionColor = new Color(0.102f, 0.102f, 0.102f, 1f);
    public float width = 300f;
}

public class CardManager : MonoBehaviour
{
    [Header("Card Templates")]
    public List<CardData> cardTemplates = new List<CardData>();
    
    [Header("References")]
    public UIDocument uiDocument;
    
    private VisualElement root;
    private VisualElement cardsContainer;
    private Dictionary<string, VisualElement> createdCards = new Dictionary<string, VisualElement>();
    
    void Start()
    {
        InitializeUI();
        CreateDefaultCards();
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
                cardsContainer = root.Q("cards-container");
                
                if (cardsContainer == null)
                {
                    Debug.LogError("Cards container не найден!");
                }
                else
                {
                    Debug.Log("Cards container найден успешно");
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
    
    void CreateDefaultCards()
    {
        if (cardsContainer == null)
        {
            Debug.LogWarning("Cards container недоступен, пропускаем создание карточек");
            return;
        }
        
        // Создаем карточки из шаблонов
        foreach (var template in cardTemplates)
        {
            CreateCardFromTemplate(template);
        }
        
        // Если шаблонов нет, создаем стандартные карточки
        if (cardTemplates.Count == 0)
        {
            CreateStandardCards();
        }
    }
    
    void CreateStandardCards()
    {
        // Создаем стандартную light карточку
        var lightCardData = new CardData
        {
            id = "light-card",
            title = "Primary Card",
            description = "Это пример карточки с основным стилем. Текст хорошо читается на светлом фоне.",
            buttonText = "Primary Action",
            buttonClass = "primary"
        };
        
        CreateCardFromTemplate(lightCardData);
        
        // Создаем brand карточку
        var brandCardData = new CardData
        {
            id = "brand-card",
            title = "Brand Warm Card",
            description = "Карточка с тёплыми фирменными цветами. Отлично подходит для акцентов.",
            buttonText = "Brand Action",
            buttonClass = "brand-warm-600",
            backgroundColor = new Color(0.996f, 0.843f, 0.667f, 1f),
            titleColor = new Color(0.604f, 0.204f, 0.071f, 1f),
            descriptionColor = new Color(0.486f, 0.176f, 0.071f, 1f)
        };
        
        CreateCardFromTemplate(brandCardData);
    }
    
    public VisualElement CreateCardFromTemplate(CardData data)
    {
        if (cardsContainer == null)
        {
            Debug.LogError("Cards container недоступен!");
            return null;
        }
        
        // Создаем карточку
        var card = new VisualElement();
        card.name = data.id;
        card.AddToClassList("card");
        
        // Применяем кастомные стили если нужно
        if (data.backgroundColor != Color.white)
        {
            card.style.backgroundColor = data.backgroundColor;
        }
        
        if (data.width != 300f)
        {
            card.style.width = data.width;
        }
        
        // Создаем заголовок
        var titleLabel = new Label(data.title);
        titleLabel.name = "card-title";
        titleLabel.AddToClassList("foreground");
        titleLabel.AddToClassList("card-title");
        
        if (data.titleColor != new Color(0.102f, 0.102f, 0.102f, 1f))
        {
            titleLabel.style.color = data.titleColor;
        }
        
        card.Add(titleLabel);
        
        // Создаем описание
        var descriptionLabel = new Label(data.description);
        descriptionLabel.name = "card-description";
        descriptionLabel.AddToClassList("foreground");
        descriptionLabel.AddToClassList("card-description");
        
        if (data.descriptionColor != new Color(0.102f, 0.102f, 0.102f, 1f))
        {
            descriptionLabel.style.color = data.descriptionColor;
        }
        
        card.Add(descriptionLabel);
        
        // Создаем кнопку
        var button = new Button();
        button.name = "card-button";
        button.text = data.buttonText;
        button.AddToClassList("card-button");
        button.AddToClassList(data.buttonClass);
        
        // Добавляем обработчик клика
        button.clicked += () => OnCardButtonClicked(data.id, data.buttonText);
        
        card.Add(button);
        
        // Добавляем карточку в контейнер
        cardsContainer.Add(card);
        
        // Сохраняем ссылку на созданную карточку
        createdCards[data.id] = card;
        
        Debug.Log($"Карточка '{data.id}' создана из шаблона");
        
        return card;
    }
    
    public void CreateDynamicCard(string title, string description, string buttonText = "Action")
    {
        var cardData = new CardData
        {
            id = "dynamic-card-" + Random.Range(1000, 9999),
            title = title,
            description = description,
            buttonText = buttonText,
            buttonClass = "secondary"
        };
        
        CreateCardFromTemplate(cardData);
    }
    
    public void RemoveCard(string cardId)
    {
        if (createdCards.ContainsKey(cardId))
        {
            var card = createdCards[cardId];
            if (cardsContainer.Contains(card))
            {
                cardsContainer.Remove(card);
                createdCards.Remove(cardId);
                Debug.Log($"Карточка '{cardId}' удалена");
            }
        }
        else
        {
            Debug.LogWarning($"Карточка с ID '{cardId}' не найдена");
        }
    }
    
    public void UpdateCard(string cardId, string newTitle = null, string newDescription = null, string newButtonText = null)
    {
        if (createdCards.ContainsKey(cardId))
        {
            var card = createdCards[cardId];
            
            if (newTitle != null)
            {
                var titleLabel = card.Q<Label>("card-title");
                if (titleLabel != null) titleLabel.text = newTitle;
            }
            
            if (newDescription != null)
            {
                var descriptionLabel = card.Q<Label>("card-description");
                if (descriptionLabel != null) descriptionLabel.text = newDescription;
            }
            
            if (newButtonText != null)
            {
                var button = card.Q<Button>("card-button");
                if (button != null) button.text = newButtonText;
            }
            
            Debug.Log($"Карточка '{cardId}' обновлена");
        }
        else
        {
            Debug.LogWarning($"Карточка с ID '{cardId}' не найдена");
        }
    }
    
    void OnCardButtonClicked(string cardId, string buttonText)
    {
        Debug.Log($"Кнопка '{buttonText}' на карточке '{cardId}' была нажата!");
        
        // Здесь можно добавить любую логику при нажатии на кнопку
        // Например, показать уведомление, открыть модальное окно и т.д.
    }
    
    [ContextMenu("Create Random Card")]
    void CreateRandomCardFromContext()
    {
        var titles = new[] { "Awesome Card", "Cool Feature", "New Item", "Special Offer", "Limited Time" };
        var descriptions = new[] { "Это удивительная карточка!", "Крутая функция для вашего проекта", "Новый элемент интерфейса", "Специальное предложение", "Ограниченное время действия" };
        
        var randomTitle = titles[Random.Range(0, titles.Length)];
        var randomDescription = descriptions[Random.Range(0, descriptions.Length)];
        
        CreateDynamicCard(randomTitle, randomDescription, "Try It!");
    }
    
    [ContextMenu("Clear All Cards")]
    void ClearAllCardsFromContext()
    {
        if (cardsContainer != null)
        {
            cardsContainer.Clear();
            createdCards.Clear();
            Debug.Log("Все карточки удалены!");
        }
    }
    
    [ContextMenu("Recreate Default Cards")]
    void RecreateDefaultCardsFromContext()
    {
        if (cardsContainer != null)
        {
            cardsContainer.Clear();
            createdCards.Clear();
            CreateDefaultCards();
            Debug.Log("Стандартные карточки пересозданы!");
        }
    }
}
