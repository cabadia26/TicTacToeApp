using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace TicTacToeApp
{
    public class Spot : INotifyPropertyChanged
    {
        bool winstatusval = false;
        TicTacToe.PlayerEnum valueval = TicTacToe.PlayerEnum.Blank;
        Color colorval;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public TicTacToe.PlayerEnum Value
        { get
            { return valueval;
            }
            set
            {
                valueval = value;
                InvokePropertyChanged();
                InvokePropertyChanged("ValueDisplay");
            }
        }
        public bool WinStatus
        {
            get
            {
                return winstatusval;
            }
            internal set
            {
                winstatusval = value;
                InvokePropertyChanged();
            }
        }
        internal int Priority { get; set; }
    public Color SpotColor
        {
            get 
            { return colorval; 
            }
            internal set 
            {  colorval = value;
                InvokePropertyChanged();
            }
        }
        public string ValueDisplay
        {
            get
            {
                string s = "";

                if (this.Value != TicTacToe.PlayerEnum.Blank)
                {
                    s = this.Value.ToString();
                }

                return s;
            }
        }

    }
}
