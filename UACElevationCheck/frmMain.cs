using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using onlyconnect;

namespace WindowsApplication12
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool bRetVal;

            try
            {

                bRetVal = onlyconnect.VistaTools.IsReallyVista();

                if (bRetVal)
                {
                    this.lbVista.Text = "I'm on Vista";

                    onlyconnect.TOKEN_ELEVATION_TYPE tet = TOKEN_ELEVATION_TYPE.TokenElevationTypeDefault;
                    bool bEv = false;

                    bEv = onlyconnect.VistaTools.IsElevated();

                    if (bEv)
                    {
                        this.lbIsAdmin.Text = "I'm elevated";
                    }
                    else
                    {
                        this.lbIsAdmin.Text = "I'm not elevated";

                    }

                    tet = onlyconnect.VistaTools.GetElevationType();

                    if (tet == TOKEN_ELEVATION_TYPE.TokenElevationTypeDefault)
                    {
                        this.lbIsElevated.Text = "I'm standard user and/or UAC is disabled";
                    }
                    else if (tet == TOKEN_ELEVATION_TYPE.TokenElevationTypeFull)
                    {
                        this.lbIsElevated.Text = "UAC is enabled and I'm elevated";
                    }
                    else if (tet == TOKEN_ELEVATION_TYPE.TokenElevationTypeLimited)
                    {
                        this.lbIsElevated.Text = "UAC is enabled and I'm not elevated";
                    }

                }
                else
                {
                    this.lbVista.Text = "I'm not on Vista";
                    this.lbIsAdmin.Text = "Not applicable";
                    this.lbIsElevated.Text = "Not applicable";
                }

            }
            catch (Exception exc)
            {
                this.Text = exc.Message;
            }
           
        }
    }
}