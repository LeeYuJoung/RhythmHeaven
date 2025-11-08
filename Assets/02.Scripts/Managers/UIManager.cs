using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    private Canvas canvas;
    public GameObject gameOverPhanel;

    public RectTransform guidRect;
    public GameObject guideBook;
    public GameObject tabObject;
    public Text guidBookContents;
    public float moveDistance = 480.0f;
    public float moveDuration = 2.0f;
    public bool isDown = false;
    public bool isArrive = false;

    public Text warningText;

    private float currentTime = 0.75f;
    private float tabTime = 0.75f;

    private string[] warningTexts = new string[2] { "규칙을 지키세요. 당신의 안전을 위한 겁니다.", "더 이상 기회는 없습니다. 주의하세요." };

    private string stage1Guid = "1. 손님이 오면 10초 이내 인사할 것\n" +
    "2. 정해진 박자에 바코드를 찍을 것\n" +
    "3. 갑자기 앞이 보이지 않아도 계산은 진행 할 것\n" +
    "4. 모든 품목은 Spacebar로 바코드를 찍을 것\n" +
    "5. 규칙을 절대 지킬 것\n" +
    "6. 계약직의 실수는 한 번 봐줄 것";
    private string stage2Guid = "<color=#A30606>1. 손님이 오면 3초 이내 인사할 것</color>\n" +
        "2. 정해진 박자에 바코드를 찍을 것\n" +
        "3. 갑자기 앞이 보이지 않아도 계산은 진행 할 것\n" +
        "<color=#A30606>4. 일반 품목은 Spacebar로 바코드를 찍고, 이상현상 품목은 Enter로 바코드를 찍을 것</color>\n" +
        "5. 규칙을 절대 지킬 것\n" +
        "6. 계약직의 실수는 한 번 봐줄 것";
    private string stage3Guid = "<color=#A30606>1. 손님에게 인사하지 말 것</color>\n" +
    "2. 정해진 박자에 바코드를 찍을 것\n" +
    "3. 갑자기 앞이 보이지 않아도 계산은 진행 할 것\n" +
    "<color=#A30606>4. 일반 품목은 Spacebar로 바코드를 찍고, 이상현상 품목은 Enter로 바코드를 찍을 것</color>\n" +
    "5. 규칙을 절대 지킬 것\n" +
    "6. 계약직의 실수는 한 번 봐줄 것";

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
        if(Input.GetKeyDown(KeyCode.Tab) && StageManager.Instance.isStageOver)
        {
            // 행동 지침서 활성화 및 비활성화
            if (isDown)
            {
                MoveGuidBook();
            }
            else
            {
                MoveGuidBook();
            }
        }

        if (guideBook.activeSelf && isArrive)
        {
            if(currentTime >= tabTime)
            {
                currentTime = 0.0f;
                StartCoroutine(OnBlinkTab());
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
        else
        {
            tabObject.SetActive(false);
        }
    }

    private void Init()
    {
        canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        guideBook = canvas.transform.GetChild(2).gameObject;
        guidBookContents = guideBook.transform.GetChild(1).GetComponent<Text>();
        tabObject = guideBook.transform.GetChild(2).gameObject;
        guidRect = guideBook.GetComponent<RectTransform>();

        gameOverPhanel = canvas.transform.GetChild(3).gameObject;
        warningText = canvas.transform.GetChild(4).GetComponent<Text>();

        OnGuidBookActive();
    }

    public void OnStageStart()
    {
        if (StageManager.Instance.isStageOver)
        {
            AudioManager.Instance.BGMPlay(1 + StageManager.Instance.currentStage);
        }

        StageManager.Instance.isStageOver = false;
    }

    // 스테이지 넘어가는 연출
    public void OnGuidBookActive()
    {
        StageManager.Instance.isStageOver = true;
        GuideBookContentsChange(StageManager.Instance.currentStage);
    }

    public void MoveGuidBook()
    {
        if (!isDown)
        {
            isDown = true;
            guidRect.DOAnchorPosY(guidRect.anchoredPosition.y - moveDistance, moveDuration)
                .SetEase(Ease.OutCubic).OnComplete(() => isArrive = true);
        }
        else
        {
            isDown = false;
            isArrive = false;
            guidRect.DOAnchorPosY(guidRect.anchoredPosition.y + moveDistance, moveDuration)
                .SetEase(Ease.InCubic).OnComplete(OnStageStart);
        }
    }

    // 행동 지침서 내용 변경
    public void GuideBookContentsChange(int _stage)
    {
        switch (_stage)
        {
            case 1:
                guidBookContents.text = stage1Guid;
                break;
            case 2:
                guidBookContents.text = stage2Guid;
                break;
            case 3:
                guidBookContents.text = stage3Guid;
                break;
        }
    }

    public IEnumerator WarningTextActive(int _index)
    {
        warningText.text = warningTexts[_index];
        warningText.DOFade(0f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(2.5f);

        warningText.text = null;
    }

    public IEnumerator OnBlinkTab()
    {
        tabObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        tabObject.SetActive(false);
    }

    public void GameOver()
    {
        //gameOverPhanel.SetActive(true);
    }
}
