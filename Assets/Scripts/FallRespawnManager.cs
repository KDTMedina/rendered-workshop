using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FallRespawnManager : MonoBehaviour
{
    [Header("Player & Checkpoints")]
    public Transform player;
    public Transform[] checkpoints;
    private int currentCheckpointIndex = 0;

    [Header("Heart System")]
    public Collecting collectingScript;

    [Header("UI Panels")]
    public GameObject fallPanel;
    public GameObject gameOverPanel;

    private bool isRespawning = false;

    private void Start()
    {
        if (fallPanel != null)
            fallPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }


    private void Update()
    {
        if (!isRespawning && player.position.y <= 0f)
        {
            StartCoroutine(HandleFall());
        }
    }

    private IEnumerator HandleFall()
    {
        isRespawning = true;

        bool heartDeducted = collectingScript.DecreaseHeart();
        int currentHearts = collectingScript.GetHeartCount();

        if (!heartDeducted)
        {
            // No hearts to deduct, show game over panel
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
        }
        else
        {
            // Respawn with fall panel
            if (fallPanel != null)
            {
                fallPanel.SetActive(true);
                yield return new WaitForSeconds(2f);
                fallPanel.SetActive(false);
            }

            // Respawn at last checkpoint
            if (checkpoints.Length > 0 && currentCheckpointIndex < checkpoints.Length)
            {
                player.position = checkpoints[currentCheckpointIndex].position;
            }
        }

        isRespawning = false;
    }


    public void SetCheckpoint(int index)
    {
        if (index >= 0 && index < checkpoints.Length)
        {
            currentCheckpointIndex = index;
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
