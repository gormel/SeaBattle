using System;
using System.Management.Instrumentation;
using System.Windows.Forms;
using Core.Packeges;

namespace WinFormsClient
{
	public partial class LoginControl : UserControl
	{
		private readonly MainForm mMainForm;

		public LoginControl(MainForm mainForm)
		{
			mMainForm = mainForm;
			InitializeComponent();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			await mMainForm.Talker.Send(mMainForm.Me, new Login
			{
				Name = mLoginTextBox.Text,
				Password = mPasswordTextBox.Text
			});
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			await mMainForm.Talker.Send(mMainForm.Me, new Register
			{
				Name = mLoginTextBox.Text,
				Password = mPasswordTextBox.Text
			});
		}

		private void LoginControl_Load(object sender, EventArgs e)
		{
			mMainForm.Resolver.AddHandler<Login>(Resolver_OnLogin);
			mMainForm.Resolver.AddHandler<Register>(Resolver_OnRegister);
		}

		void Resolver_OnRegister(Register e)
		{
			mLoginTextBox.Text = "";
			mPasswordTextBox.Text = "";
			MessageBox.Show("Регистрация прошла " + (e.Result ? "" : "не") + "успешно.", "Регистрация.");
		}

		void Resolver_OnLogin(Login e)
		{
			if (e.ID == Guid.Empty)
			{
				MessageBox.Show("Логин прошел не успешно.", "Логин.");
				return;
			}
			mLoginTextBox.Text = "";
			mPasswordTextBox.Text = "";
			mMainForm.Me.ID = e.ID;
			mMainForm.ControlStack.Push(new GameHub(mMainForm));
		}
	}
}
