using UnityEngine;

public class TimeFlow : MonoBehaviour
{
    public void SetTimeScale(int value)
    {
        if (value <= 1 && value >= 0)
        {
            Time.timeScale = value;
        }
    }
}
