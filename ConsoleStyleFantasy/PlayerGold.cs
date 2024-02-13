using System.Diagnostics;

namespace ConsoleStyleFantasy
{
    internal class PlayerGold
    {
        public static event Action? OnGoldChanged;

        private int _currentGoldAmount = 0;

        public int CurrentGoldAmount {
            get => _currentGoldAmount;
            set
            {
                _currentGoldAmount = Math.Max(value, 0);
                OnGoldChanged?.Invoke();
            }
        }

        public void AddGold(int amount)
        {

            CurrentGoldAmount += amount;
        }

        public bool RemoveGold(int amount) 
        { 
            if(CurrentGoldAmount - amount >= 0) {
                CurrentGoldAmount -= amount;
                return true;
            }

            return false;
        }
    }
}
