using UnityEngine;

namespace LahLama
{
    public class FishPersonality : MonoBehaviour
    {
        public string FishName;
        public float health = 25;
        public float hunger = 0;
        public float comfortability = 0;

        public void ModifyHealth(float amt)
        {
            health += amt;
        }
        public void ModifyHunger(float amt)
        {
            hunger += amt;
        }
        public void ModifyComfortability(float amt)
        {
            comfortability += amt;
        }
        public void ChangeName(string newName)
        {
            FishName = newName;
        }
    }
}