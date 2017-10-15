using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingcamera : MonoBehaviour
{


    public float ScrollEdge = 0.1f;

    public float PanSpeed = 10;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    private Vector2 initialPosition;



    void Start()
    {
        initialPosition = transform.position;



    }


    void Update()
    {

        {

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height * (1 - ScrollEdge))
            {
                transform.Translate(Vector2.up * Time.deltaTime * PanSpeed, Space.Self);
            }
            else if (Input.GetKey("s") || Input.mousePosition.y <= Screen.height * ScrollEdge)
            {
                transform.Translate(Vector2.up * Time.deltaTime * -PanSpeed, Space.Self);
            }

            if (Input.GetKey("q") || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge))
            {
                transform.Translate(Vector2.right * Time.deltaTime * PanSpeed, Space.Self);
            }
            else if (Input.GetKey("d") || Input.mousePosition.x <= Screen.width * ScrollEdge)
            {
                transform.Translate(Vector2.right * Time.deltaTime * -PanSpeed, Space.Self);
            }


        }

        if (bounds)

        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
              Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
              Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
            }


    }
}
    
