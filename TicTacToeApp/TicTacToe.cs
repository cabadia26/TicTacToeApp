using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace TicTacToeApp
{
    public class TicTacToe : INotifyPropertyChanged
    {
        public enum PlayerEnum { Blank, X, O }

        List<List<Spot>> lstwinningsets = new List<List<Spot>>();
        string messageval = "";
        PlayerEnum currentval = PlayerEnum.Blank;

        public event PropertyChangedEventHandler? PropertyChanged;

        public TicTacToe()
        {
            Spots = new(); //shorthand syntax for instatiating with declared data type
            //the following 2 procedures could have been coded here, but seperated out for readability
            SetupSpots();
            SetupWinningLists();

            this.ComputerPlayerLetter = PlayerEnum.O;
            this.CurrentTurn = this.NonComputerPlayerLetter;
            //data type is non-nullable string, so should be initialized
            this.Message = "";
            this.SpotWinningColor = Color.Green;
            this.SpotDefaultColor = Color.Gray;

        }
        public void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        private void SetupSpots()
        {
            for (int i = 0; i < 9; i++)
            {
                //choose priority for spot, will set property when instantiated
                var priorityval = 2; //default to lowest priority
                switch (i)
                {
                    case 4:
                        priorityval = 0;
                        break;
                    case 0:
                    case 2:
                    case 6:
                    case 8:
                        priorityval = 1;
                        break;
                }
                this.Spots.Add(new Spot() { Priority = priorityval });
            }
        }

        private void SetupWinningLists()
        {
            //horizontal
            lstwinningsets.Add(new List<Spot>() { Spots[0], Spots[1], Spots[2] });
            lstwinningsets.Add(new List<Spot>() { Spots[3], Spots[4], Spots[5] });
            lstwinningsets.Add(new List<Spot>() { Spots[6], Spots[7], Spots[8] });

            //vertical
            lstwinningsets.Add(new List<Spot>() { Spots[0], Spots[3], Spots[6] });
            lstwinningsets.Add(new List<Spot>() { Spots[1], Spots[4], Spots[7] });
            lstwinningsets.Add(new List<Spot>() { Spots[2], Spots[5], Spots[8] });

            //diagonal
            lstwinningsets.Add(new List<Spot>() { Spots[0], Spots[4], Spots[8] });
            lstwinningsets.Add(new List<Spot>() { Spots[2], Spots[4], Spots[6] });

        }
        public void TakeSpot(int spotnum)
        {
            
            if (this.GameActive == true)
            {
                if(spotnum > this.Spots.Count -1)
                {
                    throw new Exception("There is no SPOT for number " + spotnum.ToString());
                }
                if (Spots[spotnum].Value == PlayerEnum.Blank)
                {
                    Spots[spotnum].Value = this.CurrentTurn;

                    lstwinningsets.ForEach(l => CheckWinner(l));

                    if (GameActive == true) { this.CheckTie(); }

                    if (GameActive == true)
                    {
                        if (CurrentTurn == PlayerEnum.X)
                        {
                            this.CurrentTurn = PlayerEnum.O;
                        }
                        else
                        {
                            this.CurrentTurn = PlayerEnum.X;
                        }

                        if (this.IsPlayAgainstComputer == true && this.IsComputerTurn == true)
                        {
                            DoComputerMove();
                        }
                    }
                }
            }
        }

        private void CheckWinner(List<Spot> lst)
        {
            PlayerEnum s = lst[0].Value;
            if (s != PlayerEnum.Blank)
            {
                if (lst.Count(l => l.Value == s) == lst.Count)
                {
                    this.Winner = this.CurrentTurn;
                    this.Message = this.CurrentTurn.ToString() + " is the winner!!!!!";
                    lst.ForEach(l =>
                    {
                        l.WinStatus = true;
                        l.SpotColor = Color.Red;
                        l.SpotColor = this.SpotWinningColor;
                    });
                    this.GameActive = false;
                }
            }
        }

        private void CheckTie()
        {
            if (this.Spots.Count(s => s.Value == PlayerEnum.Blank) == 0)
            {
                this.GameActive = false;
                this.Message = "Tie";
            }
        }

        public void StartGame()
        {
            this.Spots.ForEach(s => { s.Value = PlayerEnum.Blank; s.WinStatus = false; s.SpotColor = this.SpotDefaultColor; });
            this.CurrentTurn = PlayerEnum.X;
            this.GameActive = true;
            this.Message = "";
        }

        private void DoComputerMove()
        {
            //offense
            DoComputerOffenseDefense(this.ComputerPlayerLetter);
            //defense
            if (this.IsComputerTurn == true) { DoComputerOffenseDefense(this.NonComputerPlayerLetter); }
            //make best move
            if (this.IsComputerTurn == true && this.Spots.Exists(s => s.Value == PlayerEnum.Blank))
            {
                int priorityval = this.Spots.Where(s => s.Value == PlayerEnum.Blank).Min(s => s.Priority);
                TakeSpot(this.Spots.IndexOf(this.Spots.First(s => s.Priority == priorityval && s.Value == PlayerEnum.Blank)));
            }
        }

        private void DoComputerOffenseDefense(PlayerEnum playerval)
        {
            var lst = lstwinningsets.Find(l => l.Count(s => s.Value == playerval) == 2
            && l.Count(s => s.Value == PlayerEnum.Blank) == 1);

            if (lst != null)
            {
                Spot? s = lst.Find(s => s.Value == PlayerEnum.Blank);
                if (s != null)
                {
                    this.TakeSpot(this.Spots.IndexOf(s));
                }
            }
        }

        private bool IsComputerTurn
        {
            get
            {
                bool b = false;

                if (this.GameActive == true && this.CurrentTurn == this.ComputerPlayerLetter)
                {
                    b = true;
                }

                return b;
            }
        }

        public PlayerEnum CurrentTurn 
        { get 
            { return currentval; 
            }
            private set
            {
                currentval = value;
                InvokePropertyChanged();
            }
        }

        public List<Spot> Spots { get; private set; }

        public string Message 
        { get
            {
                return messageval;
            } 
            private set 
            {
                messageval = value;
                InvokePropertyChanged();
            } 
        }
        public bool GameActive { get; private set; }

        public bool IsPlayAgainstComputer { get; set; }

        private PlayerEnum ComputerPlayerLetter { get; set; }
        public Color SpotWinningColor { get; set; }
        public Color SpotDefaultColor { get; set; }
        public PlayerEnum Winner { get; private set; }

        private PlayerEnum NonComputerPlayerLetter
        {
            get
            {
                PlayerEnum p = PlayerEnum.X;
                if (this.ComputerPlayerLetter == PlayerEnum.X)
                {
                    p = PlayerEnum.O;
                }
                return p;
            }
        }
    
        public string SpotsReport {
            get
            {
                string report = "";
                this.Spots.ForEach(s => report += this.Spots.IndexOf(s).ToString() + "=" + s.Value.ToString() + " , ");
                return report
; }
        }
    }
}