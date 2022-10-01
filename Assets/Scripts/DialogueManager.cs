using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public bool isInDialogue;

    [SerializeField] List<Encounter> bossEncounters = new List<Encounter>();

    [SerializeField] TextMeshProUGUI playerText;
    [SerializeField] TextMeshProUGUI enemyText;

    [SerializeField] Image enemyImage;

    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject enemyPanel;

    [SerializeField] TextMeshProUGUI enemyName;
    [SerializeField] TextMeshProUGUI instructions;

    [SerializeField] Sprite iala1;
    [SerializeField] Sprite iala2;
    [SerializeField] Sprite iala3;

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
            if (playerText.maxVisibleCharacters < playerText.text.Length || enemyText.maxVisibleCharacters < enemyText.text.Length)
            {
                playerText.maxVisibleCharacters = playerText.text.Length;
                enemyText.maxVisibleCharacters = enemyText.text.Length;
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
                    isInDialogue = false;
                }
            }
        }
    }

    Coroutine help;

    public void Dialogue1()
    {
        isInDialogue = true;
        nextDialogue = Dialogue2;
        playerPanel.SetActive(true);
        playerText.text = "I am Alexander IV of Macedon. Son and heir of the migthy Alexander the Great. I shan't let some feeble Fairies have my most renown heirloom. <u><i>MY HEIRLOOM.</i></u>";
        StartCoroutine(ClackaClacka(playerText));
        help = StartCoroutine(Help());
    }

    void Dialogue2()
    {
        StopCoroutine(help);
        instructions.gameObject.SetActive(false);
        nextDialogue = Dialogue3;
        playerText.text = "It's just some fairies. My time in Romania, living between the noble shepherds, has brought much knowledge to me. No problem shall arise, I am sure of it.";
        StartCoroutine(ClackaClacka(playerText));
    }

    void Dialogue3()
    {
        nextDialogue = null;
        playerText.text = "<i>This</i> is the start of my adventure.";
        StartCoroutine(ClackaClacka(playerText));
    }

    public void Iala1()
    {
        isInDialogue = true;
        playerPanel.SetActive(true);
        nextDialogue = Iala1_1;
        playerText.text = "My first roadblock. It seems like the Fairies had to send one of them for me. They can see the eyes of my father roaring in me.";
        StartCoroutine(ClackaClacka(playerText));
    }

    void Iala1_1()
    {
        enemyPanel.SetActive(true);
        nextDialogue = Iala1_2;
        enemyName.text = "The First Fairy";
        enemyImage.sprite = iala1;
        enemyText.text = "What a small little man. It'll be a joy to toy with you. I presume I should let our dance begin, shouldn't I. I perosnaly look very forward to it.";
        StartCoroutine(ClackaClacka(enemyText));
    }

    void Iala1_2()
    {
        nextDialogue = Iala1_3;
        playerText.text = "I am in no frame of mind for your supposed games. Give me the water I deserve back!";
        StartCoroutine(ClackaClacka(playerText));
    }

    void Iala1_3()
    {
        nextDialogue = Iala1_4;
        enemyText.text = "Awww. The little son is angry. I want to see what trickeries you pretend to hide.";
        StartCoroutine(ClackaClacka(enemyText));
    }

    void Iala1_4()
    {
        bossEncounters[0].StartFight();
        nextDialogue = null;
        playerPanel.SetActive(false);
        enemyPanel.SetActive(false);
        isInDialogue = false;
    }

    public void Iala2()
    {
        nextDialogue = Iala2_1;
        enemyPanel.SetActive(true);
        enemyName.text = "The Second Fairy";
        enemyImage.sprite = iala2;
        enemyText.text = "The other one did not manage to get rid of this vermin for us. Good for me that I am a professional. He will succumb to my charms and perish.";
        StartCoroutine(ClackaClacka(enemyText));
    }

    void Iala2_1()
    {
        nextDialogue = Iala2_2;
        enemyText.text = "<b>I assure you of it!!!</b>";
        StartCoroutine(ClackaClacka(enemyText));
    }

    void Iala2_2()
    {
        nextDialogue = Iala2_3;
        enemyName.text = "The Third Fairy";
        enemyImage.sprite = iala3;
        enemyText.text = "Give me his limbs. I am only interested in his limbs. He will be made lame.";
        StartCoroutine(ClackaClacka(enemyText));
    }

    void Iala2_3()
    {
        nextDialogue = Iala2_4;
        enemyName.text = "The Second Fairy";
        enemyImage.sprite = iala2;
        enemyText.text = "The all loving fairy. You can never behave in a proper manner in front of a guest.";
        StartCoroutine(ClackaClacka(enemyText));
    }

    void Iala2_4()
    {
        nextDialogue = Iala2_5;
        playerText.text = "If it holds any importance to you, I hold no sympathy for any of you. For my point of view, you are all witches, no matter how much you detest that word.";
        StartCoroutine(ClackaClacka(playerText));
    }

    void Iala2_5()
    {
        nextDialogue = null;
        playerText.text = "There is only one way to proceed.";
        StartCoroutine(ClackaClacka(playerText));
    }

    public void Iala3()
    {

    }

    IEnumerator ClackaClacka(TextMeshProUGUI text)
    {
        text.maxVisibleCharacters = 0;
        while (text.maxVisibleCharacters < text.text.Length)
        {
            text.maxVisibleCharacters++;
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator Help()
    {
        yield return new WaitForSeconds(1.5f);
        instructions.gameObject.SetActive(true);
    }
}
