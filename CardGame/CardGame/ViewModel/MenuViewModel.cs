using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CardGame.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private bool _easy;
        private bool _medium;
        private bool _hard;

        private double _difficulty;

        public event EventHandler<EventArgs> StartGameEvent;

        public RelayCommand<string> SetDifficultyCommand { get; private set; }
        public RelayCommand PlayCommand { get; private set; }

        public bool Easy
        {
            get => _easy;
            set
            {
                _easy = value;
                OnPropertyChanged();
            }
        }

        public bool Medium
        {
            get => _medium;
            set
            {
                _medium = value;
                OnPropertyChanged();
            }
        }

        public bool Hard
        {
            get => _hard;
            set
            {
                _hard = value;
                OnPropertyChanged();
            }
        }

        public double Difficulty
        {
            get => _difficulty;
            private set
            {
                _difficulty = value;
                OnPropertyChanged();
            }
        }

        public MenuViewModel()
        {
            SetDifficultyCommand = new RelayCommand<string>(
                param => ChangeDifficulty(param?.ToString())
            );

            PlayCommand = new RelayCommand(
        () => StartGameEvent?.Invoke(this, EventArgs.Empty)
        );

            ChangeDifficulty("Medium");
        }

        private void ChangeDifficulty(string difficulty)
        {
            switch (difficulty)
            {
                case "Easy":
                    Easy = false;
                    Medium = true;
                    Hard = true;
                    Difficulty = 0.7;
                    break;

                case "Medium":
                    Easy = true;
                    Medium = false;
                    Hard = true;
                    Difficulty = 1.0;
                    break;

                case "Hard":
                    Easy = true;
                    Medium = true;
                    Hard = false;
                    Difficulty = 1.3;
                    break;
            }
        }

    }
}
