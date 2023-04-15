using DG.Tweening;
using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum Choice
{
    Scissors,
    Paper,
    Rock
}

public enum Owner
{
    Player,
    Enemy
}

public class GameMan : MonoBehaviour
{
    public GameObject scissorsPlayerPrefab;
    public GameObject paperPlayerPrefab;
    public GameObject rockPlayerPrefab;
    public GameObject scissorsEnemyPrefab;
    public GameObject paperEnemyPrefab;
    public GameObject rockEnemyPrefab;
    public GameObject restbut;
    public GameObject mmbut;
    public Button Rockbut;
    public Button Paperbut;
    public Button Scissorbut;
    public int playerScore;
    public int enemyScore;
    public GameObject Block;
    public AudioSource QD;
    public AudioSource Draw;
    public AudioSource Backsound;
    public AudioClip[] win;
    public AudioClip[] lose;
    public AudioSource wins;
    public AudioSource loss;
    public AudioSource REALWIN;

    public Transform playerChoicePosition;
    public Transform enemyChoicePosition;

    public TMP_Text resultText;
    public TMP_Text pscore;
    public TMP_Text escore;

    private Choice playerChoice;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (enemyScore >= 5 || playerScore >= 5)
        {
            resultText.text = "Quandale Says GG!";
            restbut.SetActive(true);
            Rockbut.enabled = false;
            Paperbut.enabled = false;
            Scissorbut.enabled = false;
        }
        else
        {
            restbut.SetActive(false);
            Rockbut.enabled = true;
            Paperbut.enabled = true;
            Scissorbut.enabled = true;
        }
    }


    private GameObject InstantiateChoice(Choice choice, Owner owner)
    {
        GameObject prefab = null;
        switch (owner)
        {
            case Owner.Player:
                switch (choice)
                {
                    case Choice.Scissors:
                        prefab = scissorsPlayerPrefab;
                        break;
                    case Choice.Paper:
                        prefab = paperPlayerPrefab;
                        break;
                    case Choice.Rock:
                        prefab = rockPlayerPrefab;
                        break;
                }
                break;
            case Owner.Enemy:
                switch (choice)
                {
                    case Choice.Scissors:
                        prefab = scissorsEnemyPrefab;
                        break;
                    case Choice.Paper:
                        prefab = paperEnemyPrefab;
                        break;
                    case Choice.Rock:
                        prefab = rockEnemyPrefab;
                        break;
                }
                break;
        }

        return Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    private IEnumerator StartRound()
    {
        Block.SetActive(true);
        yield return new WaitForSeconds(1f);
        resultText.text = "3";
        yield return new WaitForSeconds(1f);
        resultText.text = "2";
        yield return new WaitForSeconds(1f);
        resultText.text = "1";
        yield return new WaitForSeconds(1f);
        Block.SetActive(false);
    }

    private void DetermineWinner()
    {
        Choice enemyChoice = (Choice)Random.Range(0, 3);

        StartCoroutine(StartRound());

        GameObject enemyChoiceObject = InstantiateChoice(enemyChoice, Owner.Enemy);
        enemyChoiceObject.transform.position = new Vector3(10, enemyChoicePosition.position.y, 0);
        enemyChoiceObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        enemyChoiceObject.transform.localScale = new Vector3(-1, 1, 1);

        GameObject playerChoiceObject = InstantiateChoice(playerChoice, Owner.Player);
        playerChoiceObject.transform.position = new Vector3(-10, playerChoicePosition.position.y, 0);

        float distance = Mathf.Abs(playerChoicePosition.position.x - enemyChoicePosition.position.x);
        playerChoiceObject.transform.DOMoveX(enemyChoicePosition.position.x, 0.5f * distance / 10f);
        enemyChoiceObject.transform.DOMoveX(playerChoicePosition.position.x, 0.5f * distance / 10f).OnComplete(() =>
        {
            if (playerChoice == enemyChoice)
            {
                resultText.text = "Same Energy";
                Destroy(playerChoiceObject, 0.6f);
                Destroy(enemyChoiceObject, 0.6f);
                Draw.Play();
            }
            else if (playerChoice != enemyChoice)
            {
                if (playerChoice == Choice.Paper)
                {
                    if (enemyChoice == Choice.Scissors)
                    {
                        resultText.text = "Points For Quandale";
                        playerChoiceObject.transform.DOMoveY(playerChoiceObject.transform.position.y - 2f, 0.5f);
                        playerChoiceObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(100f, 100f), ForceMode2D.Force);
                        Destroy(enemyChoiceObject, 0.6f);
                        Destroy(playerChoiceObject, 1.5f);
                        enemyScore++;
                        loss.clip = lose[Random.Range(0, lose.Length)];
                        loss.Play();
                    }
                    else
                    {
                        resultText.text = "Nice";
                        enemyChoiceObject.transform.DOMoveY(enemyChoiceObject.transform.position.y - 2f, 0.5f);
                        enemyChoiceObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100f, 100f), ForceMode2D.Force);
                        Destroy(playerChoiceObject, 0.6f);
                        Destroy(enemyChoiceObject, 1.5f);
                        playerScore++;
                        wins.clip = win[Random.Range(0, win.Length)];
                        wins.Play();
                    }
                }
                else if (playerChoice == Choice.Scissors)
                {
                    if (enemyChoice == Choice.Rock)
                    {
                        resultText.text = "Points For Quandale";
                        playerChoiceObject.transform.DOMoveY(playerChoiceObject.transform.position.y - 2f, 0.5f);
                        playerChoiceObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(100f, 100f), ForceMode2D.Force);
                        Destroy(enemyChoiceObject, 0.6f);
                        Destroy(playerChoiceObject, 1.5f);
                        enemyScore++;
                        loss.clip = lose[Random.Range(0, lose.Length)];
                        loss.Play();
                    }
                    else
                    {
                        resultText.text = "Good";
                        enemyChoiceObject.transform.DOMoveY(enemyChoiceObject.transform.position.y - 2f, 0.5f);
                        enemyChoiceObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100f, 100f), ForceMode2D.Force);
                        Destroy(playerChoiceObject, 0.6f);
                        Destroy(enemyChoiceObject, 1.5f);
                        playerScore++;
                        wins.clip = win[Random.Range(0, win.Length)];
                        wins.Play();
                    }
                }
                else
                {
                    if (enemyChoice == Choice.Paper)
                    {
                        resultText.text = "Points For Quandale";
                        playerChoiceObject.transform.DOMoveY(playerChoiceObject.transform.position.y - 2f, 0.5f);
                        playerChoiceObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(100f, 100f), ForceMode2D.Force);
                        Destroy(enemyChoiceObject, 0.6f);
                        Destroy(playerChoiceObject, 1.5f);
                        enemyScore++;
                        loss.clip = lose[Random.Range(0, lose.Length)];
                        loss.Play();
                    }
                    else
                    {
                        resultText.text = "Keep it up";
                        enemyChoiceObject.transform.DOMoveY(enemyChoiceObject.transform.position.y - 2f, 0.5f);
                        enemyChoiceObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100f, 100f), ForceMode2D.Force);
                        Destroy(playerChoiceObject, 0.6f);
                        Destroy(enemyChoiceObject, 1.5f);
                        playerScore++;
                        wins.clip = win[Random.Range(0, win.Length)];
                        wins.Play();
                    }
                }
            }
            pscore.text = playerScore.ToString();
            escore.text = enemyScore.ToString();
            StartCoroutine(ResetRound());
        });
    }

    private IEnumerator ResetRound()
    {
        if (playerScore == 5)
        {
            Backsound.Stop();
            REALWIN.Play();
        }
        foreach (Transform child in playerChoicePosition)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in enemyChoicePosition)
        {
            Destroy(child.gameObject);
        }
        yield return new WaitForSeconds(3f);
        resultText.text = "Pick you poison";
    }

    public void OnScissorsButton()
    {
        playerChoice = Choice.Scissors;
        if(playerScore <= 5 || enemyScore <= 5)
        {
            DetermineWinner();
        }
    }
    public void OnRockButton()
    {
        playerChoice = Choice.Rock;
        if (playerScore <= 5 || enemyScore <= 5)
        {
            DetermineWinner();
        }
    }
    public void OnPaperButton()
    {
        playerChoice = Choice.Paper;
        if (playerScore <= 5 || enemyScore <= 5)
        {
            DetermineWinner();
        }
    }

    public void OnRestartButton()
    {
        Backsound.Play();
        if (REALWIN.isPlaying)
        {
            REALWIN.Stop();
        }
        resultText.text = "";
        QD.Play();
        playerScore = 0;
        enemyScore = 0;
        pscore.text = playerScore.ToString();
        escore.text = enemyScore.ToString();
    }
    public void OnBackButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}