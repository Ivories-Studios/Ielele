using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
        LeanTween.moveY(GetComponent<RectTransform>(), 0, 2).setEase(LeanTweenType.easeOutBack);
    }

    public void StartGame()
    {
        gameRunning = true;
        LeanTween.moveY(GetComponent<RectTransform>(), 600, 2).setEase(LeanTweenType.easeInBack)
            .setOnComplete(() => 
            { 
                gameObject.SetActive(false);
                if (firstTime) 
                {
                    DialogueManager.Instance.StartCoroutine(DelayDialogue());
                } 
            });
    }

    IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(1);
        DialogueManager.Instance.Dialogue1();
    }

    public void OpenOptionsMenu(bool open)
    {
        optionsMenu.SetActive(open);
    }

    public void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
        LeanTween.moveX(victoryScreen.GetComponent<RectTransform>(), 0, 3).setDelay(1).setEase(LeanTweenType.easeOutBounce)
            .setOnComplete(() =>
            {
                LeanTween.moveX(victoryScreen.GetComponent<RectTransform>(), 1200, 3).setDelay(2).setEase(LeanTweenType.easeInBack).setOnComplete(() => victoryScreen.SetActive(false));
            });
    }

    public void Quit()
    {
        Application.Quit();
    }
}
