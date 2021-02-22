﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{

    public static SceneManagerScript Instance { get; private set; }

    public float curGameScore;

    private ScoreBase score;
    private List<ScoreBase> TopScores = new List<ScoreBase>();
    private int TopScoresCount = 3;

    void Awake() {
        if (Instance != null)
            throw new System.Exception("More than one singleton exists in the scene!");

        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit() {
        //need to check after publish to web
        Application.Quit();
    }

    public void StartGame(InputField playerNameField) {
        //Debug.Log(playerNameField.text);
        //ScoreManager.Instance.score.Name = playerNameField.text;
        score = new ScoreBase();
        score.Name = playerNameField.text;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("EndlessRunner");
    }

    public void setPlayerScore(float curScore) {
        score.Score = curScore;
        score.Time = System.DateTime.Now;

        Debug.Log(curScore);

        UpdateTopScores();
    }

    void UpdateTopScores() {
        
        if(TopScores.Count < TopScoresCount){
            TopScores.Add(score);
        }
        else{
            var LowestScore = TopScores.Min(s => s.Score);
            if(score.Score > LowestScore) {
                TopScores.Add(score);
            }
        }

        string topScoresJson = JsonUtility.ToJson(TopScores);
        PlayerPrefs.SetString("TopScores", topScoresJson);
        
    }

    public void GameOver() {
        setPlayerScore(curGameScore);
        SceneManager.LoadScene("GameOver");
    }
}
