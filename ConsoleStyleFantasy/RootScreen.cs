namespace ConsoleStyleFantasy.Scenes
{
    internal class RootScreen : ScreenObject
    {
        private Console _console;
        private PlayerStats _playerStats;

        public RootScreen()
        {
            _playerStats = new PlayerStats(10);

            _console = new Console(_playerStats);

            Children.Add(_console.PromptScreen);

            Children.Add(_playerStats.UI.HealthScreen);

            Children.Add(_playerStats.UI.GoldScreen);
        }

        public override void OnFocused()
        {
            _console.OnFocused();
        }
    }
}