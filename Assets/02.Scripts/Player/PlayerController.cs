using UnityEngine;
using Utils.EnumTypes;

public class PlayerController : MonoBehaviour
{
    public PlayerState playerState = PlayerState.Idle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(CustomerManager.Instance.currentCustomer.customerState == CustomerState.Greet)
            {
                CustomerManager.Instance.currentCustomer.currentTime = 0.0f;
                CustomerManager.Instance.currentCustomer.customerState = CustomerState.Calculate;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
                GameManager.Instance.OnWarning();
                Destroy(collision.gameObject);
            }
        }
    }
}