using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.Model;

namespace CardGame.ViewModel
{
    public class CardGameViewModel : ViewModelBase
    {
        private CardGameModel _model;

        public CardGameViewModel(CardGameModel model) 
        {
            _model = model;
        }
    }
}
