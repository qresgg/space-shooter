using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _liveImg;
    private int _scoreFrom = 0;
    // Start is called before the first frame update
    void Start()
    { 
    }
    // Update is called once per frame
    
    public void updateScore (int score)
    {
        _scoreText.text = "Score: " + score;
    }
    public void updateLives (int currentLive)
    {
        _liveImg.sprite = _livesSprites[currentLive];
    }
}
