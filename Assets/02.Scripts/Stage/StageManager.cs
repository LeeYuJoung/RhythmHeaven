using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance { get { return instance; } }

    private int currentStage = 1;          // 현재 스테이지
    private int customerCount = 2;         // 현재 등장할 손님 수
    private int currentCustomerCount = 0;  // 현재까지 등장한 손님 수

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
