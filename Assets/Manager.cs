using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject imperial, metric;
    public TextMeshProUGUI result;
    public TMP_Dropdown measure_change;
    public TMP_InputField imperialDepth, imperialTime, imperialUsed, imperialVolume, imperialPressure;
    public TMP_InputField metricDepth, metricTime, metricUsed, metricVolume, metricPressure;

    float consumed = 0;
    float sac = 0;
    float rmv = 0;
    // Start is called before the first frame update
    void Start()
    {
        ChangeMeasure(PlayerPrefs.GetInt("metric"));
        measure_change.SetValueWithoutNotify(PlayerPrefs.GetInt("metric"));
        measure_change.onValueChanged.AddListener(delegate
        {
            MeasureChanges(measure_change);
        });
    }

    void MeasureChanges(TMP_Dropdown dropdown)
    {
        ChangeMeasure(dropdown.value);
    }
    void ChangeMeasure(int i)
    {
        PlayerPrefs.SetInt("metric", i);
        imperial.SetActive(false);
        metric.SetActive(false);
        switch (i)
        {
            case 1:
                metric.SetActive(true);
                break;
            default:
                imperial.SetActive(true);
                break;
        }
    }

    public void Calculate()
    {
        if (PlayerPrefs.GetInt("metric") == 0)
        {
            consumed = float.Parse(imperialUsed.text) * float.Parse(imperialVolume.text) / float.Parse(imperialPressure.text);
            sac = float.Parse(imperialUsed.text) * 33 / (float.Parse(imperialDepth.text) + 33) / float.Parse(imperialTime.text);
            rmv = consumed / float.Parse(imperialTime.text) / (float.Parse(imperialDepth.text) / 33 + 1);
        }
        else
        {
            consumed = float.Parse(metricUsed.text) * float.Parse(metricVolume.text) / float.Parse(metricPressure.text);
            sac = float.Parse(metricUsed.text) * 10 / (float.Parse(metricDepth.text) + 10) / float.Parse(metricTime.text);
            rmv = consumed / float.Parse(metricTime.text) / (float.Parse(metricDepth.text) / 10 + 1);
        }
        DisplayResults();
    }

    public void DisplayResults()
    {
        if (PlayerPrefs.GetInt("metric") == 0)
        {
            result.text = "Consumed: " + consumed.ToString("#.#") + " cu ft of air";
            result.text += "\nSAC: " + sac.ToString("#.###") + " psi/min";
            result.text += "\nRMV: " + rmv.ToString("#.###") + " cu ft / min";
        }
        else
        {
            result.text = "Consumed: " + consumed.ToString("#,#.#") + " L of air";
            result.text += "\nSAC: " + sac.ToString("#.#") + " bar/min";
            result.text += "\nRMV: " + rmv.ToString("#.###") + " L/min";
        }
    }
}