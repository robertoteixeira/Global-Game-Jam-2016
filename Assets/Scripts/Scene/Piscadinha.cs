using UnityEngine;
using System.Collections;

public class Piscadinha : MonoBehaviour 
{

    private Animator anim;
	void Start () 
    {
        anim = GetComponent<Animator>();
        Invoke("ChamaPiscadinha", 2f);
	}
		
	void ChamaPiscadinha () 
    {
        anim.SetTrigger("piscadinha");
        Invoke("ChamaPiscadinha", (float)(Random.Range(5, 10)));
	}
}
