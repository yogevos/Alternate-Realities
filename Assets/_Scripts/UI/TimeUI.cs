using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private void OnEnable()
    {
        TimeManager.onSecondChange += UpdateTime;
        TimeManager.OnTimeToChangeReality += UpdateTime;
    }
    private void OnDisable()
    {
        TimeManager.onSecondChange -= UpdateTime;
        TimeManager.OnTimeToChangeReality -= UpdateTime;
    }
    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.minute:00}:{TimeManager.second:00}";
    }
}
