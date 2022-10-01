using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveTest : MonoBehaviour
{
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            transform.position -=new Vector3( (Input.mousePosition.x - mousePos.x) / 10,0);
            mousePos = Input.mousePosition; 
        }
    }
}
