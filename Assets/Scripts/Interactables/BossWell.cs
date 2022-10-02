using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWell : MonoBehaviour
{
    bool onlyOnce = true;
    public int index;
    public GameObject interactText;
    [SerializeField] List<GameObject> gameObjects;

    public void Interact(PlayerObject player)
    {
        interactText.SetActive(false);
        if (onlyOnce)
        {
            foreach(GameObject go in gameObjects)
            {
                go.SetActive(true);
            }
            switch (index)
            {
                case 1:
                    DialogueManager.Instance.Iala1();
                    break;
                case 2:
                    DialogueManager.Instance.Iala2();
                    break;
                case 3:
                    DialogueManager.Instance.Iala3();
                    break;
            }
            onlyOnce = false;
        }
    }
}
