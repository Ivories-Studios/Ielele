using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private Slider _energySlider;
    [SerializeField] private Color[] _colors;
    [SerializeField] private ParticleSystem _particleSystem;

    void Awake()
    {
        _energySlider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChange()
    {
        var emission = _particleSystem.emission;
        var main = _particleSystem.main;
        if (_energySlider.value < 5)
        {
            _energySlider.fillRect.GetComponent<Image>().color = _colors[0];
            emission.rateOverTime = 100;
            main.startLifetime = 0.3f;
            main.simulationSpeed = 1;
        }
        else if (_energySlider.value < 10)
        {
            _energySlider.fillRect.GetComponent<Image>().color = _colors[1];
            emission.rateOverTime = 300;
            main.startLifetime = 0.4f;
            main.simulationSpeed = 1.2f;
        }
        else
        {
            _energySlider.fillRect.GetComponent<Image>().color = _colors[2];
            emission.rateOverTime = 600;
            main.startLifetime = 0.5f;
            main.simulationSpeed = 1.4f;
        }
    }
}
