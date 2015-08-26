using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

    [SerializeField]
    private Text m_fpsCounterText;

    private int m_framesPerSecond;

    void Update()
    {
        m_framesPerSecond = (int) (1.0f / Time.smoothDeltaTime);
        m_fpsCounterText.text = m_framesPerSecond + " FPS";
    }
}
