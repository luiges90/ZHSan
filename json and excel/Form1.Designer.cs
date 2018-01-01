namespace json_and_excel
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.已导入文件 = new System.Windows.Forms.ListBox();
            this.已导入文件标题 = new System.Windows.Forms.Label();
            this.将导出文件 = new System.Windows.Forms.ListBox();
            this.将导出文件标题 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.是剧本 = new System.Windows.Forms.CheckBox();
            this.保存配置信息到存档剧本 = new System.Windows.Forms.CheckBox();
            this.保存commdata信息到存档剧本 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.单独保存commdata信息 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(131, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "导入mdb文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.导入mdb文件);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(131, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 34);
            this.button2.TabIndex = 0;
            this.button2.Text = "导入excel文件";
            this.toolTip1.SetToolTip(this.button2, "可多选");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.导入excel文件);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(131, 143);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 34);
            this.button3.TabIndex = 0;
            this.button3.Text = "导入json文件";
            this.toolTip1.SetToolTip(this.button3, "可多选");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.导入json文件);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(439, 25);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(111, 34);
            this.button4.TabIndex = 0;
            this.button4.Text = "导出为mdb文件";
            this.toolTip1.SetToolTip(this.button4, "可多选");
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(439, 84);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(111, 34);
            this.button5.TabIndex = 0;
            this.button5.Text = "导出为excel文件";
            this.toolTip1.SetToolTip(this.button5, "导出后，将在\\转换生成文件文件夹下生成同名excel文件(剧本存档信息)和同名+地形信息txt文件");
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.导出为excel文件);
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(439, 143);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(111, 34);
            this.button6.TabIndex = 0;
            this.button6.Text = "导出为json文件";
            this.toolTip1.SetToolTip(this.button6, "注意，只有同名excel文件(剧本存档信息)和同名+地形信息txt文件都存在的情况下，才能顺利将文件转换成json文件");
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.导出为json文件);
            // 
            // 已导入文件
            // 
            this.已导入文件.FormattingEnabled = true;
            this.已导入文件.HorizontalScrollbar = true;
            this.已导入文件.ItemHeight = 12;
            this.已导入文件.Location = new System.Drawing.Point(62, 215);
            this.已导入文件.Name = "已导入文件";
            this.已导入文件.Size = new System.Drawing.Size(244, 148);
            this.已导入文件.TabIndex = 1;
            // 
            // 已导入文件标题
            // 
            this.已导入文件标题.BackColor = System.Drawing.SystemColors.Control;
            this.已导入文件标题.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.已导入文件标题.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.已导入文件标题.Location = new System.Drawing.Point(62, 195);
            this.已导入文件标题.Name = "已导入文件标题";
            this.已导入文件标题.Size = new System.Drawing.Size(244, 21);
            this.已导入文件标题.TabIndex = 2;
            this.已导入文件标题.Text = "已导入文件";
            this.已导入文件标题.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 将导出文件
            // 
            this.将导出文件.FormattingEnabled = true;
            this.将导出文件.HorizontalScrollbar = true;
            this.将导出文件.ItemHeight = 12;
            this.将导出文件.Location = new System.Drawing.Point(371, 215);
            this.将导出文件.Name = "将导出文件";
            this.将导出文件.Size = new System.Drawing.Size(244, 148);
            this.将导出文件.TabIndex = 1;
            // 
            // 将导出文件标题
            // 
            this.将导出文件标题.BackColor = System.Drawing.SystemColors.Control;
            this.将导出文件标题.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.将导出文件标题.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.将导出文件标题.Location = new System.Drawing.Point(371, 195);
            this.将导出文件标题.Name = "将导出文件标题";
            this.将导出文件标题.Size = new System.Drawing.Size(244, 21);
            this.将导出文件标题.TabIndex = 2;
            this.将导出文件标题.Text = "将导出文件";
            this.将导出文件标题.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(131, 383);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(111, 34);
            this.button7.TabIndex = 0;
            this.button7.Text = "清除所有导入文件";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.清除所有导入文件);
            // 
            // button8
            // 
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(439, 383);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(111, 34);
            this.button8.TabIndex = 0;
            this.button8.Text = "确认导出";
            this.toolTip1.SetToolTip(this.button8, "文件将生成在  根目录的  \\转换生成文件   文件夹下");
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.确认导出);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // 是剧本
            // 
            this.是剧本.AutoSize = true;
            this.是剧本.Enabled = false;
            this.是剧本.Location = new System.Drawing.Point(556, 123);
            this.是剧本.Name = "是剧本";
            this.是剧本.Size = new System.Drawing.Size(60, 16);
            this.是剧本.TabIndex = 3;
            this.是剧本.Text = "是剧本";
            this.toolTip1.SetToolTip(this.是剧本, "选中后，将生成同名剧本json文件和剧本列表Scenarios.json文件，将生成的此两项复制到游戏根目录\\Data\\Scenario替换原文件后可以以此剧本开" +
        "始游戏\r\n如未选中，将生成同名存档json文件和存档列表Saves.json文件，将生成的此两项复制到\\我的文档\\WorldOfTheThreeKingdoms" +
        "\\Save替换原文件后可以以此存档开始游戏");
            this.是剧本.UseVisualStyleBackColor = true;
            // 
            // 保存配置信息到存档剧本
            // 
            this.保存配置信息到存档剧本.AutoSize = true;
            this.保存配置信息到存档剧本.Checked = true;
            this.保存配置信息到存档剧本.CheckState = System.Windows.Forms.CheckState.Checked;
            this.保存配置信息到存档剧本.Enabled = false;
            this.保存配置信息到存档剧本.Location = new System.Drawing.Point(555, 141);
            this.保存配置信息到存档剧本.Name = "保存配置信息到存档剧本";
            this.保存配置信息到存档剧本.Size = new System.Drawing.Size(156, 16);
            this.保存配置信息到存档剧本.TabIndex = 3;
            this.保存配置信息到存档剧本.Text = "保存配置信息到存档剧本";
            this.toolTip1.SetToolTip(this.保存配置信息到存档剧本, "选中后，配置信息如游戏难度、年龄是否生效等配置信息将会保存到剧本或存档中，这些信息将不会受到剧本开始时的配置设置所影响");
            this.保存配置信息到存档剧本.UseVisualStyleBackColor = true;
            // 
            // 保存commdata信息到存档剧本
            // 
            this.保存commdata信息到存档剧本.AutoSize = true;
            this.保存commdata信息到存档剧本.Enabled = false;
            this.保存commdata信息到存档剧本.Location = new System.Drawing.Point(555, 160);
            this.保存commdata信息到存档剧本.Name = "保存commdata信息到存档剧本";
            this.保存commdata信息到存档剧本.Size = new System.Drawing.Size(180, 16);
            this.保存commdata信息到存档剧本.TabIndex = 3;
            this.保存commdata信息到存档剧本.Text = "保存commdata信息到存档剧本";
            this.toolTip1.SetToolTip(this.保存commdata信息到存档剧本, "选中后，commdata如影响表称号表等commdata信息将会保存到剧本或存档中，这些信息将不会受到游戏默认的commdata所影响");
            this.保存commdata信息到存档剧本.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 30000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // 单独保存commdata信息
            // 
            this.单独保存commdata信息.AutoSize = true;
            this.单独保存commdata信息.Enabled = false;
            this.单独保存commdata信息.Location = new System.Drawing.Point(555, 179);
            this.单独保存commdata信息.Name = "单独保存commdata信息";
            this.单独保存commdata信息.Size = new System.Drawing.Size(144, 16);
            this.单独保存commdata信息.TabIndex = 3;
            this.单独保存commdata信息.Text = "单独保存commdata信息";
            this.toolTip1.SetToolTip(this.单独保存commdata信息, "选中后，你通过excel修改的如影响表称号表等commdata信息将会生成在转换文件夹中CommonData.json，把此文件替换Content\\Data\\Co" +
        "mmon文件夹同名文件\r\n之后，剧本或存档中如无这些信息，则会读取此文件中的通用信息");
            this.单独保存commdata信息.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 427);
            this.Controls.Add(this.单独保存commdata信息);
            this.Controls.Add(this.保存commdata信息到存档剧本);
            this.Controls.Add(this.保存配置信息到存档剧本);
            this.Controls.Add(this.是剧本);
            this.Controls.Add(this.将导出文件标题);
            this.Controls.Add(this.已导入文件标题);
            this.Controls.Add(this.将导出文件);
            this.Controls.Add(this.已导入文件);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "剧本存档转换工具BY月落";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ListBox 已导入文件;
        private System.Windows.Forms.Label 已导入文件标题;
        private System.Windows.Forms.ListBox 将导出文件;
        private System.Windows.Forms.Label 将导出文件标题;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox 是剧本;
        private System.Windows.Forms.CheckBox 保存配置信息到存档剧本;
        private System.Windows.Forms.CheckBox 保存commdata信息到存档剧本;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox 单独保存commdata信息;
    }
}

