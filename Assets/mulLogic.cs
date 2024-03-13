using UnityEngine;
using UnityEngine.UI;

public class MultiplicationGame : MonoBehaviour
{
    public Text questionText;
    public Button[] optionButtons;
    public Image feedbackImage;
    public Text levelCounterText;

    private int maxLevels = 5;
    private int level = 1;
    private int score = 0;

    private int currentAnswer;

    void Start()
    {
        SetQuestion();
    }

    public void SetQuestion()
    {
        if (level > maxLevels)
        {
            // Display game over or completion message
            Debug.Log("Game Over! You completed all levels.");
            return;
        }

        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);

        currentAnswer = num1 * num2;

        questionText.text = $"{num1} x {num2}";

        // Shuffle the options
        int[] options = { currentAnswer, Random.Range(1, 100), Random.Range(1, 100) };
        ShuffleArray(options);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<Text>().text = options[i].ToString();
        }
    }

    void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    public void OnOptionSelected(UnityEngine.Object selectedButtonObject)
    {
        Button selectedButton = selectedButtonObject as Button;

        if (selectedButton != null)
        {
            int selectedAnswer = int.Parse(selectedButton.GetComponentInChildren<Text>().text);

            if (selectedAnswer == currentAnswer)
            {
                // Correct Answer
                ShowFeedbackImage("crt");
                score++;

                if (level < maxLevels)
                {
                    levelCounterText.text = "Level: " + (++level);
                    Invoke("SetQuestion", 1.5f); // Wait for 1.5 seconds before setting the next question
                }
                else
                {
                    Debug.Log("Game Over! You completed all levels.");
                }
            }
            else
            {
                // Incorrect Answer
                ShowFeedbackImage("wrong");
            }
        }
    }

    void ShowFeedbackImage(string imageName)
    {
        feedbackImage.sprite = Resources.Load<Sprite>(imageName);
        feedbackImage.gameObject.SetActive(true);

        Invoke("HideFeedbackImage", 1.0f);
    }

    void HideFeedbackImage()
    {
        feedbackImage.gameObject.SetActive(false);
    }
}
