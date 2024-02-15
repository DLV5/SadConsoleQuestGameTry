namespace ConsoleStyleFantasy
{
    internal class DialogEndPoint : DialogComponent
    {
        private string[] _dialogToReturn;
        private Action _consiquences;

        public DialogEndPoint(string[] dialogToReturn, Action consiquences)
        {
            _dialogToReturn = dialogToReturn;
            _consiquences = consiquences;
        }

        public override string[] GetDialog()
        {
            _consiquences();
            return _dialogToReturn;
        }
    }
}
