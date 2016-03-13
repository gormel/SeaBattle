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
			this.button1 = new System.Windows.Forms.Button();
			this.mRoomName = new System.Windows.Forms.TextBox();
			this.mJoinButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// mRoomsListBox
			// 
			this.mRoomsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.mRoomsListBox.FormattingEnabled = true;
			this.mRoomsListBox.Location = new System.Drawing.Point(3, 32);
			this.mRoomsListBox.Name = "mRoomsListBox";
			this.mRoomsListBox.Size = new System.Drawing.Size(221, 433);
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
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(149, 469);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Create";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// mRoomName
			// 
			this.mRoomName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.mRoomName.Location = new System.Drawing.Point(3, 471);
			this.mRoomName.Name = "mRoomName";
			this.mRoomName.Size = new System.Drawing.Size(140, 20);
			this.mRoomName.TabIndex = 3;
			// 
			// mJoinButton
			// 
			this.mJoinButton.Location = new System.Drawing.Point(3, 3);
			this.mJoinButton.Name = "mJoinButton";
			this.mJoinButton.Size = new System.Drawing.Size(75, 23);
			this.mJoinButton.TabIndex = 4;
			this.mJoinButton.Text = "Join";
			this.mJoinButton.UseVisualStyleBackColor = true;
			this.mJoinButton.Click += new System.EventHandler(this.mJoinButton_Click);
			// 
			// GameHub
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mJoinButton);
			this.Controls.Add(this.mRoomName);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.mRefreshButton);
			this.Controls.Add(this.mRoomsListBox);
			this.Name = "GameHub";
			this.Size = new System.Drawing.Size(733, 494);
			this.Load += new System.EventHandler(this.GameHub_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox mRoomsListBox;
		private System.Windows.Forms.Button mRefreshButton;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox mRoomName;
		private System.Windows.Forms.Button mJoinButton;
	}
}
