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
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _restartKeyText;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    { 
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    
    public void updateScore (int score)
    {
        _scoreText.text = "Score: " + score;
    }
    public void updateLives (int currentLive)
    {
        _liveImg.sprite = _livesSprites[currentLive];
        if(currentLive == 0) gameOver();
    }
    void gameOver(){
        _gameOverText.gameObject.SetActive(true);
        _restartKeyText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _gameManager.GameOver();
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
