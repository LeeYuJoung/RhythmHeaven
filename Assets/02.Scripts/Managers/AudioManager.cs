using UnityEngine;
using Utils.EnumTypes;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    public GameObject nextStageButton;

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;
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
        bgmAudioSource = GetComponent<AudioSource>();
        sfxAudioSource = GameObject.Find("SFXAudioPlayer").GetComponent<AudioSource>();
    }

    // 오디오 재생 완료 확인
    public void OnHadEnded()
    {
        if (!isEnded && bgmAudioSource.isPlaying == false && bgmAudioSource.time >= bgmAudioSource.clip.length - 0.05f)
        {
            isEnded = true;
            nextStageButton.SetActive(true);
            Debug.Log("오디오 재생이 완료되었습니다!");
        }

        // 오디오를 다시 재생할 수도 있으므로, 리셋 조건 추가
        if (isEnded && bgmAudioSource.isPlaying && bgmAudioSource.time < 0.1f)
        {
            isEnded = false;
        }
    }

    public void BGMPlay(BGMType _type)
    {
        bgmAudioSource.clip = null;
        bgmAudioSource.clip = bgms[(int)_type];
        bgmAudioSource.playOnAwake = true;
    }

    public void SFXPlay(SFXType _type)
    {
        sfxAudioSource.clip = null;
        sfxAudioSource.clip = sfxs[(int)_type];
        sfxAudioSource.playOnAwake = true;
    }
}