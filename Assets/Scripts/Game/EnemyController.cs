using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
    public float speed;
    public TipoInimigo tipoInimigo = TipoInimigo.Mina;    
    public float distance;
    public int dano = 0;
    public GameObject particula;

    private GameObject player;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool ataquePreparado = false;
    private bool causouDano = false;
    private BoxCollider2D enemyCollider;
    private GameController gameController;
    private PlayerController playerController;
    
    public enum TipoInimigo
    {
        Mina = 0,
        Tubarao = 1,
        Pedra = 2,
        Placa = 3,
        Correio = 4
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        enemyCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (tipoInimigo == TipoInimigo.Tubarao)
        {
            anim = GetComponent<Animator>();             
        }
    }
    
    void Update()
    {
        transform.position -= new Vector3(1 * speed * Time.deltaTime, 0,0);
        if (tipoInimigo == TipoInimigo.Tubarao)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < 10.0f && !ataquePreparado)
            {
                anim.SetTrigger("PrepararAtaque");
                ataquePreparado = true;
            }
        }

        if (player.transform.position.y > transform.position.y)        
            spriteRenderer.sortingOrder = 3;        
        else        
            spriteRenderer.sortingOrder = 0;        
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.tag == "Player")
        {            
            if (playerController.invencivel)
                return;

            if(tipoInimigo == TipoInimigo.Correio)
            {
                player.GetComponent<PlayerController>().movimentoFinal = true;
                return;
            }

            StartCoroutine(CameraShake.Shake()); 
            if (!causouDano && tipoInimigo == TipoInimigo.Tubarao)
            {
                causouDano = true;
                anim.SetTrigger("Atacar");
                enemyCollider.enabled = false;                   
            }
            if (tipoInimigo == TipoInimigo.Mina)
            {                
                Instantiate(particula, transform.position, transform.rotation);
                spriteRenderer.sprite = null;
                GetComponent<BoxCollider2D>().enabled = false;
                //Destroy(gameObject);
            }
            if (tipoInimigo == TipoInimigo.Pedra || tipoInimigo == TipoInimigo.Placa)
            {
                Instantiate(particula, transform.position, transform.rotation);
                //Destroy(gameObject);
            }
            //player.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<Animator>().SetTrigger("Atingido");
            playerController.invencivel = true;
            gameController.CausarDano(dano);            
        }
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void ChangeLayer()
    {
        if (spriteRenderer.sortingOrder == 0)        
            spriteRenderer.sortingOrder = 2;
        else
            spriteRenderer.sortingOrder = 0;        
    }

    void ParticulaTubarao()
    {
        Instantiate(particula, new Vector3(transform.position.x - 1.0f, transform.position.y, 0), transform.rotation);        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
