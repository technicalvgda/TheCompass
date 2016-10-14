using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour {
    public TwineTest tt;
    public void Clicked()
    {
        tt.ChoiceSelect(GetComponentInChildren<Text>().text);
    }
}
