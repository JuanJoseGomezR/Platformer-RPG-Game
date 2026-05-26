using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _titleText;

    [SerializeField]
    private TextMeshProUGUI _descriptionText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position = Input.mousePosition + new Vector3(90f, -90f, 10f);
    }

    public void ShowTooltip(ItemDataSO itemDataSO)
    { 
        _titleText.text = itemDataSO.name;
        _descriptionText.text = itemDataSO.description;

        gameObject.SetActive(true);
    }

    public void HideTooltip() 
    {
        gameObject.SetActive(false);
    }
}
