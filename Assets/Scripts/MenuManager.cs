using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public bool gameRunning = false;

    bool firstTime = true;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
        LeanTween.moveY(GetComponent<RectTransform>(), 0, 1.5f).setEase(LeanTweenType.easeOutBack);
    }

    public void StartGame()
    {
        LeanTween.moveY(GetComponent<RectTransform>(), 1000, 1.5f).setEase(LeanTweenType.easeInBack)
            .setOnComplete(() => 
            { 
                gameObject.SetActive(false);
                gameRunning = true;
            });
    }

    public void Quit()
    {
        Application.Quit();
    }
}
