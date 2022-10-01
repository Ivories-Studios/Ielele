using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    int counter = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            counter++;
            FloatingText.CreateFloatingTextCombo(counter, Random.insideUnitCircle);
        }
    }
}
