namespace Laborator1SGBD
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonParinte = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Rid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TextR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonAdauga = new System.Windows.Forms.Button();
            this.toolTipShow = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Jid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonParinte
            // 
            this.buttonParinte.Font = new System.Drawing.Font("Mistral", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonParinte.Location = new System.Drawing.Point(389, 257);
            this.buttonParinte.Name = "buttonParinte";
            this.buttonParinte.Size = new System.Drawing.Size(80, 63);
            this.buttonParinte.TabIndex = 1;
            this.buttonParinte.Text = "Arata parinte";
            this.toolTipShow.SetToolTip(this.buttonParinte, "Arata datele din tabela parinte");
            this.buttonParinte.UseVisualStyleBackColor = true;
            this.buttonParinte.Click += new System.EventHandler(this.buttonParinte_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Rid,
            this.TextR});
            this.dataGridView2.Location = new System.Drawing.Point(525, 172);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(449, 94);
            this.dataGridView2.TabIndex = 2;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            this.dataGridView2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView2_EditingControlShowing);
            // 
            // Rid
            // 
            this.Rid.HeaderText = "Rid";
            this.Rid.Name = "Rid";
            // 
            // TextR
            // 
            this.TextR.HeaderText = "Text";
            this.TextR.Name = "TextR";
            this.TextR.Width = 300;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Font = new System.Drawing.Font("Mistral", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUpdate.Location = new System.Drawing.Point(389, 197);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 30);
            this.buttonUpdate.TabIndex = 3;
            this.buttonUpdate.Text = "Update";
            this.toolTipShow.SetToolTip(this.buttonUpdate, "Apasa aici pentru update");
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonAdauga
            // 
            this.buttonAdauga.Font = new System.Drawing.Font("Mistral", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdauga.Location = new System.Drawing.Point(389, 140);
            this.buttonAdauga.Name = "buttonAdauga";
            this.buttonAdauga.Size = new System.Drawing.Size(75, 30);
            this.buttonAdauga.TabIndex = 4;
            this.buttonAdauga.Text = "Adauga";
            this.toolTipShow.SetToolTip(this.buttonAdauga, "Adauga o inregistrare fiu");
            this.buttonAdauga.UseVisualStyleBackColor = true;
            this.buttonAdauga.Click += new System.EventHandler(this.buttonAdauga_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Jid,
            this.NumeJ});
            this.dataGridView1.Location = new System.Drawing.Point(54, 62);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(294, 279);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Jid
            // 
            this.Jid.HeaderText = "Jid";
            this.Jid.Name = "Jid";
            // 
            // NumeJ
            // 
            this.NumeJ.HeaderText = "NumeJ";
            this.NumeJ.Name = "NumeJ";
            this.NumeJ.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Uighur", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(702, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 38);
            this.label1.TabIndex = 5;
            this.label1.Text = "Recenzii";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Uighur", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(160, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 38);
            this.label2.TabIndex = 6;
            this.label2.Text = "Jocuri";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(986, 416);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAdauga);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.buttonParinte);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonParinte;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonAdauga;
        private System.Windows.Forms.ToolTip toolTipShow;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rid;
        private System.Windows.Forms.DataGridViewTextBoxColumn TextR;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jid;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeJ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
