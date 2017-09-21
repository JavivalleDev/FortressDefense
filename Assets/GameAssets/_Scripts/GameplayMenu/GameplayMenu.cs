using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMenu : MonoBehaviour
{

    [SerializeField] private Canvas _interfaceCanvas;
    [SerializeField] private Canvas _menuCanvas;

    private bool _showing = true;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_showing) _interfaceCanvas.enabled = _menuCanvas.enabled;
            _menuCanvas.enabled = !_menuCanvas.enabled;
            Time.timeScale = _menuCanvas.enabled ? 0 : 1;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!_menuCanvas.enabled)
            {
                _interfaceCanvas.enabled = !_interfaceCanvas.enabled;
                _showing = _interfaceCanvas.enabled;
            }
        }
    }
}
