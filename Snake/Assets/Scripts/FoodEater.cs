using System;
using UnityEngine;

namespace Snake
{
    public class FoodEater : MonoBehaviour
    {
        public event Action<Food> OnFoodEaten;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Food food))
            {
                OnFoodEaten?.Invoke(food);
            }
        }
    }
}