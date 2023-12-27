namespace Gyermekvasut.Forms
{
    partial class AllomasForm
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
            i_s = new Label();
            KpCsenget = new Button();
            VpCsenget = new Button();
            SuspendLayout();
            // 
            // i_s
            // 
            i_s.AutoSize = true;
            i_s.Location = new Point(19, 87);
            i_s.Name = "i_s";
            i_s.Size = new Size(25, 20);
            i_s.TabIndex = 1;
            i_s.Text = "i_s";
            // 
            // KpCsenget
            // 
            KpCsenget.Location = new Point(14, 16);
            KpCsenget.Margin = new Padding(3, 4, 3, 4);
            KpCsenget.Name = "KpCsenget";
            KpCsenget.Size = new Size(86, 31);
            KpCsenget.TabIndex = 2;
            KpCsenget.Text = "KP csenget";
            KpCsenget.UseVisualStyleBackColor = true;
            KpCsenget.Click += KpCsenget_Click;
            // 
            // VpCsenget
            // 
            VpCsenget.Location = new Point(150, 16);
            VpCsenget.Margin = new Padding(3, 4, 3, 4);
            VpCsenget.Name = "VpCsenget";
            VpCsenget.Size = new Size(86, 31);
            VpCsenget.TabIndex = 3;
            VpCsenget.Text = "VP csenget";
            VpCsenget.UseVisualStyleBackColor = true;
            VpCsenget.Click += VpCsenget_Click;
            // 
            // AllomasForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(VpCsenget);
            Controls.Add(KpCsenget);
            Controls.Add(i_s);
            Margin = new Padding(3, 4, 3, 4);
            Name = "AllomasForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label i_s;
        private Button KpCsenget;
        private Button VpCsenget;
    }
}