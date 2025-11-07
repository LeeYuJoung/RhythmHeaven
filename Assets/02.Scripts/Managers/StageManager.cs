using UnityEngine;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance { get { return instance; } }

    private int currentStage = 1;    // 현재 스테이지

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        AudioManager.Instance.OnHadEnded();
    }

    // 스테이지별 이벤트 관리
    public void OnStageEvent()
    {
        switch(currentStage)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }

    // 정전 이벤트
    public void OnBlackoutEvent()
    {

    }

    public void OnNextStage()
    {
        // 다음 스테이지로 이동
        if (currentStage >= 3)
        {
            GameManager.Instance.OnGameOver();
            return;
        }

        currentStage++;
    }
}
