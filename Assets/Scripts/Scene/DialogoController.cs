using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogoController : MonoBehaviour 
{
    public Text atendenteTxt;
    public Text cthulhuTxt;
    public Animator animAtentente;
    public Animator animCthulhu;
    public Text txtContinue;

    public bool liberarDialogo = false;

    private string falaAtiva = "";
    private bool liberaInteracao = false;
    private bool skipTimer = false;

	void Start () 
    {
        int fontSize = (int)(Screen.width * 0.017f);
        atendenteTxt.fontSize = fontSize;
        cthulhuTxt.fontSize = fontSize;
        txtContinue.fontSize = fontSize;
        txtContinue.text = "";
	}
	
	void FixedUpdate () 
    {
        if (liberarDialogo)
        {
            liberarDialogo = false;
            liberaInteracao = true;
            Fala1();            
        }
        if (liberaInteracao)
        {
            if (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Return))
            {
                Application.LoadLevel("Jogo");
            } 
        }

	}

    public void Fala1()
    {
        falaAtiva = "Fala1";
        atendenteTxt.text = LanguageManager.RetornaString("fala1");
        animAtentente.SetBool("fade", true);
        animCthulhu.SetBool("fade", false);
        if(!skipTimer)
            Invoke("Fala2", 4f);
        skipTimer = false;
    }
    public void Fala2()
    {
        falaAtiva = "Fala2";
        cthulhuTxt.text = LanguageManager.RetornaString("fala2");
        animAtentente.SetBool("fade", false);
        animCthulhu.SetBool("fade", true);
        if (!skipTimer)
            Invoke("Fala3", 4f);
        skipTimer = false;
    }
    public void Fala3()
    {
        falaAtiva = "Fala3";
        atendenteTxt.text = LanguageManager.RetornaString("fala3");
        animAtentente.SetBool("fade", true);
        animCthulhu.SetBool("fade", false);
        if (!skipTimer)
            Invoke("Fala4", 2f);
    }
    public void Fala4()
    {
        falaAtiva = "Fala4";
        cthulhuTxt.text = LanguageManager.RetornaString("fala4");
        animAtentente.SetBool("fade", false);
        animCthulhu.SetBool("fade", true);
        if (!skipTimer)
            Invoke("Fala5", 2f);
        skipTimer = false;
    }
    public void Fala5()
    {
        falaAtiva = "Fala5";
        atendenteTxt.text = LanguageManager.RetornaString("fala5");
        animAtentente.SetBool("fade", true);
        animCthulhu.SetBool("fade", false);
        if (!skipTimer)
            Invoke("Fala6", 4f);
        skipTimer = false;
    }
    public void Fala6()
    {
        falaAtiva = "Fala6";
        cthulhuTxt.text = LanguageManager.RetornaString("fala6");
        animAtentente.SetBool("fade", false);
        animCthulhu.SetBool("fade", true);
        skipTimer = false;
        Invoke("FimDialogo", 3f);
    }

    public void FimDialogo()
    {
        animAtentente.SetBool("fade", false);
        animCthulhu.SetBool("fade", false);
        txtContinue.text = LanguageManager.RetornaString("continue");
    }
}
