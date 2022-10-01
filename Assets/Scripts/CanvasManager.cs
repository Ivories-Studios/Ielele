using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [SerializeField] List<Image> healthPoints = new List<Image>();
    [SerializeField] Slider energySlider;
    [SerializeField] Sprite fullHealthSprite;
    [SerializeField] Sprite halfHealthSprite;
    [SerializeField] Sprite emptyHealthSprite;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetHealth(5);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
