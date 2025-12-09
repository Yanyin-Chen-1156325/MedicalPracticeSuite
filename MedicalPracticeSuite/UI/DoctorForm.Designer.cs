namespace MedicalPracticeSuite.UI
{
    partial class DoctorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoctorForm));
            this.lblDoctorID = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDoctorName = new DevExpress.XtraEditors.TextEdit();
            this.txtDoctorSpecialty = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.txtDoctorID = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtDoctorName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDoctorSpecialty.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDoctorID
            // 
            this.lblDoctorID.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoctorID.Appearance.Options.UseFont = true;
            this.lblDoctorID.Location = new System.Drawing.Point(13, 15);
            this.lblDoctorID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblDoctorID.Name = "lblDoctorID";
            this.lblDoctorID.Size = new System.Drawing.Size(33, 29);
            this.lblDoctorID.TabIndex = 0;
            this.lblDoctorID.Text = "ID:";
            this.lblDoctorID.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(13, 65);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 29);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Name:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(13, 123);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(101, 29);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Specialty:";
            // 
            // txtDoctorName
            // 
            this.txtDoctorName.Location = new System.Drawing.Point(119, 65);
            this.txtDoctorName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDoctorName.Name = "txtDoctorName";
            this.txtDoctorName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtDoctorName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoctorName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtDoctorName.Properties.Appearance.Options.UseBackColor = true;
            this.txtDoctorName.Properties.Appearance.Options.UseFont = true;
            this.txtDoctorName.Properties.Appearance.Options.UseForeColor = true;
            this.txtDoctorName.Size = new System.Drawing.Size(269, 44);
            this.txtDoctorName.TabIndex = 5;
            // 
            // txtDoctorSpecialty
            // 
            this.txtDoctorSpecialty.Location = new System.Drawing.Point(119, 123);
            this.txtDoctorSpecialty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDoctorSpecialty.Name = "txtDoctorSpecialty";
            this.txtDoctorSpecialty.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtDoctorSpecialty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoctorSpecialty.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtDoctorSpecialty.Properties.Appearance.Options.UseBackColor = true;
            this.txtDoctorSpecialty.Properties.Appearance.Options.UseFont = true;
            this.txtDoctorSpecialty.Properties.Appearance.Options.UseForeColor = true;
            this.txtDoctorSpecialty.Size = new System.Drawing.Size(269, 44);
            this.txtDoctorSpecialty.TabIndex = 6;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(13, 200);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.simpleButton1.Size = new System.Drawing.Size(147, 68);
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "Save";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(220, 200);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.simpleButton2.Size = new System.Drawing.Size(143, 68);
            this.simpleButton2.TabIndex = 10;
            this.simpleButton2.Text = "Cancel";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // txtDoctorID
            // 
            this.txtDoctorID.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDoctorID.Appearance.Options.UseFont = true;
            this.txtDoctorID.Location = new System.Drawing.Point(113, 15);
            this.txtDoctorID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDoctorID.Name = "txtDoctorID";
            this.txtDoctorID.Size = new System.Drawing.Size(0, 24);
            this.txtDoctorID.TabIndex = 11;
            // 
            // DoctorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 273);
            this.Controls.Add(this.txtDoctorID);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtDoctorSpecialty);
            this.Controls.Add(this.txtDoctorName);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lblDoctorID);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DoctorForm";
            this.Text = "DoctorForm";
            this.Load += new System.EventHandler(this.DoctorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDoctorName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDoctorSpecialty.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblDoctorID;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtDoctorName;
        private DevExpress.XtraEditors.TextEdit txtDoctorSpecialty;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LabelControl txtDoctorID;
    }
}