using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartRace.Components
{
    public partial class MainGameMenu : UserControl
    {
        public EventHandler OnCreateTrackButtonClick;

        public MainGameMenu()
        {
            InitializeComponent();
        }

        private void HandleCreateTrackButtonClick(object sender, EventArgs e)
        {
            OnCreateTrackButtonClick?.Invoke(this, new());
        }


    }
}
