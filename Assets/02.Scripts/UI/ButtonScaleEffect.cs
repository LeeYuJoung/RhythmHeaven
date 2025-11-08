using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonScaleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scaleUpSize = 1.2f;  // 커지는 크기 배율
    [SerializeField] private float scaleDuration = 0.2f; // 커졌다 작아지는 속도

    private Vector3 originalScale;
    private Tween scaleTween;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 커지기
        scaleTween?.Kill(); // 중복 트윈 방지
        scaleTween = transform.DOScale(originalScale * scaleUpSize, scaleDuration)
            .SetEase(Ease.OutBack); // 살짝 튀는 듯한 느낌
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 원래 크기로 돌아가기
        scaleTween?.Kill();
        scaleTween = transform.DOScale(originalScale, scaleDuration)
            .SetEase(Ease.InBack);
    }
}