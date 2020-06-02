namespace AVT
{
    partial class TimeTableForm
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
            this.gvwTimeTable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gvwTimeTable)).BeginInit();
            this.SuspendLayout();
            // 
            // gvwTimeTable
            // 
            this.gvwTimeTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvwTimeTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvwTimeTable.Location = new System.Drawing.Point(0, 0);
            this.gvwTimeTable.Name = "gvwTimeTable";
            this.gvwTimeTable.Size = new System.Drawing.Size(605, 255);
            this.gvwTimeTable.TabIndex = 0;
            // 
            // TimeTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 255);
            this.Controls.Add(this.gvwTimeTable);
            this.Name = "TimeTableForm";
            this.Text = "TimeTableForm";
            ((System.ComponentModel.ISupportInitialize)(this.gvwTimeTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView gvwTimeTable;
    }
}