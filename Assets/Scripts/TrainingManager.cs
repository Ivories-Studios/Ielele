using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingManager : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;


    private void Start()
    {
        Dialogue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (dialogueText.maxVisibleCharacters < dialogueText.text.Length)
            {
                dialogueText.maxVisibleCharacters = dialogueText.text.Length;
            }
            else
            {
                dialoguePanel.SetActive(false);
            }
        }
    }

    void Dialogue()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = "Here you can fight an invincible and immobilized enemy to train yourself. You can exit by pressing the button in the upper right corner.";
        StartCoroutine(ClackaClacka());
    }

    IEnumerator ClackaClacka()
    {
        yield return new WaitForSeconds(1f);
        dialogueText.maxVisibleCharacters = 0;
        while (dialogueText.maxVisibleCharacters < dialogueText.text.Length)
        {
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
