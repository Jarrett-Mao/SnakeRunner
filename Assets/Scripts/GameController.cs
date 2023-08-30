using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public enum GameState{MENU, GAME, GAMEOVER}
    public static GameState gameState;

    //Managers
    public SnakeMovement SM;
    public BlockManager BM;

    //canvas group
    public CanvasGroup MENU_CG;
    public CanvasGroup GAME_CG;
    public CanvasGroup GAMEOVER_CG;
    
    //score management
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI MenuScoreText;
    public TextMeshProUGUI BestScoreText;
    public static int Score;
    public static int HighScore;

    //bools
    bool speedAdded;
    void Start()
    {
        SetMenu();
        Score = 0;

        speedAdded = false;
        HighScore = PlayerPrefs.GetInt("HighScore");
    }

    void Update()
    {
        ScoreText.text = Score + "";
        MenuScoreText.text = Score + "";

        if (Score > HighScore){
            HighScore = Score;

            BestScoreText.text = Score + "";

            if(!speedAdded && Score > 150){
                SM.speed++;
                speedAdded = true;
            }
        }
    }

    public void SetGame()
    {
        //Set the GameState
        gameState = GameState.GAME;

        //Manage Canvas Groups
        EnableCG(GAME_CG);
        DisableCG(MENU_CG);
        DisableCG(GAMEOVER_CG);

        //Reset score
        Score = 0;
    }

    public void SetMenu(){
        
        gameState = GameState.MENU;

        EnableCG(MENU_CG);
        DisableCG(GAME_CG);
        DisableCG(GAMEOVER_CG);
    }

    public void SetGameOver(){
        gameState = GameState.GAMEOVER;

        EnableCG(MENU_CG);
        DisableCG(GAME_CG);
        DisableCG(GAMEOVER_CG);

        //clear all objects

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Box")){
            Destroy(g);
        }

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Bar")){
            Destroy(g);
        }
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("SimpleBox")){
            Destroy(g);
        }
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Snake")){
            Destroy(g);
        }

        
        SM.SpawnBodyPart();
        
        BM.SetPreviousPosAfterGameover();

        speedAdded = false;
        SM.speed = 3;

        PlayerPrefs.SetInt("HighScore", HighScore);
        BM.SimpleBoxPositions.Clear();
    }

    //enables menu components
    public void EnableCG(CanvasGroup cg){
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;
    }

    //disables menu components
    public void DisableCG(CanvasGroup cg){
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }

}


