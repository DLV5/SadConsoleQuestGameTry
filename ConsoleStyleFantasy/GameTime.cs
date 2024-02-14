namespace ConsoleStyleFantasy
{
    internal static class GameTime
    {
        public static event Action? OnDayEnded;

        public static event Action? OnTimeChanged;

        private static float _currentHour = 8;
        public static float CurrentHour {
            get => _currentHour;
            set
            {
                if(value > 24)
                {
                    _currentHour = 0;
                    OnDayEnded?.Invoke();
                } else
                {
                    _currentHour = value;
                }

                OnTimeChanged?.Invoke();
            }
        }
    }
}
