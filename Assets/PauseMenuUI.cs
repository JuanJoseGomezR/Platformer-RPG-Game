using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuUI : MonoBehaviour
{
    private bool _isOpen = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SaveGame()
    {
        SaveManager.Instance.SaveGame(0);
    }

    public void TogglePauseMenu()
    {
        _isOpen = !_isOpen;

        if (_isOpen)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ToggleSaveSlots()
    {
        // Not implemented yet
        return;
    }

    public void QuitGame()
    { 
        Application.Quit();
    }
}
