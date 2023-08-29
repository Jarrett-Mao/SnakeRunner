using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text ScoreText;
    public Text MenuScoreText;
    public Text BestScoreText;
    public static int Score;
    public static int HighScore;

    //bool
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

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Tag")){
            Destroy(g);
        }
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("SimpleBox")){
            Destroy(g);
        }
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Snake")){
            Destroy(g);
        }

        
        SM.SpawnBodyPart();
        
        // BM.SetPreviousPosAfterGameover();

        speedAdded = false;
        SM.speed = 3;

        PlayerPrefs.SetInt("HighScore", HighScore);
        // BM.SimpleBoxPosition.Clear();
    }

    public void EnableCG(CanvasGroup cg){
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;
    }

    public void DisableCG(CanvasGroup cg){
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }

}


