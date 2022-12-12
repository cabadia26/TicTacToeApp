using TicTacToeApp;
namespace TicTacToeWinForm
{
    public partial class frmTicTacToe : Form
    {
        TicTacToe game = new TicTacToe();
        List<Button> lstbuttons;
        public frmTicTacToe()
        {
            InitializeComponent();
            lstbuttons = new List<Button>() { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };
            lstbuttons.ForEach(b => b.Click += btn_Click);
            btnStart.Click += BtnStart_Click;

            lblMessage.DataBindings.Add("Text", game, "Message");          
            lblPlayer.DataBindings.Add("Text", game, "CurrentTurn");
            lstbuttons.ForEach(btn =>
            {
                Spot sp = game.Spots[lstbuttons.IndexOf(btn)];
                btn.DataBindings.Add("Text",sp, "Value");
               // btn.DataBindings.Add("Text", sp, "ValueDisplay");
                btn.DataBindings.Add("BackColor", sp, "SpotColor"); 
            });
            game.SpotDefaultColor = Control.DefaultBackColor;
            game.SpotWinningColor = Color.OrangeRed;

            this.StartGame();
        }

        private void StartGame()
        {
            game.StartGame();
            game.IsPlayAgainstComputer = opt1Player.Checked;
        }

        private void DoMove(int spotnum)
        {
            game.TakeSpot(spotnum);
        }

        private void btn_Click(object? sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button btn = (Button)sender;
                int i = lstbuttons.IndexOf(btn);
                DoMove(i);
            }
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            StartGame();
        }

    }
}