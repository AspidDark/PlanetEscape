using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextDisplay : MonoBehaviour {

    public Text text;

		
	// Update is called once per frame
	void Update () {
        text.text = ClosestObject.instance.GetMinimunDistance().ToString();

    }
}
