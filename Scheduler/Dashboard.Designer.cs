namespace Scheduler
{
    partial class Dashboard
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
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.label1 = new System.Windows.Forms.Label();
            this.dgCustomers = new System.Windows.Forms.DataGridView();
            this.dgAppointments = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnAddAppointment = new System.Windows.Forms.Button();
            this.btnModifyCustomer = new System.Windows.Forms.Button();
            this.btnModifyAppointment = new System.Windows.Forms.Button();
            this.btnDeleteCustomer = new System.Windows.Forms.Button();
            this.btnDeleteAppointment = new System.Windows.Forms.Button();
            this.btnTypesReport = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnMasterSchedule = new System.Windows.Forms.Button();
            this.btnCustomersByCity = new System.Windows.Forms.Button();
            this.btnWeekly = new System.Windows.Forms.Button();
            this.reminder = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAppointments)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(33, 36);
            this.monthCalendar1.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(380, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Customers";
            // 
            // dgCustomers
            // 
            this.dgCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCustomers.Location = new System.Drawing.Point(379, 66);
            this.dgCustomers.Margin = new System.Windows.Forms.Padding(4);
            this.dgCustomers.MultiSelect = false;
            this.dgCustomers.Name = "dgCustomers";
            this.dgCustomers.ReadOnly = true;
            this.dgCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCustomers.ShowEditingIcon = false;
            this.dgCustomers.Size = new System.Drawing.Size(719, 199);
            this.dgCustomers.TabIndex = 2;
            // 
            // dgAppointments
            // 
            this.dgAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAppointments.Location = new System.Drawing.Point(379, 390);
            this.dgAppointments.Margin = new System.Windows.Forms.Padding(4);
            this.dgAppointments.MultiSelect = false;
            this.dgAppointments.Name = "dgAppointments";
            this.dgAppointments.ReadOnly = true;
            this.dgAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAppointments.Size = new System.Drawing.Size(719, 199);
            this.dgAppointments.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(372, 354);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 31);
            this.label2.TabIndex = 7;
            this.label2.Text = "Appointments";
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.BackColor = System.Drawing.Color.DimGray;
            this.btnAddCustomer.ForeColor = System.Drawing.Color.White;
            this.btnAddCustomer.Location = new System.Drawing.Point(379, 274);
            this.btnAddCustomer.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(239, 59);
            this.btnAddCustomer.TabIndex = 13;
            this.btnAddCustomer.Text = "Add";
            this.btnAddCustomer.UseVisualStyleBackColor = false;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // btnAddAppointment
            // 
            this.btnAddAppointment.BackColor = System.Drawing.Color.DimGray;
            this.btnAddAppointment.ForeColor = System.Drawing.Color.White;
            this.btnAddAppointment.Location = new System.Drawing.Point(379, 598);
            this.btnAddAppointment.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddAppointment.Name = "btnAddAppointment";
            this.btnAddAppointment.Size = new System.Drawing.Size(239, 59);
            this.btnAddAppointment.TabIndex = 14;
            this.btnAddAppointment.Text = "Add";
            this.btnAddAppointment.UseVisualStyleBackColor = false;
            this.btnAddAppointment.Click += new System.EventHandler(this.btnAddAppointment_Click);
            // 
            // btnModifyCustomer
            // 
            this.btnModifyCustomer.BackColor = System.Drawing.Color.DimGray;
            this.btnModifyCustomer.ForeColor = System.Drawing.Color.White;
            this.btnModifyCustomer.Location = new System.Drawing.Point(617, 274);
            this.btnModifyCustomer.Margin = new System.Windows.Forms.Padding(5);
            this.btnModifyCustomer.Name = "btnModifyCustomer";
            this.btnModifyCustomer.Size = new System.Drawing.Size(239, 59);
            this.btnModifyCustomer.TabIndex = 15;
            this.btnModifyCustomer.Text = "Modify";
            this.btnModifyCustomer.UseVisualStyleBackColor = false;
            this.btnModifyCustomer.Click += new System.EventHandler(this.btnModifyCustomer_Click);
            // 
            // btnModifyAppointment
            // 
            this.btnModifyAppointment.BackColor = System.Drawing.Color.DimGray;
            this.btnModifyAppointment.ForeColor = System.Drawing.Color.White;
            this.btnModifyAppointment.Location = new System.Drawing.Point(620, 598);
            this.btnModifyAppointment.Margin = new System.Windows.Forms.Padding(5);
            this.btnModifyAppointment.Name = "btnModifyAppointment";
            this.btnModifyAppointment.Size = new System.Drawing.Size(239, 59);
            this.btnModifyAppointment.TabIndex = 16;
            this.btnModifyAppointment.Text = "Modify";
            this.btnModifyAppointment.UseVisualStyleBackColor = false;
            this.btnModifyAppointment.Click += new System.EventHandler(this.btnModifyAppointment_Click);
            // 
            // btnDeleteCustomer
            // 
            this.btnDeleteCustomer.BackColor = System.Drawing.Color.DimGray;
            this.btnDeleteCustomer.ForeColor = System.Drawing.Color.White;
            this.btnDeleteCustomer.Location = new System.Drawing.Point(859, 274);
            this.btnDeleteCustomer.Margin = new System.Windows.Forms.Padding(5);
            this.btnDeleteCustomer.Name = "btnDeleteCustomer";
            this.btnDeleteCustomer.Size = new System.Drawing.Size(239, 59);
            this.btnDeleteCustomer.TabIndex = 17;
            this.btnDeleteCustomer.Text = "Delete";
            this.btnDeleteCustomer.UseVisualStyleBackColor = false;
            this.btnDeleteCustomer.Click += new System.EventHandler(this.btnDeleteCustomer_Click);
            // 
            // btnDeleteAppointment
            // 
            this.btnDeleteAppointment.BackColor = System.Drawing.Color.DimGray;
            this.btnDeleteAppointment.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAppointment.Location = new System.Drawing.Point(863, 598);
            this.btnDeleteAppointment.Margin = new System.Windows.Forms.Padding(5);
            this.btnDeleteAppointment.Name = "btnDeleteAppointment";
            this.btnDeleteAppointment.Size = new System.Drawing.Size(239, 59);
            this.btnDeleteAppointment.TabIndex = 18;
            this.btnDeleteAppointment.Text = "Delete";
            this.btnDeleteAppointment.UseVisualStyleBackColor = false;
            this.btnDeleteAppointment.Click += new System.EventHandler(this.btnDeleteAppointment_Click);
            // 
            // btnTypesReport
            // 
            this.btnTypesReport.BackColor = System.Drawing.Color.DimGray;
            this.btnTypesReport.ForeColor = System.Drawing.Color.White;
            this.btnTypesReport.Location = new System.Drawing.Point(57, 431);
            this.btnTypesReport.Margin = new System.Windows.Forms.Padding(5);
            this.btnTypesReport.Name = "btnTypesReport";
            this.btnTypesReport.Size = new System.Drawing.Size(239, 59);
            this.btnTypesReport.TabIndex = 19;
            this.btnTypesReport.Text = "Appointment Types By Month";
            this.btnTypesReport.UseVisualStyleBackColor = false;
            this.btnTypesReport.Click += new System.EventHandler(this.btnTypesReport_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(59, 382);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 31);
            this.label3.TabIndex = 20;
            this.label3.Text = "Reports";
            // 
            // btnMasterSchedule
            // 
            this.btnMasterSchedule.BackColor = System.Drawing.Color.DimGray;
            this.btnMasterSchedule.ForeColor = System.Drawing.Color.White;
            this.btnMasterSchedule.Location = new System.Drawing.Point(57, 512);
            this.btnMasterSchedule.Margin = new System.Windows.Forms.Padding(5);
            this.btnMasterSchedule.Name = "btnMasterSchedule";
            this.btnMasterSchedule.Size = new System.Drawing.Size(239, 59);
            this.btnMasterSchedule.TabIndex = 21;
            this.btnMasterSchedule.Text = "Master Schedule";
            this.btnMasterSchedule.UseVisualStyleBackColor = false;
            this.btnMasterSchedule.Click += new System.EventHandler(this.btnMasterSchedule_Click);
            // 
            // btnCustomersByCity
            // 
            this.btnCustomersByCity.BackColor = System.Drawing.Color.DimGray;
            this.btnCustomersByCity.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCustomersByCity.ForeColor = System.Drawing.Color.White;
            this.btnCustomersByCity.Location = new System.Drawing.Point(57, 597);
            this.btnCustomersByCity.Margin = new System.Windows.Forms.Padding(5);
            this.btnCustomersByCity.Name = "btnCustomersByCity";
            this.btnCustomersByCity.Size = new System.Drawing.Size(239, 59);
            this.btnCustomersByCity.TabIndex = 22;
            this.btnCustomersByCity.Text = "Customers By City";
            this.btnCustomersByCity.UseVisualStyleBackColor = false;
            this.btnCustomersByCity.Click += new System.EventHandler(this.btnCustomersByCity_Click);
            // 
            // btnWeekly
            // 
            this.btnWeekly.BackColor = System.Drawing.Color.DimGray;
            this.btnWeekly.ForeColor = System.Drawing.Color.White;
            this.btnWeekly.Location = new System.Drawing.Point(59, 251);
            this.btnWeekly.Margin = new System.Windows.Forms.Padding(5);
            this.btnWeekly.Name = "btnWeekly";
            this.btnWeekly.Size = new System.Drawing.Size(239, 59);
            this.btnWeekly.TabIndex = 23;
            this.btnWeekly.Text = "Weekly Calendar";
            this.btnWeekly.UseVisualStyleBackColor = false;
            this.btnWeekly.Click += new System.EventHandler(this.btnWeekly_Click);
            // 
            // reminder
            // 
            this.reminder.Enabled = true;
            this.reminder.Interval = 60000;
            this.reminder.Tick += new System.EventHandler(this.reminder_Tick);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 677);
            this.Controls.Add(this.btnWeekly);
            this.Controls.Add(this.btnCustomersByCity);
            this.Controls.Add(this.btnMasterSchedule);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnTypesReport);
            this.Controls.Add(this.btnDeleteAppointment);
            this.Controls.Add(this.btnDeleteCustomer);
            this.Controls.Add(this.btnModifyAppointment);
            this.Controls.Add(this.btnModifyCustomer);
            this.Controls.Add(this.btnAddAppointment);
            this.Controls.Add(this.btnAddCustomer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgAppointments);
            this.Controls.Add(this.dgCustomers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.monthCalendar1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAppointments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgCustomers;
        private System.Windows.Forms.DataGridView dgAppointments;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.Button btnModifyCustomer;
        private System.Windows.Forms.Button btnModifyAppointment;
        private System.Windows.Forms.Button btnDeleteCustomer;
        private System.Windows.Forms.Button btnDeleteAppointment;
        private System.Windows.Forms.Button btnTypesReport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnMasterSchedule;
        private System.Windows.Forms.Button btnCustomersByCity;
        private System.Windows.Forms.Button btnWeekly;
        private System.Windows.Forms.Timer reminder;
    }
}