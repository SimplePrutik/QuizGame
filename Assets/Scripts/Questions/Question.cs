using System;

[Serializable]
public struct Question
{
    public int id;
    public string title;
    public string category;
    public Answer[] answers;
}