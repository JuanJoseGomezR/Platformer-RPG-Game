using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Transform _settingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        _settingsPanel.gameObject.SetActive(false);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleSettingsPanel()
    {
        _settingsPanel.gameObject.SetActive(!_settingsPanel.gameObject.activeSelf);
    }
}
