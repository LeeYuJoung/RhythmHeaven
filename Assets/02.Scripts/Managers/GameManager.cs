using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    private int warningCount = 0;    // 현재 경고 누적 횟수

    private float currentWorkingDay = 0.0f;   // 현재 근무 일자
    private float totalWorkingDay = 30.0f;    // 총 근무 일자

    private bool isWorking = false;    // 근무중인지 확인용
    private bool isGameOver = false;   // 게임 오버 확인용

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Init();
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!isGameOver)
        {

        }
    }

    private void Init()
    {
        Application.targetFrameRate = 65;
        Screen.SetResolution(854, 480, true);
    }

    // 경고 발생
    public void OnWarning()
    {
        Debug.Log("::: 경고 :::");
        warningCount++;

        if (warningCount >= 3)
            OnGameOver();
    }

    public void OnGameOver()
    {
        isGameOver = true;
    }

    public void OnExit()
    {
        
    }
}