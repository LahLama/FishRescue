using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;
namespace LahLama
{
    public class SelectHotbarItem : MonoBehaviour, IPointerDownHandler
    {
        private DropHotbarItem dropHotbarItem;
        private TankItem tankItem;
        public GameObject selector;

        void Awake()
        {
            dropHotbarItem = FindAnyObjectByType<DropHotbarItem>();
            tankItem = FindAnyObjectByType<TankItem>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //sets the slot in the script that will be dropped when the drop key is pressed
            dropHotbarItem.slot = this.gameObject;
            //Shows which slot is active.
            selector.transform.position = this.gameObject.transform.position;
            //Parses thru the current slot selected
            tankItem.MakeTankItem(this.gameObject);
        }
    }
}