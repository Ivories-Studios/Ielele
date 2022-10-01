using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public bool gameRunning = false;

    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject victoryScreen;

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
                if (firstTime) 
                {
                    DialogueManager.Instance.StartCoroutine(DelayDialogue());
                } 
            });
    }

    IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        DialogueManager.Instance.Dialogue1();
    }

    public void OpenOptionsMenu(bool open)
    {
        optionsMenu.SetActive(open);
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

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
