using UnityEngine;

public class ShopButtonScript : MonoBehaviour
{
    [SerializeField] private ItemsData _item;

    public void BuyItem()
    {
        Debug.Log("Button Buy clicked");
        ShopManager.Instance.BuyItem(_item);
    }

    public void SellItem()
    {
        ShopManager.Instance.SellItem(_item);
    }
}
