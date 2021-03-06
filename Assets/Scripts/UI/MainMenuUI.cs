﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the data display in the MainMenu UI.
/// </summary>
public class MainMenuUI : MonoBehaviour {

	[SerializeField]
    private Text m_lastHighScoreText;

    [SerializeField]
    private Text m_bestHighScoreText;

    void Start()
    {
        m_lastHighScoreText.text = PlayerPrefs.GetInt("LastHighScore").ToString();
        m_bestHighScoreText.text = PlayerPrefs.GetInt("BestHighScore").ToString();
    }

}
