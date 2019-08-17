using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(TextMesh))]
public class LocalizedText : MonoBehaviour
{

    public string key;

    // Use this for initialization
    void Start()
    {
        //set to text mesh pro!!!
        Text text = GetComponent<Text>();
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }

}