using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    private int score;
    private void Start() {
        NewGame();
    }

    public void NewGame() {
        SetScore(0);
        bestScoreText.text = LoadBestScore().ToString();
        gameOver.alpha = 0f;
        gameOver.interactable = false;
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver() {
        board.enabled = false;
        gameOver.interactable = true;
        StartCoroutine(Fade(gameOver, 1f, 1f));
    }
    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay) {
        yield return new WaitForSeconds(delay);
        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;
        while (elapsed < duration) {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
    }
    public void IncreaseScore(int points) {
        SetScore(score + points);
    }
    private void SetScore(int score) {
        this.score = score;
        scoreText.text = score.ToString();
        SaveBestScore();
    }

    private void SaveBestScore() {
        int bestScore = LoadBestScore();
        if (score > bestScore) {
            PlayerPrefs.SetInt("bestScore", score);
        }
    }
    private int LoadBestScore() {
        return PlayerPrefs.GetInt("bestScore", 0);
    }
}
