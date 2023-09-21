using System.Drawing;
using System.Windows.Forms;

namespace CompraFacil.App
{
    partial class FormRelatorioCompras
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btConsultar = new Button();
            btGerarExcel = new Button();
            dgvAnaliseCompras = new DataGridView();
            btSair = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAnaliseCompras).BeginInit();
            SuspendLayout();
            // 
            // btConsultar
            // 
            btConsultar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btConsultar.Location = new Point(672, 469);
            btConsultar.Name = "btConsultar";
            btConsultar.Size = new Size(75, 23);
            btConsultar.TabIndex = 0;
            btConsultar.Text = "Consultar";
            btConsultar.UseVisualStyleBackColor = true;
            btConsultar.Click += BtConsultar_Click;
            // 
            // btGerarExcel
            // 
            btGerarExcel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btGerarExcel.Location = new Point(753, 469);
            btGerarExcel.Name = "btGerarExcel";
            btGerarExcel.Size = new Size(75, 23);
            btGerarExcel.TabIndex = 1;
            btGerarExcel.Text = "Gerar Excel";
            btGerarExcel.UseVisualStyleBackColor = true;
            btGerarExcel.Click += BtGerarExcel_Click;
            // 
            // dgvAnaliseCompras
            // 
            dgvAnaliseCompras.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvAnaliseCompras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAnaliseCompras.Location = new Point(12, 12);
            dgvAnaliseCompras.Name = "dgvAnaliseCompras";
            dgvAnaliseCompras.RowTemplate.Height = 25;
            dgvAnaliseCompras.Size = new Size(897, 447);
            dgvAnaliseCompras.TabIndex = 2;
            // 
            // btSair
            // 
            btSair.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btSair.Location = new Point(834, 469);
            btSair.Name = "btSair";
            btSair.Size = new Size(75, 23);
            btSair.TabIndex = 3;
            btSair.Text = "Sair";
            btSair.UseVisualStyleBackColor = true;
            btSair.Click += BtSair_Click;
            // 
            // FormRelatorioCompras
            // 
            AcceptButton = btConsultar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btSair;
            ClientSize = new Size(921, 504);
            Controls.Add(btSair);
            Controls.Add(dgvAnaliseCompras);
            Controls.Add(btGerarExcel);
            Controls.Add(btConsultar);
            Name = "FormRelatorioCompras";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Relatório de Compras";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)dgvAnaliseCompras).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btConsultar;
        private Button btGerarExcel;
        private DataGridView dgvAnaliseCompras;
        private Button btSair;
    }
}