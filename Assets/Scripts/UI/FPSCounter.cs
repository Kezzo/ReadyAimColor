using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class to handle the FPS Counter.
/// Only activated for debug purposes.
/// </summary>
public class FPSCounter : MonoBehaviour {

    [SerializeField]
    private Text m_fpsCounterText;

    private int m_framesPerSecond;

    void Start()
    {
        StartCoroutine(DisplayFPS());
    }

    private IEnumerator DisplayFPS()
    {
        while(true)
        {
            m_framesPerSecond = (int)(1.0f / Time.smoothDeltaTime);
            m_fpsCounterText.text = m_framesPerSecond + " FPS";

            yield return new WaitForSeconds(0.5f);
        }
    }
}
