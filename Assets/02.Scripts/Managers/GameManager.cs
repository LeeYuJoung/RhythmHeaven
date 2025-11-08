using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    private PlayerController playerController;

    public bool IsGameOver { get { return isGameOver; } }

    public int warningCount = 0;    // 현재 경고 누적 횟수

    private float currentWorkingDay = 0.0f;   // 현재 근무 일자
    private float totalWorkingDay = 30.0f;    // 총 근무 일자

    public GameObject[] warningObject;

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
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    private void Init()
    {
        Application.targetFrameRate = 65;
        Screen.SetResolution(854, 480, true);

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // 경고 발생
    public void OnWarning()
    {
        Debug.Log("::: 경고 :::");
        warningCount++;

        if (warningCount >= 3)
        {
            OnBadGameOver();
            return;
        }
        else
        {
            playerController.playerAnimation.runtimeAnimatorController = playerController.animatorControllers[Instance.warningCount];
        }

        if (warningCount == 1)
        {
            warningObject[0].SetActive(true);
            StartCoroutine(UIManager.Instance.WarningTextActive(0));
        }
        else
        {
            warningObject[1].SetActive(true);
            StartCoroutine(UIManager.Instance.WarningTextActive(1));
        }

        AudioManager.Instance.SirenSFXPlay(Utils.EnumTypes.SFXType.Siren);
    }

    public void OnBadGameOver()
    {
        isGameOver = true;
        AudioManager.Instance.BGMPlay(Utils.EnumTypes.BGMType.DieEnding);
        UIManager.Instance.GameOver();
    }

    public void OnHappyGameOver()
    {
        isGameOver = true;
        AudioManager.Instance.BGMPlay(Utils.EnumTypes.BGMType.HappyEnding);
        UIManager.Instance.GameOver();
    }
}