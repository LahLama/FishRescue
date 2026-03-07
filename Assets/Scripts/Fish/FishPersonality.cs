using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

namespace LahLama
{
    public class FishPersonality : MonoBehaviour
    {
        public string FishName;
        public int health = 25;
        public int hunger = 0;
        public int comfortability = 0;
        TextMeshPro stats;
        TankItem tank;
        TakeFishOut takeFishOut;
        private float checkInterval = 10f;
        private float timer = 0f;
        public bool canBeReleased = false;
        public bool isDead = false;


        void Awake()
        {
            stats = transform.GetChild(0).GetComponent<TextMeshPro>();
            stats.text = $"{FishName} \t {health}💗 \n {hunger}🍗  \t {comfortability}🛋️";
            takeFishOut = GetComponent<TakeFishOut>();
            // Output: Nemo    100♥
            //         80★    90☺
        }
        public void IntializeFish()
        {
            health = Random.Range(25, 50);
            hunger = Random.Range(50, 75);
            comfortability = Random.Range(0, 10);
        }

        void FixedUpdate()
        {

            stats.text = $"{FishName} \t {health}💗 \n {hunger}🍗  \t {comfortability}🛋️";
            // Output: Nemo    100♥
            //         80★    90☺

            tank = FindFirstObjectByType<TankItem>();
            if (tank)
            {
                timer += Time.fixedDeltaTime;
                if (timer >= checkInterval)
                {
                    ModifyComfortability(-Random.Range(0, 3));
                    ModifyHunger(-Random.Range(12, 25));

                    timer = 0f;

                    tank = FindFirstObjectByType<TankItem>();
                    if (tank.numberOfItems >= 6 && tank.numberOfItems < 10)
                        ModifyComfortability(+11);
                    if (tank.numberOfFish > 1)
                        ModifyComfortability(+9);
                    if (hunger >= 67 && comfortability > 67)
                    {
                        ModifyHealth(+6);
                    }
                    else
                    {
                        ModifyHealth(-Random.Range(0, 3));
                    }


                }
            }

            if (hunger < 0)
                hunger = 0;
            if (comfortability < 0)
                comfortability = 0;
            if (health < 0)
            {
                health = 0;
                isDead = true;
            }
            if (health >= 100)
            {
                canBeReleased = true;

            }

        }
        public void ModifyHealth(int amt)
        {

            if (health < 100)
                health += amt;
            else if (amt > 0 && health > 100)
                hunger = 100;
            else if (amt < 0 && health < 0)
                health = 0;
        }
        public void ModifyHunger(int amt)
        {
            // On Touch - Goes over 100
            if (hunger < 100 || hunger > 0)
                hunger += amt;
            if (hunger > 100)
                hunger = 100;
            if (hunger < 0)
                hunger = 0;
        }
        public void ModifyComfortability(int amt)
        {

            if (comfortability < 100)
                comfortability += amt;
            else if (amt > 0 && comfortability > 100)
                hunger = 100;
            else if (amt < 0 && comfortability < 0)
                comfortability = 0;
        }
        public void ChangeName(string newName)
        {
            FishName = newName;
        }
    }
}