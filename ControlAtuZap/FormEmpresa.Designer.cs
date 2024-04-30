
namespace ControlAtuZap
{
  partial class FormEmpresa
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEmpresa));
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.bt_Abrir = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.radioButton2);
      this.groupBox1.Controls.Add(this.radioButton1);
      this.groupBox1.Font = new System.Drawing.Font("Arial", 15F);
      this.groupBox1.ForeColor = System.Drawing.Color.White;
      this.groupBox1.Location = new System.Drawing.Point(97, 32);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(406, 75);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Qual Empresa gostaria de executar?";
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.ForeColor = System.Drawing.Color.White;
      this.radioButton2.Location = new System.Drawing.Point(248, 29);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new System.Drawing.Size(105, 27);
      this.radioButton2.TabIndex = 1;
      this.radioButton2.Text = "Auto Divi";
      this.radioButton2.UseVisualStyleBackColor = true;
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.ForeColor = System.Drawing.Color.White;
      this.radioButton1.Location = new System.Drawing.Point(54, 29);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(168, 27);
      this.radioButton1.TabIndex = 0;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "Total Parts Ltda";
      this.radioButton1.UseVisualStyleBackColor = true;
      // 
      // bt_Abrir
      // 
      this.bt_Abrir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
      this.bt_Abrir.Cursor = System.Windows.Forms.Cursors.Hand;
      this.bt_Abrir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.bt_Abrir.ForeColor = System.Drawing.Color.White;
      this.bt_Abrir.Location = new System.Drawing.Point(240, 128);
      this.bt_Abrir.Name = "bt_Abrir";
      this.bt_Abrir.Size = new System.Drawing.Size(121, 33);
      this.bt_Abrir.TabIndex = 1;
      this.bt_Abrir.Text = "Executar";
      this.bt_Abrir.UseVisualStyleBackColor = false;
      this.bt_Abrir.Click += new System.EventHandler(this.bt_Abrir_Click);
      // 
      // FormEmpresa
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.ClientSize = new System.Drawing.Size(600, 173);
      this.Controls.Add(this.bt_Abrir);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Name = "FormEmpresa";
      this.Opacity = 0.98D;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Empresa";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormEmpresa_KeyDown);
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEmpresa_KeyPress);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.Button bt_Abrir;
  }
}