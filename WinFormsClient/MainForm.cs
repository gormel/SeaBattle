using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace WinFormsClient
{
	public partial class MainForm : Form
	{
		private Socket mSocket;
		internal User Me;
		internal readonly ClientTalker Talker = new ClientTalker();
		internal readonly PackageResolver Resolver = new PackageResolver();

		public ControlStack ControlStack { get; private set; }

		public MainForm()
		{
			InitializeComponent();
		}

		private async void Form1_Load(object sender, EventArgs e)
		{
			ControlStack = new ControlStack();
			ControlStack.OnUpdated += ControlStack_OnUpdated;
			ControlStack.Push(new LoginControl(this));
			mSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
			Me = new User(mSocket);
			Resolver.AddHandler<UserList>(ResolverOnUpdateUserList);
			await Connect();
		}

		void ControlStack_OnUpdated(object sender, EventArgs e)
		{
			Controls.Clear();
			ControlStack.Top.Dock = DockStyle.Fill;
			Controls.Add(ControlStack.Top);
		}

		void ResolverOnUpdateUserList(UserList e)
		{
			listBox1.Items.Clear();
			listBox1.Items.AddRange(e.Users.Cast<object>().ToArray());
		}

		internal async Task Connect()
		{
			await mSocket.ConnectTaskAsync(await Dns.GetHostAddressesAsync("localhost"), 666);
			await Talker.StartListen(Me, p =>
			{
				BeginInvoke(new Action(() => Resolver.ResolvePackage(p)));
			});
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			try
			{
				button1.Enabled = false;
				await Talker.Send(Me, new GetUserList());
			}
			finally
			{
				button1.Enabled = true;
			}
		}
	}
}
