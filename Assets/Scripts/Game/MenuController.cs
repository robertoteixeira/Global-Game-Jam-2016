using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour 
{
    public Text inicioTxt;
    public Text idiomasTxt;
    public Text creditosTxt;
    public GameObject panelIdiomas;
    public GameObject panelCreditos;

    public Text fabioTxt;
    public Text fabioTxt2;
    public Text robertoTxt;
    public Text robertoTxt2;
    public Text emailTxt;

    private bool panelAtivo = false;
    private bool creditosAtivo = false;

	void Awake() 
    {
        int fontSize = (int)(Screen.width * 0.026f);
        int fontSize2 = (int)(Screen.width * 0.015f);

        inicioTxt.fontSize = fontSize;
        idiomasTxt.fontSize = fontSize;
        creditosTxt.fontSize = fontSize;

        inicioTxt.text = LanguageManager.RetornaString("start");
        idiomasTxt.text = LanguageManager.RetornaString("languages");
        creditosTxt.text = LanguageManager.RetornaString("credits");

        fabioTxt.fontSize = fontSize2;
        fabioTxt2.fontSize = fontSize2;
        robertoTxt.fontSize = fontSize2;
        robertoTxt2.fontSize = fontSize2;

        fabioTxt2.text = LanguageManager.RetornaString("art");
        robertoTxt2.text = LanguageManager.RetornaString("program");

        emailTxt.fontSize = fontSize;
	}	
	
    void FixedUpdate()
    {
        if (creditosAtivo && (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Return)))
        {
            panelCreditos.GetComponent<Animator>().SetTrigger("hide");
            GameObject.Find("txtContinue").GetComponent<Text>().text = ""; 
            GameObject.Find("PanelMenu").GetComponent<Animator>().SetTrigger("show");
            creditosAtivo = false;
        }
    }


    public void IniciarJogo()
    {
        //Chamar animacao
        GameObject.Find("PanelMenu").GetComponent<Animator>().SetTrigger("hide");
        ChamaAtendente();
        //Application.LoadLevel("Jogo");
    }
    public void Idiomas()
    {
        panelAtivo = !panelAtivo;
        panelIdiomas.GetComponent<Animator>().SetBool("show", panelAtivo);        
    }
    public void Creditos()
    {
        GameObject.Find("PanelMenu").GetComponent<Animator>().SetTrigger("hide");
        Invoke("AtivaParticulas", 0.2f);
    }

    public void IdiomaBrasil()
    {
        PlayerPrefs.SetString("Language", "Portuguese");
        LanguageManager.languageManager.AtualizaIdioma();
        LanguageManager.languageManager.CarregaXML();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void IdiomaEUA()
    {
        PlayerPrefs.SetString("Language", "English");
        LanguageManager.languageManager.AtualizaIdioma();
        LanguageManager.languageManager.CarregaXML();
        Application.LoadLevel(Application.loadedLevel);
    }

    private void ChamaAtendente()
    {
        GameObject.Find("Atendente").GetComponent<Animator>().SetTrigger("entrar");
        Invoke("LiberaDialogo", 1f);
    }

    void LiberaDialogo()
    {
        GetComponent<DialogoController>().liberarDialogo = true;
    }

    void AtivaParticulas()
    {
        Invoke("PanelCreditos", 0.1f);
    }

    void PanelCreditos()
    {
        panelCreditos.GetComponent<Animator>().SetTrigger("show");
        GameObject.Find("txtContinue").GetComponent<Text>().text = LanguageManager.RetornaString("back");
        creditosAtivo = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}


