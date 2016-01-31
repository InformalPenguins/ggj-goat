using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Camera camera;

    private Vector2 velocity = Vector2.zero;

    private float dampTime = 0.3f;

    // Use this for initialization
    void Start()
    {
        camera = this.GetComponent<Camera>();
        target = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
        }
    }

}
