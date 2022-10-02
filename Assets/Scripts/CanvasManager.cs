using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [SerializeField] List<Image> healthPoints = new List<Image>();
    [SerializeField] Slider energySlider;
    [SerializeField] Sprite fullHealthSprite;
    [SerializeField] Sprite halfHealthSprite;
    [SerializeField] Sprite emptyHealthSprite;
    [SerializeField] MenuManager menuManager;
    [SerializeField] ParticleSystem energyParticleSystem;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuManager?.ShowMenu();
        }
    }

    public void SetHealth(int amount)
    {
        for(int i = 0; i < healthPoints.Count; i++)
        {
            if (i < amount / 2)
            {
                healthPoints[i].sprite = fullHealthSprite;
            }
            else
            {
                if (i - 1 < amount / 2 && amount % 2 == 1)
                {
                    healthPoints[i].sprite = halfHealthSprite;
                }
                else
                {
                    healthPoints[i].sprite = emptyHealthSprite;
                }
            }
        }
    }

    public void SetEnergy(int amount)
    {
        energySlider.value = amount;
        var emission = energyParticleSystem.emission;
        emission.rateOverTime = amount * 40;
    }

    public void SwitchScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
