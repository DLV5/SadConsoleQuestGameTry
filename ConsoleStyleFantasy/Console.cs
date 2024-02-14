using SadConsole.Components;
using SadConsole.UI;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleStyleFantasy
{
    internal class Console : ControlsConsole
    {
        public ScreenSurface PromptScreen { get; private set; }

        private readonly ClassicConsoleKeyboardHandler _keyboardHandlerDOS;

        private PlayerStats _playerStats;

        private SadConsole.Instructions.DrawString _typingInstruction;

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

            cursor.UseStringParser = true;

            string[] text = new string[] {
                "  ",
                "You are the slave in a deep mine, all what you can do now is to [c:r f:yellow]mine[c:undo] gold"
            };

            _typingInstruction = new SadConsole.Instructions
    .DrawString(SadConsole.ColoredString.Parser.Parse(string.Join("\r\n", text)));

            _typingInstruction.Position = cursor.Position;
            _typingInstruction.TotalTimeToPrint = TimeSpan.FromMilliseconds(500);

            _typingInstruction.Repeating += OnWritingStarted;
            _typingInstruction.Finished += OnWritingFinished;

            PromptScreen.SadComponents.Add(_typingInstruction);

            cursor.Position = new(_typingInstruction.Position.X, _typingInstruction.Position.Y + text.Length);

            //cursor.Print("You are the slave in a deep mine, all what you can do now is to [c:r f:yellow]mine[c:undo] gold").NewLine().NewLine();
            PromptScreen.Surface.TimesShiftedUp = 0;
        }

        private void DOSHandlerEnterPressed(ClassicConsoleKeyboardHandler keyboardComponent, Cursor cursor, string value)
        {
            value = value.ToLower().Trim();

            string[] text = new string[] { };

            if (value == "mine")
            {
                text = new string[]
                {
                    "  ",
                    "  You are mining succesfully",
                    "  +1 Gold"
                };

                _playerStats.Gold.AddGold(1);
            }
            else
            {
                text = new string[]
               {
                    $" {_keyboardHandlerDOS.EraseGlyph} ",
                    "  The warder whips you cause you aren't mining the gold"
               };

                _playerStats.Health.DecreaseHealth(1);
            }

            _typingInstruction.Text = SadConsole.ColoredString.Parser.Parse(string.Join("\r\n", text));

            _typingInstruction.Position = cursor.Position;

            
            cursor.Position = new (_typingInstruction.Position.X, _typingInstruction.Position.Y + text.Length);

            _typingInstruction.Repeat();

            GameTime.CurrentHour++;
        }

        private void OnWritingStarted(object? sender, EventArgs e)
        {
            PromptScreen.IsFocused = false;
        }
        
        private void OnWritingFinished(object? sender, EventArgs e)
        {
            PromptScreen.IsFocused = true;
        }
    }
}
