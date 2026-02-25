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
        private Transform player;
        void Awake()
        {
            inputActions = new PlayerInputActions(); // Initialize Input Actions
            player = GameObject.FindGameObjectWithTag("Player").transform;
            this.GetComponent<LeaveTank>().enabled = false;
            clarifyTank = GameObject.FindAnyObjectByType<ClarifyTank>();
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
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                this.GetComponent<LeaveTank>().enabled = false;
                this.GetComponent<TankItem>().enabled = false;
                clarifyTank.CurrentTank = null;
            }
        }
    }
}