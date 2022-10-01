using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpHP : MonoBehaviour
{
    [SerializeField] Slider sliderToCopy;
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        slider.maxValue = sliderToCopy.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value != sliderToCopy.value)
        {
            slider.value += (sliderToCopy.value - slider.value) * 2 * Time.deltaTime;
        }
    }
}
