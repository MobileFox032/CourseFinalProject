using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryItem _item;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private GameObject _thisObject;
    [SerializeField] private GameObject _selectedBorder;

    public void SetItem(InventoryItem item)
    {
        _item = item;
        _itemImage.sprite = item.itemData.ItemIcon;
        _amountText.text = item.stackSize.ToString();
        _thisObject.SetActive(true);
    }

    public void UpdateStackSizeText(int _amount)
    {
        _amountText.text = _amount.ToString();
    }

    public void Clear(InventoryItem item)
    {
        _item = null;
        _itemImage.sprite = null;
        _amountText.text = "";
        _thisObject.SetActive(false);
    }

    public void SelectedSlot()
    {
        _selectedBorder.SetActive(true);
    }

    public void NonSelectedSlot()
    {
        _selectedBorder.SetActive(false);
    }
}
