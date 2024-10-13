using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    public void HideMenu()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#endif
    }
}
