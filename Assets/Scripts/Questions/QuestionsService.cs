using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class QuestionsService
{
    public List<Question> QuestionsData;
    
    public QuestionsService()
    {
        ExtractData();
    }

    private void ExtractData()
    {
        string path = Application.streamingAssetsPath + "/questions.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            QuestionsData = JsonUtility.FromJson<QuestionsData>(json).questions.ToList();
        }
        else
        {
            Debug.LogError("Cannot find file! " + path);
        }
    }
}