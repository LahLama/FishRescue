using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;
namespace LahLama
{
    public class SelectHotbarItem : MonoBehaviour, IPointerDownHandler
    {
        private DropHotbarItem dropHotbarItem;
        public GameObject selector;
        ClarifyTank clarifyTank;

        void Awake()
        {
            dropHotbarItem = FindAnyObjectByType<DropHotbarItem>();
            clarifyTank = GameObject.FindAnyObjectByType<ClarifyTank>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //sets the slot in the script that will be dropped when the drop key is pressed
            dropHotbarItem.slot = this.gameObject;

            //Shows which slot is active.
            selector.transform.position = this.gameObject.transform.position;

            if (clarifyTank.CurrentTank != null)
                //Parses thru the current slot selected
                clarifyTank.CurrentTank.GetComponent<TankItem>().MakeTankItem(this.gameObject);

        }
    }
}