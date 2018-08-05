using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System.Linq;

public class LanguageManager : MonoBehaviour 
{
    public static List<TextLanguage> texts = new List<TextLanguage>();
    public static string language;
    public static bool xmlCarregado;
    public static LanguageManager languageManager;

    private TextLanguage textLanguageObj;

    void Awake()
    {
        AtualizaIdioma();
        CarregaXML();
        languageManager = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CarregaXML()
    {
        Debug.Log("CarregaXML Inicio");

        texts.Clear();

        TextAsset languageXML = (TextAsset)Resources.Load("Languages/language_" + language);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(languageXML.text);
        XmlNodeList textsList = xmlDoc.GetElementsByTagName("text");

        foreach (XmlNode node in textsList)
        {
            XmlNodeList textContent = node.ChildNodes;
            textLanguageObj = new TextLanguage();

            foreach (XmlNode item in textContent)
            {
                if (item.Name == "id")
                {
                    textLanguageObj.Id = item.InnerText;
                }

                if (item.Name == "content")
                {
                    textLanguageObj.Content = item.InnerText;
                }
            }
            texts.Add(textLanguageObj);
        }

        Debug.Log("CarregaXML Fim");
        if (Application.loadedLevelName != "Menu")
            Invoke("CarregarTelaMenu", 2f);
    }

    public void AtualizaIdioma()
    {
        Debug.Log("Language: " + PlayerPrefs.GetString("Language"));
        if (PlayerPrefs.HasKey("Language") && PlayerPrefs.GetString("Language") != "")
            language = PlayerPrefs.GetString("Language");
        else
        {
            language = Application.systemLanguage.ToString();
            PlayerPrefs.SetString("Language", language);
        }

        switch (language)
        {   
            case "English": language = "EN";
                break;
            case "Portuguese": language = "PT";
                break;
            default: language = "EN";
                break;
        }
    }

    public static string RetornaString(string id, bool breakLine = false)
    {
        if (breakLine)
        {
            return texts.First(t => t.Id == id).Content.Replace("|", "\n");
        }
        else
        {
            return texts.First(t => t.Id == id).Content;
        }
    }

    private void CarregarTelaMenu()
    {
        Application.LoadLevel("Menu");
    }

}
