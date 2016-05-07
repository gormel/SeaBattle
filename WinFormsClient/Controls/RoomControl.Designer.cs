namespace WinFormsClient.Controls
{
	partial class RoomControl
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
			this.mRoomNameLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// mRoomNameLabel
			// 
			this.mRoomNameLabel.AutoSize = true;
			this.mRoomNameLabel.Location = new System.Drawing.Point(3, 0);
			this.mRoomNameLabel.Name = "mRoomNameLabel";
			this.mRoomNameLabel.Size = new System.Drawing.Size(35, 13);
			this.mRoomNameLabel.TabIndex = 0;
			this.mRoomNameLabel.Text = "label1";
			// 
			// RoomControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mRoomNameLabel);
			this.Name = "RoomControl";
			this.Size = new System.Drawing.Size(624, 374);
			this.Load += new System.EventHandler(this.RoomControl_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label mRoomNameLabel;
	}
}
