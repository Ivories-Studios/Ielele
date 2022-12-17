using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [SerializeField] List<Image> healthPoints = new List<Image>();
    [SerializeField] Slider energySlider;
    [SerializeField] Sprite fullHealthSprite;
    [SerializeField] Sprite halfHealthSprite;
    [SerializeField] Sprite emptyHealthSprite;
    [SerializeField] MenuManager menuManager;
    [SerializeField] GameObject finish;
    [SerializeField] GameObject victoryScreen;

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
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            menuManager?.ShowMenu();
        }
    }

    public void FinishGame()
    {
        StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(5f);
        finish.SetActive(true);
    }

    public void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
        LeanTween.moveX(victoryScreen.GetComponent<RectTransform>(), 0, 2).setDelay(1).setEase(LeanTweenType.easeOutBounce)
            .setOnComplete(() =>
            {
                LeanTween.moveX(victoryScreen.GetComponent<RectTransform>(), 2000, 2).setDelay(2).setEase(LeanTweenType.easeInBack)
                .setOnComplete(() => { victoryScreen.SetActive(false); victoryScreen.GetComponent<RectTransform>().position = new Vector2(-2000, 0); });
            });
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
