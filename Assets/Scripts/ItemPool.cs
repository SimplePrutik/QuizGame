using System.Collections.Generic;
using UnityEngine;

namespace Installers
{
    public class ItemPool<T> where T : MonoBehaviour
    {
        private readonly Vector2 poolCoords = new Vector2(10000f, 10000f);
        
        private List<T> objectsToSpawn = new List<T>();
        private List<T> objectsToDespawn = new List<T>();
        private T prototype;
        private Transform root;
        
        public void Init(int capacity, T prototype, Transform root)
        {
            this.prototype = prototype;
            this.root = root;
            
            for (int i = 0; i < capacity; ++i)
                InitItem();
        }

        public T Spawn(Vector2 spawnCoords)
        {
            if (objectsToSpawn.Count == 0)
            {
                IncreaseCapacity();
            }
            
            var spawnedObject = objectsToSpawn[0];
            objectsToSpawn.RemoveAt(0);
            objectsToDespawn.Add(spawnedObject);
            spawnedObject.GetComponent<RectTransform>().anchoredPosition = spawnCoords;
            spawnedObject.gameObject.SetActive(true);
            return spawnedObject;
        }

        public void Despawn(T objectToDespawn)
        {
            objectsToDespawn.Remove(objectToDespawn);
            objectsToSpawn.Add(objectToDespawn);
            objectToDespawn.GetComponent<RectTransform>().anchoredPosition = poolCoords;
            objectToDespawn.gameObject.SetActive(false);
        }

        private void IncreaseCapacity()
        {
            var currentCapacity = objectsToDespawn.Count;
            for (int i = 0; i < currentCapacity; ++i)
                InitItem();
        }

        private void InitItem()
        {
            var spawnedObject = GameObject.Instantiate(prototype, root);
            spawnedObject.GetComponent<RectTransform>().anchoredPosition = poolCoords;
            spawnedObject.gameObject.SetActive(false);
            objectsToSpawn.Add(spawnedObject);
        }
    }
}