using UnityEngine;
using System.Collections;

[System.Serializable]
public class TextLanguage
{

    private string id;
    private string content;

    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Content
    {
        get { return content; }
        set { content = value; }
    }
}
