using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    public int BOUNDARY = 50;
    [SerializeField]
    public float MOVE_SPEED = 10;
    [SerializeField]
    public float ZOOM_SPEED = 40;
    [SerializeField]
    public float MAX_ZOOM = 4;
    [SerializeField]
    public float MIN_ZOOM = 10;
    private Camera camera;
    private CinemachineVirtualCamera virtualCamera;
    private Collider2D confiner;

    private float screenWidth;
    private float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        camera = transform.parent.GetComponentInChildren<Camera>();
        virtualCamera = transform.parent.GetComponentInChildren<CinemachineVirtualCamera>();
        confiner = transform.parent.GetComponentInChildren<CinemachineConfiner>().m_BoundingShape2D;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        float xDirection = 0, yDirection = 0;
        if (Input.GetAxis("Horizontal") != 0)
        {
            xDirection = Input.GetAxis("Horizontal")  / Mathf.Abs(Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            yDirection = Input.GetAxis("Vertical")  / Mathf.Abs(Input.GetAxis("Vertical"));
        }
        if (xDirection == 0) 
        {
            if (Input.mousePosition.x > screenWidth - BOUNDARY) 
            {
                xDirection = 1;
            } else if (Input.mousePosition.x < 0 + BOUNDARY || Input.GetAxis("Horizontal") < 0) 
            {
                xDirection = -1;
            }
        }
        if (yDirection == 0)
        {
            if (Input.mousePosition.y > screenHeight - BOUNDARY || Input.GetAxis("Vertical") > 0) 
            {
                yDirection = 1;
            } else if (Input.mousePosition.y < 0 + BOUNDARY || Input.GetAxis("Vertical") < 0) 
            {
                yDirection = -1;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Max(virtualCamera.m_Lens.OrthographicSize - ZOOM_SPEED * Time.deltaTime, MAX_ZOOM);
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Min(virtualCamera.m_Lens.OrthographicSize + ZOOM_SPEED * Time.deltaTime, MIN_ZOOM);
        }

        if (xDirection != 0 || yDirection != 0) 
        {
            transform.Translate(new Vector2(xDirection * MOVE_SPEED * Time.deltaTime, yDirection * MOVE_SPEED * Time.deltaTime));

            float camHalfWidth = ((float) Screen.width / (float) Screen.height) * virtualCamera.m_Lens.OrthographicSize;
            Vector2 minBoundary = new Vector2(confiner.bounds.min.x + camHalfWidth, confiner.bounds.min.y + virtualCamera.m_Lens.OrthographicSize);
            Vector2 maxBoundary = new Vector2(confiner.bounds.max.x - camHalfWidth, confiner.bounds.max.y - virtualCamera.m_Lens.OrthographicSize);

            if (transform.position.x < minBoundary.x)
                transform.position = new Vector2(minBoundary.x, transform.position.y);
            else if (transform.position.x > maxBoundary.x)
                transform.position = new Vector2(maxBoundary.x, transform.position.y);
            if (transform.position.y < minBoundary.y)
                transform.position = new Vector2(transform.position.x, minBoundary.y);
            else if (transform.position.y > maxBoundary.y)
                transform.position = new Vector2(transform.position.x, maxBoundary.y);

        }
    }
}
