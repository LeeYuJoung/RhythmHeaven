using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    public GameObject nextStageButton;

    public AudioSource audioSource;
    public AudioClip[] bgms;
    public AudioClip[] sfxs;

    private bool isEnded = false;

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

    }

    private void Init()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnHadEnded()
    {
        if (!isEnded && audioSource.isPlaying == false && audioSource.time >= audioSource.clip.length - 0.05f)
        {
            isEnded = true;
            nextStageButton.SetActive(true);
            Debug.Log("오디오 재생이 완료되었습니다!");
        }

        // 오디오를 다시 재생할 수도 있으므로, 리셋 조건 추가
        if (isEnded && audioSource.isPlaying && audioSource.time < 0.1f)
        {
            isEnded = false;
        }
    }
}