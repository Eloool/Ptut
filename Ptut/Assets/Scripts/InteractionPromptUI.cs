using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera _maincamera;
    public GameObject _uiPanel;
    public TextMeshProUGUI _promptText;

    private void Start()
    {
        _maincamera = Camera.main;
        _uiPanel.SetActive(false);
    }

    private void LateUpdate()
    {
        var rotation = _maincamera.transform.rotation;
        _uiPanel.transform.LookAt(_maincamera.transform);
        //_uiPanel.transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public bool IsDisplayed = false;

    public void SetUp(string prompt)
    {
        _promptText.text = prompt;
        _uiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close()
    {
        IsDisplayed = false;
        if (_uiPanel) _uiPanel.SetActive(false);
    }
}
