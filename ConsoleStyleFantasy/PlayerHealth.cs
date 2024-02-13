using System.Diagnostics;

namespace ConsoleStyleFantasy
{
    internal class PlayerHealth
    {
        public static event Action? OnHealthChanged;

        private int _currentHealth;

        public int MaxHealth { get; private set; }
        public int CurrentHealth {
            get => _currentHealth; 
            private set
            {
                _currentHealth = Math.Min(value, MaxHealth);
                OnHealthChanged?.Invoke();

                if (CurrentHealth <= 0) {
                    Death();
                }
            } 
        }

        public PlayerHealth(int maxHealth) {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        /// <summary>
        /// Decreasing health, add negative value to heal
        /// </summary>
        /// <param name="value"></param>
        public void DecreaseHealth(int value) 
        { 
            CurrentHealth -= value;
        }

        private void Death()
        {
            Debug.WriteLine("Death");
        }
    }
}
