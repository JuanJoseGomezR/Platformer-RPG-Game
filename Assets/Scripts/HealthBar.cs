using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;

    [SerializeField]
    private Image _fillImage;

    [SerializeField]
    private PlayerHealth _playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();

        _slider.maxValue = _playerHealth.maxHealth;
        _playerHealth.OnHealthChanged += playerHealth_OnHealthChanged;
    }

    private void playerHealth_OnHealthChanged(float value)
    {
        _slider.value = value;
        if (_slider.value < 35f)
        { 
            _fillImage.color = Color.red;
        
        }
    }

    // Update is called once per frame
    void OnDestroy()
    {
        _playerHealth.OnHealthChanged -= playerHealth_OnHealthChanged;
    }
}
