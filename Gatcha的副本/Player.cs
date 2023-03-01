namespace GamePage
{
    public class Player
    {
        private int money;
        public Item[] _Item; //Item -> Hero ?


        public Player()
        {
            money = 0;
            _Item = new Item[1];
        }

        public int Money
        {
            get => money;
        }

        public void AddHero(Item hero)
        {
            _Item.Append(hero);
        }
    }
}