using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }
    
    void Update()
    {
        image.color -= new Color(0, 0, 0, SceneManager.GetActiveScene().name.Equals("GameOver") ? Time.deltaTime / 10 : Time.deltaTime);
        if(image.color.a <= 0.05f) image.gameObject.SetActive(false);
    }
}
