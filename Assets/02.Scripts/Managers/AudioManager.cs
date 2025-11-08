using UnityEngine;
using Utils.EnumTypes;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    public GameObject nextStageButton;

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;
    public AudioSource beatAudioSource;
    public AudioSource sirenAudioSource;

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
        beatAudioSource = GameObject.Find("BeatAudioPlayer").GetComponent<AudioSource>();
        sirenAudioSource = GameObject.Find("SirenAudioPlayer").GetComponent<AudioSource>();
    }

    public void BGMPlay(BGMType _type)
    {
        bgmAudioSource.clip = null;
        bgmAudioSource.clip = bgms[(int)_type];
        bgmAudioSource.Play();
    }

    public void BGMPlay(int _type)
    {
        bgmAudioSource.clip = null;
        bgmAudioSource.clip = bgms[_type];
        bgmAudioSource.Play();
    }

    public void SFXPlay(SFXType _type, bool _isSFX)
    {
        if (_isSFX)
        {
            sfxAudioSource.clip = null;
            sfxAudioSource.clip = sfxs[(int)_type];
            sfxAudioSource.Play();
        }
        else
        {
            beatAudioSource.clip = null;
            beatAudioSource.clip = sfxs[(int)_type];
            beatAudioSource.Play();
        }
    }
    public void SirenSFXPlay(SFXType _type)
    {
        sirenAudioSource.clip = null;
        sirenAudioSource.clip = sfxs[(int)_type];
        sirenAudioSource.Play();
    }
}