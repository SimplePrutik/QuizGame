using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class FieldConstructor<T> where T : MonoBehaviour
    {
        #region StaticLayout

        //эти данные нужно получать с бэка
        private readonly int fieldSize = 10;
        
        private readonly List<Vector2> tilesCoords = new List<Vector2>
        {
            new Vector2(-1.5f,-2),
            new Vector2(-1.5f,-1),
            new Vector2(-0.5f,-1),
            new Vector2(-0.5f,0),
            new Vector2(-0.5f,1),
            new Vector2(-0.5f,2),
            new Vector2(0.5f,-1),
            new Vector2(1.5f,-1),
            new Vector2(1.5f,0),
            new Vector2(1.5f,1)
        };
        

        #endregion

        private float cellSize = 0;
        
        public List<T> CreateField(
            Func<Vector2, T> itemSpawnFunction,
            Action<RectTransform, RectTransform> linkSpawnFunction,
            List<(int, int)> tileLinks
            )
        {
            var itemList = new List<T>();
            for (int i = 0; i < fieldSize; ++i)
            {
                var tile = itemSpawnFunction(tilesCoords[i] * cellSize);
                itemList.Add(tile);
            }

            foreach (var tilesLink in tileLinks)
            {
                if (itemList.Count <= Mathf.Max(tilesLink.Item1, tilesLink.Item2))
                    continue;
                linkSpawnFunction(itemList[tilesLink.Item1].GetComponent<RectTransform>(),
                    itemList[tilesLink.Item2].GetComponent<RectTransform>());
            }

            return itemList;
        }

        public void InitCell(T itemPrefab)
        {
            cellSize = itemPrefab.GetComponent<RectTransform>().sizeDelta.x * 1.2f;
        }
    }
}