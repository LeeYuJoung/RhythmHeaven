using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    private static RhythmManager instance;
    public static RhythmManager Instance { get { return instance; } }

    private float[,] breats = new float[6, 5] {
        { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f },{ 0.4f, 0.5f, 0.4f, 0.4f, 0.6f },
        { 0.4f, 0.4f, 0.7f, 0.8f, 0.5f},  { 0.4f, 0.6f, 0.4f, 0.3f, 0.7f},
        { 0.4f, 0.4f, 0.6f, 0.7f, 0.5f},  { 0.4f, 0.3f, 0.3f, 0.4f, 0.5f}
    };

    private float[,] breats2 = new float[6, 7] {
        { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.7f, 0.8f },{ 0.4f, 0.5f, 0.4f, 0.4f, 0.6f, 0.4f, 0.5f },
        { 0.4f, 0.4f, 0.7f, 0.8f, 0.5f, 0.45f, 0.4f},  { 0.4f, 0.6f, 0.4f, 0.3f, 0.7f, 0.3f, 0.5f},
        { 0.4f, 0.4f, 0.6f, 0.7f, 0.5f, 0.4f, 0.3f,},  { 0.4f, 0.3f, 0.3f, 0.4f, 0.5f, 0.8f, 0.9f}
    };

    private float[,] breats3 = new float[6, 8] {
        { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.7f, 0.8f, 0.4f },{ 0.4f, 0.5f, 0.4f, 0.4f, 0.6f, 0.4f, 0.5f, 0.8f },
        { 0.4f, 0.4f, 0.7f, 0.8f, 0.5f, 0.45f, 0.4f, 0.5f},  { 0.4f, 0.6f, 0.4f, 0.3f, 0.7f, 0.3f, 0.5f, 0.6f},
        { 0.4f, 0.4f, 0.6f, 0.7f, 0.5f, 0.4f, 0.3f, 0.8f},  { 0.4f, 0.3f, 0.3f, 0.4f, 0.5f, 0.8f, 0.9f, 0.4f}
    };

    //private float[,] breats = new float[6, 5] {
    //    { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f },{ 0.3f, 0.5f, 0.3f, 0.3f, 0.7f },
    //    { 0.4f, 0.4f, 0.8f, 0.3f, 0.3f},  { 0.3f, 0.6f, 0.3f, 0.3f, 0.7f},
    //    { 0.4f, 0.4f, 0.7f, 0.7f, 0.3f},  { 0.5f, 0.5f, 0.3f, 0.3f, 0.5f}
    //};

    //private float[,] breats2 = new float[6, 7] {
    //    { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.6f, 0.7f },{ 0.3f, 0.5f, 0.3f, 0.3f, 0.7f, 0.4f, 0.3f },
    //    { 0.4f, 0.4f, 0.8f, 0.3f, 0.3f, 0.6f, 0.3f},  { 0.3f, 0.6f, 0.3f, 0.3f, 0.7f, 0.3f, 0.3f},
    //    { 0.4f, 0.4f, 0.7f, 0.7f, 0.3f, 0.3f, 0.6f},  { 0.5f, 0.5f, 0.3f, 0.3f, 0.5f, 0.3f, 0.7f}
    //};

    //private float[,] breats3 = new float[6, 8] {
    //    { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.6f, 0.7f, 0.8f },{ 0.3f, 0.5f, 0.3f, 0.3f, 0.7f, 0.4f, 0.3f, 0.8f },
    //    { 0.4f, 0.4f, 0.8f, 0.3f, 0.3f, 0.6f, 0.3f, 0.5f},  { 0.3f, 0.6f, 0.3f, 0.3f, 0.7f, 0.3f, 0.3f, 0.5f},
    //    { 0.4f, 0.4f, 0.7f, 0.7f, 0.3f, 0.3f, 0.6f, 0.8f},  { 0.5f, 0.5f, 0.3f, 0.3f, 0.5f, 0.3f, 0.7f, 0.3f}
    //};

    public int currentBeatInMeasure = 3;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public float OnBeat(int _beatIndex, int _currentIndex)
    {
        float _time = 0.0f;

        switch (StageManager.Instance.currentStage)
        {
            case 1:
                _time = breats[_beatIndex, _currentIndex];
                break;
            case 2:
                _time = breats2[_beatIndex, _currentIndex];
                break;
            case 3:
                _time = breats3[_beatIndex, _currentIndex];
                break;
        }

        return _time;
    }
}