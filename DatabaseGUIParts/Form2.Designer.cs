namespace DatabaseGUIParts
{
    partial class Form2
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
            label1 = new Label();
            dataGridView1 = new DataGridView();
            button3 = new Button();
            viewInventory = new Button();
            viewMachine = new Button();
            viewFloor = new Button();
            viewWarehouse = new Button();
            viewRegion = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(251, 30);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(151, 41);
            label1.TabIndex = 0;
            label1.Text = "Welcome!";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(251, 85);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1324, 707);
            dataGridView1.TabIndex = 3;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button3
            // 
            button3.Font = new Font("Microsoft Sans Serif", 12F);
            button3.Location = new Point(25, 467);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(104, 52);
            button3.TabIndex = 4;
            button3.Text = "Sign Out";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // viewInventory
            // 
            viewInventory.Font = new Font("Microsoft Sans Serif", 12F);
            viewInventory.Location = new Point(17, 85);
            viewInventory.Name = "viewInventory";
            viewInventory.Size = new Size(228, 59);
            viewInventory.TabIndex = 8;
            viewInventory.Text = "View Your Inventories";
            viewInventory.UseVisualStyleBackColor = true;
            viewInventory.Click += viewInventory_Click;
            // 
            // viewMachine
            // 
            viewMachine.Font = new Font("Microsoft Sans Serif", 12F);
            viewMachine.Location = new Point(17, 157);
            viewMachine.Name = "viewMachine";
            viewMachine.Size = new Size(228, 59);
            viewMachine.TabIndex = 9;
            viewMachine.Text = "View Your Machines";
            viewMachine.UseVisualStyleBackColor = true;
            viewMachine.Click += viewMachine_Click;
            // 
            // viewFloor
            // 
            viewFloor.Font = new Font("Microsoft Sans Serif", 12F);
            viewFloor.Location = new Point(17, 229);
            viewFloor.Name = "viewFloor";
            viewFloor.Size = new Size(228, 59);
            viewFloor.TabIndex = 10;
            viewFloor.Text = "View Your Floors";
            viewFloor.UseVisualStyleBackColor = true;
            viewFloor.Click += viewFloor_Click;
            // 
            // viewWarehouse
            // 
            viewWarehouse.Font = new Font("Microsoft Sans Serif", 12F);
            viewWarehouse.Location = new Point(17, 306);
            viewWarehouse.Name = "viewWarehouse";
            viewWarehouse.Size = new Size(228, 59);
            viewWarehouse.TabIndex = 11;
            viewWarehouse.Text = "View Your Warehouses";
            viewWarehouse.UseVisualStyleBackColor = true;
            viewWarehouse.Click += viewWarehouse_Click;
            // 
            // viewRegion
            // 
            viewRegion.Font = new Font("Microsoft Sans Serif", 12F);
            viewRegion.Location = new Point(17, 377);
            viewRegion.Name = "viewRegion";
            viewRegion.Size = new Size(228, 59);
            viewRegion.TabIndex = 12;
            viewRegion.Text = "View Regions";
            viewRegion.TextImageRelation = TextImageRelation.TextAboveImage;
            viewRegion.UseVisualStyleBackColor = true;
            viewRegion.Click += viewRegion_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1589, 832);
            Controls.Add(viewRegion);
            Controls.Add(viewWarehouse);
            Controls.Add(viewFloor);
            Controls.Add(viewMachine);
            Controls.Add(viewInventory);
            Controls.Add(button3);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form2";
            Text = "Warehouse Tracker";
            Load += Form2_Load_2;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private Button button3;
        private Button viewInventory;
        private Button viewMachine;
        private Button viewFloor;
        private Button viewWarehouse;
        private Button viewRegion;
    }
}