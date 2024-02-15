using Newtonsoft.Json.Linq;
using SadConsole.Components;
using SadConsole.Instructions;
using SadConsole.UI;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleStyleFantasy
{
    internal class Console : ControlsConsole
    {
        public ScreenSurface PromptScreen { get; private set; }

        private readonly ClassicConsoleDrawStringKeyboardHandler _keyboardHandlerDOS;

        private PlayerStats _playerStats;

        private DrawString _typingInstruction;

        private DialogBranch _dialogBranch;
        private DialogBranch _currentBranch;

        public Console(PlayerStats playerStats) : base(28, 4)
        {
            _playerStats = playerStats;

            _keyboardHandlerDOS = new ClassicConsoleDrawStringKeyboardHandler("Type your action> ");

            PromptScreen = new ScreenSurface(GameSettings.GAME_WIDTH - 20, GameSettings.GAME_HEIGHT - this.Height - 20)
            {
                Position = (8, this.Height + 15),
                UseKeyboard = true
            };

            PromptScreen.SadComponents.Add(new Cursor() { IsEnabled = false });

            Border.CreateForSurface(PromptScreen, "");

            SetupHandlers();

            PromptScreen.SadComponents.Add(_keyboardHandlerDOS);


            DialogEndPoint _dialogGoldLeaf = new DialogEndPoint(
                    new string[]
                    {
                        " ",
                        "  You are mining succesfully",
                        "  +100 Gold",
                        "  You giving almost all gold to warden but ably to save a small piece in your pocket",
                        "  -99 gold",
                        " "
                    },
                    () =>
                    {
                        _playerStats.Gold.AddGold(1);
                    }
                );
            
            DialogEndPoint _dialogAnotherLeaf = new DialogEndPoint(
                    new string[]
                    {
                        " ",
                        "  The warder whips you cause you aren't mining the gold",
                        " "
                    },
                    () =>
                    {
                        _playerStats.Health.DecreaseHealth(1);
                    }
                );

            _dialogBranch = new DialogBranch(
                    new string[] {
                "You are the slave in a deep mine, all what you can do now is to [c:r f:yellow]mine[c:undo] gold" },
                    (string prompt) =>
                    {
                        if (prompt == "mine")
                        {
                            return _dialogGoldLeaf;
                        }
                        else
                        {
                            return _dialogAnotherLeaf;
                        }
                    }
                );

            _currentBranch = _dialogBranch;
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

            string[] text = _dialogBranch.GetDialog();

            _typingInstruction = new DrawString(ColoredString.Parser.Parse(string.Join("\r\n", text)));

            _typingInstruction.Position = cursor.Position;
            _typingInstruction.TotalTimeToPrint = TimeSpan.FromMilliseconds(500);

            _typingInstruction.Finished += _typingInstruction_Finished;
            _typingInstruction.RemoveOnFinished = true;

            _keyboardHandlerDOS.IsReady = false;

            PromptScreen.SadComponents.Add(_typingInstruction);

            cursor.Position = new(_typingInstruction.Position.X, _typingInstruction.Position.Y + text.Length);

            PromptScreen.Surface.TimesShiftedUp = 0;
        }

        private void DOSHandlerEnterPressed(ClassicConsoleDrawStringKeyboardHandler keyboardComponent, Cursor cursor, string value)
        {
            value = value.ToLower().Trim();

            string[] text = ProceedPrompt(value);

            _typingInstruction.Text = ColoredString.Parser.Parse(string.Join("\r\n", text));
            _typingInstruction.Position = cursor.Position;

            cursor.Position = new(_typingInstruction.Position.X, _typingInstruction.Position.Y);

            _typingInstruction.Repeat();
            _typingInstruction.Position = cursor.Position;
            _typingInstruction.Cursor = cursor;
            _typingInstruction.TotalTimeToPrint = TimeSpan.FromMilliseconds(1000);
            _typingInstruction.Finished += _typingInstruction_Finished;
            _typingInstruction.RemoveOnFinished = true;

            keyboardComponent.IsReady = false;

            PromptScreen.SadComponents.Add(_typingInstruction);

            GameTime.CurrentTime = GameTime.CurrentTime.AddMinutes(60);
        }

        private void _typingInstruction_Finished(object? sender, EventArgs e) =>
            _keyboardHandlerDOS.IsReady = true;

        /// <summary>
        /// Handles player input
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        private string[] ProceedPrompt(string prompt)
        {
            DialogComponent _nextBranch = _currentBranch.GetNextDialog(prompt);
            _currentBranch = (DialogBranch)_nextBranch;
            return _nextBranch.GetDialog();
        }
    }
}
