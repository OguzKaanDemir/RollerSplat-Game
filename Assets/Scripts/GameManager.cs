using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Grounds, Levels;
    [SerializeField] GameObject StartScene, WinScene;
    public float _groundNumbers;
    public bool isWon;
    public int levelIndex, a;
    BallController ball;

    private void Awake()
    {
        ball = FindObjectOfType<BallController>();
    }

    void Update()
    {
        _groundNumbers = Grounds.Length;
    }

    public void StartGameScene()
    {
        StartScene.SetActive(false);
        Levels[levelIndex].SetActive(true);
        FindGrounds();
        LevelBarTexts();
    }

    public void CheckWin()
    {
        if (isWon && a == 0)
        {
            WinScene.transform.GetChild(1).GetComponent<TMP_Text>().text = $"LEVEL {levelIndex + 1} COMPLATED!";
            LevelBarTexts();
            levelIndex++;
            isWon = true;
            WinScene.SetActive(true);
            a++;
            Time.timeScale = 0;
        }
    }

    public void NextLevel()
    {
        Levels[levelIndex-1].SetActive(false);
        Levels[levelIndex].SetActive(true);
        ball.gameObject.transform.position = new Vector3(0, .3f, 0);
        FindGrounds();
        ball.LevelBar.fillAmount = 0;
        isWon = false;
        a = 0;
        ball.a = 0;
        Time.timeScale = 1;
        WinScene.SetActive(false);
        ball.canSwipe = true;
    }

    void FindGrounds()
    {
        Grounds = GameObject.FindGameObjectsWithTag("Ground");
    }

    void LevelBarTexts()
    {
        ball.LevelBar.transform.parent.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = (levelIndex + 1).ToString();
        ball.LevelBar.transform.parent.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = (levelIndex + 2).ToString();
    }
}
