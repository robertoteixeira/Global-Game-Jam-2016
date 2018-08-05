using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{

    public float minX = -10.82f;
    public float maxX = 9.94f;
    public float minY = -5.701f;
    public float maxY = 1.55f;
    public float speed = 20.0f;
    public bool movimentoFinal = false;
    public Transform posMovimentoFinal;
    public bool invencivel = false;

    private Animator anim;
    private bool firstMove = false;
    private bool secondMove = false;
    private float velocity = 1f;
		
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

	void Update () 
    {
        if (!movimentoFinal)
        {
            if (firstMove && secondMove)
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                Vector3 move = new Vector3(horizontal, vertical, 0);

                if (transform.position.x <= minX && horizontal < 0)
                {
                    transform.position = new Vector3(minX, transform.position.y, 0);
                }
                if (transform.position.x >= maxX && horizontal > 0)
                {
                    transform.position = new Vector3(maxX, transform.position.y, 0);
                }
                if (transform.position.y <= minY && vertical < 0)
                {
                    transform.position = new Vector3(transform.position.x, minY, 0);
                }
                if (transform.position.y >= maxY && vertical > 0)
                {
                    transform.position = new Vector3(transform.position.x, maxY, 0);
                }
                transform.position += move * speed * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(velocity, 0, 0) * speed * Time.deltaTime;
                if (transform.position.x >= -4.5f && !firstMove)
                {
                    velocity = 0.5f;
                    firstMove = true;
                }
                if (transform.position.x >= -1.94f && !secondMove)
                {
                    velocity = -0.2f;
                }
                if (transform.position.x <= -7f && firstMove && !secondMove)
                {
                    secondMove = true;
                }
            } 
        }
        else
        {
            MovimentoFinal();
        }
	}
    
    void MovimentoFinal()
    {
        velocity = 1.0f;
        transform.position += new Vector3(velocity, 0, 0) * speed * Time.deltaTime;
    }   
    
    public void Invencivel()
    {
        invencivel = false;
    }

    void OnBecameInvisible()
    {
        if (movimentoFinal)
        {
            GameObject.Find("Fading").GetComponent<Fading>().fadeSpeed = 0.1f;
            GameObject.Find("Fading").GetComponent<Fading>().BeginFade(1);
            Invoke("CenaFinal", 6f);
        }
    }

    void CenaFinal()
    {
        Application.LoadLevel("Final");
    }


}
