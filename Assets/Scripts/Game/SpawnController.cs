using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour 
{
    public float maxY = -0.43f;
    public float minY = -6.18f;
    public GameObject pedra1;
    public GameObject pedra2;
    public GameObject mina;
    public GameObject tubarao;    
    public float tempoJogo = 0;    

    private int maxObj = 1;
    private float interval = 3.0f;
    private float speed = 10.0f;
    private List<GameObject> listaInimigos = new List<GameObject>();
    private GameController gameController;

    private bool passo1 = false;
    private bool passo2 = false;
    private bool passo3 = false;
    private bool passo4 = false;
    private bool passo5 = false;

	void Start () 
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        listaInimigos.Add(pedra1);
        listaInimigos.Add(pedra2);
        Invoke("SpawnEnemy", 2f);
	}
		
	void FixedUpdate () 
    {
        tempoJogo += Time.deltaTime;
        if (tempoJogo > 10.0f && !passo1)
        {
            interval = 2.0f;
            passo1 = true;
        }
        if (tempoJogo >= 20.0f && !passo2)
        {
            listaInimigos.Add(mina);
            passo2 = true;
        }
        if (tempoJogo > 30.0f && !passo3)
        {
            maxObj = 3;
            passo3 = true;
        }
        if (tempoJogo > 45.0f && !passo4)
        {
            listaInimigos.Add(tubarao);
            speed = 15.0f;
            passo4 = true;
            interval = 1.5f;
        }
        if (tempoJogo > 60.0f && !passo5)
        {
            interval = 1.0f;
            speed = 20.0f;
        }

        if (tempoJogo > 100.0f)
        {
            gameController.VenceuJogo();
            Destroy(gameObject);
        }
	}

    void SpawnEnemy()
    {
        int maxEnemies = Random.Range(1, maxObj);
        int chosenEnemy = Random.Range(0, listaInimigos.Count);
        bool verificarDistancia = maxEnemies > 1 ? true : false;
        float posY = 0.0f;

        for (int i = 1; i <= maxEnemies; i++)
        {
            GameObject enemy = Instantiate(listaInimigos[chosenEnemy], new Vector3(transform.position.x, Random.Range(minY, maxY), 0), transform.rotation) as GameObject;
            enemy.GetComponent<EnemyController>().speed = speed;

            if (verificarDistancia)
            {
                if(posY == 0.0f)
                    posY = enemy.transform.position.y;
                else if(posY < 0.0f)
                {
                    float diferenca = Mathf.Abs(posY - enemy.transform.position.y);
                    if (diferenca < 2.0f)
                    {
                        if (posY >= enemy.transform.position.y)
                        {
                            enemy.transform.position = new Vector3(enemy.transform.position.x, posY - 2.0f, 0);
                        }
                        else
                        {
                            enemy.transform.position = new Vector3(enemy.transform.position.x, posY + 2.0f, 0);
                        }
                    }
                }
            }
            
        }
        verificarDistancia = false;
        Invoke("SpawnEnemy", interval);
    }


}
