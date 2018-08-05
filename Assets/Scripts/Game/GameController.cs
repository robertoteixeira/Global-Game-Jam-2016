using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour 
{
    public bool paused;
    public bool gameOver;
    public CanvasGroup pauseMenu;
    public GameObject particleExplosion;
    public GameObject maozinha;
    public int playerHealth = 50;
    public GameObject cxCorreio;    

    private GameObject player;
    private AudioSource[] allAudioSources;
    private List<GameObject> hpBar;


    void Awake()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];        
    }

	void Start () 
    {
        hpBar = GameObject.FindGameObjectsWithTag("HPBar").OrderBy(hp => hp.name).ToList();
        player = GameObject.FindGameObjectWithTag("Player");
        pauseMenu.gameObject.transform.Find("PauseTxt").GetComponent<Text>().fontSize = (int)(Screen.width * 0.04f);
        pauseMenu.gameObject.transform.Find("PauseTxt").GetComponent<Text>().text = LanguageManager.RetornaString("paused");
        int btnFontSize = (int)(Screen.width * 0.020f);
        GameObject.Find("RestartTxt").GetComponent<Text>().fontSize = btnFontSize;
        GameObject.Find("MenuTxt").GetComponent<Text>().fontSize = btnFontSize;
        GameObject.Find("QuitTxt").GetComponent<Text>().fontSize = btnFontSize;
        GameObject.Find("RestartTxt").GetComponent<Text>().text = LanguageManager.RetornaString("restart");
        GameObject.Find("QuitTxt").GetComponent<Text>().text = LanguageManager.RetornaString("quit");        
	}
		
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            if (paused)
            {
                pauseMenu.alpha = 0;
                pauseMenu.interactable = false;
                pauseMenu.blocksRaycasts = false;
                paused = false;
                PlayAllAudio();
                Time.timeScale = 1;
            }
            else
            {
                pauseMenu.alpha = 1;
                pauseMenu.interactable = true;
                pauseMenu.blocksRaycasts = true;
                paused = true;
                StopAllAudio();
                Time.timeScale = 0;
            }
        }
        if (gameOver)
        {
            particleExplosion.transform.position -= new Vector3(1, 0, 0) * 8.0f * Time.deltaTime;
        }
	}

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1;
    }

    public void VoltarMenu()
    {
        Application.LoadLevel("Menu");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        particleExplosion = Instantiate(particleExplosion, player.transform.position, player.transform.rotation) as GameObject;
        Instantiate(maozinha, player.transform.position, player.transform.rotation);
        player.SetActive(false);        
        gameOver = true;
        Destroy(GameObject.Find("SpawnPoint"));
        Invoke("GameOverMenu", 1.5f);
    }

    private void GameOverMenu()
    {
        pauseMenu.gameObject.transform.Find("PauseTxt").GetComponent<Text>().text = "GAMEOVER";
        pauseMenu.alpha = 1;
        pauseMenu.interactable = true;
        pauseMenu.blocksRaycasts = true;                
    }

    public void CausarDano(int dano)
    {
        playerHealth -= dano;

        int qtdHP = dano / 10;
        if ((hpBar.Count - qtdHP) <= 0)
            qtdHP = 1;
        for (int i = 1; i <= qtdHP; i++)
        {
            Destroy(hpBar[hpBar.Count - 1]);
            hpBar.RemoveAt(hpBar.Count - 1);
        }

        if (playerHealth <= 0)
        {
            if (hpBar.Count >= 1)
            {
                foreach (GameObject bar in hpBar)
                {
                    Destroy(bar);
                }
            }
            GameOver();
        }
    }

    public void VenceuJogo()
    {
        Debug.Log("Venceu");
        cxCorreio.SetActive(true);
    }

    public void StopAllAudio()
    {
        foreach (AudioSource audio in allAudioSources)
        {  
            if(audio.isPlaying)
                audio.Pause();
        }
    }
    public void PlayAllAudio()
    {
        foreach (AudioSource audio in allAudioSources)
        {            
            audio.Play();
        }
    }
}
