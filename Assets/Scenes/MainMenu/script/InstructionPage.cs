using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstructionPage : MonoBehaviour
{
    public Button previous, next;

    Animator anim;
    int[] triggers;
    int current = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();

        triggers = new int[5]
        {
            Animator.StringToHash("Player Movement"), // -1398569765
            Animator.StringToHash("Tractor Beam"),    //  1527144663
            Animator.StringToHash("Level Objective"), //  851372120
            Animator.StringToHash("Fuel"),            // -1852849961
            Animator.StringToHash("Shield")           //  423614430
        };

        //for (int i = 0; i < triggers.Length; i++)
        //    Debug.Log(triggers[i]);
    }

    void OnEnable()
    {
        current = 0;
        rate = 0;
        anim.SetTrigger(triggers[current]);
    }

    public void Previous()
    {
        if (current > 0)
        {
            current--;
            anim.SetTrigger(triggers[current]);
        }
    }

    public void Next()
    {
        if (current < triggers.Length - 1)
        {
            current++;
            anim.SetTrigger(triggers[current]);
        }
    }

    // Slightly more precise axis check than just using GetAxis, with rate
    // Gouged straight from UI Input since only the instruction menu really needs the check

    const float DEAD_ZONE = 0.15f;
    float xPrev = 0;
    int rate = 0;

    void Update()
    {
        float xCurrent = UIInput.horizontalAxis;

        bool isPositiveX = //UIInput.isRightKey;
            UIInput.isRightKey ||
            !UIInput.isRightKey && (xCurrent >= xPrev && xCurrent > DEAD_ZONE);

        bool isNegativeX = //UIInput.isLeftKey;
            UIInput.isLeftKey ||
            !UIInput.isLeftKey && (xCurrent <= xPrev && xCurrent < -DEAD_ZONE);

        xPrev = xCurrent;

        if (isPositiveX)
        {
            if (rate % 20 == 0 && next.IsInteractable())
            {
                rate = 0;
                Next();
            }
            rate++;
        }
        else if (isNegativeX)
        {
            if (rate % 20 == 0 && previous.IsInteractable())
            {
                rate = 0;
                Previous();
            }
            rate++;
        }
        else rate = 0;
    }
}
