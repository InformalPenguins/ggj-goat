using UnityEngine;

public class StartGameController : MonoBehaviour
{

    public GameObject Highlighted;

    private BoxCollider2D collider;

    bool over = false; //hax
                       // Use this for initialization
    void Start()
    {
        Highlighted.SetActive(false);
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && over)
        {
            startGame();
        }
    }

    private void startGame()
    {
        //PlayerPrefs.SetInt("SeenIntro", 1);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Lives", 3);
        Application.LoadLevel("intro");
    }

    void OnMouseEnter()
    {
        Highlighted.SetActive(true);
        over = true;
    }

    void OnMouseExit()
    {
        Highlighted.SetActive(false);
        over = false;
    }

}
