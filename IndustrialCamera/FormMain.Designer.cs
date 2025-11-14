namespace IndustrialCamera
{
    partial class FormMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_takePicture = new TGMTcontrols.AltoButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_configFile = new TGMTcontrols.BrowseFile();
            this.txt_serial = new TGMTcontrols.AltoTextBox();
            this.btn_stop = new TGMTcontrols.DangerButton();
            this.btn_start = new TGMTcontrols.AltoButton();
            this.panelCamera = new System.Windows.Forms.Panel();
            this.picCamera = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panelCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_takePicture);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_configFile);
            this.panel1.Controls.Add(this.txt_serial);
            this.panel1.Controls.Add(this.btn_stop);
            this.panel1.Controls.Add(this.btn_start);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1034, 84);
            this.panel1.TabIndex = 0;
            // 
            // btn_takePicture
            // 
            this.btn_takePicture.Active1 = System.Drawing.Color.DodgerBlue;
            this.btn_takePicture.Active2 = System.Drawing.Color.DeepSkyBlue;
            this.btn_takePicture.BackColor = System.Drawing.Color.Transparent;
            this.btn_takePicture.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_takePicture.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn_takePicture.ForeColor = System.Drawing.Color.White;
            this.btn_takePicture.Icon = null;
            this.btn_takePicture.ImageLocation = new System.Drawing.Point(0, 0);
            this.btn_takePicture.ImageSize = new System.Drawing.Size(0, 0);
            this.btn_takePicture.Inactive1 = System.Drawing.Color.DeepSkyBlue;
            this.btn_takePicture.Inactive2 = System.Drawing.Color.DodgerBlue;
            this.btn_takePicture.Location = new System.Drawing.Point(878, 19);
            this.btn_takePicture.Name = "btn_takePicture";
            this.btn_takePicture.Radius = 6;
            this.btn_takePicture.Size = new System.Drawing.Size(110, 40);
            this.btn_takePicture.Stroke = false;
            this.btn_takePicture.StrokeColor = System.Drawing.Color.Gray;
            this.btn_takePicture.TabIndex = 6;
            this.btn_takePicture.Text = "Capture";
            this.btn_takePicture.Transparency = false;
            this.btn_takePicture.Click += new System.EventHandler(this.btn_takePicture_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Config file";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Serial";
            // 
            // txt_configFile
            // 
            this.txt_configFile.BackColor = System.Drawing.Color.Transparent;
            this.txt_configFile.BackgroundColor = System.Drawing.Color.White;
            this.txt_configFile.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(133)))), ((int)(((byte)(200)))));
            this.txt_configFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txt_configFile.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_configFile.Location = new System.Drawing.Point(115, 48);
            this.txt_configFile.Name = "txt_configFile";
            this.txt_configFile.Padding = new System.Windows.Forms.Padding(5);
            this.txt_configFile.Pattern = "";
            this.txt_configFile.Size = new System.Drawing.Size(525, 30);
            this.txt_configFile.TabIndex = 3;
            this.txt_configFile.TextChanged += new System.EventHandler(this.txt_configFile_TextChanged);
            // 
            // txt_serial
            // 
            this.txt_serial.BackColor = System.Drawing.Color.Transparent;
            this.txt_serial.BackgroundColor = System.Drawing.Color.White;
            this.txt_serial.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(133)))), ((int)(((byte)(200)))));
            this.txt_serial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txt_serial.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_serial.Location = new System.Drawing.Point(115, 12);
            this.txt_serial.Multiline = false;
            this.txt_serial.Name = "txt_serial";
            this.txt_serial.Padding = new System.Windows.Forms.Padding(5);
            this.txt_serial.Radius = 4;
            this.txt_serial.ReadOnly = false;
            this.txt_serial.Size = new System.Drawing.Size(525, 30);
            this.txt_serial.TabIndex = 2;
            this.txt_serial.TextChanged += new System.EventHandler(this.txt_serial_TextChanged);
            // 
            // btn_stop
            // 
            this.btn_stop.Active1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btn_stop.Active2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_stop.BackColor = System.Drawing.Color.Transparent;
            this.btn_stop.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_stop.Enabled = false;
            this.btn_stop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn_stop.ForeColor = System.Drawing.Color.White;
            this.btn_stop.Icon = null;
            this.btn_stop.ImageLocation = new System.Drawing.Point(0, 0);
            this.btn_stop.ImageSize = new System.Drawing.Size(0, 0);
            this.btn_stop.Inactive1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_stop.Inactive2 = System.Drawing.Color.Red;
            this.btn_stop.Location = new System.Drawing.Point(762, 19);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Radius = 10;
            this.btn_stop.Size = new System.Drawing.Size(110, 40);
            this.btn_stop.Stroke = false;
            this.btn_stop.StrokeColor = System.Drawing.Color.Gray;
            this.btn_stop.TabIndex = 1;
            this.btn_stop.Text = "Stop";
            this.btn_stop.Transparency = false;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_start
            // 
            this.btn_start.Active1 = System.Drawing.Color.DodgerBlue;
            this.btn_start.Active2 = System.Drawing.Color.DeepSkyBlue;
            this.btn_start.BackColor = System.Drawing.Color.Transparent;
            this.btn_start.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_start.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btn_start.ForeColor = System.Drawing.Color.White;
            this.btn_start.Icon = null;
            this.btn_start.ImageLocation = new System.Drawing.Point(0, 0);
            this.btn_start.ImageSize = new System.Drawing.Size(0, 0);
            this.btn_start.Inactive1 = System.Drawing.Color.DeepSkyBlue;
            this.btn_start.Inactive2 = System.Drawing.Color.DodgerBlue;
            this.btn_start.Location = new System.Drawing.Point(646, 19);
            this.btn_start.Name = "btn_start";
            this.btn_start.Radius = 6;
            this.btn_start.Size = new System.Drawing.Size(110, 40);
            this.btn_start.Stroke = false;
            this.btn_start.StrokeColor = System.Drawing.Color.Gray;
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Start";
            this.btn_start.Transparency = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // panelCamera
            // 
            this.panelCamera.Controls.Add(this.picCamera);
            this.panelCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCamera.Location = new System.Drawing.Point(0, 84);
            this.panelCamera.Name = "panelCamera";
            this.panelCamera.Size = new System.Drawing.Size(1034, 477);
            this.panelCamera.TabIndex = 1;
            // 
            // picCamera
            // 
            this.picCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCamera.Location = new System.Drawing.Point(0, 0);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(1034, 477);
            this.picCamera.TabIndex = 0;
            this.picCamera.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 561);
            this.Controls.Add(this.panelCamera);
            this.Controls.Add(this.panel1);
            this.Name = "FormMain";
            this.Text = "Form Camera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelCamera.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelCamera;
        private System.Windows.Forms.PictureBox picCamera;
        private TGMTcontrols.AltoButton btn_start;
        private TGMTcontrols.DangerButton btn_stop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private TGMTcontrols.BrowseFile txt_configFile;
        private TGMTcontrols.AltoTextBox txt_serial;
        private TGMTcontrols.AltoButton btn_takePicture;
    }
}

