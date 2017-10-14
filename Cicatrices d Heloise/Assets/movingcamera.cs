using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingcamera : MonoBehaviour
{


    public float ScrollEdge = 0.1f;

    public float PanSpeed = 10;
     
 

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



    }
}
    
