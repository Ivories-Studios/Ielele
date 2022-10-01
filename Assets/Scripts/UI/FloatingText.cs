using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{

    static GameObject comboPrefabObject;

    static float lastGlowTime = 0;
    static GameObject comboPrefab
    {
        get
        {
            return Resources.Load<GameObject>("FloatingText/ComboFloatingText");
        }
    }
    public static GameObject CreateFloatingTextCombo(int combo, Vector2 pos)
    {
        if(Time.time - lastGlowTime < 0.5)
        {
            return comboPrefabObject;
        }

        GameObject obj;
        if (comboPrefabObject != null)
        {
            Destroy(comboPrefabObject);
        }
        obj = Instantiate(comboPrefab);
        obj.gameObject.transform.position = pos;
        obj.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
            = "<size=75%> COMBO </size> <size= " + (120+(combo/25.0f)) + "%>" + combo + "</size>";
        Destroy(obj, 3.3f);

        if(combo%10 == 0)
        {
            obj.GetComponent<Animator>().SetTrigger("Glow");
            lastGlowTime = Time.time;
        }

        comboPrefabObject = obj;

        return obj;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
