﻿using SchaebigeSchaetzungen.Command;
using SchaebigeSchaetzungen.Model;
using SchaebigeSchaetzungen.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchaebigeSchaetzungen.ViewModel
{
    public class GameModeSelectionViewModel : ViewModelBase
    {
        private Player playerOne;

        public Player PlayerOne
        {
            get { return playerOne; }
            set { playerOne = value; }
        }


        public ICommand SingleplayerCommand { get; }
        public ICommand MultiplayerCommand { get; }

        public GameModeSelectionViewModel(Player playerOne, NavigationStore navigationStore, Func<LoginPlayerTwoViewModel> createLoginPlayerTwoViewModel, Func<SingleplayerGameViewModel> createSingleplayerGameViewModel)
        {
            this.playerOne = playerOne;
            this.SingleplayerCommand = new NavigateCommand(navigationStore, createSingleplayerGameViewModel);
            this.MultiplayerCommand = new NavigateCommand(navigationStore, createLoginPlayerTwoViewModel);
        }
    }
}
