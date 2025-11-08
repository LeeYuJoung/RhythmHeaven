using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    private static EndingManager instance;
    public static EndingManager Instance { get { return instance; } }

    private Tween blinkTween;

    public GameObject retryButton;
    public Image endingImage;
    public Sprite[] bedEndings;
    public int endingIndex = 0;

    public GameObject redLightObbject;
    public Image redLight;
    private float blinkDuration = 0.5f; // 한 번 깜빡이는 속도

    public float currentTime = 0.0f;
    public float endingChangeTime = 2.5f;

    public bool isEnding = false;

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
        if (isEnding && endingIndex != 2)
        {
            OnBedEnding();
        }

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 3)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }

    public void OnBedEnding()
    {
        if(currentTime >= endingChangeTime)
        {
            endingIndex++;
            currentTime = 0.0f;
            endingImage.sprite = bedEndings[endingIndex];

            if(endingIndex == 2)
                retryButton.SetActive(true);
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    public void OnHappyEnding()
    {

    }

    public void StartBlink()
    {
        redLightObbject.SetActive(true);

        blinkTween = redLight
            .DOFade(0f, blinkDuration)  // 투명해지기
            .SetLoops(6, LoopType.Yoyo) // 6번 반복
            .SetEase(Ease.InOutSine).OnComplete(() => 
            { 
                redLight.DOFade(1f, 0.1f);
                redLightObbject.SetActive(false);
                UnityEngine.SceneManagement.SceneManager.LoadScene(4);
            });
    }

    public void StopBlink()
    {
        blinkTween.Kill();
        redLight.DOFade(1f, 0.2f); // 깜빡임 멈출 때 다시 보이게
    }
}
