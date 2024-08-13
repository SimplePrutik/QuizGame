using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class QuestionsService
{
    public List<Question> QuestionsData;
    
    public IEnumerator ExtractData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "questions.json");
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string fileContents = www.downloadHandler.text;
            QuestionsData = JsonUtility.FromJson<QuestionsData>(fileContents).questions.ToList();
        }
        else
        {
            Debug.LogError("Failed to load file: " + www.error);
        }
    }
}