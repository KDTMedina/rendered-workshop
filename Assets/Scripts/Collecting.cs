using UnityEngine;
using TMPro;

public class Collecting : MonoBehaviour
{
    [Header("UI Text References")]
    public TextMeshProUGUI heartText; 
    public TextMeshProUGUI starText;  
    public TextMeshProUGUI coinText;  

    private int heartCount = 0;
    private int starCount = 0;
    private int coinCount = 0;

    private void Start()
    {
        // Initialize UI
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            heartCount++;
            UpdateUI();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Star"))
        {
            starCount++;
            UpdateUI();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Coin"))
        {
            coinCount++;
            UpdateUI();
            Destroy(other.gameObject);
        }
    }

    private void UpdateUI()
    {
        if (heartText != null)
            heartText.text = heartCount.ToString();

        if (starText != null)
            starText.text = starCount.ToString();

        if (coinText != null)
            coinText.text = coinCount.ToString();
    }

    public bool DecreaseHeart()
    {
        if (heartCount > 0)
        {
            heartCount--;
            UpdateUI();
            return true;
        }
        return false;
    }


    public int GetHeartCount()
    {
        return heartCount;
    }

}
