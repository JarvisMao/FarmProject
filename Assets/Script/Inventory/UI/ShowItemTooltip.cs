using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MFarm.Inventory
{
    [RequireComponent(typeof(SlotUI))]
    public class ShowItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private SlotUI slotUI;
        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (slotUI.itemDetails != null)
            {
                inventoryUI.itemToolTip.gameObject.SetActive(true);
                inventoryUI.itemToolTip.SetupToolTip(slotUI.itemDetails, slotUI.slotType);

                inventoryUI.itemToolTip.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
                inventoryUI.itemToolTip.transform.position = transform.position + Vector3.up * 60;
            }
            else
            {
                inventoryUI.itemToolTip.gameObject.SetActive(false);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            inventoryUI.itemToolTip.gameObject.SetActive(false);
        }

        private void Awake()
        {
            slotUI = GetComponent<SlotUI>();
        }

    }
}
