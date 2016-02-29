namespace WinFormsClient
{
	partial class GameHub
	{
		/// <summary> 
		/// Требуется переменная конструктора.
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

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Обязательный метод для поддержки конструктора - не изменяйте 
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.mRoomsListBox = new System.Windows.Forms.ListBox();
			this.mRefreshButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// mRoomsListBox
			// 
			this.mRoomsListBox.FormattingEnabled = true;
			this.mRoomsListBox.Location = new System.Drawing.Point(3, 32);
			this.mRoomsListBox.Name = "mRoomsListBox";
			this.mRoomsListBox.Size = new System.Drawing.Size(221, 303);
			this.mRoomsListBox.TabIndex = 0;
			// 
			// mRefreshButton
			// 
			this.mRefreshButton.Location = new System.Drawing.Point(149, 3);
			this.mRefreshButton.Name = "mRefreshButton";
			this.mRefreshButton.Size = new System.Drawing.Size(75, 23);
			this.mRefreshButton.TabIndex = 1;
			this.mRefreshButton.Text = "Refresh";
			this.mRefreshButton.UseVisualStyleBackColor = true;
			this.mRefreshButton.Click += new System.EventHandler(this.mRefreshButton_Click);
			// 
			// GameHub
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mRefreshButton);
			this.Controls.Add(this.mRoomsListBox);
			this.Name = "GameHub";
			this.Size = new System.Drawing.Size(719, 514);
			this.Load += new System.EventHandler(this.GameHub_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox mRoomsListBox;
		private System.Windows.Forms.Button mRefreshButton;
	}
}
