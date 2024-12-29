using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardForGUI : MonoBehaviour
{
    public GameObject statsPanel;
    [Tooltip("Links between keys and UI elements (hide/show if pressed/unpressed)")]
    public List<KeyLinking> keyboardConnecting;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var keyLinking in keyboardConnecting)
        {
            if (!keyLinking.isStayIfUnpressed)
            {
                keyLinking.obj.SetActive(Input.GetKey(keyLinking.code));
            }
            else
            {
                keyLinking.obj.SetActive(true);
            }
            print(!keyLinking.isStayIfUnpressed);
        }

        Cursor.visible = Input.GetKey(KeyCode.LeftAlt);
    }
}

[Serializable]
public struct KeyLinking
{
    public KeyCode code;
    public GameObject obj;
    public bool isStayIfUnpressed;
}