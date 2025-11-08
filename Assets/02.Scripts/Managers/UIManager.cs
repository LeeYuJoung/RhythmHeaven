using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    private Canvas canvas;
    private GameObject gameOverPhanel;
    //public Image blinkPhanel;
    public GameObject guideBook;
    public Text guidBookContents;

    private string stage1Guid = "1. 손님이 오면 10초 이내 인사할 것\n" +
    "2. 정해진 박자에 바코드를 찍을 것\n" +
    "3. 갑자기 앞이 보이지 않아도 계산은 진행 할 것\n" +
    "4. 모든 품목은 Spacebar로 바코드를 찍을 것\n" +
    "5. 규칙을 절대 지킬 것\n" +
    "6. 중도 퇴사 불가능";
    private string stage2Guid = "<color=red>1. 손님이 오면 10초 이내 인사할 것</color>\n" +
    "2. 정해진 박자에 바코드를 찍을 것\n" +
    "3. 갑자기 앞이 보이지 않아도 계산은 진행 할 것\n" +
    "4. 모든 품목은 Spacebar로 바코드를 찍을 것\n" +
    "5. 규칙을 절대 지킬 것\n" +
    "6. 중도 퇴사 불가능";
    private string stage3Guid = "<color=red>1. 손님에게 인사하지 말 것</color>\n" +
    "2. 정해진 박자에 바코드를 찍을 것\n" +
    "3. 갑자기 앞이 보이지 않아도 계산은 진행 할 것\n" +
    "<color=red>4. 일반 품목은 Spacebar로 바코드를 찍고, 이상현상 품목은 Enter로 바코드를 찍을 것</color>\n" +
    "5. 규칙을 절대 지킬 것\n" +
    "6. 중도 퇴사 불가능";

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
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            // 행동 지침서 활성화 및 비활성화
            if (!guideBook.activeSelf)
            {
                guideBook.SetActive(true);
            }
            else
            {
                if (StageManager.Instance.isStageOver)
                {
                    AudioManager.Instance.BGMPlay(1 + StageManager.Instance.currentStage);
                }

                guideBook.SetActive(false);
                StageManager.Instance.isStageOver = false;
            }
        }
    }

    private void Init()
    {
        canvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        //blinkPhanel = canvas.transform.GetChild(1).GetComponent<Image>();
        guideBook = canvas.transform.GetChild(2).gameObject;
        guidBookContents = guideBook.transform.GetChild(1).GetComponent<Text>();
        gameOverPhanel = canvas.transform.GetChild(3).gameObject;

        OnGuidBookActive();
    }

    // 스테이지 넘어가는 연출
    public void OnGuidBookActive()
    {
        StageManager.Instance.isStageOver = true;
        guideBook.SetActive(true);
        GuideBookContentsChange(StageManager.Instance.currentStage);
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

    public void GameOver()
    {
        gameOverPhanel.SetActive(true);
    }
}
