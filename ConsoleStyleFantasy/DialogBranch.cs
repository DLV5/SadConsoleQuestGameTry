using System.Security.Cryptography.X509Certificates;

namespace ConsoleStyleFantasy
{
    internal class DialogBranch : DialogComponent
    {
        private string[] _dialogToReturn;
        private Action _consiquences;

        private Func<string, DialogComponent> _promptInstructions;

        public DialogBranch(string[] dialogToReturn, Func<string, DialogComponent> promptInstructions)
        {
            _dialogToReturn = dialogToReturn;
            _promptInstructions = promptInstructions;
        }
        
        public DialogBranch(string[] dialogToReturn, Action consiquences, Func<string, DialogComponent> promptInstructions)
        {
            _dialogToReturn = dialogToReturn;
            _consiquences = consiquences;
            _promptInstructions = promptInstructions;
        }

        public override string[] GetDialog()
        {
            _consiquences();
            return _dialogToReturn;
        }

        public DialogComponent GetNextDialog(string prompt)
        {
            return _promptInstructions(prompt);
        }
    }
}
