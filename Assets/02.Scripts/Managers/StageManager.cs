using UnityEngine;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance { get { return instance; } }

    public float flashDuration = 1.25f;     // 깜빡이는 속도
    public Color flashColor = Color.black; // 깜빡이는 색상

    private float currentTime = 0.0f;
    private float blinkTime = 10.0f;

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

    private void Start()
    {
        // 초기 상태 투명
        UIManager.Instance.blinkPhanel.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        AudioManager.Instance.OnHadEnded();

        if(currentTime >= blinkTime)
        {
            currentTime = 0.0f;
            Blink();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
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
    public void Blink()
    {
        // 1. 투명 → 밝게 (0→1)
        UIManager.Instance.blinkPhanel.DOFade(1f, flashDuration * 0.5f)
            .OnComplete(() =>
            {
                // 2. 다시 어둡게 (1→0)
                UIManager.Instance.blinkPhanel.DOFade(0f, flashDuration * 0.5f);
            });
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
