namespace ConsoleStyleFantasy
{
    internal static class GameTime
    {
        public static event Action? OnDayEnded;

        public static event Action? OnTimeChanged;

        private static TimeOnly _currentTime = new TimeOnly(8,0);
        public static TimeOnly CurrentTime {
            get => _currentTime;
            set
            {
                if(value >= TimeOnly.MaxValue)
                {
                    _currentTime = TimeOnly.MinValue;
                    OnDayEnded?.Invoke();
                } else
                {
                    _currentTime = value;
                }

                OnTimeChanged?.Invoke();
            }
        }
    }
}
