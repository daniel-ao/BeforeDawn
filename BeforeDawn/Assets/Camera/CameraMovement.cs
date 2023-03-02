using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float speed = 10f;
    public float scrollspeed = 1000f;

    private float BoarderThickness = 10.0f;

    public float scrollmin = 0f;
    public float scrollmax = 20f;
    private int fps = 30;

    public Vector2 limit;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = fps;
    }

    // Update is called once per frame 
    void Update()
    {
        Vector3 position = transform.position;
        if (Input.GetKey("z") ||Input.mousePosition.y >= Screen.height - BoarderThickness)
        {
            position.z += speed * Time.deltaTime;
        }
        if ( Input.GetKey("s") ||Input.mousePosition.y <= BoarderThickness)
        {
            position.z -= speed * Time.deltaTime;
        }
        if ( Input.GetKey("d") ||Input.mousePosition.x >= Screen.width - BoarderThickness)
        {
            position.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("q") || Input.mousePosition.x <= BoarderThickness)
        {
            position.x -= speed * Time.deltaTime;
        }
        //clamp allows us to limit a number
        position.x = Mathf.Clamp(position.x, -limit.x, limit.x);
        position.y = Mathf.Clamp(position.y, scrollmin, scrollmax);
        position.z = Mathf.Clamp(position.z, -limit.y, limit.y);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        position.y -= scrollspeed * scroll * Time.deltaTime;
        transform.position = position;
    }
}
