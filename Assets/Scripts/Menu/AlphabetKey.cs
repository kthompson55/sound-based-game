using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class AlphabetKey : MonoBehaviour, KeyboardKey
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate { HandlePressEvent(); });
    }

    public void HandlePressEvent()
    {
        Transform parent = transform.parent;
        while(!parent.GetComponent<Canvas>())
        {
            parent = parent.transform.parent;
        }

        Canvas parentCanvas = parent.gameObject.GetComponent<Canvas>();
        InputField fieldToModify = null;
        for(int i = 0; i < parentCanvas.transform.childCount; i++)
        {
            InputField field = parentCanvas.transform.GetChild(i).GetComponent<InputField>();
            if(field)
            {
                fieldToModify = field;
                break;
            }
        }

        fieldToModify.text += gameObject.name;
    }
}
