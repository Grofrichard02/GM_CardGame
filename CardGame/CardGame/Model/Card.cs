using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Model
{

    public partial class Card
    {
            public enum Actions {
        Attack,
        Heal,
        Shield,
        Empty
        }
        private string name {  get; set; }
        private Actions action { get; set; }
        private int value { get; set; }

        public string Name
        {
            get {  return name; }
        }
        public Actions Action
        {
            get { return action; }
        }
        public int Value
        {
            get { return value; }
        }
        public Card(string NAME,Actions ACTION,int VALUE) {
        name = NAME;
        action = ACTION;
        value = VALUE;
        }

    }
}
