using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckpointHandler : MonoBehaviour
{
    [Header("Checkpoint UI Panels")]
    public GameObject ui1;
    public GameObject ui2;
    public GameObject ui3;

    [Header("Play Again Button (on UI3)")]
    public Button playAgainButton;

    private bool checkpoint1Reached = false;
    private bool checkpoint2Reached = false;
    private bool checkpoint3Reached = false;

    void Start()
    {
        // Hide all checkpoint UIs at the start
        if (ui1 != null) ui1.SetActive(false);
        if (ui2 != null) ui2.SetActive(false);
        if (ui3 != null) ui3.SetActive(false);

        // Hook up restart button if assigned
        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(RestartScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player triggered with: " + other.gameObject.name); // Debug info

        if (other.CompareTag("Checkpoint1") && !checkpoint1Reached)
        {
            checkpoint1Reached = true;
            StartCoroutine(ShowUIForSeconds(ui1, 2f));
        }
        else if (other.CompareTag("Checkpoint2") && !checkpoint2Reached)
        {
            checkpoint2Reached = true;
            StartCoroutine(ShowUIForSeconds(ui2, 2f));
        }
        else if (other.CompareTag("Checkpoint3") && !checkpoint3Reached)
        {
            checkpoint3Reached = true;
            ShowFinalUI();
        }
    }

    private IEnumerator ShowUIForSeconds(GameObject uiElement, float seconds)
    {
        if (uiElement != null)
        {
            uiElement.SetActive(true);
            yield return new WaitForSeconds(seconds);
            uiElement.SetActive(false);
        }
    }

    private void ShowFinalUI()
    {
        if (ui3 != null)
        {
            ui3.SetActive(true);
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
