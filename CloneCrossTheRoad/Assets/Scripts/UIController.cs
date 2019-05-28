using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text ResultScoreText;
    public Text TopPanelScoreText;
    public GameObject ResultsPanel;

    public void SetNewScore(int score)
    {
        TopPanelScoreText.text = ResultScoreText.text = score.ToString();
    }

    public void ShowResultsPanel()
    {
        ResultsPanel.SetActive(true);
    }

    public void HideResultsPanel()
    {
        ResultsPanel.SetActive(false);
    }
}
