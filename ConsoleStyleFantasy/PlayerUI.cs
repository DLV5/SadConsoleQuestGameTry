using SadConsole.UI;

namespace ConsoleStyleFantasy
{
    internal class PlayerUI : ControlsConsole
    {
        public ScreenSurface HealthScreen;
        public ScreenSurface GoldScreen;

        private PlayerStats _playerStats;
        private SadConsole.Instructions.DrawString _healthUI;
        private SadConsole.Instructions.DrawString _goldUI;

        public PlayerUI(PlayerStats playerStats) : base(28, 4)
        {
            _playerStats = playerStats;

            HealthScreen = new ScreenSurface(GameSettings.GAME_WIDTH - 20, GameSettings.GAME_HEIGHT - this.Height - 30)
            {
                Position = (8, this.Height + 3),
                UseKeyboard = false
            };

            GoldScreen = new ScreenSurface(GameSettings.GAME_WIDTH - 20, GameSettings.GAME_HEIGHT - this.Height - 30)
            {
                Position = (95, this.Height + 3),
                UseKeyboard = false
            };

            _healthUI = new SadConsole.Instructions.DrawString(
                (SadConsole.ColoredString
                .Parser
                .Parse($"Current health: [c:r f:green]{_playerStats.Health.CurrentHealth}/{_playerStats.Health.MaxHealth}" ))
            );

            HealthScreen.SadComponents.Add(_healthUI);
            PlayerHealth.OnHealthChanged += UpdateHealthText;

            _goldUI = new SadConsole.Instructions.DrawString(
                (SadConsole.ColoredString
                .Parser
                .Parse($"Gold: [c:r f:yellow]{_playerStats.Gold.CurrentGoldAmount}" ))
            );

            GoldScreen.SadComponents.Add(_goldUI);
            PlayerGold.OnGoldChanged += UpdateGoldText;
        }

        public void UpdateHealthText()
        {
            _healthUI.Text = SadConsole.ColoredString
                .Parser
                .Parse($"Current health: [c:r f:green]{_playerStats.Health.CurrentHealth}/{_playerStats.Health.MaxHealth}    ");

            _healthUI.TotalTimeToPrint = TimeSpan.Zero;

            _healthUI.Repeat();
        }

        public void UpdateGoldText()
        {
            _goldUI.Text = SadConsole.ColoredString
                .Parser
                .Parse($"Gold: [c:r f:yellow]{_playerStats.Gold.CurrentGoldAmount}");

            _goldUI.TotalTimeToPrint = TimeSpan.Zero;

            _goldUI.Repeat();
        }

    }
}
