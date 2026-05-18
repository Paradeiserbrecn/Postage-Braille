namespace Utility
{
    public class CharFactory
    {
        private readonly string _input;
        private int _index;

        public char Curr { get; private set; }
        public char La { get; private set; }

        public bool HasNext => _index + 1 < _input.Length;

        public CharFactory(string input)
        {
            _input = input ?? string.Empty;
            _index = 0;

            Curr = _input.Length > 0 ? _input[0] : '\0';
            La   = _input.Length > 1 ? _input[1] : '\0';
        }

        public void Next()
        {
            if (!HasNext)
            {
                Curr = La;
                La = '\0';
                _index = _input.Length;
                return;
            }

            _index++;

            Curr = La;
            La = (_index + 1 < _input.Length) ? _input[_index + 1] : '\0';
        }
    }
}