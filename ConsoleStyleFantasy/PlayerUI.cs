using SadConsole.Instructions;
using SadConsole.UI;

namespace ConsoleStyleFantasy
{
    internal class PlayerUI : ControlsConsole
    {
        public ScreenSurface HealthScreen;
        public ScreenSurface GoldScreen;
        public ScreenSurface TimeScreen;

        private PlayerStats _playerStats;

        private DrawString _healthUI;
        private DrawString _goldUI;
        private DrawString _timeUI;

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

            TimeScreen = new ScreenSurface(GameSettings.GAME_WIDTH - 20, GameSettings.GAME_HEIGHT - this.Height - 30)
            {
                Position = (55, this.Height + 3),
                UseKeyboard = false
            };

            _healthUI = new DrawString(
                (SadConsole.ColoredString
                .Parser
                .Parse($"Current health: [c:r f:green]{_playerStats.Health.CurrentHealth}/{_playerStats.Health.MaxHealth}" ))
            );

            HealthScreen.SadComponents.Add(_healthUI);
            PlayerHealth.OnHealthChanged += OnHealthChanged;

            _goldUI = new DrawString(
                (SadConsole.ColoredString
                .Parser
                .Parse($"Gold: [c:r f:yellow]{_playerStats.Gold.CurrentGoldAmount}" ))
            );

            GoldScreen.SadComponents.Add(_goldUI);
            PlayerGold.OnGoldChanged += OnGoldChanged;

            _timeUI = new DrawString(
                (SadConsole.ColoredString
                .Parser
                .Parse($"{GameTime.CurrentTime}:00"))
            );

            TimeScreen.SadComponents.Add(_timeUI);
            GameTime.OnTimeChanged += OnGameTimeChanged;
        }

        public void UpdateText(DrawString instruction, string textToUpdate)
        {
            instruction.Text = ColoredString.Parser.Parse(textToUpdate);

            instruction.TotalTimeToPrint = TimeSpan.Zero;

            instruction.Repeat();
        }

        private void OnHealthChanged()
        {
            UpdateText(
                _healthUI,
                $"Current health: [c:r f:green]{_playerStats.Health.CurrentHealth}/{_playerStats.Health.MaxHealth}    "
                );
        }

        private void OnGoldChanged()
        {
            UpdateText(
                _goldUI,
                $"Gold: [c:r f:yellow]{_playerStats.Gold.CurrentGoldAmount}"
                );
        }  

        private void OnGameTimeChanged()
        {
            UpdateText(
                _timeUI,
                $"{GameTime.CurrentTime}    "
                );
        }
    }
}
