using MVCEventSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleNumberGame
{
    public partial class Form1 : Form
    {
        private EventListener<GameEvent, Status> _listener; 
        public Form1(EventListener<GameEvent,Status> l)
        {
            InitializeComponent();
            _listener = l;

            uxTextbox.Text = "Type an int in range 0..10";
        }

        private void uxOKButton_Click(object sender, EventArgs e)
        {
            Status s = _listener(new GameEvent(uxTextbox.Text, "buttonPressed"));

            switch (s.State)
            {
                case State.Start: {
                        uxOutputLabel.Text = "Type an int in range 0..10";
                        break;
                    }
                case State.HaveMN: {
                        uxOutputLabel.Text = "Type an int p such that: n + m + p = 10";
                        break;
                    }
                case State.Win: {
                        uxOutputLabel.Text = "You win";
                        break;
                    }
                case State.Lose:
                    {
                        uxOutputLabel.Text = "You lose";
                        break;
                    }
            }
        }
    }
}
