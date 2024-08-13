using System.Collections.Generic;
using UI;
using UnityEngine;

public class CategoryService
{
    //Тут немного идет повторение с TileCategory, но он используется, чисто чтоб передать данные из монобеха, которые надо получать с бэка
    public struct CategoryContent
    {
        public Sprite IconSprite;
        public Color Color;
    }
    
    public Dictionary<string, CategoryContent> Categories = new Dictionary<string, CategoryContent>();
    //Надо получать откуда-нибудь с бэка, но в данных условиях пусть придет из скрина
    public void InitCategories(List<TileCategory> categories)
    {
        foreach (var category in categories)
        {
            var categoryContent = new CategoryContent
            {
                IconSprite = category.categorySprite, Color = category.categoryColor
            };
            Categories[category.categoryName] = categoryContent;
        }
    }
}