using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWell : MonoBehaviour
{
    bool onlyOnce = true;
    public int index;

    public void Interact(PlayerObject player)
    {
        if (onlyOnce)
        {
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
