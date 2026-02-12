namespace CardGame.Model
{
    public class NavigationModel
    {
        private Player _player;
        private NavigationChoice _choice1;
        private NavigationChoice _choice2;

        public Player Player => _player;
        public NavigationChoice Choice1 => _choice1;
        public NavigationChoice Choice2 => _choice2;

        public NavigationModel(Player player)
        {
            _player = player;
            GenerateNewNavigation();
        }

        public void PickChoice(int index)
        {
            if (index == 1)
            {
                _choice1.ApplyChoice();
            }
            else if (index == 2)
            {
                _choice2.ApplyChoice();
            }
        }

        public void GenerateNewNavigation()
        {
            _choice1 = new NavigationChoice(_player);
            _choice2 = new NavigationChoice(_player);
        }
    }
}
