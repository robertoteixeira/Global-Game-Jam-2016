using UnityEngine;
using System.Collections;

public class AguaPlano2Controller : MonoBehaviour {

    public float speed = 10.0f;
    public GameObject background1;
    public GameObject background2;
    Vector3 backgroundSize;

    void Awake()
    {
        background1.transform.position = new Vector3(0, 2.06f, 0);
        backgroundSize = background1.GetComponent<SpriteRenderer>().bounds.size;
        background2.transform.position = new Vector3(0 + backgroundSize.x, background1.transform.position.y, 0);
    }

    void Update()
    {
        if (background1.transform.position.x <= (backgroundSize.x - (backgroundSize.x * 2)))
        {
            background1.transform.position = new Vector3(background2.transform.position.x + backgroundSize.x, background1.transform.position.y, 0);
        }
        if (background2.transform.position.x <= (backgroundSize.x - (backgroundSize.x * 2)))
        {
            background2.transform.position = new Vector3(background1.transform.position.x + backgroundSize.x, background1.transform.position.y, 0);
        }

        else
        {
            background1.transform.position -= new Vector3(1 * speed * Time.deltaTime, 0, 0);
            background2.transform.position -= new Vector3(1 * speed * Time.deltaTime, 0, 0);
        }
    }
}
