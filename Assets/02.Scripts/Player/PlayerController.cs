using UnityEngine;
using Utils.EnumTypes;

public class PlayerController : MonoBehaviour
{
    public PlayerState playerState = PlayerState.Idle;
    public Animator playerAnimation;

    private int mistakeCount = 0;
    private const int maxMistakeCount = 2;

    private void Awake()
    {
        playerAnimation = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(CustomerManager.Instance.currentCustomer.customerState == CustomerState.Greet)
            {
                playerAnimation.SetTrigger("Greet");
                CustomerManager.Instance.currentCustomer.currentTime = 0.0f;
                CustomerManager.Instance.currentCustomer.customerState = CustomerState.Calculate;
            }
            else
            {
                OnScanner();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            OnScanner();
        }
    }

    public void OnScanner()
    {
        AudioManager.Instance.SFXPlay(SFXType.Scanner, true);
        playerAnimation.SetTrigger("Scanner");
    }

    public void OnMistake()
    {
        mistakeCount++;

        if (mistakeCount >= maxMistakeCount)
        {
            GameManager.Instance.OnWarning();
            mistakeCount = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnScanner();

            if (CustomerManager.Instance.currentCustomer.customerState == CustomerState.Calculate)
            {
                ProductController _product = collision.gameObject.GetComponent<ProductController>();
                if (_product != null && _product.productType == ProductType.Basic)
                {
                    Debug.Log("::: 계산 성공 :::");
                    _product.isActive = true;
                    Destroy(collision.gameObject);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            OnScanner();

            if (CustomerManager.Instance.currentCustomer.customerState == CustomerState.Calculate)
            {
                ProductController _product = collision.gameObject.GetComponent<ProductController>();
                if (_product != null && _product.productType == ProductType.Strange)
                {
                    Debug.Log("::: 계산 성공 :::");
                    _product.isActive = true;
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CustomerManager.Instance.currentCustomer.customerState == CustomerState.Calculate)
        {
            ProductController _product = collision.gameObject.GetComponent<ProductController>();
            if (_product != null && !_product.isActive)
            {
                OnMistake();
                Destroy(collision.gameObject);
            }
        }
    }
}