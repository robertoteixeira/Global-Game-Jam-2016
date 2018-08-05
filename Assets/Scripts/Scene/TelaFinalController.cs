using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TelaFinalController : MonoBehaviour {

    public Text dialogTxt;
    public Text txtContinue;
    public bool liberaInteracao;
	
	void Start () 
    {
        dialogTxt.fontSize = (int)(Screen.width * 0.010f);
        txtContinue.fontSize = (int)(Screen.width * 0.017f);
        dialogTxt.text = LanguageManager.RetornaString("tip");
        txtContinue.text = "";
        Invoke("LiberaInteracao", 2f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (liberaInteracao)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                Application.LoadLevel("Menu");
            }
            
        }
	}

    void LiberaInteracao()
    {
        liberaInteracao = true;
        txtContinue.text = LanguageManager.RetornaString("continue");
    }
}
