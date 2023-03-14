using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject loseUI; //bu paneli açıp kapatmak için bir obje oluşturmamız gerekiyor
    public int score;
    public Text losescoreText,winscoreText;
    public Text InGameScoreText;
    public GameObject winUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LevelEnd() {
        loseUI.SetActive(true);
        losescoreText.text = "Toplam Puan:  " + score;
        InGameScoreText.gameObject.SetActive(false);
    }
    public void WinLevel() {
        winUI.SetActive(true);
        winscoreText.text = "Toplam Puan:  " + score;
        InGameScoreText.gameObject.SetActive(false);
    }
    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void AddScore(int pointValue) {
        score += pointValue;
        //scoreText.text = "Toplam Puan:  " + score;
        InGameScoreText.text = "Puan:  " + score;
    }
    public void StartApp() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //aktif sahneyi tekrar yükletmek için kullandığımız kalıp
    }
    public void AppQuit() {
        Application.Quit();
    }
}
