using SadConsole.Components;
using SadConsole.UI;

namespace ConsoleStyleFantasy
{
    internal class Console : ControlsConsole
    {
        public ScreenSurface PromptScreen { get; private set; }

        private readonly ClassicConsoleKeyboardHandler _keyboardHandlerDOS;

        private PlayerStats _playerStats;

        public Console(PlayerStats playerStats) : base(28, 4)
        {
            _playerStats = playerStats;

            _keyboardHandlerDOS = new ClassicConsoleKeyboardHandler("Type your action> ");

            PromptScreen = new ScreenSurface(GameSettings.GAME_WIDTH - 20, GameSettings.GAME_HEIGHT - this.Height - 20)
            {
                Position = (8, this.Height + 15),
                UseKeyboard = true
            };

            PromptScreen.SadComponents.Add(new Cursor() { IsEnabled = false });

            Border.CreateForSurface(PromptScreen, "");

            SetupHandlers();

            PromptScreen.SadComponents.Add(_keyboardHandlerDOS);
        }

        public override void OnFocused()
        {
            PromptScreen.IsFocused = true;
        }

        private void SetupHandlers()
        {
            _keyboardHandlerDOS.EnterPressedAction = DOSHandlerEnterPressed;

            Cursor cursor = PromptScreen.GetSadComponent<Cursor>()!;

            cursor.Print("You are the slave in a deep mine, all what you can do now is to mine gold").NewLine().NewLine();
            PromptScreen.Surface.TimesShiftedUp = 0;
        }

        private void DOSHandlerEnterPressed(ClassicConsoleKeyboardHandler keyboardComponent, Cursor cursor, string value)
        {
            value = value.ToLower().Trim();

            if (value == "mine")
            {
                cursor.NewLine().
                              Print("  You are mining succesfully").NewLine().
                              Print("  +1 Gold").NewLine();
                _playerStats.Gold.AddGold(1);
            }
            else
            {
                cursor.Print("  The warder whips you cause you aren't mining the gold").NewLine();
                _playerStats.Health.DecreaseHealth(1);
            }
        }
    }
}
