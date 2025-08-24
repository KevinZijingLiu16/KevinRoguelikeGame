using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class WaveManagerUI : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI timerText;
    
    public void UpdateWaveText(string waveString) => waveText.text = waveString;
    public void UpdateTimerText(string timerString) => timerText.text = timerString;
}
