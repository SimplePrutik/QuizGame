using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private List<TileCategory> categories;
        [SerializeField] private Tile tilePrefab;
        [SerializeField] private RectTransform linesRoot;
        [SerializeField] private RectTransform tilesRoot;
        [SerializeField] private RectTransform tilePoolRoot;

        public List<TileCategory> Categories => categories;

        private List<Tile> spawnedTiles;
        private TilePool tilePool = new TilePool();

        private const float TILE_TO_LINE_WIDTH_KOEF = 32f;
        
        //должно приходить с бэка
        private readonly List<(int, int)> tileLinks = new List<(int, int)>
        {
            (0, 1),
            (1, 2),
            (2, 3),
            (3, 4),
            (4, 5),
            (2, 6),
            (6, 7),
            (7, 8),
            (8, 9)
        };

        public void InitField(
            List<Question> questionsData, 
            CategoryService categoryService,
            ScreensService screensService)
        {
            tilePool.Init(20, tilePrefab, tilePoolRoot);
            var fieldConstructor = new FieldConstructor<Tile>();
            fieldConstructor.InitCell(tilePrefab);
            spawnedTiles = fieldConstructor.CreateField(SpawnTile, SpawnLine, tileLinks);
            
            //проверка на то что количество вопросов соответствует количеству тайлам по идее должно быть в FieldConstructor при динамической верстке
            for (int i = 0; i < spawnedTiles.Count; ++i)
            {
                spawnedTiles[i].Init(categoryService.Categories[questionsData[i].category], i);
                spawnedTiles[i].OnButtonPressed
                    .Subscribe(id =>
                    {
                        var screen = screensService.ChangeScreen<QuestionScreen>();
                        screen.InitQuestion(id);
                        spawnedTiles[id].SetEnabled(TileState.Answered);
                        tileLinks
                            .Where(link => link.Item1 == id)
                            .Select(link => link.Item2)
                            .ToList()
                            .ForEach(tileId => spawnedTiles[tileId].SetEnabled(TileState.Enabled));
                    })
                    .AddTo(this);
            }
            spawnedTiles[0].SetEnabled(TileState.Enabled);
        }

        private Tile SpawnTile(Vector2 spawnLocation)
        {
            var tile = tilePool.Spawn(spawnLocation);
            tile.transform.SetParent(tilesRoot);
            tile.GetComponent<RectTransform>().localScale = Vector3.one;
            return tile;
        }

        private void SpawnLine(RectTransform tileStart, RectTransform tileEnd)
        {
            var line = new GameObject("Line");
            var lineImage = line.AddComponent<Image>();
            var startPos = tileStart.anchoredPosition;
            var endPos = tileEnd.anchoredPosition;
            var lineWidth = tileStart.sizeDelta.x / TILE_TO_LINE_WIDTH_KOEF;
            var rect = line.GetComponent<RectTransform>();
            
            lineImage.color = Color.black;
            
            rect.SetParent(linesRoot);
            rect.localScale = Vector3.one;
            rect.anchoredPosition = Vector2.Lerp(startPos,endPos,0.5f);
            rect.sizeDelta = new Vector2(
                Mathf.Max(lineWidth, Mathf.Abs(startPos.x - endPos.x)),
                Mathf.Max(lineWidth,Mathf.Abs(startPos.y - endPos.y)));
        }
    }
}