using Unity.VisualScripting;
using UnityEngine;
using Utils.EnumTypes;

[System.Serializable]
public struct TimeSignature
{
    public int beatsPerMeasure; // 분자 (예: 4/4 -> 4)
    public int noteValue;       // 분모 (예: 4/4 -> 4)
}

public class RhythmManager : MonoBehaviour
{
    //private static RhythmManager instance;
    //public static RhythmManager Instance { get { return instance; } }

    //public static event System.Action OnBeat;

    //public float bpm = 120f;
    //private float beatInterval;  // 한 박자의 시간 (초)
    //private float nextBeatTime;

    //public TimeSignature timeSignature = new TimeSignature { beatsPerMeasure = 7, noteValue = 8 };
    //private int currentBeatInMeasure = 0;

    //private bool isPlaying = false;

    //private void Awake()
    //{
    //    if (instance != null && instance != this)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    instance = this;
    //}

    //void Start()
    //{
    //    beatInterval = 60f / bpm;
    //}

    //void Update()
    //{
    //    // 아직 리듬 재생 시작 안 함
    //    if (!isPlaying) 
    //        return;

    //    // 다음 비트 시점 도달 시 비트 처리
    //    if (Time.time >= nextBeatTime)
    //    {
    //        Beat();
    //        nextBeatTime += beatInterval;
    //    }
    //}

    //void Beat()
    //{
    //    currentBeatInMeasure++;
    //    AudioManager.Instance.SFXPlay(SFXType.Beat, false);
    //    OnBeat?.Invoke();

    //    if (currentBeatInMeasure >= timeSignature.beatsPerMeasure)
    //    {
    //        Debug.Log("=== Measure End ===");
    //        isPlaying = false; // 4/4 한 마디 끝나면 자동 정지
    //    }
    //}

    //// 외부에서 호출할 수 있는 메서드
    //public void StartRhythm()
    //{
    //    if (isPlaying) 
    //        return;

    //    Debug.Log("Rhythm Started!");
    //    isPlaying = true;
    //    currentBeatInMeasure = 0;
    //    nextBeatTime = Time.time + beatInterval;
    //}

    //public void StopRhythm()
    //{
    //    Debug.Log("Rhythm Stopped!");
    //    isPlaying = false;
    //}
}
