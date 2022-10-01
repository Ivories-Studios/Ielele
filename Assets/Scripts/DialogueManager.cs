using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] TextMeshProUGUI playerText;
    [SerializeField] TextMeshProUGUI enemyText;

    [SerializeField] Image enemyImage;

    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject enemyPanel;

    [SerializeField] TextMeshProUGUI enemyName;
    [SerializeField] TextMeshProUGUI instructions;

    delegate void Dialogue();
    Dialogue nextDialogue;

    public void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (playerText.maxVisibleCharacters < playerText.text.Length)
            {
                playerText.maxVisibleCharacters = playerText.text.Length;
            }
            else
            {
                if(nextDialogue != null)
                {
                    nextDialogue.Invoke();
                }
                else
                {
                    playerPanel.SetActive(false);
                    enemyPanel.SetActive(false);
                }
            }
        }
    }

    public void Dialogue1()
    {
        nextDialogue = Dialogue2;
        playerPanel.SetActive(true);
        playerText.text = "I am Alexander IV of Macedon. Son and heir of the migthy Alexander the Great. I shan't let some feeble Fairies have my most renown heirloom. <u><i>MY HEIRLOOM.</i></u>";
        StartCoroutine(ClackaClacka());
        StartCoroutine(Help());
    }

    void Dialogue2()
    {
        instructions.gameObject.SetActive(false);
        nextDialogue = Dialogue3;
        playerText.text = "It's just some fairies. My time in Romania, living between the noble shepherds, has brought much knowledge to me. No problem shall arise, I am sure of it.";
        StartCoroutine(ClackaClacka());
    }

    void Dialogue3()
    {
        nextDialogue = null;
        playerText.text = "Let's begin.";
        StartCoroutine(ClackaClacka());
    }

    public void Iala1()
    {

    }

    public void Iala2()
    {

    }

    public void Iala3()
    {

    }

    IEnumerator ClackaClacka()
    {
        playerText.maxVisibleCharacters = 0;
        while (playerText.maxVisibleCharacters < playerText.text.Length)
        {
            playerText.maxVisibleCharacters++;
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator Help()
    {
        yield return new WaitForSeconds(1);
        instructions.gameObject.SetActive(true);
    }
}
