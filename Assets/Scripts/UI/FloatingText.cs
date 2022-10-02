using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{

    static GameObject comboPrefabObject;

    static float lastGlowTime = 0;
    static float lastUpdate = 0;
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
        lastUpdate = Time.time;

        GameObject obj;
        if (comboPrefabObject != null && Time.time - lastUpdate > 0.5f)
        {
            Destroy(comboPrefabObject);
        }
        obj = comboPrefabObject;
        if (comboPrefabObject == null)
        {
            obj = Instantiate(comboPrefab);
        }
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
}
