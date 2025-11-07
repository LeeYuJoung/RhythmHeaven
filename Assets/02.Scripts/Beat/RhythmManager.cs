using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    private static RhythmManager instance;
    public static RhythmManager Instance { get { return instance; } }

    private float[,] breats = new float[4, 5] {
        { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f },{ 0.4f, 0.5f, 0.4f, 0.4f, 0.6f },
        { 0.4f, 0.2f, 0.6f, 0.5f, 0.2f},  { 0.2f, 0.6f, 0.2f, 0.3f, 0.6f}};

    public int currentBeatInMeasure = 4;

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
        float _time = breats[_beatIndex, _currentIndex];

        return _time;
    }
}
