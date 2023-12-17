using UnityEngine.UI;
using UnityEngine;

public class QualitySetting : MonoBehaviour
{
    [SerializeField] private Toggle[] qualityToggles;

    private void Awake()
    {
        GetValue();
    }

    public void GetValue()
    {
        int quality = PlayerPrefs.GetInt("QualityIndex", 0);
        QualitySettings.SetQualityLevel(quality);

        foreach (var toggle in qualityToggles)
            toggle.isOn = false;

        qualityToggles[quality].isOn = true;
    }

    public void SetValue(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityIndex", qualityIndex);

        EventsHolder.SetQualityLevel(qualityIndex);
    }
}