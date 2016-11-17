using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class MenuBack : MonoBehaviour
{
    public UIMenu current;
    public string exit, enter;
    int exitHash, enterHash;
    Button btn;

    void Start()
    {
        exitHash = Animator.StringToHash(exit);
        enterHash = Animator.StringToHash(enter);
        btn = GetComponent<Button>();

        btn.onClick.AddListener(
            () => current.Back(exitHash, enterHash));
    }
}
