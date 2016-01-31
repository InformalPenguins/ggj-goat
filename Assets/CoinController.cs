using UnityEngine;

public class CoinController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.transform.parent.gameObject);
    }

}
