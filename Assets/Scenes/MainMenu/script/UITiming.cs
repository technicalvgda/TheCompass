using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UITiming : MonoBehaviour
{
    // WaitForSeconds that works under timeScale 0
    // http://blog.projectmw.net/wait-for-real-seconds-class-using-static-function-in-unity
    // http://answers.unity3d.com/questions/7544/how-do-i-pause-my-game.html

    public static IEnumerator WaitForRealSeconds(float seconds)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + seconds)
        {
            yield return null;
        }
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
    }
}
