using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    //Show score on the Score Text object.
    public Text ScoreText;

    private void Update()
    {
        ScoreText.text = AddScore.score.ToString();
    }
}
