using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
        
    }

    public void SkipDialogue(InputAction.CallbackContext context)
    {
        if (context.performed)
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
        dialogueText.maxVisibleCharacters = 0;
        dialogueText.text = "Here you can fight an invincible and immobilized enemy to train yourself. You can exit by pressing the arrow in the upper right corner. Close the dialogue by pressing Enter.";
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
