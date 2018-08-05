using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour 
{	
	void Awake() 
    {
        Invoke("DestroyParticle", 2f);
	}
	
    void DestroyParticle()
    {
        Destroy(gameObject);
    }
}
