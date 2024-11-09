using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] numberSprites; // Array to hold number sprites (0-9)
    [SerializeField] private GameObject numberImagePrefab; // Prefab for the individual number Image
    public int currentScore = 0;

    private void Start()
    {
        UpdateScore(0);
    }

    public void UpdateScore(int newScore)
    {
        currentScore = newScore;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        // Clear previous score images
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Convert score to string to display each digit
        string scoreString = currentScore.ToString();

        // Create Image for each digit
        foreach (char digit in scoreString)
        {
            int digitValue = digit - '0'; // Convert char to int (e.g., '3' to 3)

            // Create a new Image for the digit
            GameObject numberImage = Instantiate(numberImagePrefab, transform);
            Image imageComponent = numberImage.GetComponent<Image>();
            imageComponent.sprite = numberSprites[digitValue]; // Set the corresponding sprite

            // Optionally, adjust position or size here
            RectTransform rectTransform = numberImage.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(rectTransform.sizeDelta.x * transform.childCount, 0); // Adjust position based on number count
        }
    }
}
