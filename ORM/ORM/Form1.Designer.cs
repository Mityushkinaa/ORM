﻿namespace ORM
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvMyDach = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvMyDachas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyDach)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyDachas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMyDach
            // 
            this.dgvMyDach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMyDach.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyDach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMyDach.Location = new System.Drawing.Point(3, 3);
            this.dgvMyDach.Margin = new System.Windows.Forms.Padding(4);
            this.dgvMyDach.Name = "dgvMyDach";
            this.dgvMyDach.Size = new System.Drawing.Size(772, 399);
            this.dgvMyDach.TabIndex = 3;
            this.dgvMyDach.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMyDach_CellValueChanged);
            this.dgvMyDach.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvMyDach_RowValidating);
            this.dgvMyDach.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvMyDach_UserDeletingRow);
            this.dgvMyDach.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dgvMyDach_PreviewKeyDown);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(786, 434);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvMyDach);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(778, 405);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Инф о собственности";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvMyDachas);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(778, 405);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Дачи";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvMyDachas
            // 
            this.dgvMyDachas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMyDachas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMyDachas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMyDachas.Location = new System.Drawing.Point(3, 3);
            this.dgvMyDachas.Margin = new System.Windows.Forms.Padding(4);
            this.dgvMyDachas.Name = "dgvMyDachas";
            this.dgvMyDachas.Size = new System.Drawing.Size(772, 399);
            this.dgvMyDachas.TabIndex = 1;
            this.dgvMyDachas.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMyDachas_CellValueChanged);
            this.dgvMyDachas.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvMyDachas_RowValidating);
            this.dgvMyDachas.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvMyDachas_UserDeletingRow);
            this.dgvMyDachas.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dgvMyDachas_PreviewKeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyDach)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMyDachas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMyDach;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvMyDachas;
    }
}

