namespace GamePage
{
    public class Gatcha
    {
        private Random _Random= new Random();
        private Item[] _Item; //Item -> Hero || can directly be a hero[], idk what's better
        private uint Cost;
        private int DropRate;
        private int Pity;

        public Gatcha(Item[] items)
        {
            _Item = items; //need to sorted _Item
            Cost = 100;
            DropRate = 10; //drop item if it 10 or below (on 100)
            Pity = 0; //guarantee to drop
        }

        private void putHero(Item hero)
        {
            _Item.Append(hero);
        }

        private void RealPull(Player id) //this func is used to know if you already have the 
        {
            int randomHero = _Random.Next(0, _Item.Length - 1); //to be sure that was within the good range
            Item Hero = _Item[randomHero];
            int i = 0;
            int maxSize = id._Item.Length; //player inventory of hero max size
            while (i<maxSize && Hero!=id._Item[i]) // to know if the player already have the hero pulled
            {
                i++;
            }
            if (Hero!=id._Item[i])
            {
                Console.WriteLine($"unlucky, you already have {Hero.Name}");
            }
            else
            {
                Console.WriteLine($"You unlock the hero {Hero.Name}");
                id.AddHero(Hero);
            }
        }
        public void Pull(Player id)
        {
            if (id.Money<100)
            {
                Console.WriteLine("not enough Money"); //we need to do something for this error or nothing, just to pass this func
            }
            else
            {
                int drop = _Random.Next(0, 99);
                id.money -= 100;
                if (drop < DropRate || Pity==10)
                {
                    RealPull(id);
                    Pity = 0;
                }
                else
                {
                    Console.WriteLine($"unlucky You didn't drop any Hero");
                    Pity += 1;
                }
            }
        }
    }

    /*
     * need to do to the animation of dropping an hero
     */
}
