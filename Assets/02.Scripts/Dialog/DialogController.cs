using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private string[] dialogues = new string[5] { "생활비가 급한 주인공은 우연히 길을 걷다 야간 캐셔 구인 전단지를 발견했다." ,
    "주인공은 전단지를 들고, 마트로 찾아간다.", "마트 내부를 둘러본다.", "야간 직원 행동 지침서입니다. 외부 유출은 절대 금지입니다.", "이게 무슨... (지침서에는 기묘한 규칙들이 가득하다.)"};

    private string[] normalEndingDilogues = new string[8]
    {
        "축하합니다.",
        "휴...감사합니다.",
        "30일 동안 손님들이 많이 왔죠. 당신이 손님을 기다렸나요, 손님이 당신을 기다렸나요?",
        "30일 동안 손님들이 많이 왔죠. 당신이 손님을 기다렸나요, 손님이 당신을 기다렸나요?",
        "이전 직원인데 혹시 아시나요?",
        "이전 직원인데 혹시 아시나요?",
        "",
        ""
    };

    public Image introPhanel;
    public GameObject[] introGameObject;
    public Text[] dialogText;
    public string dialog;

    public Image normalPhanel;
    private float normalEndingDelay = 2.0f;
    private float delay = 3.0f;
    private int currentIndex = 0;

    private float fadeDuration = 2.0f;

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 1)
        {
            // 인트로
            ShowNextDialogue();
        }
        else if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 3)
        {
            // 노말 엔딩
            ShowNormalEndingDialogue();
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 4)
        {
            // 배드 엔딩
            string fullText = "규칙 위반이 누적되었습니다. 직원을 교체하겠습니다.";
            dialogText[0].text = ""; // 초기화

            // DOTween 문자열 타이핑 효과
            dialogText[0].DOText(fullText, delay)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    // 잠시 대기 후 다음 대사로 넘어가기
                    DOVirtual.DelayedCall(2.5f, () =>
                    {
                        dialogText[0].text = "";
                        PadeOut();
                        EndingManager.Instance.isEnding = true;
                    });
                });
        }
    }

    // 인트로 다이어그램 순서대로 활성화
    void ShowNextDialogue()
    {
        if (currentIndex == 4)
        {
            string fullText = dialogues[currentIndex];
            dialogText[currentIndex].text = ""; // 초기화

            // DOTween 문자열 타이핑 효과
            dialogText[currentIndex].DOText("", delay)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    // 잠시 대기 후 다음 대사로 넘어가기
                    DOVirtual.DelayedCall(2.5f, () =>
                    {
                        dialogText[currentIndex].text = "";
                        PadeIn();
                    });
                });
        }
        else
        {
            string fullText = dialogues[currentIndex];
            dialogText[currentIndex].text = ""; // 초기화

            // DOTween 문자열 타이핑 효과
            dialogText[currentIndex].DOText(fullText, delay)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    // 잠시 대기 후 다음 대사로 넘어가기
                    DOVirtual.DelayedCall(2.5f, () =>
                    {
                        dialogText[currentIndex].text = "";
                        currentIndex++;

                        // 다음 UI 활성화
                        introGameObject[currentIndex].SetActive(true);

                        // 다음 대사 실행
                        ShowNextDialogue();
                    });
                });
        }
    }

    public void ShowNormalEndingDialogue()
    {
        if (currentIndex == 2 || currentIndex == 4)
        {
            string fullText = normalEndingDilogues[currentIndex];
            dialogText[currentIndex].text = ""; // 초기화

            // DOTween 문자열 타이핑 효과
            dialogText[currentIndex].DOText(fullText, normalEndingDelay)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    // 잠시 대기 후 다음 대사로 넘어가기
                    DOVirtual.DelayedCall(1.5f, () =>
                    {
                        currentIndex++;

                        // 다음 UI 활성화
                        introGameObject[currentIndex].SetActive(true);

                        // 다음 대사 실행
                        ShowNormalEndingDialogue();
                    });
                });
        }
        else if (currentIndex == 3 || currentIndex == 5)
        {
            // 잠시 대기 후 다음 대사로 넘어가기
            DOVirtual.DelayedCall(1.5f, () =>
            {
                currentIndex++;
                introGameObject[currentIndex].SetActive(true);
                ShowNormalEndingDialogue();
            });
            dialogText[currentIndex - 1].text = "";// 초기화
        }
        else if (currentIndex == 7)
        {
            string fullText = dialogues[currentIndex];
            dialogText[currentIndex].text = ""; // 초기화

            // DOTween 문자열 타이핑 효과
            dialogText[currentIndex].DOText("", delay)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    // 잠시 대기 후 다음 대사로 넘어가기
                    DOVirtual.DelayedCall(2.5f, () =>
                    {
                        dialogText[currentIndex].text = "";
                    });
                });
        }
        else
        {
            string fullText = normalEndingDilogues[currentIndex];
            dialogText[currentIndex].text = ""; // 초기화

            // DOTween 문자열 타이핑 효과
            dialogText[currentIndex].DOText(fullText, normalEndingDelay)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    // 잠시 대기 후 다음 대사로 넘어가기
                    DOVirtual.DelayedCall(1.5f, () =>
                    {
                        dialogText[currentIndex].text = "";
                        currentIndex++;

                        // 다음 UI 활성화
                        introGameObject[currentIndex].SetActive(true);

                        // 다음 대사 실행
                        ShowNormalEndingDialogue();
                    });
                });
        }
    }

    // 페이드인
    public void PadeIn()
    {
        introPhanel.color = new Color(0, 0, 0);
        introPhanel.DOFade(1f, fadeDuration).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("Fade");
                dialogText[4].text = "";
                dialogText[4].DOText(dialogues[4], delay)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(2.0f, () =>
                    {
                        // 게임 시작
                        Debug.Log("Fade In");
                        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                    });
                });
            });
    }

    // 페이드인
    public void NormalPadeIn()
    {
        normalPhanel.color = new Color(0, 0, 0);
        normalPhanel.DOFade(1f, fadeDuration).SetEase(Ease.Linear);
    }

    // 페이드 아웃
    public void PadeOut()
    {
        // 0 → 1로 알파값 변화 (점점 어두워짐)
        introPhanel.DOFade(0f, fadeDuration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                Debug.Log("페이드아웃 완료!");
                // 예: Scene 전환
                // SceneManager.LoadScene("NextScene");
            });
    }
}