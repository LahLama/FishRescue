using UnityEngine;
using UnityEngine.EventSystems;
namespace LahLama
{
    public class DragTankItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        TankItem tankItem;


        void Awake()
        {
            tankItem = GameObject.FindAnyObjectByType<TankItem>();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var mousePos = eventData.position;
            var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;
            this.transform.position = worldPos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }
}