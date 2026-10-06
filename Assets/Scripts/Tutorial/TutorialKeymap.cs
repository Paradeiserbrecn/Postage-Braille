namespace Tutorial
{
    public class TutorialKeymap
    {
        public bool Left;
        public bool Right;
        public bool Tab;
        public bool Enter;

        public TutorialKeymap(bool left, bool right, bool tab, bool enter)
        {
            this.Left = left;
            this.Right = right;
            this.Tab = tab;
            this.Enter = enter;
        }
    }
}
