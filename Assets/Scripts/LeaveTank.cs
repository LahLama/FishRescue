using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LahLama
{
    public class LeaveTank : MonoBehaviour
    {
        private PlayerInputActions inputActions;
        float isEscPressed;
        public Transform aquarium;
        ClarifyTank clarifyTank;
        private Camera cam;
        private Transform player;
        void Awake()
        {
            inputActions = new PlayerInputActions(); // Initialize Input Actions
            player = GameObject.FindGameObjectWithTag("Player").transform;
            this.GetComponent<LeaveTank>().enabled = false;
            clarifyTank = GameObject.FindAnyObjectByType<ClarifyTank>();

            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        }

        void OnEnable()
        {
            inputActions.Enable();
        }

        void OnDisable()
        {
            inputActions.Disable();
        }

        void FixedUpdate()
        {
            OnTankLeave();
        }
        void OnTankLeave()
        {
            isEscPressed = inputActions.UI.Cancel.ReadValue<float>();
            if (isEscPressed > 0)
            {
                player.position = aquarium.position;
                clarifyTank.CurrentTank = null;
                player.GetComponent<Collider2D>().enabled = true;
                player.GetComponent<SpriteRenderer>().enabled = true;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                this.GetComponent<LeaveTank>().enabled = false;
                this.GetComponent<TankItem>().enabled = false;
                cam.enabled = !cam.isActiveAndEnabled;

            }
        }
    }
}