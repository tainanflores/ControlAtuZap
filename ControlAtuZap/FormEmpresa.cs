using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlAtuZap
{
  public partial class FormEmpresa : Form
  {
    private int IdEmp = 0;
    public FormEmpresa()
    {
      InitializeComponent();
    }

    public int GetEmpresa()
    {
      return IdEmp;
    }

    private void FormEmpresa_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar.Equals('\r'))
        SendKeys.Send("{TAB}");
    }

    private void FormEmpresa_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Escape)
      {
        IdEmp = 0;
        Close();
      }
    }

    private void bt_Abrir_Click(object sender, EventArgs e)
    {
      if (radioButton1.Checked)
        IdEmp = 1;
      else
        IdEmp = 2;

      Close();
    }
  }
}
